using WebApplicationReflectionApi.Models;

namespace WebApplicationReflectionApi.Services;

public class Service : IService
{
    #region Public Methods

    public async Task<BaseResultModel> GetUser()
    {
        BaseResultModel _response = new();

        try
        {
            var _obj = this.GetUsers().FirstOrDefault();
            _response.AddResultObject(_obj);
        }
        catch (Exception ex)
        {
            _response.AddError(ex.Message);
        }

        return _response;
    }

    public async Task<BaseResultModel> GetUserList()
    {
        BaseResultModel _response = new();

        try
        {
            _response.AddResultObject(this.GetUsers());
        }
        catch (Exception ex)
        {
            _response.AddError(ex.Message);
        }

        return _response;
    }

    #endregion Public Methods

    #region Private Methods

    private List<User> GetUsers()
    {
        return
        [
            new User{ Id= 1, Name="AAA", Surname="DDDD",Address = new Address{ Name="E",Description="F",CreatedOn=DateTime.Now } , Addresses = [ new() { Name = "A", Description = "B", CreatedOn = DateTime.Now },new() { Name="C",Description="D",CreatedOn=DateTime.Now} ] },
            new User{Id = 2, Name="BBB", Surname="DDDD",UpdatedOn = DateTime.Now},
            new User{Id = 3, Name="CCC", Surname="DDDD", UpdatedOn = DateTime.Now}
        ];
    }

    #endregion Private Methods
}