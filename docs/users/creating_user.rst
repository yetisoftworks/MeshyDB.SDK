.. |parameters| raw:: html

   <h4>Parameters</h4>

-------------
Creating User
-------------
Creates a new user that can log into the system.


.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{clientKey}/users HTTP/1.1
        Authentication: Bearer {access_token}
        Content-Type: application/json

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
            "newPassword": "newPassword"
          }

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      access_token  : string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
      username : string, required
         Username of user.
      newPassword : string, required
         Password of user to use for login.
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
      
        var database = new MeshyDB(clientKey, publicKey);

        var user = new NewUser();

        await database.CreateNewUserAsync(user);

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string, required
         Username of user.
      newPassword : string, required
         Password of user to use for login.
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
