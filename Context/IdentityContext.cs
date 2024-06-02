using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace identity;

public class IdentityContext : IdentityDbContext
{
  public IdentityContext(DbContextOptions<IdentityContext> options)
  : base(options)
  {
    
  }
}
