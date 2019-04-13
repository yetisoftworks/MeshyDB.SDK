.. |parameters| raw:: html

   <h4>Parameters</h4>
   
------------------
Resetting Password
------------------
Uses result from Forgot password to allow a user to reset their password.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{clientKey}/users/resetpassword  HTTP/1.1
         Content-Type: application/json

           {
             "username": "username_testermctesterson",
             "expires": "1-1-2019",
             "hash": "randomlygeneratedhash",
             "newPassword": "newPassword"
           }

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
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
      
         var database = new MeshyDB(clientKey, publicKey);

         await database.ResetPasswordAsync(resetHash, newPassword);

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
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
