using ApricodTestProject.MapperProfile;
using AutoMapper;

namespace ApricodTestProject.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(conf =>
            {
                conf.AddProfile<GameProfile>();
            });

            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
