====
REST
====
The first thing we need is some MeshyDB credentials. If you have not you can get started with a free account at `MeshyDB.com <https://meshydb.com/>`_.

Once we have done that we can go to Account and get our Client Key and Public Key.

Now that we have the required information let's jump in and see how easy it is to start with MeshyDB.

.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-----
Login
-----
Let's log in using our MeshyDB credentials.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http

         POST https://auth.meshydb.com/{clientKey}/connect/token HTTP/1.1
         Content-Type: application/x-www-form-urlencoded

            client_id={publicKey}&
            grant_type=password&
            username={username}&
            password={password}&
            scope=meshy.api offline_access

      (Form-encoding removed and line breaks added for readability)

      |parameters|

      clientKey : string
         Indicates which tenant you are connecting for authentication.
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

      |parameters|

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

      |parameters|

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

      |parameters|

      clientKey: string
         Indicates which tenant you are connecting for authentication.
      access_token: string
         Token identifying authorization with MeshyDB requested during `Login`_.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.

--------
Sign out
--------
Now the user is complete. Let us sign out so someone else can have a try.

.. tabs::

   .. group-tab:: REST
   
      .. sourcecode:: http

         POST https://auth.meshydb.com/{clientKey}/connect/revocation HTTP/1.1
         Content-Type: application/x-www-form-urlencoded

           client_id={clientKey}&
           grant_type=refresh_token&
           token={refresh_token}

         
      (Line breaks added for readability)
         
      |parameters|

      clientKey: string
         Indicates which tenant you are connecting for authentication.
      refresh_token: string
        Token to allow reauthorization with MeshyDB after the access token expires requested during `Login`_.
      mesh : string
         Identifies name of mesh collection. e.g. person.
      id : string
         Idenfities location of what Mesh data to replace.
