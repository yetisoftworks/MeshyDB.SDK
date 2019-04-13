.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-------------
Logging Out
-------------
Log user out.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{clientKey}/connect/revocation HTTP/1.1
        Content-Type: application/json

          {
            "token": {refresh_token},
            "token_type_hint": "refresh_token",
            "client_id": {publicKey}
          }

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      refresh_token  : string
         Refresh token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      publicKey : string
         Public accessor for application.
         
   .. group-tab:: C#
   
      .. code-block:: c#
      
        var database = new MeshyDB(clientKey, publicKey);
        var client = await database.LoginWithAnonymouslyAsync();

        await client.SignoutAsync();

      |parameters|
      
      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
