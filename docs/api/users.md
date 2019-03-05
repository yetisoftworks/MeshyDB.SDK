## Search
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
await client.Users.GetUsersAsync(nameParts, roles, activeOnly, page, pageSize);
```

## Get Me
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
await client.Users.GetLoggedInUserAsync();
```

## Get by id
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
await client.Users.GetUserAsync({id});
```

## Forgot Password
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
await database.ForgotPasswordAsync({username});
```

## Reset Password
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
await database.ResetPasswordAsync({resetHash},{newPassword});
```

## Change my Password
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
await client.UpdatePasswordAsync({previousPassword},{newPassword});
```

## Create User
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
await database.CreateNewUserAsync({user});
```

## Update User
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
await client.Users.UpdateUserAsync({id},{user});
```

## Delete User
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
await client.Users.DeleteUserAsync({id});
```
