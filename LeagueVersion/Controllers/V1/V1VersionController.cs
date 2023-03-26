using LeagueVersion.Data;
using LeagueVersion.Model.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeagueVersion.Controllers.V1
{
    [ApiController]
    [Route("V1/Version")]
    public class V1VersionController : ControllerBase
    {
        private readonly ILogger<V1VersionController> _logger;

        public V1VersionController(ILogger<V1VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<V1Result<IEnumerable<V1Version>>> Get()
        {
            var dbContext = new VersionDBContext();

            var responseVersions = await dbContext.Versions
                .Select(version => new V1Version
                {
                    Id = version.Id,
                    Name = version.Name
                })
                .ToListAsync();
            
            return new V1Result<IEnumerable<V1Version>>(responseVersions);
        }

        [HttpPost]
        public async Task<V1Result<V1Version>> CreateVersion(V1PostVersion postVersion)
        {
            var dbContext = new VersionDBContext();
            var version = new Data.Version
            {
                Id = Guid.NewGuid(),
                Name = postVersion.Name
            };

            dbContext.Versions.Add(version);
            await dbContext.SaveChangesAsync();

            var result = new V1Result<V1Version>(new V1Version
            {
                Id = version.Id,
                Name = postVersion.Name
            });
            
            return result;
        }

        [HttpPatch]
        public void PatchVersion(string name)
        {
            var db = new VersionDBContext();

            var version = db.Versions.First();

            version.Name = name;

            db.SaveChanges();
        }
    }
}