using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Proof
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}