
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.DataAccess.EntityConfigurations
{
    public class MovieActorEntityTypeConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(e => new { e.MovieId, e.ActorId });
        }
    }
}
