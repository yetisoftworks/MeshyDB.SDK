.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------
Deleting
--------
Permanently remove Mesh data from collection.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         DELETE https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id} HTTP/1.1
         Authentication: Bearer {access_token}
         tenant: {tenant}
         
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
      
         await client.Meshes.DeleteAsync(person);

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
         Idenfities location of what Mesh data to delete. In this case it will be from the Person mesh.
