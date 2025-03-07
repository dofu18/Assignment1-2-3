using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Data;
using Assignment.Data.Models;
using Assignment.Data.Repository;
using Assignment.Service.Dto;
using AutoMapper;

namespace Assignment.Service
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _unitOfWork.UserRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UserDto userDto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (existingUser == null)
                return null;

            _mapper.Map(userDto, existingUser);
            _unitOfWork.UserRepository.Update(existingUser);
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            _unitOfWork.UserRepository.Remove(user);
            return true;
        }
    }
}
