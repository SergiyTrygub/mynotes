using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using MyNotes.Web.Services;
using System.Collections;
using MyNotes.Web.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNotes.Web.Controllers.Api.v1
{
    [Route("api/[controller]")]
    public class WishItemsController : Controller
    {
        private readonly IWishItemsService _service;
        private readonly ILogger<WishItemsController> _logger;

        public WishItemsController(
            IWishItemsService service,
            ILogger<WishItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{wishDayId}")]
        public async Task<IEnumerable<WishItem>> Get(int wishDayId)
        {
            _logger.LogInformation($"Geting all wish items, day id: {wishDayId}");
            return await _service.GetAsync(wishDayId);
        }

        public async Task<IActionResult> Post([FromBody]WishItem item)
        {
            _logger.LogInformation($"Beginning POST: {item.Text}");
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var result = await _service.SaveAsync(item);
            if (result.Succeeded)
            {
                return Created("/", result);
            }
            return HttpBadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]WishItem item)
        {
            _logger.LogInformation($"Beginning PUT: {item.Text}");
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            
            var result = await _service.SaveAsync(item);
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
