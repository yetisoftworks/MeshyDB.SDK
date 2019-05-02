.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-------------------
Forgetting Password
-------------------
Creates a request for password reset that must have the matching data to reset to ensure request parity.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{clientKey}/users/forgotpassword HTTP/1.1
         Content-Type: application/json
         tenant: {tenant}
         
           {
             "username": "username_testermctesterson"
           }

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      username : string
        User name to be reset.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(clientKey, tenant, publicKey);

         await database.ForgotPasswordAsync(username);

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
        User name to be reset.

Example Response:

.. code-block:: json

  {
    "username": "username_testermctesterson",
    "expires": "1900-01-01T00:00:00.000Z",
    "hash": "randomlygeneratedhash"
  }
