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
      access_token : string
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

         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();
         
         var person = await client.Meshes.CreateAsync(new Person(){
           FirstName="Bob",
           LastName="Bobberson"
         });

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
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
  
-----------
Update data
-----------
If we need to make a modificaiton let's update our Mesh!

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         PUT https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}  HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json

         {
          "firstName": "Bobbo",
          "lastName": "Bobberson"
         }

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.

   .. group-tab:: C#
   
      .. code-block:: c#

         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();
         
         person.FirstName = "Bobbo";

         person = await client.Meshes.UpdateAsync(person);
         
      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      mesh : string
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

-----------
Search data
-----------
Let's see if we can find Bobbo.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         GET https://api.meshydb.com/{clientKey}/meshes/{mesh}?filter={filter}&
                                                               orderby={orderby}&
                                                               page={page}&
                                                               pageSize={pageSize} HTTP/1.1
         Authentication: Bearer {access_token}

         
      (Line breaks added for readability)

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
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
      
         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();

         var pagedPersonResult = await client.Meshes.SearchAsync<Person>(filter, page, pageSize);

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
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


Example Response:

.. code-block:: json

  {
    "page": 1,
    "pageSize": 25,
    "results": [{
                 "_id":"5c78cc81dd870827a8e7b6c4",
                 "firstName": "Bobbo",
                 "lastName": "Bobberson",
                 "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
               }],
    "totalRecords": 1
  }

-----------
Delete data
-----------
We are now done with our data, so let us clean up after ourselves.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         DELETE https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id} HTTP/1.1
         Authentication: Bearer {access_token}

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
      access_token : string
         Token identifying authorization with MeshyDB requested during `Generate Access Token <auth.html#generate-access-token>`_.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.

   .. group-tab:: C#
   
      .. code-block:: c#
         
         var database = new MeshyDB(clientKey, publicKey);
         var client = await database.LoginWithAnonymouslyAsync();
      
         await client.Meshes.DeleteAsync(person);

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to delete. In this case it will be from the Person mesh.
