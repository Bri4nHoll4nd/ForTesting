using LeagueChampion.Data;
using LeagueChampion.Model.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeagueChampion.Controllers.V1
{
    [ApiController]
    [Route("V1/Champion")]
    public class V1ChampionController : ControllerBase
    {
        private readonly ILogger<V1ChampionController> _logger;

        public V1ChampionController(ILogger<V1ChampionController> logger)
        {
            _logger = logger;
        }

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
    }
}