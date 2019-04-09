using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCalendar.Abstracts.Interface;

namespace SimpleCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleCalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public SimpleCalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        // GET api/values/5
        [HttpGet("{year}/{month}")]
        public async Task<ActionResult> Get(int year, int month)
        {
            var result = await _calendarService.GetMonthDays(year, month);

            return Ok(result);
        }
    }
}
