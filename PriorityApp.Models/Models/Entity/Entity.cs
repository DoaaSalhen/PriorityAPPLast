using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PriorityApp.Models.Models.Entity
{
    public class Entity<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public T Id { get; set; }
        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }

    public class LookupEntity : Entity<int>
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

    }

    public class EntityWithIdentityId<T>
    {
        public T Id { get; set; }
        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }

}

