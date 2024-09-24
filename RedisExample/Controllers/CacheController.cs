using Microsoft.AspNetCore.Mvc;
using RedisExample.Models;
using RedisExample.Services;

namespace RedisExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : Controller
    {
        private readonly RedisService _redisService;

        public CacheController(RedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            return Ok(await _redisService.GetValueAsync(key));
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] RedisRequestModel redisCacheRequestModel)
        {
            await _redisService.SetValueAsync(redisCacheRequestModel.Key, redisCacheRequestModel.Value);
            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _redisService.Clear(key);
            return Ok();
        }
    }
}
