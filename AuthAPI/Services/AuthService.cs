using AuthAPI.Data;
using AuthAPI.Models.DTOs;
using AuthAPI.Models.Entities;
using AuthAPI.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContextFactory<DataContext> dbContextFactory;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IValidator<RegistrationRequestDto> validator;
        private readonly IMapper mapper;
        private readonly IUserStore<AppUser> userStore;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthService(
            IDbContextFactory<DataContext> dbContextFactory,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IValidator<RegistrationRequestDto> validator,
            IMapper mapper,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            this.dbContextFactory = dbContextFactory;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.validator = validator;
            this.mapper = mapper;
            this.userStore = userStore;
            this.signInManager = signInManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async ValueTask<Result<string>> AssignRole(string email, string roleName)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var user = await userManager.FindByNameAsync(email);

            if (user == null)
            {
                return new(new Exception($"Cannot find user with email: {email}"));
            }

            var isRoleExisting = await roleManager.RoleExistsAsync(roleName);
            
            if (!isRoleExisting)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var hasUserRole = await userManager.IsInRoleAsync(user, roleName);

            if (hasUserRole)
            {
                return new Result<string>(new Exception("User already has this role."));
            }

            var result = await userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return $"Role: {roleName} has successful assigned to user.";
            }

            return new Result<string>(new Exception(result.Errors.First().Description));
        }

        public async ValueTask<Result<bool>> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            return new();
        }

        public async ValueTask<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDTO)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var user = await userManager.FindByEmailAsync(loginRequestDTO.Email);

            if (user == null)
            {
                return new(new Exception($"Cannot find user with email: {loginRequestDTO.Email}. Check your email address."));
            }

            var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isValidPassword)
            {
                return new(new Exception("Invalid password. Please check your password."));
            }

            var result = await signInManager.PasswordSignInAsync(user, loginRequestDTO.Password, false, false);

            if (result.Succeeded)
            {
                var token = jwtTokenGenerator.GenerateTokenAsync(user);
                return new Result<LoginResponseDto>(new LoginResponseDto()
                {
                    Token = token,
                    User = mapper.Map<UserDto>(user)
                });
            }

            return new(new Exception("Something went wrong while tried to log in. Please try again later or contact us."));
        }

        public async ValueTask<Result<UserDto>> Register(RegistrationRequestDto registrationRequestDTO)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var validationResult = await validator.ValidateAsync(registrationRequestDTO);

            if (validationResult.IsValid)
            {
                AppUser user = mapper.Map<AppUser>(registrationRequestDTO);

                try
                {
                    var result = await userStore.CreateAsync(user, cancellationToken: new CancellationToken());

                    if (result.Succeeded)
                    {
                        var returnUser = await context.Users.FirstOrDefaultAsync(x => x.Email == registrationRequestDTO.Email);
                        var some = await userManager.AddPasswordAsync(user, registrationRequestDTO.Password);
                        var temp = mapper.Map<UserDto>(returnUser);
                        return new Result<UserDto>(temp);
                    }

                    return new Result<UserDto>(new Exception(result.Errors.First().Description));
                }
                catch (Exception ex)
                {
                    return new Result<UserDto>(new Exception(ex.Message));
                }
            }

            return new Result<UserDto>(new ValidationException(validationResult.Errors.First().ErrorMessage));
        }
    }
}
