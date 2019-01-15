using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime LastChange { get; set; }
    }
}
