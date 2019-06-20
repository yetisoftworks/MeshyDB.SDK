.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------------
Retieving Self
--------------
Retrieve details about the logged in user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         GET https://api.meshydb.com/{accountName}/users/me HTTP/1.1
         Authentication: Bearer {access_token}
         tenant: {tenant}
         
      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName: string
         Indicates which account you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(accountName, tenant, publicKey);
         var client = await database.LoginAnonymouslyAsync(username);

         await client.Users.GetLoggedInUserAsync();

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.

   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(accountName, tenant, publicKey);
         
         database.loginAnonymously(username)
                 .then(function (meshyDBClient){
                     meshyDBClient.usersService.getSelf()
                                               .then(function(self) { });
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
		 
Example Response:

.. code-block:: json

  {
    "id": "5c78cc81dd870827a8e7b6c4",
    "username": "username_testermctesterson",
    "firstName": "Tester",
    "lastName": "McTesterton",
    "verified": true,
    "isActive": true,
    "phoneNumber": "5555555555",
    "roles": [
                "admin",
                "test"
             ],
    "securityQuestions": [
                            {
                               "question": "What would you say to this question?",
                               "answer": "mceasy123"
                            }
                         ],
    "anonymous": true
  }
