.. |parameters| raw:: html

   <h4>Parameters</h4>
   
----------------
Refreshing Token
----------------

Using the token request made to generate an access token, a refresh token will also be generated. Once the token expires the refresh token can be used to generate a new set of credentials for authorized calls.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://auth.meshydb.com/{clientKey}/connect/token HTTP/1.1
         Content-Type: application/x-www-form-urlencoded
         tenant: {tenant}
         
            client_id={publicKey}&
            grant_type=refresh_token&
            refresh_token={refresh_token}

        
      (Form-encoding removed and line breaks added for readability)

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      refresh_token : string
         Refresh token generated from  previous access token generation.

   .. group-tab:: C#
   
      .. code-block:: c#

        var database = new MeshyDB(clientKey, tenant, publicKey);
        var client = database.LoginWithPassword(username, password);
        var refreshToken = client.RetrievePersistanceToken();
        
        client = await database.LoginWithPersistanceAsync(refreshToken);

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.
      password : string
         User password.
      refreshToken : string
         Refresh token generated from  previous access token generation.
         
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(clientKey, tenant, publicKey);
         var client;
         database.login(username,password)
                  .then(function (meshyDBClient){
                     var refreshToken = meshyDBClient.retrievePersistanceToken();
                     
                     database.loginWithPersistance(refreshToken)
                             .then(function(refreshedMeshyDBClient){
                                 client = refreshedMeshyDBClient;
                             });
                  });
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.
      password : string
         User password.
         
Example Response:

.. code-block:: json

  {
    "access_token": "ey...",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "ab23cd3343e9328g"
  }
