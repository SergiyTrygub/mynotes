using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Services;
using MyNotes.Web.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNotes.Web.Controllers.Api.v1
{
    [Route("api/[controller]")]
    public class NoteDaysController : Controller
    {
        private readonly IUserContextService _userContextService;
        private readonly ILogger<NoteDaysController> _logger;
        private readonly INoteDaysService _service;

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<NoteDay>> Get(string tenantId)
        {
            return await _service.GetAsync(tenantId);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<NoteDay> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NoteDay noteDay)
        {
            _logger.LogInformation($"Beginning POST: {noteDay.Date}");
            if (!ModelState.IsValid)
            {                
                return HttpBadRequest(ModelState);
            }
            _logger.LogInformation("Posting new Habit with name {habitName}", noteDay.Date);
            var result = await _service.SaveAsync(noteDay);
            if (result.Succeeded)
            {
                return Created("/", result);
            }
            return HttpBadRequest(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]NoteDay noteDay)
        {
            _logger.LogInformation($"Beginning POST: {noteDay.Date}");
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            _logger.LogInformation("Posting new Habit with name {habitName}", noteDay.Date);
            var result = await _service.SaveAsync(noteDay);
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
