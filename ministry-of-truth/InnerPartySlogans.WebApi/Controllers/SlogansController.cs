using InnerPartySlogans.WebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InnerPartySlogans.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class SlogansController : ControllerBase
    {
        private readonly IDbContextFactory<PartySlogansContext> _db;

        public SlogansController(IDbContextFactory<PartySlogansContext> db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult First()
        {
            string clientData = Request.Headers["Client-Data"];

            using var context = _db.CreateDbContext();

            var result = context.Slogan.ToListAsync().Result;

            return Ok(new { ClientData = clientData, Message = result.FirstOrDefault()?.Phrase ?? "No slogans found" });
        }

        [HttpGet(Name = "GetSlogans")]
        public async Task<List<string>> All()
        {
            using var context = _db.CreateDbContext();

            var result = await context.Slogan.ToListAsync();

            return result.Select(s => s.Phrase).ToList() ?? ["No slogans found"];
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
