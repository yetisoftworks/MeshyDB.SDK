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
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName: string, required
         Indicates which account you are connecting for authentication.
      access_token : string, required
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = await client.LoginAnonymouslyAsync(username);

         await connection.Users.GetLoggedInUserAsync();

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
                        meshyConnection.usersService.getSelf()
                                                    .then(function(self) { });
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
