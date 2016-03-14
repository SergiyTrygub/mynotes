using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Services;
using MyNotes.Web.Models;
using System;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNotes.Web.Controllers.Api.v1
{
    [Route("api/[controller]/{tenantId}")]
    public class WishDaysController : Controller
    {
        private readonly IUserContextService _userContextService;
        private readonly ILogger<WishDaysController> _logger;
        private readonly IWishDaysService _service;

        public WishDaysController(
            IWishDaysService service, 
            ILogger<WishDaysController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<WishDay>> Get(string tenantId)
        {
            _logger.LogInformation($"Geting all data");
            return await _service.GetAsync(tenantId);
        }

        // GET api/values/5
        [HttpGet("{date}")]
        public async Task<WishDay> Get(string tenantId, DateTime date)
        {
            _logger.LogInformation($"Geting item");
            return await _service.GetWishDayAsync(tenantId, date);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(string tenantId, [FromBody]WishDay wishDay)
        {
            _logger.LogInformation($"Beginning POST: {wishDay.Date}");
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _logger.LogInformation("Posting new Habit with name {habitName}", wishDay.Date);
            var result = await _service.SaveAsync(tenantId, wishDay);
            if (result.Succeeded)
            {
                return Created("/", result);
            }
            return HttpBadRequest(result);
        }

        // PUT api/values/5
        [HttpPut("{date}")]
        public async Task<IActionResult> Put(string tenantId, DateTime date, [FromBody]WishDay noteDay)
        {
            _logger.LogInformation($"Beginning POST: {noteDay.Date}");
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            _logger.LogInformation("Posting new Habit with name {habitName}", noteDay.Date);
            var result = await _service.SaveAsync(tenantId, noteDay);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return HttpBadRequest(result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return HttpBadRequest(result);
        }
    }
}
