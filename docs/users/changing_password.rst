.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-----------------
Changing Password
-----------------
Allows the logged in user to change their password.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{accountName}/users/me/password HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json
         tenant: {tenant}
         
           {
             "newPassword": "newPassword",
             "previousPassword": "previousPassword"
           }

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      access_token: string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
      previousPassword : string
        Previous password of user.
      newPassword : string
        New password of user.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = await client.LoginWithPasswordAsync(username, password);

         await connection.UpdatePasswordAsync(previousPassword, newPassword);

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
      previousPassword : string
        Previous password of user.
      newPassword : string
        New password of user.


   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);
         
         client.login(username, password)
               .then(function (meshyConnection){
                     meshyConnection.updatePassword(previousPassword, newPassword)
                                    .then(function(_) { });
                     }); 
      
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
      previousPassword : string
        Previous password of user.
      newPassword : string
        New password of user.
