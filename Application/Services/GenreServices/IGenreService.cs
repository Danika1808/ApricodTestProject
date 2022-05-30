using Domain;
using Domain.Results;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.GenreServices
{
    public interface IGenreService
    {
        public Result<Genre> CreateGenre(string name);
        public Result<Genre> UpdateGenre(Guid id, JsonPatchDocument<Genre> jsonPatch);
        public Result RemoveGenre(Guid id);
        public Result<IReadOnlyCollection<Genre>> GetAllGenres(Guid id = default, string name = default);
    }
}
