.. role:: required

.. role:: type

.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------------------
Retrieving User Info
--------------------
Retrieve user information.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        GET https://auth.meshydb.com/{accountName}/connect/userinfo HTTP/1.1
        Authentication: Bearer {access_token}
        tenant: {tenant}
         
      |parameters|
      
      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      access_token : :type:`string`, :required:`required`
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.

   .. group-tab:: C#
   
      .. code-block:: c#
      
        var client = new MeshyClient(accountName, tenant, publicKey);
        var connection = await client.LoginAnonymouslyAsync(username);

        var userInfo = await connection.GetMyUserInfoAsync();

      |parameters|
      
      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      publicKey : :type:`string`, :required:`required`
         Public accessor for application.
      username : :type:`string`, :required:`required`
         User name.

   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);
         
         client.loginAnonymously(username)
               .then(function (meshyConnection){
                        meshyConnection.getMyUserInfo().then(function(info) { });
               }); 
      
      |parameters|

      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      publicKey : :type:`string`, :required:`required`
         Public accessor for application.
      username : :type:`string`, :required:`required`
         User name.
		 
Example Response:

.. code-block:: json

   {
       "sub": "5c990a772a8fc94ec4b3dc20",
       "name": "",
       "given_name": "",
       "family_name": "",
       "id": "login@email.com",
       "rate_limit": "10",
       "role": "admin"
   }
