.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-------------
Updating self
-------------
Update details about the logged in user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         PUT https://api.meshydb.com/{clientKey}/users/me HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json
         tenant: {tenant}
         
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

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      username : string, required
         Username of user.
      id : string
         Identifier of user.
      firstName : string
         First name of user.
      lastName : string
         Last name of user.
      verified : boolean
         Identifies whether or not the user is verified.
      isActive : boolean
         Identifies whether or not the user is active.
      phoneNumber : string
         Phone number of user.
      roles : string[]
         Collection of roles user has access.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(clientKey, tenant, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();

         var user = new User();

         await client.Users.UpdateUserAsync(id, user);

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey  : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string, required
         Username of user.
      id : string
         Identifier of user.
      firstName : string
         First name of user.
      lastName : string
         Last name of user.
      verified : boolean
         Identifies whether or not the user is verified.
      isActive : boolean
         Identifies whether or not the user is active.
      phoneNumber : string
         Phone number of user.
      roles : string[]
         Collection of roles user has access.


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
