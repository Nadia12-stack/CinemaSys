namespace CinemaSystem.Models
{
    public class SocialLink
    {
       
        public string Platform { get; set; } = string.Empty; 
        public string Url { get; set; } = string.Empty;

        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
    }
}
