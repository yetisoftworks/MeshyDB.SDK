# Getting Started

The following endpoints require to be authenticated.

``` REST fct_label="REST"
POST https://auth.meshydb.com/{clientKey}/connect/token
Body(x-www-form-urlencoded):  
  client_id={publicKey}&grant_type=password&username={username}&password={password}&scope=meshy.api%20offline_access

Example Response:
  {
    "access_token": "ey...",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "ab23cd3343e9328g"
  }
```

```c#
  var database = new MeshyDB({clientKey}, {publicKey});
  var client = database.LoginWithPassword({username}, {password});
```



| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_clientKey_  | Indicates which tenant you are connecting for authentication. | _string_|
|_publicKey_  | Public accessor for application.                              | _string_|
|_username_   | User name.                                                    | _string_|
|_password_   | User password.                                                | _string_|
  
## Create
Creates a new user that can log into the system.

``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users
Headers:
  Authentication: Bearer {access_token}
Body(json):
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
  
Example Response:
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

await database.CreateNewUserAsync(user);
```



| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_username_   | **required**  Username of user.                               | _string_|
|_id_  		    | Identifier of user.                                           | _string_|
|_newPassword_| **required**  Password of user to use for login.              | _string_|
|_firstName_  | First name of user.                                           | _string_|
|_lastName_   | Last name of user.                                            | _string_|
|_verified_   | Identifies whether or not the user is verified.               | _boolean_|
|_isActive_   | Identifies whether or not the user is active.                 | _boolean_|
|_phoneNumber_| Phone number of user.                                         | _string_|
|_roles_      | Collection of roles user has access.                          | _string[]_|

    
## Retrieve a single user
Retrieves details about an existing user.

``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users/{id}
Headers:
  Authentication: Bearer {access_token}
Example Response:
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
  
await client.Users.GetUserAsync(id);
```

## Retrieve myself
Retrieve details about the logged in user.

``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users/me
Headers:
  Authentication: Bearer {access_token}
Example Response:
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

await client.Users.GetLoggedInUserAsync();
```

## Update User
Update a specific  user based on supplied object.

``` REST fct_label="REST"
PUT https://api.meshydb.com/{clientKey}/users/{id}
Headers:
  Authentication: Bearer {access_token}
Body(json):
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
  
Example Response:
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

await client.Users.UpdateUserAsync(id, user);
```

## Delete User
Permanently deletes a user. It cannot be undone.

``` REST fct_label="REST"
DELETE https://api.meshydb.com/{clientKey}/users/{id}
Headers:
  Authentication: Bearer {access_token}
  
Example Response:
  {
    "deletedCount": 1,
    "isAcknowledged": true
  }
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();

await client.Users.DeleteUserAsync(id);
```

## Search
Returns a paged result of users.

``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users?query={query}&roles={roles}&activeOnly={activeOnly}&page={page}&pageSize={pageSize}
Headers:
  Authentication: Bearer {access_token}
Example Response:
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

``` c#
  var database = new MeshyDB({clientKey}, {publicKey});
  var client = await database.LoginWithAnonymouslyAsync();
  
  await client.Users.GetUsersAsync(query, roles, activeOnly, page, pageSize);
```

## Forgot Password
Creates a request for password reset that must have the matching data to reset to ensure request parity.

``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/forgotpassword
Headers:
  Authentication: Bearer {access_token}
Body(json):
  {
    "username": "username_testermctesterson"
  }
  
Example Response:
  {
    "username": "username_testermctesterson",
    "expires": "1-1-2019",
    "hash": "randomlygeneratedhash"
  }
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
  
await database.ForgotPasswordAsync(username);
```

## Reset Password
Uses result from Forgot password to allow a user to reset their password.

``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/resetpassword
Headers:
  Authentication: Bearer {access_token}
Body(json):
  {
    "username": "username_testermctesterson",
    "expires": "1-1-2019",
    "hash": "randomlygeneratedhash",
    "newPassword":"newPassword"
  }
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});

await database.ResetPasswordAsync(resetHash, newPassword);
```

## Change my Password
Allows the logged in user to change their password.

``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/me/password
Headers:
  Authentication: Bearer {access_token}
Body(json):
  {
    "newPassword": "newPassword",
    "previousPassword: "previousPassword"
  }
```

``` c#
var database = new MeshyDB({clientKey}, {publicKey});
var client = await database.LoginWithAnonymouslyAsync();

await client.UpdatePasswordAsync(previousPassword, newPassword);
```
