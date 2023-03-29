using LeagueChampion.Data;
using LeagueChampion.Model.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata;

namespace LeagueChampion.Controllers.V1
{
    [ApiController]
    [Route("V1/Champion")]
    public class V1ChampionController : ControllerBase
    {
        /*
        private readonly ILogger<V1ChampionController> _logger;

        public V1ChampionController(ILogger<V1ChampionController> logger)
        {
            _logger = logger;
        }
        */

        [HttpGet("")]
        public async Task<V1Result<IEnumerable<V1Champion>>> Get()
        {
            var dbContext = new ChampionDBContext();

            var responseChampions = await dbContext.Champions
                .Select(champion => new V1Champion
                {
                    Id = champion.Id,
                    Name = champion.Name,
                    RiotId = champion.RiotId,
                    RiotKey = champion.RiotKey,
                    Title = champion.Title,
                    Blurb = champion.Blurb,
                    Tag1 = champion.Tag1,
                    Tag2 = champion.Tag2,
                    Partype = champion.Partype
                })
                .ToListAsync();

            return new V1Result<IEnumerable<V1Champion>>(responseChampions);
        }

        [HttpPost]
        public async Task<V1Result<V1Champion>> CreateChampion(V1PostChampion postChampion)
        {
            var dbContext = new ChampionDBContext();

            var image = new Data.Image
            {
                Full = postChampion.Full,
                Sprite = postChampion.Sprite,
                Group = postChampion.Group,
                X = postChampion.X,
                Y = postChampion.Y,
                Width = postChampion.Width,
                Height = postChampion.Height
            };
            var info = new Data.Info
            {
                Attack = postChampion.Attack,
                Defence = postChampion.Defence,
                Magic = postChampion.Magic,
                Difficulty = postChampion.Difficulty
            };
            var stats = new Data.Stats
            {
                Hp = postChampion.Hp,
                HpPerLevel = postChampion.HpPerLevel,
                Mp = postChampion.Mp,
                MpPerLevel = postChampion.MpPerLevel,
                Armour = postChampion.Armour,
                ArmourPerLevel = postChampion.ArmourPerLevel,
                SpellBlock = postChampion.SpellBlock,
                SpellBlockPerLevel = postChampion.SpellBlockPerLevel,
                AttackRange = postChampion.AttackRange,
                HpRegen = postChampion.HpRegen,
                HpRegenPerLevel = postChampion.HpRegenPerLevel,
                MpRegen = postChampion.MpRegen,
                MpRegenPerLevel = postChampion.MpRegenPerLevel,
                Crit = postChampion.Crit,
                CritPerLevel = postChampion.CritPerLevel,
                AttackDamage = postChampion.AttackDamage,
                AttackDamagePerLevel = postChampion.AttackDamagePerLevel,
                AttackSpeedPerLevel = postChampion.AttackSpeedPerLevel,
                AttackSpeed = postChampion.AttackSpeed
            };

            var champion = new Data.Champion
            {
                Id = Guid.NewGuid(),
                Version = postChampion.Version,
                RiotId = postChampion.RiotId,
                RiotKey = postChampion.RiotKey,
                Name = postChampion.Name,
                Title = postChampion.Title,
                Blurb = postChampion.Blurb,
                Info = info,
                Image = image,
                Tag1 = postChampion.Tag1,
                Tag2 = postChampion.Tag2,
                Partype= postChampion.Partype,
                Stats = stats
            };
            

            dbContext.Champions.Add(champion);
            await dbContext.SaveChangesAsync();

            var result = new V1Result<V1Champion>(new V1Champion
            {
                Id = champion.Id,
                Version = postChampion.Version,
                RiotId = postChampion.RiotId,
                RiotKey = postChampion.RiotKey,
                Name = postChampion.Name,
                Title = postChampion.Title,
                Blurb = postChampion.Blurb,
                Tag1 = postChampion.Tag1,
                Tag2 = postChampion.Tag2,
                Partype = postChampion.Partype
            });

            return result;
        }

        [HttpPatch]
        public void PatchChampion(String name, V1PostChampion postChampion)
        {
            var db = new ChampionDBContext();

            var champion = db.Champions.Where(champion => champion.Name == name).FirstOrDefault();

            var image = new Data.Image
            {
                Full = postChampion.Full,
                Sprite = postChampion.Sprite,
                Group = postChampion.Group,
                X = postChampion.X,
                Y = postChampion.Y,
                Width = postChampion.Width,
                Height = postChampion.Height
            };
            var info = new Data.Info
            {
                Attack = postChampion.Attack,
                Defence = postChampion.Defence,
                Magic = postChampion.Magic,
                Difficulty = postChampion.Difficulty
            };
            var stats = new Data.Stats
            {
                Hp = postChampion.Hp,
                HpPerLevel = postChampion.HpPerLevel,
                Mp = postChampion.Mp,
                MpPerLevel = postChampion.MpPerLevel,
                Armour = postChampion.Armour,
                ArmourPerLevel = postChampion.ArmourPerLevel,
                SpellBlock = postChampion.SpellBlock,
                SpellBlockPerLevel = postChampion.SpellBlockPerLevel,
                AttackRange = postChampion.AttackRange,
                HpRegen = postChampion.HpRegen,
                HpRegenPerLevel = postChampion.HpRegenPerLevel,
                MpRegen = postChampion.MpRegen,
                MpRegenPerLevel = postChampion.MpRegenPerLevel,
                Crit = postChampion.Crit,
                CritPerLevel = postChampion.CritPerLevel,
                AttackDamage = postChampion.AttackDamage,
                AttackDamagePerLevel = postChampion.AttackDamagePerLevel,
                AttackSpeedPerLevel = postChampion.AttackSpeedPerLevel,
                AttackSpeed = postChampion.AttackSpeed
            };

            champion.Version = postChampion.Version;
            champion.RiotId = postChampion.RiotId;
            champion.RiotKey = postChampion.RiotKey;
            champion.Name = postChampion.Name;
            champion.Title = postChampion.Title;
            champion.Blurb = postChampion.Blurb;
            champion.Info = info;
            champion.Image = image;
            champion.Tag1 = postChampion.Tag1;
            champion.Tag2 = postChampion.Tag2;
            champion.Partype = postChampion.Partype;
            champion.Stats = stats;
                        
            db.SaveChanges();
        }
    }
}