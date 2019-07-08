.. |parameters| raw:: html

   <h4>Parameters</h4>
   
------------------
Resetting Password
------------------
Uses result from Forgot password to allow a user to reset their password.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{accountName}/users/resetpassword  HTTP/1.1
         Content-Type: application/json
         tenant: {tenant}
         
           {
             "username": "username_testermctesterson",
             "expires": "1-1-2019",
             "hash": "randomlygeneratedhash",
             "newPassword": "newPassword"
           }

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      username : string
        User name that is being reset.
      expires : date
        Expiration of hash.
      hash : string
        Forgot password hash.
      newPassword : string
        New password of user.
        
   .. group-tab:: C#
   
      .. code-block:: c#
      
         var client = new MeshyClient(accountName, tenant, publicKey);

         await client.ResetPasswordAsync(resetHash, newPassword);

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
        User name that is being reset.
      expires : date
        Expiration of hash.
      hash : string
        Forgot password hash.
      newPassword : string
        New password of user.


   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);
         
         client.forgotPassword(username)
                .then(function(passwordResetHash){
                        database.resetPassword(passwordResetHash, newPassword)
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
        User name that is being reset.
      expires : date
        Expiration of hash.
      hash : string
        Forgot password hash.
      newPassword : string
        New password of user.
