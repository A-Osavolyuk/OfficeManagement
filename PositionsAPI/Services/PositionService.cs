using AutoMapper;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using PositionsAPI.Data;
using PositionsAPI.Models.DTOs;
using PositionsAPI.Models.Entities;
using PositionsAPI.Services.Interfaces;

namespace PositionsAPI.Services
{
    public class PositionService : IPositionsService
    {
        private readonly DateTimeOffset expirationTime = DateTimeOffset.UtcNow.AddMinutes(1);
        private readonly IDbContextFactory<DataContext> dbContextFactory;
        private readonly IValidator<PositionDto> validator;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public PositionService(
            IDbContextFactory<DataContext> dbContextFactory,
            IValidator<PositionDto> validator,
            IMapper mapper,
            ICacheService cacheService)
        {
            this.dbContextFactory = dbContextFactory;
            this.validator = validator;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async ValueTask<Result<Position>> Create(PositionDto positionDto)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var result = await validator.ValidateAsync(positionDto);

            if (result.IsValid)
            {
                var position = mapper.Map<Position>(positionDto);

                await context.Positions.AddAsync(position);
                await context.SaveChangesAsync();

                await RefreshData();

                return position;
            }

            return new Result<Position>(new ValidationException(result.Errors.First().ErrorMessage));
        }

        public async ValueTask<Result<bool>> DeleteById(int id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var position = await context.Positions.FirstOrDefaultAsync(x => x.PositionId == id);

            if (position is not null)
            {
                context.Positions.Remove(position);
                await context.SaveChangesAsync();

                await cacheService.RemoveAsync($"position-id-{id}");
                await RefreshData();

                return true;
            }

            return new Result<bool>(new Exception($"Cannot find position with id: {id}."));
        }

        public async ValueTask<Result<IEnumerable<Position>>> GetAll()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var positions = await cacheService.GetAsync<IEnumerable<Position>>(key: "positions");

            if (positions is null)
            {
                positions = await context.Positions.ToListAsync();
                await cacheService.SetAsync(key: "positions", positions, expirationTime);

                return new Result<IEnumerable<Position>>(positions);
            }

            return new Result<IEnumerable<Position>>(positions);
        }

        public async ValueTask<Result<IEnumerable<Position>>> GetAllByDepartmentId(int departmentId)
        {
            using (var context = await dbContextFactory.CreateDbContextAsync())
            {
                var positions = await cacheService.GetAsync<IEnumerable<Position>>(key: $"positions-id-{departmentId}");

                if (positions is null)
                {
                    positions = context.Positions.Where(x => x.DepartmentId == departmentId).Select(x => x).ToList();

                    if (!positions.Any())
                    {
                        return new Result<IEnumerable<Position>>(new Exception($"Cannot find positions with departmentId: {departmentId}."));
                    }

                    await cacheService.SetAsync(key: $"positions-id-{departmentId}", positions, expirationTime);
                    return new Result<IEnumerable<Position>>(positions);
                }
                return new Result<IEnumerable<Position>>(positions);
            }
            
        }

        public async ValueTask<Result<Position>> GetById(int id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var position = await cacheService.GetAsync<Position>($"position-id-{id}");

            if (position is null)
            {
                position = await context.Positions.FirstOrDefaultAsync(x => x.PositionId == id);

                if (position is null)
                {
                    return new Result<Position>(new Exception($"Cannot find positions with id: {id}"));
                }

                await cacheService.SetAsync(key: $"position-id-{id}", position, expirationTime);
                return new Result<Position>(position);
            }

            return new Result<Position>(position);
        }

        public async ValueTask<Result<Position>> GetByName(string name)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var position = await cacheService.GetAsync<Position>($"position-name-{name}");

            if (position is null)
            {
                position = await context.Positions.FirstOrDefaultAsync(x => x.PositionName.ToUpper() == name.ToUpper());

                await cacheService.SetAsync(key: $"position-name-{name}", position, expirationTime);

                return new Result<Position>(position);
            }

            return new Result<Position>(new Exception($"Cannot find positions with name: {name}"));
        }

        public async ValueTask<Result<Position>> Update(PositionDto positionDto, int id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var position = await context.Positions.FirstOrDefaultAsync(x => x.PositionId == id);
            var result = await validator.ValidateAsync(positionDto);

            if (position is null)
            {
                return new Result<Position>(new Exception($"Cannot find position with id: {id}"));
            }

            if (!result.IsValid)
            {
                return new Result<Position>(new ValidationException(result.Errors.First().ErrorMessage));
            }

            var tempPosition = mapper.Map<Position>(position);
            tempPosition.PositionName = positionDto.PositionName;
            tempPosition.DepartmentId = positionDto.DepartmentId;

            context.Positions.Update(tempPosition);
            await context.SaveChangesAsync();

            await RefreshData();
            await cacheService.SetAsync(key: $"position-id-{id}", position, expirationTime);

            return new Result<Position>(tempPosition);
        }

        private async ValueTask RefreshData()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var positions = await context.Positions.ToListAsync();
            await cacheService.SetAsync("positions", positions, expirationTime);
        }
    }
}
