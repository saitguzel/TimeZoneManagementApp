namespace WebApplicationReflectionApi.Models;

public class BaseResultModel
{
    public BaseResultModel()
    {
        this.Errors = new List<string>();
        this.ResultObject = new object();
    }

    public bool Success
    {
        get { return (this.Errors.Count == 0 && this.ResultObject != null); }
    }

    public void AddError(string error)
    {
        this.Errors.Add(error);
    }

    public void AddResultObject(object resulObject)
    {
        this.ResultObject = resulObject;
    }

    public List<string> Errors { get; set; }
    public object ResultObject { get; set; }
    public string Message { get; set; }
}