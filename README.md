# MeshyDB.SDK [![Build Status](https://yetisoftworks.visualstudio.com/CloudX/_apis/build/status/yetisoftworks.MeshyDB.SDK)](https://yetisoftworks.visualstudio.com/CloudX/_build/latest?definitionId=3)

The Meshy SDK is meant to integration with [MeshyDB](http://meshydb.com).

This sdk is developed in a .Net framework and is distributed using .Net Framework 4.7.1 and .Net Core 2.0.

## Getting Started
Create a [MeshyDB](http://meshydb.com) account if one does not exist.

Once created retrieve the Public Key and Private Key to integrate MeshyDB.SDK.

Add the reference to [MeshyDB.SDK](https://www.nuget.org/packages/MeshyDB.SDK/) via [NuGet](https://www.nuget.org).

Once the package is installed you can instantiate a new instance of the MeshyDB Client to communicate with the Meshy DB API.

``` c#
var client = new MeshyDBClient({accountName},{publicKey},{privateKey});
```

### Creating a Mesh
Before we create a Mesh we need to create create a Mesh Data Definition data. 

When we create our definition it will define what fields we want to collect as well as the name of our Mesh. This is defined by the name of the class.

We will create a definition by inheriting and extending the MeshData abstract class.

``` c#
using MeshyDB.SDK.Models;

public class ExampleData : MeshData
{
  // Your Mesh Data Properties will go here
  
  #region Example Properties
  
  public string Name { get; set; }
  public int FavoriteNumber { get; set; }
  
  #endregion
}
```

Aditionally, we can override our Mesh name by providing something more specific.

``` c#
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Attributes;

[MeshName("ExampleMeshName")]
public class ExampleData : MeshData
{
  // Your Mesh Data Properties will go here
  
  #region Example Properties
  ...  
  #endregion
}
```

Now that we have our Mesh Data definition we can create or add to an existing Mesh. We will need to identify which Mesh requires the data.

We can commit this data via our client we defined in [Getting Started](#getting-started).

``` c#
  // Create Example Data to be committed
  var data = new ExampleData(){
    Name = "Tester McTesterton",
    FavoriteNumber = 64983
  };
  
  // Create Mesh Data for a specific Mesh and get the data returned with the committed id from the API
  data = await client.Meshes.CreateAsync(data);
```

If we are not able to work in an asynchrnous system we can simply call `client.Meshes.Create(data)`. This pattern applies to all methods.

### Update Mesh Data
Updating Mesh Data is similar to creating a Mesh. The largest difference is we need to supply which location requires the data to be commited.

This use the same model we used during in [Creating a Mesh](#creating-a-mesh).

``` c#
  // Update Mesh Data for a specific Mesh and get the data returned with the committed id from the API
  data = await client.Meshes.UpdateAsync(data.id, data);
```

### Get Mesh Data
If we have a specific id we can get the individual record based on id. We will use this using the same object used in [Creating a Mesh](#creating-a-mesh).

```c#
  // Get will get a Mesh with a specific id and cast the response into the provided class definition as long as it extends MeshData
  var dataById = await client.Meshes.GetAsync<ExampleData>(data.id);
```

### Search Mesh Data
MeshyDB uses NoSQL to store and search data so that it can best fit a varity of needs from dynamic or normalized data.

Every search will be paged based on the [Page Result]() definition in the SDK.

Because of that we can use NoSQL search strings to find data based on your needs. This can be done in two different ways. Manually creating a NoSQL search string or using Linq that will be converted into proper search criteria using MongoDB Driver.

NoSQL Example:
```c#
  var pagedSearchResult = await client.Meshes.SearchAsync<ExampleData>("{ 'FavoriteNumber': { '$gt': 5000 } }");
```

Linq Example:
```c#
  var pagedSearchResult = await client.Meshes.SearchAsync<ExampleData>((t) => t.FavoriteNumber > 5000);
```

### Delete Mesh Data
We are able to delete our data if we not longer need it. 

**__Caution__** This will perform a hard delete and remove it from your system. If you still need the data consider adding a IsDeleted property and filter those from your results if needed.

We will delete the example we started with in [Creating a Mesh](#creating-a-mesh).
```c#
  await client.Meshes.DeleteAsync<ExampleData>(data.Id);
```
