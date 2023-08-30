using Microsoft.AspNetCore.Mvc;
using Stream_Csharp_Kafka.models;
using Stream_Csharp_Kafka.services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stream_Charp_Kafka.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StreamController : ControllerBase
    {
        private readonly StreamService _streamService;

        public StreamController(StreamService streamService)
        {
            _streamService = streamService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StreamModel model)
        {
            await _streamService.CreateAndPublish(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var model = await _streamService.GetAndConsume(id);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _streamService.GetAllAndConsume();
            return Ok(models);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StreamModel updatedModel)
        {
            var model = await _streamService.UpdateAndPublish(id, updatedModel);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _streamService.DeleteAndPublish(id);
            return NoContent();
        }
    }
}
