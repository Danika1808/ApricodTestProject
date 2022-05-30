using Application.Model;
using Domain;
using Domain.Results;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.GameServices
{
    public interface IGameService
    {
        public Result<Game> CreateGame(CreateGameViewModel model);
        public Result<Game> UpdateGame(Guid id, JsonPatchDocument<Game> jsonPatch);
        public Result DeleteGame(Guid id);
        public Result<IReadOnlyCollection<Game>> GetAllGames(SearchGameViewModel model);
    }
}
