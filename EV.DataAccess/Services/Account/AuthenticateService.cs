using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EV.Model.Models.Response.AccountResponse;
using EV.Model.Models.Interfaces;

namespace EV.DataAccess.Services.Account
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private string _Server_EV_2;
        private string JWT_keySha256;
        public AuthenticateService(IConfiguration configuration)
        {
            _configuration = configuration;
            _Server_EV_2 = _configuration.GetConnectionString("SQL_EV_2_MAIN");
            JWT_keySha256 = _configuration.GetSection("Token")["keySha256"];
        }
        public async Task<AuthenticateResponse> Authenticate(string UserName, string Password)
        {
            AuthenticateResponse result = new AuthenticateResponse();
            try
            {

                string sql = " select * from ApiCompanyName where UserName=@UserName and Password=@Password";

                using (var conn = new SqlConnection(_Server_EV_2))
                {
                    var param = new
                    {
                        UserName = UserName,
                        Password = Password
                    };
                    result = await conn.QueryFirstAsync<AuthenticateResponse>(sql, param, null, commandTimeout: 30, commandType: System.Data.CommandType.Text);
                }
                if (result != null)
                {
                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(JWT_keySha256);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                         {
                            new Claim(ClaimTypes.Name, UserName),
                            new Claim("username", UserName),
                            new Claim("AgentCode", result.AgentCode)

                        }),
                        Expires = DateTime.UtcNow.AddHours(3),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    result.SetToken(tokenHandler.WriteToken(token));
                    result.Expires = DateTime.Now.AddHours(3);
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }

        }
    }
}
