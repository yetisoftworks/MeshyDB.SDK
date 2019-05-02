.. |parameters| raw:: html

   <h4>Parameters</h4>
   
--------
Creating
--------
Create new data into Mesh collection. If it is the first time this Mesh is being used a new collection will be created automatically.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         POST https://api.meshydb.com/{clientKey}/meshes/{mesh} HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json
         tenant: {tenant}
         
            {
               "firstName": "Bob",
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
   
   .. group-tab:: C#
   
      .. code-block:: c#

         // Mesh is derived from class name
         public class Person: MeshData
         {
           public string FirstName { get; set; }
           public string LastName { get; set; }
         }

         var database = new MeshyDB(clientKey, tenant, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();
         
         var person = await client.Meshes.CreateAsync(new Person(){
           FirstName="Bob",
           LastName="Bobberson"
         });

      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      clientKey : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      mesh : string
         Identifies name of mesh collection. e.g. person.

Example Response:

.. code-block:: json

  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bob",
    "lastName": "Bobberson",
    "_rid": "https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
