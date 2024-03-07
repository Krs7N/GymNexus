using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface ITokenService
{
    string CreateJwtToken(ApplicationUser user, IEnumerable<string> roles);
}