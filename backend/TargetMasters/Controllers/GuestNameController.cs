using Microsoft.AspNetCore.Mvc;
using TargetMasters.Models;

namespace TargetMasters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestNameController : ControllerBase
    {
        [HttpGet("GetRandomName")]
        public IActionResult GetRandomName()
        {
            List<GuestName> defaultNames = new()
            {
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Super", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Adventurer", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Brave", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Warrior", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Clever", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Explorer", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Fierce", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Mage", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Mighty", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Hunter", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Nimble", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Knight", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Legendary", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Rogue", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Swift", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Guardian", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Valiant", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Berserker", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Epic", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Champion", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Fearless", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Heroic", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Bold", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Invincible", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Powerful", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Radiant", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Noble", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Stoic", WordType=WordType.Adjective },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Sage", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Seeker", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Vanguard", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Defender", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Gladiator", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Wanderer", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Prophet", WordType=WordType.Noun },
                new() { Id=Guid.NewGuid().ToString(), Deleted=false, CreatedAt=DateTime.Now, Word= "Champion", WordType=WordType.Noun },
            };

            return Ok(new { GuestName = GenerateGuestName(defaultNames) });
        }

        private string GenerateGuestName(List<GuestName> defaultNames)
        {
            Random random = new();

            var adjectives = defaultNames.Where(name => name.WordType == WordType.Adjective).ToList();
            var nouns = defaultNames.Where(name => name.WordType == WordType.Noun).ToList();

            var randomAdjective = adjectives[random.Next(adjectives.Count)].Word;
            var randomNoun = nouns[random.Next(nouns.Count)].Word;

            var randomNumber = random.Next(1, 1000).ToString("D3");

            return $"{randomAdjective}{randomNoun}{randomNumber}";
        }
    }
}
