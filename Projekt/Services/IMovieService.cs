using Projekt.Dto.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public interface IMovieService
    {
        List<MovieDto> GetAll();
        MovieDto GetById(int id);
        int Create(CreateMovieDto dto);
        void Update(UpdateMovieDto dto);
        void Delete(int id);
    }
}
