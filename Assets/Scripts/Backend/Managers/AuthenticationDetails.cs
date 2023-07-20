using JetBrains.Annotations;

namespace Backend.Managers
{
    public class AuthenticationDetails
    {
        public AuthenticationType Type { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }

    public enum AuthenticationType
    {
        Anonymous,
        Email
    }
}