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

        public AuthService(
            IDbContextFactory<DataContext> dbContextFactory,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IValidator<RegistrationRequestDto> validator,
            IMapper mapper,
            IUserStore<AppUser> userStore)
        {
            this.dbContextFactory = dbContextFactory;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.validator = validator;
            this.mapper = mapper;
            this.userStore = userStore;
        }

        public async ValueTask<Result<bool>> AssignRole(string email, string roleName)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            return new();
        }

        public async ValueTask<Result<bool>> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            return new();
        }

        public async ValueTask<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDTO)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            return new();
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
