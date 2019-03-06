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
``` http  fct_label="REST"
POST https://api.meshydb.com/{clientKey}/meshes/{mesh}
Authentication: Bearer {access_token}
Content-Type: application/json

Body:
  {
    "firstName": "Bob",
    "lastName": "Bobberson"
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

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_clientKey_  | Indicates which tenant you are connecting for authentication. | _string_|
|_access_token_| Token identifying authorization with MeshyDB requested during [Login](#login)| _string_|
|_mesh_   | Identifies name of mesh collection. e.g. person.                                                    | _string_|

Example Response:
```
  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bob",
    "lastName": "Bobberson"
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
```

## Update data
``` http  fct_label="REST"
PUT https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}
Authentication: Bearer {access_token}
Content-Type: application/json

Body:
  {
    "firstName": "Bobbo",
    "lastName": "Bobberson"
  }
```

```c#
person.FirstName = "Bobbo";

person = await client.Meshes.UpdateAsync(person);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_clientKey_  | Indicates which tenant you are connecting for authentication. | _string_|
|_access_token_| Token identifying authorization with MeshyDB requested during [Login](#login)| _string_|
|_mesh_   | Identifies name of mesh collection. e.g. person.                                                    | _string_|
|_id_| Idenfities location of what Mesh data to replace.| _string_|

Example Response:
```
  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson"
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
```

## Search data
``` http  fct_label="REST"
GET https://api.meshydb.com/{clientKey}/meshes/{mesh}?filter={filter}&
                                                      orderby={orderby}&
                                                      page={page}&
                                                      pageSize={pageSize}
Authentication: Bearer {access_token}

(Line breaks added for readability)
```

```c#
var pagedPersonResult = await client.Meshes.SearchAsync<Person>(filter, page, pageSize);
```


| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_clientKey_  | Indicates which tenant you are connecting for authentication. | _string_|
|_access_token_| Token identifying authorization with MeshyDB requested during [Login](#login)| _string_|
|_mesh_   | Identifies name of mesh collection. e.g. person.                                                    | _string_|
|_filter_| Filter criteria for search. Uses MongoDB format. | _string_|
|_orderby_| How to order results. Uses MongoDB format. | _string_|
|_page_  | Page number of users to bring back.                                           | _integer_|
|_pageSize_  | Number of results to bring back per page. Maximum is 200.                                           | _integer_|

Example Response:
```
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
## Get data by id
``` REST fct_label="REST"
GET https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}
Authentication: Bearer {access_token}
```

```c#
var pagedPersonResult = await client.Meshes.GetAsync<Person>({id});
```
| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_clientKey_  | Indicates which tenant you are connecting for authentication. | _string_|
|_access_token_| Token identifying authorization with MeshyDB requested during| _string_|
|_mesh_   | Identifies name of mesh collection. e.g. person.                                                    | _string_|
|_id_| Idenfities location of what Mesh data to retrieve.| _string_|
  
Example Response:
```
  {
    "_id":"5c78cc81dd870827a8e7b6c4",
    "firstName": "Bobbo",
    "lastName": "Bobberson"
    "_rid":"https://api.meshydb.com/{clientKey}/meshes/{mesh}/5c78cc81dd870827a8e7b6c4"
  }
```
## Delete data
``` http  fct_label="REST"
DELETE https://api.meshydb.com/{clientKey}/meshes/{mesh}/{id}
Authentication: Bearer {access_token}
```

```c#
await client.Meshes.DeleteAsync(person);
```

| Parameter   | Description                                                   | Type    |
|:------------|:--------------------------------------------------------------|:--------|
|_clientKey_  | Indicates which tenant you are connecting for authentication. | _string_|
|_access_token_| Token identifying authorization with MeshyDB requested during [Login](#login)| _string_|
|_mesh_   | Identifies name of mesh collection. e.g. person.                                                    | _string_|
|_id_| Idenfities location of what Mesh data to replace.| _string_|
  
