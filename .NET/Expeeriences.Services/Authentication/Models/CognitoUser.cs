using Amazon.CognitoIdentityProvider;

namespace Authentication.Models
{
    public class CognitoUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatusType Status { get; set; }
    }
}
