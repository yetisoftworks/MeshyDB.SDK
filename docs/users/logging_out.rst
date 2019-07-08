.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-------------
Logging Out
-------------
Log user out.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{accountName}/connect/revocation HTTP/1.1
        Content-Type: application/x-www-form-urlencoded
        tenant: {tenant}
         
          token={refresh_token}&
          token_type_hint=refresh_token&
          client_id={publicKey}

        (Form-encoding removed and line breaks added for readability)

      |parameters|
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      refresh_token  : string, required
         Refresh token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      publicKey : string, required
         Public accessor for application.
         
   .. group-tab:: C#
   
      .. code-block:: c#
      
        var client = new MeshyClient(accountName, tenant, publicKey);
        var connection = await client.LoginAnonymouslyAsync(username);

        await connection.SignoutAsync();

      |parameters|
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         User name.

   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);

         client.loginAnonymously(username)
               .then(function (meshyConnection){
                        meshyConnection.signout()
                                       .then(function(result) { });
               }); 
      
      |parameters|

      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         User name.