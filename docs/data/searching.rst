.. |parameters| raw:: html

   <h4>Parameters</h4>
   
---------
Searching
---------
Filter Mesh data from collection based on query parameters.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         GET https://api.meshydb.com/{accountName}/meshes/{mesh}?filter={filter}&
                                                               orderby={orderby}&
                                                               page={page}&
                                                               pageSize={pageSize} HTTP/1.1
         Authentication: Bearer {access_token}
         tenant: {tenant}
         
      (Line breaks added for readability)

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      filter : string
         Filter criteria for search. Uses MongoDB format.
      orderby : string
         How to order results. Uses MongoDB format.
      page : integer
         Page number of users to bring back.
      pageSize : integer, max: 200
         Number of results to bring back per page.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = await client.LoginAnonymouslyAsync(username);

         var pagedPersonResult = await connection.Meshes.SearchAsync<Person>(filter, page, pageSize);

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      filter : string
         Filter criteria for search. Uses MongoDB format.
      orderby : string
         How to order results. Uses MongoDB format.
      page : integer
         Page number of users to bring back.
      pageSize : integer, max: 200
         Number of results to bring back per page.


   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);

         client.loginAnonymously(username)
               .then(function (meshyConnection){
                  meshyConnection.meshes.search(meshName, 
                                                {
                                                   filter: filter,
                                                   orderby: orderby,
                                                   pageNumber: page,
                                                   pageSize: pageSize
                                                })
                                        .then(function(results){ });
                    }); 
      
      |parameters|
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      meshName : string
         Identifies name of mesh collection. e.g. person.
      username : string
         User name.
      filter : string
         Filter criteria for search. Uses MongoDB format.
      orderby : string
         How to order results. Uses MongoDB format.
      page : integer
         Page number of users to bring back.
      pageSize : integer, max: 200
         Number of results to bring back per page.
         
Example Response:

.. code-block:: json

  {
    "page": 1,
    "pageSize": 25,
    "results": [{
                 "_id":"5c78cc81dd870827a8e7b6c4",
                 "firstName": "Bobbo",
                 "lastName": "Bobberson"
               }],
    "totalRecords": 1
  }
