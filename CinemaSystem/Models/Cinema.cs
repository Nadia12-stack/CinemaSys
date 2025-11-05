using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{
    public class Cinema : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }

        public string Img { get; set; } = "defaultImg.png";
        //public List<Movie> Movies { get; set; } = new();
        public List<MovieCinema> MovieCinemas { get; set; } = new();

    }
}
