using Microsoft.AspNetCore.Mvc;

namespace ClientSideCaching.Controllers;

[ApiController]
[Route("[controller]")]
public class CacheController : ControllerBase
{
    private readonly ClientSideCache _cache;

    public CacheController(ClientSideCache cache)
    {
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string key)
    {
        return new JsonResult(await _cache.Get(key));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CacheRequest req)
    {
        await _cache.Set(req.Key, req.Value);
        return Ok(req);
    }
}