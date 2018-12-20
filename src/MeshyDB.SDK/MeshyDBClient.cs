using MeshyDB.SDK.Services;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MeshyDB.SDK.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace MeshyDB.SDK
{
    /// <summary>
    /// MeshyDB Client is used to connect with the MeshyDB REST API
    /// </summary>
    public class MeshyDBClient
    {
        /// <summary>
        /// Initializes a new instance of <seealso cref="MeshyDBClient"/> that is used to communicate with the MeshyDB REST API
        /// </summary>
        /// <param name="tenant">Name of MeshyDB tenant required for communication</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant</param>
        /// <param name="privateKey">Private Api credential supplied from MeshyDB to communicate with tenant</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured</exception>
        public MeshyDBClient(string tenant, string publicKey, string privateKey)
        {
            if (string.IsNullOrWhiteSpace(tenant))
            {
                throw new ArgumentException($"{nameof(tenant)} was not supplied", nameof(tenant));
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException($"{nameof(publicKey)} was not supplied", nameof(publicKey));
            }

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentException($"{nameof(privateKey)} was not supplied", nameof(privateKey));
            }

            Tenant = tenant.Trim();

            _publicKey = publicKey.Trim();
            _privateKey = privateKey.Trim();

            ConfigureServices();
        }
        private string _publicKey;
        private string _privateKey;

        private IHttpService _httpService = new HttpService();

        public IHttpService HttpService
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
                    this.ConfigureServices();
                }
            }
        }

        /// <summary>
        /// Gets the name of the tenant for MeshyDB communication
        /// </summary>
        internal string Tenant { get; }

        /// <summary>
        /// Gets the Api Url configured for the supplied Tenant
        /// </summary>
        /// <returns>The configured tenant Api Url communication</returns>
        internal string GetApiUrl()
        {
            return Constants.TemplateApiUrl.Replace("{tenant}", this.Tenant);
        }

        /// <summary>
        /// Gets the Auth Url configured for the supplied Tenant
        /// </summary>
        /// <returns>The configured tenant Auth Url communication</returns>
        internal string GetAuthUrl()
        {
            return Constants.TemplateAuthUrl.Replace("{tenant}", this.Tenant);
        }

        /// <summary>
        /// Gets the service configured to communicate with tenant meshes endpoint
        /// </summary>
        public IMeshesService Meshes { get; private set; }

        /// <summary>
        /// Instantiates services used for api communication with required dependencies
        /// </summary>
        private void ConfigureServices()
        {
            var httpService = this.HttpService;
            var authRequestService = new RequestService(httpService, this.GetAuthUrl());
            var tokenService = new TokenService(authRequestService, _publicKey, _privateKey);
            var requestService = new RequestService(httpService, this.GetApiUrl(), tokenService);
            Meshes = new MeshesService(requestService);
        }
    }
}
