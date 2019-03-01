# Getting started
The first thing we need is some MeshyDB credentials. If you have not you can get started with a free account at [MeshyDB.com](https://meshydb.com).

Once we have done that we can go to Account and get our Client Key and Public Key.

Now that we have the required information let's jump in and see how easy it is to start with MeshyDB.

## Login
Let's log in using our MeshyDB credentials.

``` rest
POST https://api.meshydb.com/{clientKey}/connect/token
Body:  
  {
      'client_id': {publicKey},
      'username': {username},
      'password': {password},
      'grant_type': "password",
      'scope': 'meshy.api offline_access'
  }
```

```c#
  var database = new MeshyDB({clientKey},{publicKey});
  var client = database.LoginWithPassword(userName, password);
```

## Create data

## Update data

## Search data

## Delete data

## Sign out
