using AuthAPI.Models.Dto;
using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using AuthAPI.Service.IService;
using AuthAPI.Data;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()

            {

                UserName = registrationRequestDto.Email,

                Email = registrationRequestDto.Email,

                NormalizedEmail = registrationRequestDto.Email.ToUpper(),

                Name = registrationRequestDto.Name,

                PhoneNumber = registrationRequestDto.PhoneNumber

            };

            try

            {

                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

                if (result.Succeeded)

                {

                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()

                    {

                        Email = userToReturn.Email,

                        ID = userToReturn.Id,

                        Name = userToReturn.Name,

                        Phonenumber = userToReturn.PhoneNumber

                    };

                    return "";

                }

                else

                {

                    return result.Errors.FirstOrDefault().Description;

                }

            }

            catch (Exception ex)

            {

            }

            return "Error Encountered";

        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            // Find user by username
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            // Check password validity
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            // If user is not found or password is incorrect, return null for User and empty token
            if (user == null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            // Generate the JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            // Prepare the response object with user details and token
            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                Phonenumber = user.PhoneNumber
            };

            return new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
        }


        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
