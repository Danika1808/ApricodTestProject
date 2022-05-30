using Application;
using Application.Model;
using Application.Services.GameServices;
using Domain;
using Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApricodTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        public readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("CreateGame")]
        public ActionResult<ResponseResult<Game>> CreateGame([FromBody] CreateGameViewModel model)
        {
            var result = _gameService.CreateGame(model);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpGet("Games/{id}")]
        public ActionResult<ResponseResult<IReadOnlyCollection<Game>>> GetGame([FromRoute] Guid id)
        {
            var model = new SearchGameViewModel() { Id = id };

            var result = _gameService.GetAllGames(model);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpGet("GetGames")]
        public ActionResult<ResponseResult<IReadOnlyCollection<Game>>> GetGames([FromQuery] SearchGameViewModel model)
        {
            var result = _gameService.GetAllGames(model);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpDelete("DeleteGame")]
        public ActionResult<ResponseResult> GeleteGame([Required][FromBody] Guid id)
        {
            var result = _gameService.DeleteGame(id);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpPatch("UpdateGame")]
        public ActionResult<ResponseResult<Game>> UpdateGame([FromQuery][Required] Guid id, [FromBody] JsonPatchDocument<Game> jsonPatch)
        {
            var result = _gameService.UpdateGame(id, jsonPatch);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }
    }
}
