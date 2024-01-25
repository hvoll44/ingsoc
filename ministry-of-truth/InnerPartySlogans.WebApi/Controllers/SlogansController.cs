using InnerPartySlogans.WebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InnerPartySlogans.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlogansController : ControllerBase
    {
        private readonly IDbContextFactory<SlogansContext> _db;

        public SlogansController(IDbContextFactory<SlogansContext> db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetSlogan")]
        public async Task<string> GetSloganPhrase()
        {
            using var context = _db.CreateDbContext();

            var result = await context.Slogan.ToListAsync();

            return result.FirstOrDefault()?.Phrase ?? "No slogans found";
        }

        [HttpPost(Name = "AddSlogan")]
        public async Task<int> AddSlogan(Slogan slogan)
        {
            using var context = _db.CreateDbContext();
            await context.Slogan.AddAsync(slogan);
            return await context.SaveChangesAsync();
        }
    }
}
