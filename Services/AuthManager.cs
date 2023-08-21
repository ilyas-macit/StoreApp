using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace Services;

public class AuthManager : IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;

    public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
    }

    public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

    public async Task<IEnumerable<IdentityUser>> GetAllUsers() => await _userManager.Users.ToListAsync();

    public async Task<IdentityResult> CreateUser(UserDtoForCreation userDtoForCreation)
    {
        var user = _mapper.Map<IdentityUser>(userDtoForCreation);
        var result = await _userManager.CreateAsync(user, userDtoForCreation.Password);
        if (!result.Succeeded)
        {
            throw new Exception();
        }

        if (userDtoForCreation.Roles.Count > 0)
        {
            var rolesResult = await _userManager.AddToRolesAsync(user, userDtoForCreation.Roles);
            if (!rolesResult.Succeeded)
            {
                throw new Exception();
            }
        }

        return result;
    }

    public async Task<IdentityUser> GetOneUser(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName)
    {
        var user = await GetOneUser(userName);
        if (user is not null)
        {
            var userDto = _mapper.Map<UserDtoForUpdate>(user);
            userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList());
            userDto.userRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
            return userDto;
        }

        throw new Exception("An error occured");
    }

    public async Task Update(UserDtoForUpdate userDto)
    {
        var user = await GetOneUser(userDto.UserName);
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;
        if (user is not null)
        {
            var result = await _userManager.UpdateAsync(user);
            if (userDto.Roles.Count > 0)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles);
                var r2 = await _userManager.AddToRolesAsync(user, userDto.Roles);
            }

            return;
        }

        throw new Exception("System has problem with user update!");
    }

    public async Task<IdentityResult> ResetPassword(ResetPasswordDto passwordDto)
    {
        var user = await GetOneUser(passwordDto.UserName);
        if (user is not null)
        {
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, passwordDto.Password);
            return result;
        }

        throw new Exception("User could not be found.");
    }

    public async Task<IdentityResult> DeleteOneUser(string userName)
    {
        var user = await GetOneUser(userName);
        var result = await _userManager.DeleteAsync(user);
        return result;
    }
}