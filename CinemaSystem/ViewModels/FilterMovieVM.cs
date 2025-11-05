namespace CinemaSystem.ViewModels
{
    public record FilterMovieVM (
        string name,  int? categoryId, int? cinemaId,  bool isHot
    );
}
