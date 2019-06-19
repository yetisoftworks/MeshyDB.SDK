.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-------------------
Forgetting Password
-------------------
Creates a request for password reset that must have the matching data to reset to ensure request parity.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{accountName}/users/forgotpassword HTTP/1.1
         Content-Type: application/json
         tenant: {tenant}
         
           {
             "username": "username_testermctesterson",
             "attempt": 1
           }

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      username : string, required
        User name to be reset.
	  attempt: int, required
		Identifies which number of times of request.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(accountName, tenant, publicKey);

         await database.ForgotPasswordAsync(username, attempt);

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
        User name to be reset.
	  attempt: int, required
		Identifies which number of times of request.


   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(accountName, tenant, publicKey);
         
         database.forgotPassword(username, attempt)
                 .then(function(passwordResetHash) { });
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
        User name to be reset.
	  attempt: int, required
		Identifies which number of times of request.

         
Example Response:

.. code-block:: json

	{
		username: "username_testermctesterson",
		attempt: 1:
		hash: "1900-01-01T00:00:00.000Z",
		expires: "1900-01-01T00:00:00.000Z",
		hint: "xxxx"
	}