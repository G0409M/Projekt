using Projekt.Contracts;
using Projekt.Dto.Movie;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public class MovieService : IMovieService
    {
        private readonly IProjektUnitOfWork _uow;

        public MovieService(IProjektUnitOfWork context)
        {
            this._uow = context;
        }

        public List<MovieDto> GetAll()
        {
            var movies = _uow.MovieRepository.GetAll();
            return movies.Select(m => new MovieDto
            {
                Id = m.Id_movie,
                FilmLength = m.FilmLength,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate
            }).ToList();
        }

        public MovieDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is less than zero");
            }

            var movie = _uow.MovieRepository.Get(id);
            if (movie == null)
            {
                throw new Exception($"Could not find movie with id = {id}");
            }

            return new MovieDto
            {
                Id = movie.Id_movie,
                FilmLength = movie.FilmLength,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };
        }

        public int Create(CreateMovieDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No movie data");
            }

            var id = _uow.MovieRepository.GetMaxId() + 1;

            var movie = new Movie
            {
                Id_movie = id,
                FilmLength = dto.FilmLength,
                Title = dto.Title,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate
            };

            _uow.MovieRepository.Insert(movie);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateMovieDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No movie data");
            }

            var movie = _uow.MovieRepository.Get(dto.Id);
            if (movie == null)
            {
                throw new Exception($"Could not find movie with id = {dto.Id}");
            }

            movie.FilmLength = dto.FilmLength;
            movie.Title = dto.Title;
            movie.Genre = dto.Genre;
            movie.ReleaseDate = dto.ReleaseDate;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            var movie = _uow.MovieRepository.Get(id);
            if (movie == null)
            {
                throw new Exception($"Could not find movie with id = {id}");
            }

            _uow.MovieRepository.Delete(movie);
            _uow.Commit();
        }
    }
}
