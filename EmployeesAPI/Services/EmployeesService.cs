using AutoMapper;
using EmployeesAPI.Data;
using EmployeesAPI.Models.DTOs;
using EmployeesAPI.Models.Entities;
using EmployeesAPI.Services.Interfaces;
using FluentValidation;
using LanguageExt.Common;
using LanguageExt.Pretty;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Services
{
    public class EmployeesService : IEmployeesService
    {
        private DateTimeOffset expirationTime = DateTimeOffset.UtcNow.AddMinutes(1);
        private readonly IDbContextFactory<DataContext> dbContextFactory;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;
        private readonly IValidator<EmployeeDto> validator;

        public EmployeesService(
            IDbContextFactory<DataContext> dbContextFactory,
            ICacheService cacheService,
            IMapper mapper,
            IValidator<EmployeeDto> validator)
        {
            this.dbContextFactory = dbContextFactory;
            this.cacheService = cacheService;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async ValueTask<Result<Employee>> Create(EmployeeDto employeeDto)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var result = await validator.ValidateAsync(employeeDto);

            if (result.IsValid)
            {
                var employee = mapper.Map<Employee>(employeeDto);
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                await RefreshData();

                return new Result<Employee>(employee);
            }

            return new Result<Employee>(new ValidationException(result.Errors.First().ErrorMessage));
        }

        public async ValueTask<Result<bool>> DeleteById(int id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (employee is null)
            {
                return new Result<bool>(new Exception($"Cannot find employee with id: {id}."));
            }

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();

            await cacheService.RemoveAsync("employee-id-{id}");
            await RefreshData();

            return new Result<bool>(true);
        }

        public async ValueTask<Result<IEnumerable<Employee>>> GetAll()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employees = await cacheService.GetAsync<IEnumerable<Employee>>(key:"employees");

            if (employees is null)
            {
                employees = await context.Employees.ToListAsync();

                if (employees is null)
                {
                    return new Result<IEnumerable<Employee>>(new Exception("Something went wrong."));
                }

                await cacheService.SetAsync(key: "employees", employees, expirationTime);
                return new Result<IEnumerable<Employee>>(employees);
            }

            return new Result<IEnumerable<Employee>>(employees);
        }

        public async ValueTask<Result<IEnumerable<Employee>>> GetAllByPositionId(int positionId)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employees = await cacheService.GetAsync<IEnumerable<Employee>>($"employees-id-{positionId}");

            if(employees is null)
            {
                employees = await context.Employees.Where(x => x.PositionId == positionId).Select(x => x).ToListAsync();

                if(!employees.Any())
                {
                    return new Result<IEnumerable<Employee>>(new Exception($"Cannot find employees with position id: {positionId}"));
                }

                await cacheService.SetAsync(key: $"employees-id-{positionId}", employees, expirationTime);
                return new Result<IEnumerable<Employee>>(employees);
            }

            return new Result<IEnumerable<Employee>>(employees);
        }

        public async ValueTask<Result<Employee>> GetByEmail(string email)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employee = await cacheService.GetAsync<Employee>($"employee-email-{email}");

            if(employee is null)
            {
                if(employee is null)
                {
                    employee = await context.Employees.FirstOrDefaultAsync(x => x.Email == email);

                    return new Result<Employee>(new Exception($"Cannot find employee with email: {email}."));
                }

                await cacheService.SetAsync($"employee-email-{email}", employee, expirationTime);

                return new Result<Employee>(employee);
            }

            return new Result<Employee>(employee);
        }

        public async ValueTask<Result<Employee>> GetById(int id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employee = await cacheService.GetAsync<Employee>($"employee-id-{id}");

            if (employee is null)
            {
                if (employee is null)
                {
                    employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

                    return new Result<Employee>(new Exception($"Cannot find employee with id: {id}."));
                }

                await cacheService.SetAsync($"employee-id-{id}", employee, expirationTime);

                return new Result<Employee>(employee);
            }

            return new Result<Employee>(employee);
        }

        public async ValueTask<Result<Employee>> Update(int id, EmployeeDto employeeDto)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            var result = await validator.ValidateAsync(employeeDto);

            if (!result.IsValid)
            {
                return new(new ValidationException(result.Errors.First().ErrorMessage));
            }

            if (employee is null)
            {
                return new(new Exception($"Cannot find employee with id: {id}."));
            }

            var tempEmployee = mapper.Map<Employee>(employeeDto);
            tempEmployee.EmployeeId = id;

            context.Update(tempEmployee);
            await context.SaveChangesAsync();

            await cacheService.SetAsync($"employee-id-{id}", tempEmployee, expirationTime);
            await RefreshData();

            return new(tempEmployee);
        }

        private async ValueTask RefreshData()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var employees = await context.Employees.ToListAsync();
            await cacheService.SetAsync(key: "employees", employees, expirationTime);
        }
    }
}
