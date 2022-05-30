using Domain.Attributes;

namespace Domain
{
    public class Genre
    {
        public Guid Id { get; set; }
        [JsonPatchAllow]
        public string Name { get; set; }
        [JsonPatchAllow]
        public List<Game> Games { get; set; }
    }
}