=====
Users
=====

---------------
Getting Started
---------------

The following endpoints require to be authenticated.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://auth.meshydb.com/{clientKey}/connect/token HTTP/1.1
        Content-Type: application/x-www-form-urlencoded
        
        Body:
          client_id={publicKey}&
          grant_type=password&
          username={username}&
          password={password}&
          scope=meshy.api offline_access

        (Form-encoding removed and line breaks added for readability)

   .. group-tab:: C#
   
      .. code-block:: c#

        var database = new MeshyDB(clientKey, publicKey);
        var client = database.LoginWithPassword(username, password);

Parameters
^^^^^^^^^^
clientKey : string
   Indicates which tenant you are connecting for authentication.
publicKey : string
   Public accessor for application.
username : string
   User name.
password : string
   User password.
   
Example Response:

.. code-block:: json

  {
    "access_token": "ey...",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "ab23cd3343e9328g"
  }
  
------
Create
------
Creates a new user that can log into the system.


.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{clientKey}/users HTTP/1.1
        Authentication: Bearer {access_token}
        Content-Type: application/json

        Body:
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

   .. group-tab:: C#
   
      .. code-block:: c#
      
        var database = new MeshyDB(clientKey, publicKey);

        var user = new NewUser();

        await database.CreateNewUserAsync(user);

Parameters
^^^^^^^^^^
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

----------------------
Retrieve a single user
----------------------
Retrieves details about an existing user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         GET https://api.meshydb.com/{clientKey}/users/{id} HTTP/1.1
         Authentication: Bearer {access_token}

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});
         var client = await database.LoginWithAnonymouslyAsync();

         await client.Users.GetUserAsync(id);

Parameters
^^^^^^^^^^
id : string
   Identifier of user.

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
Retrieve myself
---------------
Retrieve details about the logged in user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         GET https://api.meshydb.com/{clientKey}/users/me
         Authentication: Bearer {access_token}

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});
         var client = await database.LoginWithAnonymouslyAsync();

         await client.Users.GetLoggedInUserAsync();

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
Update User
-----------
Update a specific  user based on supplied object.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         PUT https://api.meshydb.com/{clientKey}/users/{id} HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json

         Body:
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

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});
         var client = await database.LoginWithAnonymouslyAsync();

         var user = new User();

         await client.Users.UpdateUserAsync(id, user);

Parameters
^^^^^^^^^^
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

-----------
Delete User
-----------
Permanently deletes a user. It cannot be undone.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         DELETE https://api.meshydb.com/{clientKey}/users/{id} HTTP/1.1
         Authentication: Bearer {access_token}

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});
         var client = await database.LoginWithAnonymouslyAsync();

         await client.Users.DeleteUserAsync(id);

Parameters
^^^^^^^^^^
id : string
   Identifier of user.
   
Example Response:

.. code-block:: json

  {
    "deletedCount": 1,
    "isAcknowledged": true
  }

------
Search
------
Returns a paged result of users.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         GET https://api.meshydb.com/{clientKey}/users?query={query}&
                                                 roles={roles}&
                                                 activeOnly={activeOnly}&
                                                 page={page}&
                                                 pageSize={pageSize}
         Authentication: Bearer {access_token}

         (Line breaks added for readability)

   .. group-tab:: C#
   
      .. code-block:: c#
      
        var database = new MeshyDB({clientKey}, {publicKey});
        var client = await database.LoginWithAnonymouslyAsync();

        await client.Users.GetUsersAsync(query, roles, activeOnly, page, pageSize);

Parameters
^^^^^^^^^^
query : string
  Criteria is split on space and each  containing part must be  contained within a user's first, last or user name.
roles : string
  Collection of roles where a user must contain at least one of the roles supplied.
activeOnly : string
  If false it will also bring back all inactive users.
page : integer, default: 1
  Page number of users to bring back.
pageSize : integer, default: 25, max: 200
   Number of results to bring back per  page.

Example Response:

.. code-block:: json

  {
    "page": 1,
    "pageSize": 25,
    "resultss": [
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
    ],
    "totalRecords": 1
  }

---------------
Forgot Password
---------------
Creates a request for password reset that must have the matching data to reset to ensure request parity.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{clientKey}/users/forgotpassword HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json

         Body:
           {
             "username": "username_testermctesterson"
           }

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});

         await database.ForgotPasswordAsync(username);

Parameters
^^^^^^^^^^
username : string
  User name to be reset.

Example Response:

.. code-block:: json

  {
    "username": "username_testermctesterson",
    "expires": "1-1-2019",
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
         Authentication: Bearer {access_token}
         Content-Type: application/json

         Body:
           {
             "username": "username_testermctesterson",
             "expires": "1-1-2019",
             "hash": "randomlygeneratedhash",
             "newPassword": "newPassword"
           }
           
   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});

         await database.ResetPasswordAsync(resetHash, newPassword);

Parameters
^^^^^^^^^^
username : string
  User name that is being reset.
expires : date
  Expiration of hash.
hash : string
  Forgot password hash.
newPassword : string
  New password of user.
  
------------------
Change my Password
------------------
Allows the logged in user to change their password.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         POST https://api.meshydb.com/{clientKey}/users/me/password HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json

         Body:
           {
             "newPassword": "newPassword",
             "previousPassword": "previousPassword"
           }

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var database = new MeshyDB({clientKey}, {publicKey});
         var client = await database.LoginWithAnonymouslyAsync();

         await client.UpdatePasswordAsync(previousPassword, newPassword);

Parameters
^^^^^^^^^^
_previousPassword_ : string
  Previous password of user.
_newPassword_ : string
  New password of user.
