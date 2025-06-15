namespace EFCoreCinemaAPI.Models
{
    public class Merchandising : Product 
    {
        public double Weight { get; set; } //peso
        public double Volume { get; set; } //volumen
        public bool IsAvailable { get; set; } //disponible
        public bool IsCollectible { get; set; } //coleccionable
        public bool IsClothes { get; set; } //ropa
    }
}
