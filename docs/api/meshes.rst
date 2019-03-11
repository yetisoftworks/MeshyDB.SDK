=====
Meshes
=====

The following endpoints are used to manage Mesh data. 

A Mesh is a container that we use to store your data dynamically.

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

Parameters
^^^^^^^^^^
clientKey: string
   Indicates which tenant you are connecting for authentication.
access_token: string
   Token identifying authorization with MeshyDB requested during `Login`_.
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
           
   .. group-tab:: C#
   
      .. code-block:: c#

         person.FirstName = "Bobbo";

         person = await client.Meshes.UpdateAsync(person);


Parameters
^^^^^^^^^^
clientKey: string
   Indicates which tenant you are connecting for authentication.
access_token: string
   Token identifying authorization with MeshyDB requested during `Login`_.
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

   .. group-tab:: C#
   
      .. code-block:: c#

         var pagedPersonResult = await client.Meshes.SearchAsync<Person>(filter, page, pageSize);

Parameters
^^^^^^^^^^
clientKey: string
   Indicates which tenant you are connecting for authentication.
access_token: string
   Token identifying authorization with MeshyDB requested during `Login`_.
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

   .. group-tab:: C#
   
      .. code-block:: c#
      
         await client.Meshes.DeleteAsync(person);

Parameters
^^^^^^^^^^
clientKey: string
   Indicates which tenant you are connecting for authentication.
access_token: string
   Token identifying authorization with MeshyDB requested during `Login`_.
mesh : string
   Identifies name of mesh collection. e.g. person.
id : string
   Idenfities location of what Mesh data to replace.
