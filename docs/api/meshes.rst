=====
Meshes
=====

The following endpoints are used to manage Mesh data. 

A Mesh is a container that we use to store your data dynamically.

.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-----------
Create data
-----------
Now that we are logged in we can use our Bearer token to authenticate requests with MeshyDB and create some data.

The data object can whatever information you would like to capture. The following example will have some data fields with example data.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         POST https://api.meshydb.com/{clientKey}/meshes/{mesh} HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json

            {
               "firstName": "Bob",
               "lastName": "Bobberson"
            }
            
      |parameters|

      clientKey: string
         Indicates which tenant you are connecting for authentication.
      access_token: string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
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

         var person = await client.Meshes.CreateAsync(new Person(){
           FirstName="Bob",
           LastName="Bobberson"
         });

      |parameters|

      clientKey: string
         Indicates which tenant you are connecting for authentication.
      access_token: string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
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
  
