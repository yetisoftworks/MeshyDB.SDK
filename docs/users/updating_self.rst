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
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      access_token : string, required
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      username : string, required
         Username of user.
      id : string
         Identifier of user.
      firstName : string
         First name of user.
      lastName : string
         Last name of user.
      verified : boolean
         Identifies whether or not the user is verified.
      isActive : boolean
         Identifies whether or not the user is active.
      phoneNumber : string, required if using phone verification
         Phone number of user.
      emailAddress : string, required if using email verification
         Email address of user.
      roles : string[]
         Collection of roles user has access.
      securityQuestions : object[], required if using question verification
         Collection of questions and answers used for password recovery if question security is configured.
      anonymous : boolean
         Identifies whether or not the user is anonymous.

   .. group-tab:: C#
   
      .. code-block:: c#
      
         var client = new MeshyClient(accountName, tenant, publicKey);
         var connection = await client.LoginAnonymouslyAsync(username);

         var user = new User();

         await connection.Users.UpdateUserAsync(id, user);

      |parameters|
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName  : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         Username of user.
      id : string
         Identifier of user.
      firstName : string
         First name of user.
      lastName : string
         Last name of user.
      verified : boolean
         Identifies whether or not the user is verified.
      isActive : boolean
         Identifies whether or not the user is active.
      phoneNumber : string, required if using phone verification
         Phone number of user.
      emailAddress : string, required if using email verification
         Email address of user.
      roles : string[]
         Collection of roles user has access.
      securityQuestions : object[], required if using question verification
         Collection of questions and answers used for password recovery if question security is configured.
      anonymous : boolean
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

      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName  : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         Username of user.
      id : string
         Identifier of user.
      firstName : string
         First name of user.
      lastName : string
         Last name of user.
      verified : boolean
         Identifies whether or not the user is verified.
      isActive : boolean
         Identifies whether or not the user is active.
      phoneNumber : string, required if using phone verification
         Phone number of user.
      emailAddress : string, required if using email verification
         Email address of user.
      roles : string[]
         Collection of roles user has access.
      securityQuestions : object[], required if using question verification
         Collection of questions and answers used for password recovery if question security is configured.
      anonymous : boolean
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
