using Microsoft.AspNetCore.Identity;

namespace asp_book.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
