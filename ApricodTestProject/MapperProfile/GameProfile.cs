using Application.Model;
using AutoMapper;
using Domain;

namespace ApricodTestProject.MapperProfile
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreateGameViewModel, Game>().ForMember(x => x.Genres, options => options.Ignore());
        }
    }
}
