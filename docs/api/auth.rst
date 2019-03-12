=============
Authorization
=============

The following supported endpoints conform to the `OIDC <https://openid.net/developers/specs/>`_ protocol.

Currently MeshyDB only supports the password grant flow.

------
Generate Access Token
------

The following endpoints require to be authenticated.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://auth.meshydb.com/{clientKey}/connect/token HTTP/1.1
        Content-Type: application/x-www-form-urlencoded
        
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
Generate Refresh Token
------

The following endpoints require to be authenticated.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://auth.meshydb.com/{clientKey}/connect/token HTTP/1.1
        Content-Type: application/x-www-form-urlencoded
        
          client_id={publicKey}&
          grant_type=refresh_token&
          refresh_token={refresh_token}

        (Form-encoding removed and line breaks added for readability)

   .. group-tab:: C#
   
      .. code-block:: c#

        var database = new MeshyDB(clientKey, publicKey);
        var client = database.LoginWithPassword(username, password);
        var refreshToken = client.RetrievePersistanceToken();
        
        client = await database.LoginWithPersistanceAsync(refreshToken);

Parameters
^^^^^^^^^^
clientKey : string
   Indicates which tenant you are connecting for authentication.
publicKey : string
   Public accessor for application.
refresh_token : string
   Refresh token generated from  previous access token generation.
   
Example Response:

.. code-block:: json

  {
    "access_token": "ey...",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "ab23cd3343e9328g"
  }
