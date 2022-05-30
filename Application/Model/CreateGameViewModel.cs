using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class CreateGameViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DevelopmentStudio { get; set; }
        public ICollection<Guid> GenresId { get; set; }
    }
}
