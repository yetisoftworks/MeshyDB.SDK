# Getting started
The first thing we need is some MeshyDB credentials. If you have not you can get started with a free account at [MeshyDB.com](https://meshydb.com).

Once we have done that we can go to Account and get our Client Key and Public Key.

Now that we have the required information let's jump in and see how easy it is to start with MeshyDB.

## Login
Let's log in using our MeshyDB credentials.

``` rest
POST https://api.meshydb.com/{clientKey}/connect/token
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

_clientKey_: 
  Indicates which tenant you are connecting for authentication.
  
_publicKey_: 
  Public accessor for application.
  
_username_:
  User name.

_password_:
  User password.
 
## Create data

## Update data

## Search data

## Delete data

## Sign out
