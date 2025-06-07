namespace EFCoreCinemaAPI.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Character { get; set; } //personaje
        public int Order { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Actor Actor { get; set; }

    }
}

