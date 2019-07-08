.. |parameters| raw:: html

   <h4>Parameters</h4>

-------------
Checking hash
-------------
Verifies user verification hash request.


.. tabs::

   .. group-tab:: REST
   
      .. code-block:: http
      
        POST https://api.meshydb.com/{accountName}/users/checkhash HTTP/1.1
        Content-Type: application/json
        tenant: {tenant}
         
          {
             "username": "username_testermctesterson",
             "attempt": 1,
             "hash": "...",
             "expires": "1/1/1900",
             "hint": "...",
             "verificationCode": "...",
          }

      |parameters|
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      access_token  : string, required
         Token identifying authorization with MeshyDB requested during `Generating Token <../authorization/generating_token.html#generating-token>`_.
      username : string, required
         Username of user.
      attempt: int, required
         Identifies which attempt hash was generated against.
      hash: string, required
         Generated hash from verification request.
      expires: date, required
         Identifies when the request expires.
      hint: string, required
         Hint for verification code was generated
      verificationCode: string, required
         Value to verify against verification request.

   .. group-tab:: C#
   
      .. code-block:: c#
      
        var client = new MeshyClient(accountName, tenant, publicKey);

        var check = new UserVerificationCheck();
		
        var isValid = await client.CheckHashAsync(check);

      |parameters|
      
      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         Username of user.
      attempt: int, required
         Identifies which attempt hash was generated against.
      hash: string, required
         Generated hash from verification request.
      expires: date, required
         Identifies when the request expires.
      hint: string, required
         Hint for verification code was generated
      verificationCode: string, required
         Value to verify against verification request.
		
   .. group-tab:: NodeJS
      
      .. code-block:: javascript
         
         var client = initializeMeshyClientWithTenant(accountName, tenant, publicKey);
         
         client.checkHash({
                               username: username,
                               attempt: attempt:
                               hash: hash,
                               expires: expires,
                               hint: hint,
                               verificationCode: verificationCode
						    })
               .then(function(isValid) { });
      
      |parameters|

      tenant : string, required
         Indicates which tenant data to use. If not provided, it will use the configured default.
      accountName : string, required
         Indicates which account you are connecting for authentication.
      publicKey : string, required
         Public accessor for application.
      username : string, required
         Username of user.
      attempt: int, required
         Identifies which attempt hash was generated against.
      hash: string, required
         Generated hash from verification request.
      expires: date, required
         Identifies when the request expires.
      hint: string, required
         Hint for verification code was generated
      verificationCode: string, required
         Value to verify against verification request.
		
Example Response:

.. code-block:: boolean

	true
