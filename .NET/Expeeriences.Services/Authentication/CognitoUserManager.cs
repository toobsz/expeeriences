using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.DynamoDBv2;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Authentication
{
    public class CognitoUserManager : UserManager<CognitoUser>
    {
        private readonly AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId;
        private readonly string _poolId;
        private readonly string _identityPoolId;
        private readonly string _identityProvider;

        public CognitoUserManager(IUserStore<CognitoUser> store, IConfiguration configuration)
            : base(store, null, null, null, null, null, null, null, null)
        {
            _clientId = configuration["AWS_Cognito:CLIENT_ID"];
            _poolId = configuration["AWS_Cognito:USERPOOL_ID"];
            _identityPoolId = configuration["AWS_Cognito:IDENTITYPOOL_ID"];
            _identityProvider = configuration["AWS_Cognito:IDENTITY_PROVIDER"];
        }

        public override Task<bool> CheckPasswordAsync(CognitoUser user, string password)
        {
            return CheckPasswordAsync(user.Email, password);
        }

        private async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            try
            {
                var authReq = new AdminInitiateAuthRequest
                {
                    UserPoolId = _poolId,
                    ClientId = _clientId,
                    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
                };
                authReq.AuthParameters.Add("USERNAME", userName);
                authReq.AuthParameters.Add("PASSWORD", password);

                AdminInitiateAuthResponse authResp = await _client.AdminInitiateAuthAsync(authReq);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
