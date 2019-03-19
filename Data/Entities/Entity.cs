using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <summary>
    /// Abstract class Entity which every entity class has to derive from. Provides essential properties.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the last change.
        /// </summary>
        /// <value>The last change.</value>
        public DateTime LastChange { get; set; }
    }
}
