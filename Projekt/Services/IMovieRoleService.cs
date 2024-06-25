using Projekt.Dto.MovieRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public interface IMovieRoleService
    {
        List<MovieRoleDto> GetAll();
        MovieRoleDto GetById(int id);
        int Create(CreateMovieRoleDto dto);
        void Update(UpdateMovieRoleDto dto);
        void Delete(int id);
    }
}
