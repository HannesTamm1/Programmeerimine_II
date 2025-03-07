using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductApiController(IProductService service)
        {
            _service = service;
        }
        // GET: api/<ProductApiController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var result = await _service.List(1, 10000);
            return result.Results;
        }

        // GET api/<ProductApiController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var list = await _service.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // POST api/<ProductApiController>
        [HttpPost]
        public async Task<object> Post([FromBody] Product list)
        {
            await _service.Save(list);

            return Ok(list);
        }

        // PUT api/<ProductApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            await _service.Save(list);

            return Ok();
        }


        // DELETE api/<ProductApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _service.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok();
        }
    }
}
