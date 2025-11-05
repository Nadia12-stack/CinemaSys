
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.DataAccess.EntityConfigurations
{
    public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<MovieSubImage>
    {
        public void Configure(EntityTypeBuilder<MovieSubImage> builder)
        {
            builder.HasKey(e => new { e.MovieId, e.Img });
        }

    }
}
