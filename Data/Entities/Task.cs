using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        public IEnumerable<Proof> Proofs { get; set; }

        public String Officers { get; set; }

        public string Activity { get; set; }

        public string Description { get; set; }

        public DateTime Due { get; set; }

        public string Title { get; set; }

        public int Progress { get; set; }
    }
}
