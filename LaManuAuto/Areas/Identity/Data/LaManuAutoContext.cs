using LaManuAuto.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using LaManuAuto.Models;

namespace LaManuAuto.Data;

public class LaManuAutoContext : IdentityDbContext<LaManuAutoUser>
{
    public LaManuAutoContext(DbContextOptions<LaManuAutoContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<LaManuAutoUser>
    {
        public void Configure(EntityTypeBuilder<LaManuAutoUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
    }

    public DbSet<Tutorial> Tutorials { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<LaManuAutoUser> Users { get; set; }

}
