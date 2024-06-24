using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public enum RoleType
    {
        Actor,
        Screenwriter,
        Director,
        Producer,
        Cinematographer,
        Editor,
        Other 
    }
    public class MovieRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string PersonName { get; set; }
        public int MovieId { get; set; }
        public RoleType Type { get; set; }
    }
}
