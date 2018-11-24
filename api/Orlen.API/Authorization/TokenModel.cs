namespace Orlen.API.Authorization
{
    public class TokenModel
    {
        public string ExpiryDate { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}