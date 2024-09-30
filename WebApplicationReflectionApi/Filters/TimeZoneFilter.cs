using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Reflection;
using WebApplicationReflectionApi.Models;

namespace WebApplicationReflectionApi.Filters;

public class TimeZoneFilter : IActionFilter
{
    #region Public Constructors

    public TimeZoneFilter()
    {
    }

    #endregion Public Constructors

    #region Public Methods

    public static void UpdateNonDefaultNullableDateTimes<T>(T obj)
    {
        if (obj is not null)
        {
            if (obj is IEnumerable<object>)
            {
                foreach (var item in (IEnumerable<object>)obj)
                {
                    UpdateNonDefaultNullableDateTimes(item);
                }
            }
            else
            {
                var _typeObject = obj.GetType();
                var properties = _typeObject.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(DateTime?) && property.CanWrite)
                    {
                        var currentValue = (DateTime?)property.GetValue(obj);
                        if (currentValue.HasValue) // Check if it has a value
                        {
                            property.SetValue(obj, currentValue.Value.AddDays(-11));
                        }
                    }
                    else if (property.PropertyType == typeof(DateTime) && property.CanWrite)
                    {
                        var currentValue = (DateTime)property.GetValue(obj);
                        if (currentValue != DateTime.MinValue)
                        {
                            property.SetValue(obj, DateTime.Now.AddDays(-11)); // update value
                        }
                    }
                    else if (IsListType(property.PropertyType))
                    {
                        var _listItems = property.GetValue(obj);
                        if (_listItems is not null)
                        {
                            foreach (var _item in (IEnumerable<object>)_listItems)
                            {
                                UpdateNonDefaultNullableDateTimes(_item);
                            }
                        }
                    }
                    else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    {
                        var _item = property.GetValue(obj);
                        if (_item is not null)
                        {
                            UpdateNonDefaultNullableDateTimes(_item);
                        }
                    }
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is OkObjectResult objectResult)
        {
            var _objectResultValue = objectResult.Value;

            if (_objectResultValue is BaseResultModel _baseResultModel)
            {
                if (_baseResultModel.Success && _baseResultModel.ResultObject is not null)
                {
                    UpdateNonDefaultNullableDateTimes(_baseResultModel.ResultObject);
                }
            }
            else
            {
                UpdateNonDefaultNullableDateTimes(objectResult.Value);
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Code that runs before the action method executes
    }

    #endregion Public Methods

    #region Private Methods

    private static bool IsListType(Type type)
    {
        return type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition());
    }

    #endregion Private Methods
}