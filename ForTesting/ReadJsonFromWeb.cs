using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class Program
{
    static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();
      
        var response = await client.GetAsync("https://ddragon.leagueoflegends.com/api/versions.json");

        var responseString = await response.Content.ReadAsStringAsync();

        var versionList = JsonConvert.DeserializeObject<List<string>>(responseString);

        string temp = versionList[0];
        Console.WriteLine(temp);

        var response2 = await client.GetAsync($"https://ddragon.leagueoflegends.com/cdn/{temp}/data/en_US/champion.json");

        var response2String = await response2.Content.ReadAsStringAsync();

        dynamic championDynObj = JsonConvert.DeserializeObject<dynamic>(response2String);

        int counter = 0;

        foreach (var obj in championDynObj.SelectTokens("data.*.tags"))
        {
            counter += obj.Count;
            if (obj.Count == 3)
            {
                Console.WriteLine("3 Tags");
            }
        }

        foreach (var obj in championDynObj.SelectTokens("data.*"))
        {
            Console.WriteLine(obj["name"]);
            Console.WriteLine(obj["tags"][0]);
            break;
        }

        Console.WriteLine(counter);
    }
}