namespace EV.Model.Models.Response.AccountResponse
{
    public class AuthenticateResponse
    {
        public string Message { get; set; }
        public string CompanyName { get; set; }
        public string Token { get; set; }
        public string AgentCode { get; set; } = "";
        public DateTime? Expires { get; set; }
        public string GetToken()
        {
            return Token;
        }

        public void SetToken(string _token)
        {
            Token = _token;
        }
    }
}
