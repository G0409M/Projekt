using AutoMapper;
using Projekt.Contracts;
using Projekt.Dto.Movie;
using Projekt.Exceptions;
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
        private readonly IMapper _mapper;

        public MovieService(IProjektUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CreateMovieDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No movie data");
            }

            var id = _uow.MovieRepository.GetMaxId() + 1;
            var movie = _mapper.Map<Movie>(dto);
            movie.Id_movie = id;

            _uow.MovieRepository.Insert(movie);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var movie = _uow.MovieRepository.Get(id);
            if (movie == null)
            {
                throw new NotFoundException($"Could not find movie with id = {id}");
            }

            _uow.MovieRepository.Delete(movie);
            _uow.Commit();
        }

        public List<MovieDto> GetAll()
        {
            var movies = _uow.MovieRepository.GetAll();
            var result = _mapper.Map<List<MovieDto>>(movies);
            return result;
        }

        public MovieDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var movie = _uow.MovieRepository.Get(id);
            if (movie == null)
            {
                throw new NotFoundException($"Could not find movie with id = {id}");
            }

            var result = _mapper.Map<MovieDto>(movie);
            return result;
        }

        public void Update(UpdateMovieDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No movie data");
            }

            var movie = _uow.MovieRepository.Get(dto.Id);
            if (movie == null)
            {
                throw new NotFoundException($"Could not find movie with id = {dto.Id}");
            }

            movie.FilmLength = dto.FilmLength;
            movie.Title = dto.Title;
            movie.Genre = dto.Genre;
            movie.ReleaseDate = dto.ReleaseDate;

            _uow.Commit();
        }

        public bool IsInUse(string title)
        {
            return _uow.MovieRepository.GetAll().Any(m => m.Title == title);
        }
    }
}
