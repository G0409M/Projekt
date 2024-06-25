using Projekt.SharedKernel.Dto.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Application.Services
{
    public interface IMovieService
    {
        List<MovieDto> GetAll();
        MovieDto GetById(int id);
        int Create(CreateMovieDto dto);
        void Update(UpdateMovieDto dto);
        void Delete(int id);
        bool IsInUse(string title);
    }
}
