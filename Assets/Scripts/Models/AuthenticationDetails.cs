using System;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class AuthenticationDetails
    {
        public AuthenticationType type = AuthenticationType.Anonymous;

        public string email;

        public string password;
    }

    [Serializable]
    public enum AuthenticationType
    {
        Anonymous,
        Email
    }
}