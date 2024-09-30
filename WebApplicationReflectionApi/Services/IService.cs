using WebApplicationReflectionApi.Models;

namespace WebApplicationReflectionApi.Services;

public interface IService
{
    Task<BaseResultModel> GetUser();
    Task<BaseResultModel> GetUserList();

}
