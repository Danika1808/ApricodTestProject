using Application.Extensions;
using Application.Model;
using AutoMapper;
using Domain;
using Domain.Results;
using Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.GameServices
{
    public class GameService : IGameService
    {
        public readonly AppDbContext _context;
        public readonly IMapper _mapper;
        public GameService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result<Game> CreateGame(CreateGameViewModel model)
        {
            var game = _mapper.Map<Game>(model);

            var genres = new List<Genre>();

            foreach (var id in model.GenresId)
            {
                var genre = _context.Genres.FirstOrDefault(x => x.Id == id);

                if (genre == null)
                {
                    return Result<Game>.CreateFailure(Error.NotFound, "Жанр не найден");
                }

                if (!genres.Any(x => x.Id == id))
                {
                    genres.Add(genre);
                }
            }

            game.Genres = genres;

            _context.Games.Add(game);

            _context.SaveChanges();

            return Result<Game>.CreateSuccess(game);
        }

        public Result DeleteGame(Guid id)
        {
            var game = _context.Games.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return Result.CreateFailure(Error.NotFound, "Игра не найдена");
            }

            _context.Games.Remove(game);

            _context.SaveChanges();

            return Result.CreateSuccess("Игра успешно удалена");
        }

        public Result<IReadOnlyCollection<Game>> GetAllGames(SearchGameViewModel model)
        {
            if (model.Id != default || model.Name != default)
            {
                var game = _context.Games.Include(x => x.Genres).Where(x => x.Id == model.Id || x.Name == model.Name).ToList();

                if (game == null)
                {
                    return Result<IReadOnlyCollection<Game>>.CreateFailure(Error.NotFound, "Игра не найдена");
                }

                return Result<IReadOnlyCollection<Game>>.CreateSuccess(game);
            }
            var query = _context.Games.Include(x => x.Genres).AsNoTracking().AsQueryable();

            if (model.Genres != null)
            {
                foreach (var name in model.Genres)
                {
                    query = query.Where(x => x.Genres.Any(x => x.Name.Equals(name)));
                }
            }
            if (model.DevelopmentStudio != null)
            {
                query = query.Where(x => x.DevelopmentStudio.Equals(model.DevelopmentStudio));
            }

            var result = query.ToList();

            return Result<IReadOnlyCollection<Game>>.CreateSuccess(result);
        }

        public Result<Game> UpdateGame(Guid id, JsonPatchDocument<Game> jsonPatch)
        {
            var game = _context.Games.Include(x => x.Genres).FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return Result<Game>.CreateFailure(Error.NotFound, "Игра не найдена");
            }

            jsonPatch.Sanitize();

            var paths = jsonPatch.Operations.Select(x => x.path.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault().ToLower());

            if (!paths.Any())
            {
                return Result<Game>.CreateFailure(Error.BadRequest, "Ошибка при попытке изменить сущность");
            }

            jsonPatch.ApplyTo(game);

            _context.Update(game);

            _context.SaveChanges();

            return Result<Game>.CreateSuccess(game);
        }
    }
}
