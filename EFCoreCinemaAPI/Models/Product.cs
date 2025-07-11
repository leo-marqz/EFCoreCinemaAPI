﻿using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
    }
}
