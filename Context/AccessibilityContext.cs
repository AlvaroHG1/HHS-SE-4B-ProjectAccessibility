using Microsoft.EntityFrameworkCore;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Context;

public class AccessibilityContext : DbContext
{
    public AccessibilityContext(DbContextOptions<AccessibilityContext> options) : base(options)
    {
        
    }

    public DbSet<User> UserList { get; set; } = null!;
}