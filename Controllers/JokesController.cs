using System.Threading.Tasks;
using dadsjoke.Services;
using Microsoft.AspNetCore.Mvc;

namespace DadJokesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : ControllerBase
    {
        private readonly JokeService _jokeService;

        public JokeController(JokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [HttpGet("random")]
        public async Task<ActionResult<string>> GetRandomJoke()
        {
            var joke = await _jokeService.GetRandomJoke();
            return Ok(joke);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetJokeCount()
        {
            var count = await _jokeService.GetJokeCount();
            return Ok(count);
        }
    }
}
