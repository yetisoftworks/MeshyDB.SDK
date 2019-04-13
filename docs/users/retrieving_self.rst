.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------------
Retieving self
--------------
Retrieve details about the logged in user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         GET https://api.meshydb.com/{clientKey}/users/me HTTP/1.1
         Authentication: Bearer {access_token}

      |parameters|
      
      clientKey: string
         Indicates which tenant you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();

         await client.Users.GetLoggedInUserAsync();

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
         
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
             ]
  }
