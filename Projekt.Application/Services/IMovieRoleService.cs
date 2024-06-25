using Projekt.SharedKernel.Dto.MovieRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Application.Services
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
