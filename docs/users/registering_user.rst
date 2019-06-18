.. |parameters| raw:: html

   <h4>Parameters</h4>

----------------
Registering User
----------------
Creates a new user that can log into the system.


.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{accountName}/users/register HTTP/1.1
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
            "newPassword": "newPassword"
          }

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      access_token  : string
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      username : string, required
         Username of user.
      newPassword : string, required
         Password of user to use for login.
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
      phoneNumber : string
         Phone number of user.
      roles : string[]
         Collection of roles user has access.
	  securityQuestions : object[]
		Collection of questions and answers used for password recovery if question security is configured.

   .. group-tab:: C#
   
      .. code-block:: c#
      
        var database = new MeshyDB(accountName, tenant, publicKey);

        var user = new RegisterUser();

        await database.RegisterUserAsync(user);

      |parameters|
      
      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string, required
         Username of user.
      newPassword : string, required
         Password of user to use for login.
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
      phoneNumber : string
         Phone number of user.
      roles : string[]
         Collection of roles user has access.
	  securityQuestions : object[]
		Collection of questions and answers used for password recovery if question security is configured.
		
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var database = initializeMeshyDB(accountName, tenant, publicKey);
         
         database.registerUser({
                                 username: username,
                                 newPassword: newPassword,
                                 id: id,
                                 firstName: firstName,
                                 lastName: lastName,
                                 verified: verified,
                                 isActive: isActive,
                                 phoneNumber: phoneNumber,
                                 roles: roles,
								 securityQuestions: securityQuestions
                             })
                 .then(function(user) { });
      
      |parameters|

      tenant : string
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string
         Indicates which account you are connecting for authentication.
      publicKey : string
         Public accessor for application.
      username : string, required
         Username of user.
      newPassword : string, required
         Password of user to use for login.
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
      phoneNumber : string
         Phone number of user.
      roles : string[]
         Collection of roles user has access.
	  securityQuestions : object[]
		Collection of questions and answers used for password recovery if question security is configured.
         
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
    "roles": [
                "admin",
                "test"
             ]
  }
