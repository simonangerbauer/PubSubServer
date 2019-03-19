using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <summary>
    /// A Proof for task completion
    /// </summary>
    public class Proof : Entity
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}