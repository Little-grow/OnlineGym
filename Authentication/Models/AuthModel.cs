namespace OnlineGym.Authentication.Models
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}


