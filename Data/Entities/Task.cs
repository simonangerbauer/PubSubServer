using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <summary>
    /// Derives from Entity and represents a task.
    /// </summary>
    public class Task : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Data.Task"/> class.
        /// </summary>
        public Task()
        {
            Proofs = new List<Proof>();
        }

        /// <summary>
        /// Gets or sets the proofs.
        /// </summary>
        /// <value>The proofs.</value>
        public List<Proof> Proofs { get; set; }

        /// <summary>
        /// Gets or sets the officers.
        /// </summary>
        /// <value>The officers.</value>
        public String Officers { get; set; }

        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        public string Activity { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due.
        /// </summary>
        /// <value>The due.</value>
        public DateTime Due { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public int Progress { get; set; }
    }
}
