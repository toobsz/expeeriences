using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Authentication
{
    public class CognitoUserStore : IUserStore<CognitoUser>,
                                    IUserLockoutStore<CognitoUser>,
                                    IUserTwoFactorStore<CognitoUser>
    {
        private readonly AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId;
        private readonly string _poolId;
        private readonly string _identityPoolId;
        private readonly string _identityProvider;

        public CognitoUserStore(IConfiguration configuration)
        {
            _clientId = configuration["AWS_Cognito:CLIENT_ID"];
            _poolId = configuration["AWS_Cognito:USERPOOL_ID"];
            _identityPoolId = configuration["AWS_Cognito:IDENTITYPOOL_ID"];
            _identityProvider = configuration["AWS_Cognito:IDENTITY_PROVIDER"];
        }

        public Task CreateAsync(CognitoUser user)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Password = user.Password,
                Username = user.Email,
            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = user.Email
            };
            signUpRequest.UserAttributes.Add(emailAttribute);

            return _client.SignUpAsync(signUpRequest);
        }

        public Task<IdentityResult> CreateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<CognitoUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<CognitoUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(CognitoUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(CognitoUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(CognitoUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(CognitoUser user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(CognitoUser user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool _alreadyDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_alreadyDisposed) return;
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            _alreadyDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
