# MeshyDb.SDK

The Meshy SDK is meant to integration with [MeshyDB](http://meshydb.com).

This sdk is developed in a .Net framework and is distributed using .Net Framework 4.5 and .Net Core 2.0.

## Getting Started
Add the reference to MeshDb.SDK via NuGet.

Once the package is installed you can instantiate a new instance of the Meshy Db Client to communicate with the Meshy Db API.

``` c#
var client = new MeshyDbClient({accountName},{publicKey},{privateKey});
```

### Creating a Mesh
Before we create a Mesh we need to create some data. We will do this by inheriting and extending the MeshData abstract class.

This will tell us our class is meant to be Mesh data.

``` c#
using MeshyDb.SDK.Models;

public class ExampleData : MeshData
{
  // Your Mesh Data Properties will go here
  
  #region Example Properties
  
  public string Name { get; set; }
  public int FavoriteNumber { get; set; }
  
  #endregion
}
```

Now that we have our mesh data definition we can create or add to an existing mesh. We will need to identify which Mesh requires the data.

We can commit this data via our client we defined in [Getting Started](#getting-started).

``` c#
  // Create Example Data to be committed
  var data = new ExampleData(){
    Name = "Tester McTesterton",
    FavoriteNumber = 64983
  };
  
  var meshName = "testmesh";
  // Create Mesh data for a specific mesh and get the data returned with the committed id from the API
  data = await client.Meshes.CreateAsync(meshName, data);
```

If we are not able to work in an asynchrnous system we can simply call `client.Meshes.Create({meshName}, data)`. This pattern applies to all methods.

### Update Mesh Data
Updating Mesh data is similar to creating a mesh. The largest difference is we need to supply which location requires the data to be commited.

This use the same model we used during in [Creating a Mesh](#creating-a-mesh).

``` c#
  // Update Mesh data for a specific mesh and get the data returned with the committed id from the API
  data = await client.Meshes.UpdateAsync(meshName, data.id, data);
```

### Get Mesh Data
If we have a specific id we can get the individual record based on id. We will use this using the same object used in [Creating a Mesh](#creating-a-mesh).

```c#
  // Get will get a mesh with a specific id and cast the response into the provided class definition as long as it extends MeshData
  var dataById = await client.Meshes.GetAsync<ExampleData>(meshName, data.id);
```

### Search Mesh Data
MeshyDb uses NoSQL to store and search data so that it can best fit a varity of needs from dynamic or normalized data.

Every search will be paged based on the [Page Result]() definition in the SDK.

Because of that we can use NoSQL search strings to find data based on your needs. This can be done in two different ways. Manually creating a NoSQL search string or using Linq that will be converted into proper search criteria using MongoDb Driver.

NoSQL Example:
```c#
  var pagedSearchResult = await client.Meshes.SearchAsync<ExampleData>(meshName, "{ 'FavoriteNmber': { '$gt': 5000 } }");
```

Linq Example:
```c#
  var pagedSearchResult = await client.Meshes.SearchAsync<ExampleData>(meshName, (t) => t.FavoriteNmber > 5000);
```

### Delete Mesh Data
We are able to delete our data if we not longer need it. 

**__Caution__** This will perform a hard delete and remove it from your system. If you still need the data consider adding a IsDeleted property and filter those from your results if needed.

We will delete the example we started with in [Creating a Mesh](#creating-a-mesh).
```c#
  await client.Meshes.DeleteAsync(meshName, data.Id);
```
