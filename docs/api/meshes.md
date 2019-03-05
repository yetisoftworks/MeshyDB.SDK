# Meshes

The Mesh end points can be used for retrieving, updating and deleting your custom information.

All the endpoints require authorization before use.

## Model Definition
All meshes will have the following pieces  of information.

``` REST fct_label="REST"
  {
    "_id":"...",
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/..."
  }
```

``` c#
  // Base fields existing in the MeshData class
  public class Person: MeshData
  {
    // your custom information
  }
```

*_id*
  Identifier of the mesh.
  
*_rid*
  Reference url for more detailed data.
  
## Create data
``` REST fct_label="REST"
POST https://api.meshydb.com/{clientKey}/meshes/{mesh}
Headers:
  Authentication: Bearer {access_token}
Body(json):
  {
    "firstName": "Bob",
    "lastName": "Bobberson"
  }
  
Example Response:
  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bob",
    "lastName": "Bobberson"
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
```

```c#
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
```

_clientKey_: 
  Indicates which tenant you are connecting for authentication.
 
_access_token_:
  Token identifying authorization with MeshyDB requested during [Login](#login)
  
_mesh_:
  Identifies name of mesh collection. e.g. person.

## Update data
``` REST fct_label="REST"
PUT https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}
Headers:
  Authentication: Bearer {access_token}
Body(json):
  {
    "firstName": "Bobbo",
    "lastName": "Bobberson"
  }
  
Example Response:
  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson"
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
```

```c#
person.FirstName = "Bobbo";

person = await client.Meshes.UpdateAsync(person);
```

_clientKey_: 
  Indicates which tenant you are connecting for authentication.
 
_access_token_:
  Token identifying authorization with MeshyDB requested during [Login](#login)
  
_mesh_:
  Identifies name of mesh collection. e.g. person.

_id_:
  Idenfities location of what Mesh data to replace.

## Search data
``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/meshes/{mesh}?filter={filter}&orderby={orderby}&page={page}&pageSize={pageSize}
Headers:
  Authentication: Bearer {access_token}
  
Example Response:
  {
    "page": 1,
    "pageSize": 25,
    "results": [{
                 "_id":"5c78cc81dd870827a8e7b6c4",
                 "firstName": "Bobbo",
                 "lastName": "Bobberson"
                 "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
               }],
    "totalRecords": 1
  }
```

```c#
var pagedPersonResult = await client.Meshes.SearchAsync<Person>({filter},{page},{pageSize});
```

_clientKey_: 
  Indicates which tenant you are connecting for authentication.
 
_access_token_:
  Token identifying authorization with MeshyDB requested during [Login](#login)
  
_mesh_:
  Identifies name of mesh collection. e.g. person.

_filter_:
  Filter criteria for search. Uses MongoDB format.
  
_orderby_:
  How to order results.
  
_page_:
  Which page to return

_pageSize_:
  Number of results to bring  back. Maximum is 200.
  

## Get data by id
``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}
Headers:
  Authentication: Bearer {access_token}
  
Example Response:
  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson"
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
```

```c#
var pagedPersonResult = await client.Meshes.GetAsync<Person>({id});
```

_clientKey_: 
  Indicates which tenant you are connecting for authentication.
 
_access_token_:
  Token identifying authorization with MeshyDB requested during [Login](#login)
  
_mesh_:
  Identifies name of mesh collection. e.g. person.

_id_:
  Identifier of mesh to retrieve.
  
## Delete data
``` REST fct_label="REST"
DELETE https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}
Headers:
  Authentication: Bearer {access_token}
```

```c#
await client.Meshes.DeleteAsync(person);
```

_clientKey_: 
  Indicates which tenant you are connecting for authentication.
 
_access_token_:
  Token identifying authorization with MeshyDB requested during [Login](#login)
  
_mesh_:
  Identifies name of mesh collection. e.g. person.

_id_:
  Idenfities location of what Mesh data to delete.
