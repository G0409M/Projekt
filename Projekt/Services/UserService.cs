using AutoMapper;
using Projekt.Contracts;
using Projekt.Dto.User;
using Projekt.Exceptions;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public class UserService : IUserService
    {
        private readonly IProjektUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IProjektUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public List<UserDto> GetAll()
        {
            var users = _uow.UserRepository.GetAll();
            List<UserDto> result = _mapper.Map<List<UserDto>>(users);
            return result;
        }

        public UserDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var user = _uow.UserRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException($"Could not find user with id = {id}");
            }

            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public int Create(CreateUserDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No user data");
            }

            var id = _uow.UserRepository.GetMaxId() + 1;
            var user = _mapper.Map<User>(dto);
            user.Id = id;
            user.RegistrationDate = DateTime.Now;

            _uow.UserRepository.Insert(user);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateUserDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No user data");
            }

            var user = _uow.UserRepository.Get(dto.Id);
            if (user == null)
            {
                throw new NotFoundException($"Could not find user with id = {dto.Id}");
            }

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = dto.Password;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            var user = _uow.UserRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException($"Could not find user with id = {id}");
            }

            _uow.UserRepository.Delete(user);
            _uow.Commit();
        }
    }
}

