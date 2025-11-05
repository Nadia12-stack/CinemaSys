
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.DataAccess.EntityConfigurations
{
    public class SocialLinkEntityTypeConfiguration : IEntityTypeConfiguration<SocialLink>
    {
        public void Configure(EntityTypeBuilder<SocialLink> builder)
        {
            builder.HasKey(e => new { e.ActorId, e.Platform });
        }
    }
}
