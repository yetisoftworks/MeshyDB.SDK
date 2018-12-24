using MeshyDB.SDK.Models;
using MeshyDB.SDK.Attributes;

namespace MeshyDB.SDK.Examples.Console.Net471
{
    [MeshName("ExampleMeshName")]
    public class AdvancedExampleData : MeshData
    {
        // Your Mesh Data Properties will go here

        #region Example Properties

        public string Name { get; set; }
        public int FavoriteNumber { get; set; }

        #endregion
    }
}