.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------
Deleting
--------
Permanently remove Mesh data from collection.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         DELETE https://api.meshydb.com/{accountName}/meshes/{mesh}/{id} HTTP/1.1
         Authentication: Bearer {access_token}
         tenant: {tenant}
         
      |parameters|

      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      access_token : string, required
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      mesh : string, required
         Identifies name of mesh collection. e.g. person.
      id : string, required
         Idenfities location of what Mesh data to replace.

   .. group-tab:: C#
   
      .. code-block:: c#
         
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = await client.LoginAnonymouslyAsync(username);
      
         await connection.Meshes.DeleteAsync(person);

      |parameters|

      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         User name.
      meshName : string, required, default: class name
         Identifies name of mesh collection. e.g. person.
      id : string, required
         Idenfities location of what Mesh data to replace.
		 
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);
         
         client.loginAnonymously(username)
               .then(function (meshyConnection){
                        meshyConnection.meshes.delete(meshName, id)
                                              .then(function(_){ });
               }); 
      
      |parameters|

      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         User name.
      mesh : string, required
         Identifies name of mesh collection. e.g. person.
      id : string, required
         Idenfities location of what Mesh data to delete. In this case it will be from the Person mesh.
