.. role:: required

.. role:: type

.. |parameters| raw:: html

   <h4>Parameters</h4>
   
-------------
Updating Self
-------------
Update details about the logged in user.

.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
         PUT https://api.meshydb.com/{accountName}/users/me HTTP/1.1
         Authentication: Bearer {access_token}
         Content-Type: application/json
         tenant: {tenant}
         
           {
             "id": "5c78cc81dd870827a8e7b6c4",
             "username": "username_testermctesterson",
             "firstName": "Tester",
             "lastName": "McTesterton",
             "verified": true,
             "isActive": true,
             "phoneNumber": "+15555555555",
             "emailAddress": "test@test.com"
             "roles": [
                         "admin",
                         "test"
                      ],
             "securityQuestions": [
                                    {
                                        "question": "What would you say to this question?",
                                        "answer": "..."
                                    }
                                  ],
             "anonymous": false
           }

      |parameters|
      
      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      access_token : :type:`string`, :required:`required`
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      username : :type:`string`, :required:`required`
         Username of user.
      id : :type:`string`
         Identifier of user.
      firstName : :type:`string`
         First name of user.
      lastName : :type:`string`
         Last name of user.
      verified : :type:`boolean`
         Identifies whether or not the user is verified.
      isActive : :type:`boolean`
         Identifies whether or not the user is active.
      phoneNumber : :type:`string`, :required:`required` *if using phone verification*
         Phone number of user.
      emailAddress : :type:`string`, :required:`required` *if using email verification*
         Email address of user.
      roles : :type:`string[]`
         Collection of roles user has access.
      securityQuestions : :type:`object[]`, :required:`required` *if using question verification*
         Collection of questions and answers used for password recovery if question security is configured.
      anonymous : :type:`boolean`
         Identifies whether or not the user is anonymous.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = await client.LoginAnonymouslyAsync(username);

         var user = new User();

         await connection.Users.UpdateUserAsync(id, user);

      |parameters|
      
      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName  : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      publicKey : :type:`string`, :required:`required`
         Public accessor for application.
      username : :type:`string`, :required:`required`
         Username of user.
      id : :type:`string`
         Identifier of user.
      firstName : :type:`string`
         First name of user.
      lastName : :type:`string`
         Last name of user.
      verified : :type:`boolean`
         Identifies whether or not the user is verified.
      isActive : :type:`boolean`
         Identifies whether or not the user is active.
      phoneNumber : :type:`string`, :required:`required` *if using phone verification*
         Phone number of user.
      emailAddress : :type:`string`, :required:`required` *if using email verification*
         Email address of user.
      roles : :type:`string[]`
         Collection of roles user has access.
      securityQuestions : :type:`object[]`, :required:`required` *if using question verification*
         Collection of questions and answers used for password recovery if question security is configured.
      anonymous : :type:`boolean`
         Identifies whether or not the user is anonymous.

   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);
         
         client.loginAnonymously(username)
               .then(function (meshyConnection){
                        meshyConnection.usersService.updateSelf({
                                                                  username: username,
                                                                  id: id,
                                                                  firstName: firstName,
                                                                  lastName: lastName,
                                                                  verified:  verified,
                                                                  isActive: isActive,
                                                                  phoneNumber: phoneNumber,
                                                                  emailAddress: emailAddress,
                                                                  roles: roles,
															                     securityQuestions: securityQuestions,
															                     anonymous:  anonymous
                                                               })
                                                    .then(function(self) { });
               }); 
      
      |parameters|

      tenant : :type:`string`, :required:`required`
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName  : :type:`string`, :required:`required`
         Indicates which account you are connecting for authentication.
      publicKey : :type:`string`, :required:`required`
         Public accessor for application.
      username : :type:`string`, :required:`required`
         Username of user.
      id : :type:`string`
         Identifier of user.
      firstName : :type:`string`
         First name of user.
      lastName : :type:`string`
         Last name of user.
      verified : :type:`boolean`
         Identifies whether or not the user is verified.
      isActive : :type:`boolean`
         Identifies whether or not the user is active.
      phoneNumber : :type:`string`, :required:`required` *if using phone verification*
         Phone number of user.
      emailAddress : :type:`string`, :required:`required` *if using email verification*
         Email address of user.
      roles : :type:`string[]`
         Collection of roles user has access.
      securityQuestions : :type:`object[]`, :required:`required` *if using question verification*
         Collection of questions and answers used for password recovery if question security is configured.
      anonymous : :type:`boolean`
         Identifies whether or not the user is anonymous.
         
Example Response:

.. code-block:: json

  {
    "id": "5c78cc81dd870827a8e7b6c4",
    "username": "username_testermctesterson",
    "firstName": "Tester",
    "lastName": "McTesterton",
    "verified": true,
    "isActive": true,
    "phoneNumber": "+15555555555",
    "emailAddress": "test@test.com",
    "roles": [
                "admin",
                "test"
             ],
    "securityQuestions": [
                            {
                               "question": "What would you say to this question?",
                               "answer": "mceasy123"
                            }
                         ],
    "anonymous": false
  }
