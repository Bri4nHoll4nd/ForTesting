using LeagueChampion.Controllers.V1;
using LeagueChampion.Model.V1;
using LeagueVersion.Controllers.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security;

internal class Program
{
    static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();

        V1VersionController v1VersionController = new V1VersionController();

        var version = await v1VersionController.Get();
      
        var response = await client.GetAsync("https://ddragon.leagueoflegends.com/api/versions.json");

        var responseString = await response.Content.ReadAsStringAsync();

        var versionList = JsonConvert.DeserializeObject<List<string>>(responseString);

        string temp = version.Value.Name;
        if (temp != versionList[0])
        {
            v1VersionController.PatchVersion(versionList[0]);
            temp = v1VersionController.Get().Result.Value.Name;
        }
        Console.WriteLine(temp);

        var response2 = await client.GetAsync($"https://ddragon.leagueoflegends.com/cdn/{temp}/data/en_US/champion.json");

        var response2String = await response2.Content.ReadAsStringAsync();

        dynamic championDynObj = JsonConvert.DeserializeObject<dynamic>(response2String);

        /*
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
        */

        V1ChampionController v1ChampionController = new V1ChampionController();

        //await v1ChampionController.CreateChampion(champion);

        foreach (var obj in championDynObj.SelectTokens("data.*"))
        {
            V1PostChampion champ = new V1PostChampion();
            champ.Version = obj["version"];
            champ.RiotId = obj["id"];
            champ.RiotKey = obj["key"];
            champ.Name = obj["name"];
            champ.Title = obj["title"];
            champ.Blurb = obj["blurb"];
            champ.Tag1 = obj["tags"][0];
            if (obj["tags"].Count == 2)
            {
                champ.Tag2 = obj["tags"][1];
            }
            champ.Partype = obj["partype"];
            champ.Full = obj["image"]["full"];
            champ.Sprite = obj["image"]["sprite"];
            champ.Group = obj["image"]["group"];
            champ.X = obj["image"]["x"];
            champ.Y = obj["image"]["y"];
            champ.Width = obj["image"]["w"];
            champ.Height = obj["image"]["h"];
            champ.Attack = obj["info"]["attack"];
            champ.Defence = obj["info"]["defense"];
            champ.Magic = obj["info"]["magic"];
            champ.Difficulty = obj["info"]["difficulty"];
            champ.Hp = obj["stats"]["hp"];
            champ.HpPerLevel = obj["stats"]["hpperlevel"];
            champ.Mp = obj["stats"]["mp"];
            champ.MpPerLevel = obj["stats"]["mpperlevel"];
            champ.MoveSpeed = obj["stats"]["movespeed"];
            champ.Armour = obj["stats"]["armor"];
            champ.ArmourPerLevel = obj["stats"]["armorperlevel"];
            champ.SpellBlock = obj["stats"]["spellblock"];
            champ.SpellBlockPerLevel = obj["stats"]["spellblockperlevel"];
            champ.AttackRange = obj["stats"]["attackrange"];
            champ.HpRegen = obj["stats"]["hpregen"];
            champ.HpRegenPerLevel = obj["stats"]["hpregenperlevel"];
            champ.MpRegen = obj["stats"]["mpregen"];
            champ.MpRegenPerLevel = obj["stats"]["mpregenperlevel"];
            champ.Crit = obj["stats"]["crit"];
            champ.CritPerLevel = obj["stats"]["critperlevel"];
            champ.AttackDamage = obj["stats"]["attackdamage"];
            champ.AttackDamagePerLevel = obj["stats"]["attackdamageperlevel"];
            champ.AttackSpeedPerLevel = obj["stats"]["attackspeedperlevel"];
            champ.AttackSpeed = obj["stats"]["attackspeed"];

            await v1ChampionController.CreateChampion(champ);
        }
    }
}