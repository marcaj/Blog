using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Review")]
        public double Value { get; set; }

        [Required]
        [Display(Name = "Restaurant")]
        public Restaurant Restaurant { get; set; }

        [Required]
        public Post Post { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
