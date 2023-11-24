using AutoMapper;
using DepartmentsAPI.Data;
using DepartmentsAPI.Models.DTOs;
using DepartmentsAPI.Models.Entities;
using DepartmentsAPI.Services.Interfaces;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace DepartmentsAPI.Services
{
    public class DepartmentService : IDepartmentsService
    {
        private readonly DateTimeOffset expirationTime = DateTimeOffset.UtcNow.AddMinutes(1);

        private readonly ICacheService cacheService;
        private readonly IDbContextFactory<DataContext> factory;
        private readonly IValidator<DepartmentDto> validator;
        private readonly IMapper mapper;

        public DepartmentService(
            ICacheService cacheService, 
            IDbContextFactory<DataContext> factory, 
            IValidator<DepartmentDto> validator, 
            IMapper mapper)
        {
            this.cacheService = cacheService;
            this.factory = factory;
            this.validator = validator;
            this.mapper = mapper;
        }

        private async void RefreshData()
        {
            using var context = await factory.CreateDbContextAsync();

            var departments = context.Departments.ToListAsync();
            await cacheService.SetAsync("departments", departments, expirationTime);
        }

        public async ValueTask<Result<Department>> Create(DepartmentDto departmentDto)
        {
            using var context = await factory.CreateDbContextAsync();

            var validationResult = await validator.ValidateAsync(departmentDto);

            if (validationResult.IsValid)
            {
                var department = mapper.Map<Department>(departmentDto);
                await context.Departments.AddAsync(department);
                await context.SaveChangesAsync();

                RefreshData();

                return department;
            }

            return new Result<Department>(new ValidationException(validationResult.Errors.First().ErrorMessage));
        }

        public async ValueTask<Result<bool>> DeleteById(int id)
        {
            using var context = await factory.CreateDbContextAsync();

            var department = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                return new Result<bool>(new Exception($"Cannot find department with id: {id}"));
            }

            context.Departments.Remove(department);
            await context.SaveChangesAsync();

            RefreshData();
            await cacheService.RemoveAsync($"department-id-{id}");

            return true;
        }

        public async ValueTask<Result<IEnumerable<Department>>> GetAll()
        {
            using var context = await factory.CreateDbContextAsync();

            var departments = await context.Departments.ToListAsync();
            
            await cacheService.SetAsync("departments", departments, expirationTime);

            return departments;
        }

        public async ValueTask<Result<Department>> GetById(int id)
        {
            using var context = await factory.CreateDbContextAsync();

            var department = await cacheService.GetAsync<Department>($"department-id-{id}");

            if (department == null)
            {
                department = context.Departments.FirstOrDefault(x => x.DepartmentId == id);

                if (department == null)
                {
                    return new Result<Department>(new Exception($"Cannot find department with id: {id}"));
                }

                await cacheService.SetAsync($"department-id-{id}", department, expirationTime);
                return department;
            }

            return department;
        }

        public async ValueTask<Result<Department>> Update(DepartmentDto departmentDto, int id)
        {
            using var context = await factory.CreateDbContextAsync();

            var validationResult = await validator.ValidateAsync(departmentDto);
            var department = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);

            if(department is not null)
            {
                if (!validationResult.IsValid)
                {
                    return new Result<Department>(new ValidationException(validationResult.Errors.First().ErrorMessage));
                }

                department = mapper.Map<Department>(departmentDto);
                department.DepartmentId = id;

                context.Departments.Update(department);
                await context.SaveChangesAsync();

                RefreshData();
                await cacheService.SetAsync($"department-id-{id}", department, expirationTime);

                return department;
            }

            return new Result<Department>(new Exception($"Cannot find department with id: {id}"));
        }
    }
}
