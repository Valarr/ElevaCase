using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEscola.Models;

namespace ApiEscola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EscolaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Escola
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Escola>>> GetEscola()
        {
            return await _context.Escola.ToListAsync();
        }

        // GET: api/Escola/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Escola>> GetEscolaId(int id)
        {
            var escola = await _context.Escola.FindAsync(id);

            if (escola == null)
            {
                return NotFound();
            }

            return escola;
        }

        // PUT: api/Escola/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscola(int id, Escola escola)
        {
            if (id != escola.escolaId)
            {
                return BadRequest();
            }

            _context.Entry(escola).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Escola
        [HttpPost]
        public async Task<ActionResult<Escola>> PostEscola(Escola escola)
        {
            _context.Escola.Add(escola);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEscola", new { id = escola.escolaId }, escola);
        }
        // DELETE: api/Escola/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Escola>> DeleteEscola(int id)
        {
            var escola = await _context.Escola.FindAsync(id);
            if (escola == null)
            {
                return NotFound();
            }

            _context.Escola.Remove(escola);
            await _context.SaveChangesAsync();

            return escola;
        }

        private bool EscolaExists(int id)
        {
            return _context.Escola.Any(e => e.escolaId == id);
        }
    }
}
