## Search
``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users
```

``` c#
await client.Users.GetUsersAsync({nameParts},{roles},{activeOnly},{page},{pageSize});
```

## Get Me
``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users/me
```

``` c#
await client.Users.GetLoggedInUserAsync();
```

## Get by id
``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/users/{id}
```

``` c#
await client.Users.GetUserAsync({id});
```

## Forgot Password
``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/forgotpassword
```

``` c#
await database.ForgotPasswordAsync({username});
```

## Reset Password
``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/resetpassword
```

``` c#
await database.ResetPasswordAsync({resetHash},{newPassword});
```

## Change my Password
``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users/me/password
```

``` c#
await client.UpdatePasswordAsync({previousPassword},{newPassword});
```

## Create User
``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/users
```

``` c#
await database.CreateNewUserAsync({user});
```

## Update User
``` REST fct_label="REST"
PUT https://api.meshydb.com/{clientKey}/users/{id}
```

``` c#
```

## Delete User
``` REST fct_label="REST"
DELETE https://api.meshydb.com/{clientKey}/users/{id}
```

``` c#
await client.Users.DeleteUserAsync({id});
```
