using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesApi.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<News>>> GetNews(int page = 1, int pageSize = 20)
        {
            var skip = (page - 1) * pageSize;
            var news = await _context.Newss.Skip(skip).Take(pageSize).ToListAsync();
            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsById(int id)
        {
            var news = await _context.Newss.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditNews(int id, News news)
        {
            if (id != news.Id)
            {
                return BadRequest();
            }

            _context.Entry(news).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.Newss.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            _context.Newss.Remove(news);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsExists(int id)
        {
            return _context.Newss.Any(e => e.Id == id);
        }
    }
}
