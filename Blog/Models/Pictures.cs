using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        [Required]
        public Post Post { get; set; }
    }
}
