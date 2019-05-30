.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------
Updating
--------
Update Mesh data in collection by id.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         PUT https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}  HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json
         tenant: {tenant}

         {
          "firstName": "Bobbo",
          "lastName": "Bobberson"
         }

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.

   .. group-tab:: C#
   
      .. code-block:: c#

         var database = new MeshyDB(clientKey, tenant, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();
         
         person.FirstName = "Bobbo";

         person = await client.Meshes.UpdateAsync(person);
         
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.


   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(clientKey, tenant, publicKey);
         var client;
         database.loginAnonymously()
                 .then(function (meshyDBClient){
                     var refreshToken = meshyDBClient.meshes.update(meshName, 
                                                                    {
                                                                        firstName:"Bob",
                                                                        lastName:"Bobberson"
                                                                    },
                                                                    id)
                                                             .then(function(result){ });
                  }); 
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      meshName : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.
         
Example Response:

.. code-block:: json

  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson",
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
