using AuthAPI.Models.Entities;

namespace AuthAPI.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateTokenAsync(AppUser user, IEnumerable<string> roles);
    }
}
