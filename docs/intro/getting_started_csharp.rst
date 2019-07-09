.. role:: required

.. role:: type

==
C#
==
The first thing we need is some MeshyDB credentials. If you have not you can get started with a free account at `MeshyDB.com <https://meshydb.com/>`_.

Once we have done that we can go to Account and get our Account Name. Next, we can go to Clients and get the Public Key.

Now that we have the required information let's jump in and see how easy it is to start with MeshyDB.

.. |parameters| raw:: html

   <h4>Parameters</h4>
  
-----------
Install SDK
-----------
Now that we have the required information let's start coding!

The  supporting SDK is open source and your able to use .Net Framework 4.7.1+ or .Net Core 2.x.

Let's install the `MeshyDB.SDK <https://www.nuget.org/packages/MeshyDB.SDK/>`_ NuGet package with the following command:

.. code-block:: powershell

   Install-Package MeshyDb.SDK

Now we are configured and we can get started!

-----
Login
-----
Let's log in using our MeshyDB credentials.

.. tabs::
   
   .. group-tab:: C#
   
      .. code-block:: c#
   
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = client.LoginWithPassword(username, password);
         
         // Or log in anonomously
         connection = client.LoginAnonymouslyAsync(username);
         
      |parameters|

      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      publicKey : :type:`string`, :required:`required`
         Public accessor for application.
      username : :type:`string`, :required:`required`
         User name.
      password : :type:`string`, :required:`required`
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
   
   .. group-tab:: C#
   
      .. code-block:: c#

         // Mesh is derived from class name
         public class Person: MeshData
         {
           public string FirstName { get; set; }
           public string LastName { get; set; }
         }

         var person = await connection.Meshes.CreateAsync(new Person(){
           FirstName="Bob",
           LastName="Bobberson"
         });

      |parameters|

      mesh : :type:`string`, :required:`required`, default: class name
         Identifies name of mesh collection. e.g. person.

Example Response:

.. code-block:: json

  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bob",
    "lastName": "Bobberson"
  }
  
-----------
Update data
-----------
If we need to make a modificaiton let's update our Mesh!

.. tabs::

   .. group-tab:: C#
   
      .. code-block:: c#

         person.FirstName = "Bobbo";

         person = await connection.Meshes.UpdateAsync(person);

      |parameters|

      mesh : :type:`string`, :required:`required`, default: class name
         Identifies name of mesh collection. e.g. person. The id of the person to be updated will be derived from the object.

Example Response:

.. code-block:: json

  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson"
  }

-----------
Search data
-----------
Let's see if we can find Bobbo.

.. tabs::

   .. group-tab:: C#
   
      .. code-block:: c#

         var pagedPersonResult = await connection.Meshes.SearchAsync<Person>(filter, page, pageSize);

      |parameters|

      mesh : :type:`string`, :required:`required`, default: class name
         Identifies name of mesh collection. e.g. person.
      filter : :type:`string`
         Filter criteria for search. Uses MongoDB format.
      orderby : :type:`string`
         How to order results. Uses MongoDB format.
      page : :type:`integer`
         Page number of users to bring back.
      pageSize : :type:`integer`, max: 200, default: 25
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

-----------
Delete data
-----------
We are now done with our data, so let us clean up after ourselves.

.. tabs::

   .. group-tab:: C#
   
      .. code-block:: c#
      
         await connection.Meshes.DeleteAsync(person);

      |parameters|

      mesh : :type:`string`, :required:`required`, default: class name
         Identifies name of mesh collection. e.g. person. The id of the person to be deleted will be derived from the object.

--------
Sign out
--------
Now the user is complete. Let us sign out so someone else can have a try.

.. tabs::

   .. group-tab:: C#
   
      .. code-block:: c#

         await connection.SignoutAsync();
         
      |parameters|

      No parameters provided. The client is aware of who needs to be signed out.
