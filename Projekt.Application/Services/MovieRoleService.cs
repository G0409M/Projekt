using AutoMapper;
using Projekt.Domain.Contracts;
using Projekt.SharedKernel.Dto.MovieRole;
using Projekt.Domain.Exceptions;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Application.Services
{
    public class MovieRoleService : IMovieRoleService
    {
        private readonly IProjektUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MovieRoleService(IProjektUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public List<MovieRoleDto> GetAll()
        {
            var roles = _uow.MovieRoleRepository.GetAll();
            List<MovieRoleDto> result = _mapper.Map<List<MovieRoleDto>>(roles);
            return result;
        }

        public MovieRoleDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var role = _uow.MovieRoleRepository.Get(id);
            if (role == null)
            {
                throw new NotFoundException($"Could not find movie role with id = {id}");
            }

            var result = _mapper.Map<MovieRoleDto>(role);
            return result;
        }

        public int Create(CreateMovieRoleDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No movie role data");
            }

            var id = _uow.MovieRoleRepository.GetMaxId() + 1;
            var role = _mapper.Map<MovieRole>(dto);
            role.Id = id;

            _uow.MovieRoleRepository.Insert(role);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateMovieRoleDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No movie role data");
            }

            var role = _uow.MovieRoleRepository.Get(dto.Id);
            if (role == null)
            {
                throw new NotFoundException($"Could not find movie role with id = {dto.Id}");
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
                throw new NotFoundException($"Could not find movie role with id = {id}");
            }

            _uow.MovieRoleRepository.Delete(role);
            _uow.Commit();
        }
    }
}