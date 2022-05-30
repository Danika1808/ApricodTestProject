using Application.Services.GenreServices;
using Domain;
using Domain.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApricodTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost("CreateGenre")]
        public ActionResult<ResponseResult<Genre>> CreateGenre([FromBody] string name)
        {
            var result = _genreService.CreateGenre(name);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpGet("Genres/{id}")]
        public ActionResult<ResponseResult<IReadOnlyCollection<Genre>>> GetGenre([FromRoute] Guid id)
        {

            var result = _genreService.GetAllGenres(id);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpGet("GetGenres")]
        public ActionResult<ResponseResult<IReadOnlyCollection<Genre>>> GetGenres()
        {
            var result = _genreService.GetAllGenres();

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpDelete("GeleteGenre")]
        public ActionResult<ResponseResult> GeleteGenre([Required][FromBody] Guid id)
        {
            var result = _genreService.RemoveGenre(id);

            if (result.IsSucceess)
            {
                return GetResponse(result);
            }
            else
            {
                return GetErrorResponse(result);
            }
        }

        [HttpPatch("UpdateGenre")]
        public ActionResult<ResponseResult<Genre>> UpdateGenre([FromQuery][Required] Guid id, [FromBody] JsonPatchDocument<Genre> jsonPatch)
        {
            var result = _genreService.UpdateGenre(id, jsonPatch);

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
