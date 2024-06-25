using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Dto.MovieRole
{
    public class UpdateMovieRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string PersonName { get; set; }
        public int MovieId { get; set; }
        public RoleType RoleType { get; set; }
    }
}
