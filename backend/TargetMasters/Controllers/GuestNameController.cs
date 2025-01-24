using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TargetMasters.Context;
using TargetMasters.Models;

namespace TargetMasters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestNameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuestNameController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetRandomName")]
        public IActionResult GetRandomName()
        {
            var names = _context.GuestNames
                .AsNoTracking()
                .ToList();

            if(names.Count == 0 || names == null)
                return Ok(new { GuestName = InsertNames() });

            return Ok(new { GuestName = GenerateGuestName(names) });
        }

        private string InsertNames()
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
            };

            _context.GuestNames.AddRange(defaultNames);
            _context.SaveChanges();

            return GenerateGuestName(defaultNames);
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
