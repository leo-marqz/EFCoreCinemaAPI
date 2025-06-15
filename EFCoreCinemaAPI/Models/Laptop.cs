namespace EFCoreCinemaAPI.Models
{
    public class Laptop : Product
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; } // in GB
        public int Storage { get; set; } // in GB
        public string GraphicsCard { get; set; }
        public double ScreenSize { get; set; } // in inches
    }
}
