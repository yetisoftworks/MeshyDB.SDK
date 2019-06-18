======
NodeJS
======
The first thing we need is some MeshyDB credentials. If you have not you can get started with a free account at `MeshyDB.com <https://meshydb.com/>`_.

Once we have done that we can go to Account and get our Client Key and Public Key.

Now that we have the required information let's jump in and see how easy it is to start with MeshyDB.

.. |parameters| raw:: html

   <h4>Parameters</h4>
  
-----------
Install SDK
-----------
Now that we have the required information let's start coding!

The supporting SDK is open source and your able to use a browser or NodeJS application.

Let's install the `MeshyDB.SDK <https://www.nuget.org/packages/MeshyDB.SDK/>`_ NuGet package with the following command:

.. code-block:: powershell

   npm install @meshydb/sdk

Now we are configured and we can get started!

-----
Login
-----
Let's log in using our MeshyDB credentials.

.. tabs::
   
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(accountName, tenant, publicKey);

         var meshyDBClient;
        
         database.login(username,password)
                 .then(function (client) { meshyDBClient = client; });
				 
		// Or log in anonomously
		database.loginAnonmously()
				.then(function (client) { meshyDBClient = client; });
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string
         User name.
      password : string
         User password.


Example Response:

.. code-block:: json

  {
    "access_token": "ey...",
    "expires_in": 3600,
    "token_type": "Bearer",
    "refresh_token": "ab23cd3343e9328g"
  }
 
-----------
Create data
-----------
Now that we are logged in we can use our Bearer token to authenticate requests with MeshyDB and create some data.

The data object can whatever information you would like to capture. The following example will have some data fields with example data.

.. tabs::
   
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
        
         var person = {
                            firstName:"Bob",
                            lastName:"Bobberson"
                      };
                      
         meshyDBClient.meshes.create(meshName, person)
                             .then(function(result) { person = result; });
      
      |parameters|

      meshName : string
         Identifies name of mesh collection. e.g. person.

Example Response:

.. code-block:: json

  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bob",
    "lastName": "Bobberson",
    "_rid": "https://api.meshydb.com/{accountName}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
  
-----------
Update data
-----------
If we need to make a modificaiton let's update our Mesh!

.. tabs::

   .. group-tab:: NodeJS
      
      .. code-block:: javascript

        person.firstName = "Bobbo";
        
        meshyDBClient.meshes.update(meshName, person, person._id)
                            .then(function(result){ person = result; });
      
      |parameters|

      meshName : string
         Identifies name of mesh collection. e.g. person.
      id : string

Example Response:

.. code-block:: json

  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson",
    "_rid":"https://api.meshydb.com/{accountName}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }

-----------
Search data
-----------
Let's see if we can find Bobbo.

.. tabs::

   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         

         meshyDBClient.meshes.search(meshName, 
                                    {
                                       filter: { "firstName": "Bobbo" },
                                       orderby: null,
                                       pageNumber: 1,
                                       pageSize: 25
                                    })
                             .then(function(results){ });
      
      |parameters|

      meshName : string
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
                 "_rid":"https://api.meshydb.com/{accountName}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
               }],
    "totalRecords": 1
  }

-----------
Delete data
-----------
We are now done with our data, so let us clean up after ourselves.

.. tabs::


   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         meshyDBClient.meshes.delete(meshName, person._id)
                             .then(function(_){ });
         
      |parameters|

      meshName : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.

--------
Sign out
--------
Now the user is complete. Let us sign out so someone else can have a try.

.. tabs::

   .. group-tab:: NodeJS
      
      .. code-block:: javascript

         meshyDBClient.signout()
                      .then(function(result) { });
      
      |parameters|

      No parameters provided. The client is aware of who needs to be signed out.
