.. |parameters| raw:: html

   <h4>Parameters</h4>
   
----------------
Generating Token
----------------

Create a shortlived access token to be used for authorized API calls. Typically a token will last 3600 seconds(one hour).

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://auth.meshydb.com/{accountName}/connect/token HTTP/1.1
         Content-Type: application/x-www-form-urlencoded
         tenant: {tenant}

            client_id={publicKey}&
            grant_type=password&
            username={username}&
            password={password}&
            scope=meshy.api offline_access

        
      (Form-encoding removed and line breaks added for readability)

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.
      password : string
         User password.
   
   .. group-tab:: C#
   
      .. code-block:: c#

        var client = new MeshyClient(accountName, tenant, publicKey);
        var connection = client.LoginWithPassword(username, password);

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.
      password : string
         User password.

   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);

         client.login(username,password)
               .then(function (meshyConnection) { });
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
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
