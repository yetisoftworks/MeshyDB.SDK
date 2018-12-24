namespace MeshyDB.SDK.Examples.Console.Net471
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new MeshyDBClient("{meshName}", "{publicKey}", "{privateKey}");

            RunBasicExample(client);

            RunAdvancedExample(client);
        }

        private static void RunBasicExample(MeshyDBClient client)
        {
            var createdExampleData = client.Meshes.Create(new ExampleData() { FavoriteNumber= 64983, Name = "Tester McTesterton" });

            createdExampleData.FavoriteNumber = 6000;

            client.Meshes.Update(createdExampleData);
            client.Meshes.Delete(createdExampleData);
        }

        private static void RunAdvancedExample(MeshyDBClient client)
        {
            var createdAdvancedExampleData = client.Meshes.Create(new AdvancedExampleData() { FavoriteNumber = 64983, Name = "Tester McTesterton" });

            createdAdvancedExampleData.FavoriteNumber = 6000;

            client.Meshes.Update(createdAdvancedExampleData);
            client.Meshes.Delete(createdAdvancedExampleData);
        }

    }
}
