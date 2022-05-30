using Application.Extensions;
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

namespace Application.Services.GenreServices
{
    public class GenreService : IGenreService
    {
        public readonly AppDbContext _context;
        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        public Result<Genre> CreateGenre(string name)
        {
            if (_context.Genres.Any(x => x.Name.Equals(name)))
            {
                return Result<Genre>.CreateFailure(Error.Conflict, "Жанр уже существует");
            }

            var genre = new Genre() { Name = name };

            _context.Add(genre);

            _context.SaveChanges();

            return Result<Genre>.CreateSuccess(genre);
        }

        public Result<IReadOnlyCollection<Genre>> GetAllGenres(Guid id = default, string name = default)
        {
            if (id != default || name != default)
            {
                var genre = _context.Genres.Include(x => x.Games).Where(x => x.Id == id || x.Name == name).ToList();

                if (genre == null)
                {
                    return Result<IReadOnlyCollection<Genre>>.CreateFailure(Error.NotFound, "Жанр не найден");
                }

                return Result<IReadOnlyCollection<Genre>>.CreateSuccess(genre);
            }
            var genres = _context.Genres.Include(x => x.Games).AsNoTracking().ToList();

            return Result<IReadOnlyCollection<Genre>>.CreateSuccess(genres);

        }

        public Result RemoveGenre(Guid id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == id);

            if (genre is null)
            {
                return Result.CreateFailure(Error.NotFound, "Жанр не найден");
            }

            _context.Genres.Remove(genre);

            _context.SaveChanges();

            return Result.CreateSuccess("Жанр успешно удален");
        }

        public Result<Genre> UpdateGenre(Guid id, JsonPatchDocument<Genre> jsonPatch)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == id);

            if (genre is null)
            {
                return Result<Genre>.CreateFailure(Error.NotFound, "Жанр не найден");
            }

            jsonPatch.Sanitize();

            var paths = jsonPatch.Operations.Select(x => x.path.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault().ToLower());

            if (!paths.Any())
            {
                return Result<Genre>.CreateFailure(Error.BadRequest, "Ошибка при попытке изменить сущность");
            }

            jsonPatch.ApplyTo(genre);

            _context.Update(genre);

            _context.SaveChanges();

            return Result<Genre>.CreateSuccess(genre);
        }
    }
}
