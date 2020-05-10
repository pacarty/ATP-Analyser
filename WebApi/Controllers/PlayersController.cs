using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DataContext _context;

        public PlayersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(_context.Players);
        }

        [HttpGet("{code}")]
        public IActionResult GetPlayerWeeks(string code)
        {
            var result = _context.RankDates
                .Where(x => x.PlayerId == code);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetDateRange")]
        public IActionResult GetDateRange(DateRangeModel model)
        {
            var allDates = _context.RankDates
                .Where(x => x.PlayerId == model.PlayerId);

            var result = allDates
                .Where(x => x.Date >= model.StartDate && x.Date <= model.EndDate);

            return Ok(result);
        }
    }
}
