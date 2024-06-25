using Projekt.Contracts;
using Projekt.Dto.User;
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

        public UserService(IProjektUnitOfWork context)
        {
            this._uow = context;
        }

        public List<UserDto> GetAll()
        {
            var users = _uow.UserRepository.GetAll();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                RegistrationDate = u.RegistrationDate
            }).ToList();
        }

        public UserDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is less than zero");
            }

            var user = _uow.UserRepository.Get(id);
            if (user == null)
            {
                throw new Exception($"Could not find user with id = {id}");
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate
            };
        }

        public int Create(CreateUserDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No user data");
            }

            var id = _uow.UserRepository.GetMaxId() + 1;

            var user = new User
            {
                Id = id,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password,
                RegistrationDate = DateTime.Now
            };

            _uow.UserRepository.Insert(user);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateUserDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No user data");
            }

            var user = _uow.UserRepository.Get(dto.Id);
            if (user == null)
            {
                throw new Exception($"Could not find user with id = {dto.Id}");
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
                throw new Exception($"Could not find user with id = {id}");
            }

            _uow.UserRepository.Delete(user);
            _uow.Commit();
        }
    }
}
