using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesPageAPI.Models;

namespace SalesPageAPI.Data
{
    public class UserDbContext : IdentityDbContext<User>
    {

        public UserDbContext(DbContextOptions<UserDbContext> opts) : base(opts)
        { }
    }
}
