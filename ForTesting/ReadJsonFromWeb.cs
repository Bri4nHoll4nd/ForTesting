using LeagueChampion.Controllers.V1;
using LeagueChampion.Model.V1;
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

        dynamic Aatrox = championDynObj["data"]["Aatrox"];
        
        V1PostChampion champion = new V1PostChampion();
        champion.Version = Aatrox["version"];
        champion.RiotId = Aatrox["id"];
        champion.RiotKey = Aatrox["key"];
        champion.Name = Aatrox["name"];
        champion.Title = Aatrox["title"];
        champion.Blurb = Aatrox["blurb"];
        champion.Tag1 = Aatrox["tags"][0];
        champion.Tag2 = Aatrox["tags"][1];
        champion.Partype = Aatrox["partype"];
        champion.Full = Aatrox["image"]["full"];
        champion.Sprite = Aatrox["image"]["sprite"];
        champion.Group = Aatrox["image"]["group"];
        champion.X = Aatrox["image"]["x"];
        champion.Y = Aatrox["image"]["y"];
        champion.Width = Aatrox["image"]["w"];
        champion.Height = Aatrox["image"]["h"];
        champion.Attack = Aatrox["info"]["attack"];
        champion.Defence = Aatrox["info"]["defense"];
        champion.Magic = Aatrox["info"]["magic"];
        champion.Difficulty = Aatrox["info"]["difficulty"];
        champion.Hp = Aatrox["stats"]["hp"];
        champion.HpPerLevel = Aatrox["stats"]["hpperlevel"];
        champion.Mp = Aatrox["stats"]["mp"];
        champion.MpPerLevel = Aatrox["stats"]["mpperlevel"];
        champion.MoveSpeed = Aatrox["stats"]["movespeed"];
        champion.Armour = Aatrox["stats"]["armor"];
        champion.ArmourPerLevel = Aatrox["stats"]["armorperlevel"];
        champion.SpellBlock = Aatrox["stats"]["spellblock"];
        champion.SpellBlockPerLevel = Aatrox["stats"]["spellblockperlevel"];
        champion.AttackRange = Aatrox["stats"]["attackrange"];
        champion.HpRegen = Aatrox["stats"]["hpregen"];
        champion.HpRegenPerLevel = Aatrox["stats"]["hpregenperlevel"];
        champion.MpRegen = Aatrox["stats"]["mpregen"];
        champion.MpRegenPerLevel = Aatrox["stats"]["mpregenperlevel"];
        champion.Crit = Aatrox["stats"]["crit"];
        champion.CritPerLevel = Aatrox["stats"]["critperlevel"];
        champion.AttackDamage = Aatrox["stats"]["attackdamage"];
        champion.AttackDamagePerLevel = Aatrox["stats"]["attackdamageperlevel"];
        champion.AttackSpeedPerLevel = Aatrox["stats"]["attackspeedperlevel"];
        champion.AttackSpeed = Aatrox["stats"]["attackspeed"];

        V1ChampionController v1ChampionController = new V1ChampionController();

        v1ChampionController.PatchChampion("Ahri", champion);

        /*

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

        */
    }
}