using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class SearchGameViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? DevelopmentStudio { get; set; }
        public ICollection<string>? Genres { get; set; }
    }
}
