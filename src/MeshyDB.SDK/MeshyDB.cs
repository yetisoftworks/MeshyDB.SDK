using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MeshyDB.SDK.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace MeshyDB.SDK
{
    /// <summary>
    /// MeshyDB Client is used to connect with the MeshyDB REST API
    /// </summary>
    public class MeshyDB
    {
        /// <summary>
        /// Initializes a new instance of <seealso cref="MeshyDB"/> that is used to communicate with the MeshyDB REST API
        /// </summary>
        /// <param name="clientKey">Name of MeshyDB client key required for communication</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with client</param>
        /// <param name="privateKey">Private Api credential supplied from MeshyDB to communicate with client</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured</exception>
        public MeshyDB(string clientKey, string publicKey, IHttpService httpService = null) : this(clientKey, null, publicKey, httpService)
        {
        }

        /// <summary>
        /// Initializes a new instance of <seealso cref="MeshyDB"/> that is used to communicate with the MeshyDB REST API
        /// </summary>
        /// <param name="clientKey">Name of MeshyDB client key required for communication</param>
        /// <param name="tenant">Tenant of data used for partitioning</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with client</param>
        /// <param name="httpService">Http Service to use for making requests</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured</exception>
        public MeshyDB(string clientKey, string tenant, string publicKey, IHttpService httpService = null)
        {
            if (string.IsNullOrWhiteSpace(clientKey))
            {
                throw new ArgumentException($"{nameof(clientKey)} was not supplied", nameof(clientKey));
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException($"{nameof(publicKey)} was not supplied", nameof(publicKey));
            }

            ClientKey = clientKey.Trim();
            Tenant = tenant?.Trim();
            _publicKey = publicKey.Trim();

            HttpService = httpService ?? new HttpService();
        }

        private string _publicKey;

        private IHttpService _httpService;

        internal IHttpService HttpService
        {
            get
            {
                return _httpService;
            }
            set
            {
                if (value != null)
                {
                    _httpService = value;
                }
            }
        }

        private IAuthenticationService _authenticationService;

        internal IAuthenticationService AuthenticationService
        {
            get
            {
                if (_authenticationService == null)
                {
                    var authRequestService = new RequestService(HttpService, this.GetAuthUrl(), this.Tenant);
                    var tokenService = new TokenService(authRequestService, _publicKey);
                    var apiRequestService = new RequestService(HttpService, this.GetApiUrl(), this.Tenant);

                    _authenticationService = new AuthenticationService(tokenService, apiRequestService);
                }

                return _authenticationService;
            }
            set
            {
                _authenticationService = value;
            }
        }

        /// <summary>
        /// Gets the name of the client key for MeshyDB communication
        /// </summary>
        internal string ClientKey { get; }

        /// <summary>
        /// Gets the name of the tenant for MeshyDB communication
        /// </summary>
        internal string Tenant { get; }

        /// <summary>
        /// Gets the Api Url configured for the supplied Client
        /// </summary>
        /// <returns>The configured client Api Url communication</returns>
        internal string GetApiUrl()
        {
            return Constants.TemplateApiUrl.Replace("{clientKey}", this.ClientKey);
        }

        /// <summary>
        /// Gets the Auth Url configured for the supplied Client
        /// </summary>
        /// <returns>The configured client Auth Url communication</returns>
        internal string GetAuthUrl()
        {
            return Constants.TemplateAuthUrl.Replace("{clientKey}", this.ClientKey);
        }

        internal IRequestService RequestService { get; private set; }

        /// <summary>
        /// Instantiates services used for api communication with required dependencies
        /// </summary>
        private Tuple<ITokenService, IRequestService> GenerateAPIRequestService(string identifier)
        {
            var httpService = this.HttpService;
            var authRequestService = new RequestService(httpService, this.GetAuthUrl(), this.Tenant);
            var tokenService = new TokenService(authRequestService, _publicKey);
            var requestService = new RequestService(httpService, this.GetApiUrl(), this.Tenant, tokenService, identifier);

            return new Tuple<ITokenService, IRequestService>(tokenService, requestService);
        }

        #region Auth checks

        /// <summary>
        /// Log user in with username and password
        /// </summary>
        /// <param name="username">User name of user to login</param>
        /// <param name="password">Password of user to log in</param>
        /// <returns>Meshy client for user upon successful login</returns>
        public async Task<IMeshyClient> LoginWithPasswordAsync(string username, string password)
        {
            var identifier = await AuthenticationService.LoginWithPasswordAsync(username, password);
            var services = this.GenerateAPIRequestService(identifier);

            return new MeshyClient(services.Item1, services.Item2, identifier);
        }

        /// <summary>
        /// Log user in with username and password
        /// </summary>
        /// <param name="username">User name of user to login</param>
        /// <param name="password">Password of user to log in</param>
        /// <returns>Meshy client for user upon successful login</returns>
        public IMeshyClient LoginWithPassword(string username, string password)
        {
            var t = this.LoginWithPasswordAsync(username, password).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Login anonymously with randomly generated user
        /// </summary>
        /// <param name="username">Identify anonymous user. If nothing is provided it will be automatically generated.</param>
        /// <returns>Meshy client for user upon successful login</returns>
        public async Task<IMeshyClient> LoginAnonymouslyAsync(string username = null)
        {
            var identifier = await AuthenticationService.LoginAnonymouslyAsync(username);
            var services = this.GenerateAPIRequestService(identifier);

            return new MeshyClient(services.Item1, services.Item2, identifier);
        }

        /// <summary>
        /// Login anonymously with randomly generated user
        /// </summary>
        /// <param name="username">Identify anonymous user. If nothing is provided it will be automatically generated.</param>
        /// <returns>Meshy client for user upon successful login</returns>
        public IMeshyClient LoginAnonymously(string username = null)
        {
            var t = this.LoginAnonymouslyAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">User to be created with login credentials</param>
        /// <returns>User that was newly created</returns>
        public async Task<User> CreateNewUserAsync(NewUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentException($"Missing required information: {nameof(user.Username)}");
            }

            if (string.IsNullOrWhiteSpace(user.NewPassword))
            {
                throw new ArgumentException($"Missing required information: {nameof(user.Username)}");
            }

            return await this.AuthenticationService.CreateUserAsync(user);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">User to be created with login credentials</param>
        /// <returns>User that was newly created</returns>
        public User CreateNewUser(NewUser user)
        {
            var t = this.CreateNewUserAsync(user).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Request forgot password based on username
        /// </summary>
        /// <param name="username">Username of forgotton user</param>
        /// <returns>Hashed object to ensure forgot password parity request</returns>
        public async Task<PasswordResetHash> ForgotPasswordAsync(string username)
        {
            return await this.AuthenticationService.ForgotPasswordAsync(username);
        }

        /// <summary>
        /// Request forgot password based on username
        /// </summary>
        /// <param name="username">Username of forgotton user</param>
        /// <returns>Hashed object to ensure forgot password parity request</returns>
        public PasswordResetHash ForgotPassword(string username)
        {
            var t = this.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Resets password for forgot password request
        /// </summary>
        /// <param name="resetHash">Forgot password hash object to ensure forgot password was requested</param>
        /// <param name="newPassword">Password for new reset</param>
        /// <returns>Task to await success of reset</returns>
        public async Task ResetPasswordAsync(PasswordResetHash resetHash, string newPassword)
        {
            await this.AuthenticationService.ResetPasswordAsync(resetHash, newPassword);
        }

        /// <summary>
        /// Resets password for forgot password request
        /// </summary>
        /// <param name="resetHash">Forgot password hash object to ensure forgot password was requested</param>
        /// <param name="newPassword">Password for new reset</param>
        public void ResetPassword(PasswordResetHash resetHash, string newPassword)
        {
            var t = this.ResetPasswordAsync(resetHash, newPassword).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <summary>
        /// Login with peristance token from another session
        /// </summary>
        /// <param name="persistanceToken">Persistance token of previous session for login</param>
        /// <returns>Meshy client for user upon successful login</returns>
        public async Task<IMeshyClient> LoginWithPersistanceAsync(string persistanceToken)
        {
            var identifier = await AuthenticationService.LoginWithPersistanceAsync(persistanceToken);
            var services = this.GenerateAPIRequestService(identifier);

            return new MeshyClient(services.Item1, services.Item2, identifier);
        }

        /// <summary>
        /// Login with peristance token from another session
        /// </summary>
        /// <param name="persistanceToken">Persistance token of previous session for login</param>
        /// <returns>Meshy client for user upon successful login</returns>
        public IMeshyClient LoginWithPersistance(string persistanceToken)
        {
            var t = this.LoginWithPersistanceAsync(persistanceToken).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }
        #endregion
    }
}
