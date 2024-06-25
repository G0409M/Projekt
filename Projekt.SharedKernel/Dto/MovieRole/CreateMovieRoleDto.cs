using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.SharedKernel.Dto.MovieRole
{
    public class CreateMovieRoleDto
    {
        public string RoleName { get; set; }
        public string PersonName { get; set; }
        public int MovieId { get; set; }
        public RoleType RoleType { get; set; }
    }
}
