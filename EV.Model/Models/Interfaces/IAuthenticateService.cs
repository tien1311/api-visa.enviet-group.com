
using EV.Model.Models.Response.AccountResponse;

namespace EV.Model.Models.Interfaces
{
    public interface IAuthenticateService
    {
        Task<AuthenticateResponse> Authenticate(string UserName, string PassWord);
    }
}
