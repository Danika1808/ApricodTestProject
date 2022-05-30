using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        [JsonPatchAllow]
        public string Name { get; set; }
        [JsonPatchAllow]
        public string DevelopmentStudio { get; set; }
        [JsonPatchAllow]
        public List<Genre> Genres { get; set; }
    }
}
