using Projekt.SharedKernel.Dto.Movie;
using Projekt.SharedKernel.Dto.MovieRole;
using Projekt.SharedKernel.Dto.Review;
using Projekt.SharedKernel.Dto.User;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Projekt.Application.Mappings
{
    public class ProjektMapper : Profile
    {
        public ProjektMapper()
        {
            // Mapowania dla Movie
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<Movie, MovieDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id_movie))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dst => dst.FilmLength, opt => opt.MapFrom(src => src.FilmLength))
                .ForMember(dst => dst.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dst => dst.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate));

            // Mapowania dla MovieRole
            CreateMap<CreateMovieRoleDto, MovieRole>();
            CreateMap<MovieRole, MovieRoleDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dst => dst.PersonName, opt => opt.MapFrom(src => src.PersonName))
                .ForMember(dst => dst.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dst => dst.RoleType, opt => opt.MapFrom(src => src.RoleType));

            // Mapowania dla Review
            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReviewDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

            // Mapowania dla User
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate));
        }
    }
}
