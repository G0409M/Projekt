using Projekt.Contracts;
using Projekt.Dto.MovieRole;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public class MovieRoleService : IMovieRoleService
    {
        private readonly IProjektUnitOfWork _uow;

        public MovieRoleService(IProjektUnitOfWork context)
        {
            this._uow = context;
        }

        public List<MovieRoleDto> GetAll()
        {
            var roles = _uow.MovieRoleRepository.GetAll();
            return roles.Select(r => new MovieRoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName,
                PersonName = r.PersonName,
                MovieId = r.MovieId,
                RoleType = r.RoleType
            }).ToList();
        }

        public MovieRoleDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is less than zero");
            }

            var role = _uow.MovieRoleRepository.Get(id);
            if (role == null)
            {
                throw new Exception($"Could not find movie role with id = {id}");
            }

            return new MovieRoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName,
                PersonName = role.PersonName,
                MovieId = role.MovieId,
                RoleType = role.RoleType
            };
        }

        public int Create(CreateMovieRoleDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No movie role data");
            }

            var id = _uow.MovieRoleRepository.GetMaxId() + 1;

            var role = new MovieRole
            {
                Id = id,
                RoleName = dto.RoleName,
                PersonName = dto.PersonName,
                MovieId = dto.MovieId,
                RoleType = dto.RoleType
            };

            _uow.MovieRoleRepository.Insert(role);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateMovieRoleDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No movie role data");
            }

            var role = _uow.MovieRoleRepository.Get(dto.Id);
            if (role == null)
            {
                throw new Exception($"Could not find movie role with id = {dto.Id}");
            }

            role.RoleName = dto.RoleName;
            role.PersonName = dto.PersonName;
            role.MovieId = dto.MovieId;
            role.RoleType = dto.RoleType;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            var role = _uow.MovieRoleRepository.Get(id);
            if (role == null)
            {
                throw new Exception($"Could not find movie role with id = {id}");
            }

            _uow.MovieRoleRepository.Delete(role);
            _uow.Commit();
        }
    }
}
