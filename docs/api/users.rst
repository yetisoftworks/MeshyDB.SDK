=====
Users
=====

The following endpoints are used to manage Users.

They can help update user information or reset credentials.

.. |parameters| raw:: html

   <h4>Parameters</h4>

-------------
Create a User
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

---------------
Retrieve self
---------------
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
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.

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


-----------
Update self
-----------
Update details about the logged in user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         PUT https://api.meshydb.com/{clientKey}/users/me HTTP/1.1
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
                      ]
           }

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
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
      
         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();

         var user = new User();

         await client.Users.UpdateUserAsync(id, user);

      |parameters|
      
      clientKey  : string
         Indicates which tenant you are connecting for authentication.
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
  
---------------
Forgot Password
---------------
Creates a request for password reset that must have the matching data to reset to ensure request parity.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{clientKey}/users/forgotpassword HTTP/1.1
         Content-Type: application/json

           {
             "username": "username_testermctesterson"
           }

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      username : string
        User name to be reset.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(clientKey, publicKey);

         await database.ForgotPasswordAsync(username);

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
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

--------------
Reset Password
--------------
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
  
------------------
Change Password
------------------
Allows the logged in user to change their password.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{clientKey}/users/me/password HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json

           {
             "newPassword": "newPassword",
             "previousPassword": "previousPassword"
           }

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      access_token: string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
      previousPassword : string
        Previous password of user.
      newPassword : string
        New password of user.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();

         await client.UpdatePasswordAsync(previousPassword, newPassword);

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      previousPassword : string
        Previous password of user.
      newPassword : string
        New password of user.
