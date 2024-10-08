﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserServer.Core.DTO;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;

namespace UserServer.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private const string _defaultRoleUser = "User";
        private readonly IRoleService _roleService;

        public UserService(IUserRepository userRepository, IMapper mappre, UserManager<User> userManager, IRoleService roleService )
        {
            _userRepository = userRepository;
            _mapper = mappre;
            _userManager = userManager;
            _roleService = roleService;
        }

        public async Task<IdentityResult> CreateUserAsync(UserDto userDto, string password)
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
               var resultAddRole =  await _roleService.AddUserToRoleAsync(user.Id, new RoleDto { Name = _defaultRoleUser });
                if (!resultAddRole.Succeeded) return resultAddRole; 
            }

            return result;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<ResponceUserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ResponceUserDto>>(users);
        }

        public async Task<ResponceUserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            
            return _mapper.Map<ResponceUserDto>(user);
        }

        public async Task<ResponceUserDto> GetUserByUserName(string userName)
        {
            User user = await _userRepository.GetByUserNameAsync(userName);

            return _mapper.Map<ResponceUserDto>(user);
        }

        public async Task UpdateUserAsync(UserDto user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _userRepository.UpdateAsync(userEntity);
        }
    }
}
