.. |parameters| raw:: html

   <h4>Parameters</h4>

--------------------------
Registering Anonymous User
--------------------------
Creates an anonymous user that can log into the system.


.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{accountName}/users/register/anonymous HTTP/1.1
        Content-Type: application/json
        tenant: {tenant}
         
          {
            "username": "username_testermctesterson"
          }

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      access_token  : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      username : string, required
         Username of user.

   .. group-tab:: C#
   
      .. code-block:: c#
      
        var database = new MeshyDB(accountName, tenant, publicKey);

        var anonymousUser = await database.RegisterAnonymousUserAsync(userName);

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string, optional
         Username of user.
		
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(accountName, tenant, publicKey);
         
         database.registerAnonymousUser(username)
                 .then(function(anonymousUser) { });
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string, optional
         Username of user.
         
Example Response:

.. code-block:: json

  {
    "id": "5c78cc81dd870827a8e7b6c4",
    "username": "username_testermctesterson",
    "firstName": null,
    "lastName": null,
    "verified": false,
    "isActive": true,
    "phoneNumber": null,
    "roles": [],
    "securityQuestions": [],
	"anonymous": true
  }
