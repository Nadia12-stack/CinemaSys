using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.DataAccess.EntityConfigurations
{
    public class MovieCinemaEntityTypeConfiguration : IEntityTypeConfiguration<MovieCinema>
    {
        public void Configure(EntityTypeBuilder<MovieCinema> builder)
        {
            builder.HasKey(e => new { e.MovieId, e.CinemaId });
        }
    }
}
