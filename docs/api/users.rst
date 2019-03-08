=====
Users
=====

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
----------
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
      
        POST https://api.meshydb.com/{clientKey}/users
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
_username_ : string, required
   Username of user.
_newPassword_ : string, required
   Password of user to use for login.
_id_ : string
   Identifier of user.
_firstName_ : string
   First name of user.
_lastName_ : string
   Last name of user.
_verified_ : boolean
   Identifies whether or not the user is verified.
_isActive_ : boolean
   Identifies whether or not the user is active.
_phoneNumber_ : string
   Phone number of user.
_roles_ : string[]
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

## Retrieve a single user
Retrieves details about an existing user.

``` http  fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users/{id}
Authentication: Bearer {access_token}
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();
  
await client.Users.GetUserAsync(id);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_id_  		    | Identifier of user.                                           | _string_|

Example Response:
```
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
```

## Retrieve myself
Retrieve details about the logged in user.

``` http  fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users/me
Authentication: Bearer {access_token}
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();

await client.Users.GetLoggedInUserAsync();
```

Example Response:
```
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

```
## Update User
Update a specific  user based on supplied object.

``` http  fct_label="REST"
PUT https://api.meshydb.com/{clientKey}/users/{id}
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
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();

var user = new User();

await client.Users.UpdateUserAsync(id, user);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_username_   | **required**  Username of user.                               | _string_|
|_id_  		    | Identifier of user.                                           | _string_|
|_firstName_  | First name of user.                                           | _string_|
|_lastName_   | Last name of user.                                            | _string_|
|_verified_   | Identifies whether or not the user is verified.               | _boolean_|
|_isActive_   | Identifies whether or not the user is active.                 | _boolean_|
|_phoneNumber_| Phone number of user.                                         | _string_|
|_roles_      | Collection of roles user has access.                          | _string[]_|

Example Response:
```
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
```
## Delete User
Permanently deletes a user. It cannot be undone.

``` http  fct_label="REST"
DELETE https://api.meshydb.com/{clientKey}/users/{id}
Authentication: Bearer {access_token}
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();

await client.Users.DeleteUserAsync(id);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_id_  		    | Identifier of user.                                           | _string_|

Example Response:
```
  {
    "deletedCount": 1,
    "isAcknowledged": true
  }

```
## Search
Returns a paged result of users.

``` http  fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users?query={query}&
                                              roles={roles}&
                                              activeOnly={activeOnly}&
                                              page={page}&
                                              pageSize={pageSize}
Authentication: Bearer {access_token}

(Line breaks added for readability)
```
``` c#
  var database = new MeshyDB({clientKey}, {publicKey});
  var client = await database.LoginWithAnonymouslyAsync();
  
  await client.Users.GetUsersAsync(query, roles, activeOnly, page, pageSize);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_query_      | Criteria is split on space and each  containing part must be  contained within a user's first, last or user name.                               | _string_|
|_roles_  		    | Collection of roles where a user must contain at least one of the roles supplied.                                          | _string[]_|
|_activeOnly_  | If false it will also bring back all inactive users.                                           | _boolean_|
|_page_  | Page number of users to bring back.                                           | _integer_|
|_pageSize_  | Number of results to bring back per  page. Maximum is 200.                                           | _integer_|

Example Response:
```
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
```

## Forgot Password
Creates a request for password reset that must have the matching data to reset to ensure request parity.

``` http  fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/forgotpassword
Authentication: Bearer {access_token}
Content-Type: application/json

Body:
  {
    "username": "username_testermctesterson"
  }
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_username_   | **required** User name to be reset.                           | _string_|

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
  
await database.ForgotPasswordAsync(username);
```

Example Response:
```
  {
    "username": "username_testermctesterson",
    "expires": "1-1-2019",
    "hash": "randomlygeneratedhash"
  }
```
## Reset Password
Uses result from Forgot password to allow a user to reset their password.

``` http  fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/resetpassword
Authentication: Bearer {access_token}
Content-Type: application/json

Body:
  {
    "username": "username_testermctesterson",
    "expires": "1-1-2019",
    "hash": "randomlygeneratedhash",
    "newPassword": "newPassword"
  }
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});

await database.ResetPasswordAsync(resetHash, newPassword);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_username_   | **required** User name that is being reset.                           | _string_|
|_expires_    | **required** Expiration of hash.                           | _date_|
|_hash_       | **required** Forgot password hash.                           | _string_|
|_newPassword_| **required** New password of user.                           | _string_|

## Change my Password
Allows the logged in user to change their password.

``` http  fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/me/password
Authentication: Bearer {access_token}
Content-Type: application/json

Body:
  {
    "newPassword": "newPassword",
    "previousPassword": "previousPassword"
  }
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();

await client.UpdatePasswordAsync(previousPassword, newPassword);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_previousPassword_   | **required** Previous password of user.                           | _string_|
|_newPassword_| **required** New password of user.                           | _string_|
