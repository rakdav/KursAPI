using KursAPI.Models;
using KursAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace KursAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class ChitateliController : ControllerBase
    {
        private readonly ChitateliService chitService;

        public ChitateliController(ChitateliService service)
        {
            this.chitService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chitateli>>> GetAllChitatels()
        {
            return Ok(await chitService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Chitateli>> GetChitatelById(int id)
        {
            var chit = await chitService.GetById(id);
            if (chit == null) return NotFound();
            return Ok(chit);
        }
        [HttpPost]
        public async Task<ActionResult<Chitateli>> CreateChitatel([FromBody] Chitateli chit)
        {
            await chitService.Create(chit);
            return CreatedAtAction(nameof(GetChitatelById), new { Id= chit.ChitatelId }, chit);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Chitateli>> UpdateChitatel(int id,[FromBody] Chitateli chit)
        {
            if (chit.ChitatelId != id) return BadRequest();
            await chitService.Update(chit);
            return Ok(chit);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await chitService.Delete(id);
            return NoContent();
        }   
    }
}
