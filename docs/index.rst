Welcome
=======

Meshy DB is a relational json solution as a backend for rapid and flexible implementation.

| **Authorization**
| Utilize OpenID Connect authentication to allow access to your application.

| **Data Management**
| Create, Update, Retrieval and Deletion of your tenanted data.

| **User Management**
| Create new users to allow access. Allow logged in users to manage their information and passwords.

.. toctree::
   :maxdepth: 1
   :hidden:
   :caption: Getting Started
   
   intro/getting_started_rest
   intro/getting_started_csharp
   intro/getting_started_nodejs
   
.. toctree::
   :maxdepth: 3
   :hidden:
   :caption: Authorization
   
   authorization/generating_token
   authorization/refreshing_token
   
.. toctree::
   :maxdepth: 3
   :hidden:
   :caption: Data
   
   data/creating
   data/updating
   data/searching
   data/deleting
   
.. toctree::
   :maxdepth: 3
   :hidden:
   :caption: Users
   
   users/creating_user
   users/retrieving_self
   users/updating_self
   users/forgetting_password
   users/resetting_password
   users/changing_password
   users/retrieving_userinfo
   users/logging_out
