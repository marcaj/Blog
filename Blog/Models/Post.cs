using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [JsonIgnore]
        [Display(Name = "Publish date")]
        public DateTime PublishDate { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Review")]
        public Review Review { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        [NotMapped]
        public string TagsString
        {
            get { return Tags != null ? Tags.Aggregate(String.Empty, (tags, tag) => tags + "," + tag.Name) : String.Empty; }
        }

        [NotMapped]
        public string PublishDateString
        {
            get
            {
                return PublishDate.ToString("dd-MM-yyyy");
            }
        }
    }

    public static class PostExtensions
    {
        public static IQueryable<Post> Include(this IQueryable<Post> query)
        {
            return query.Include(x => x.Tags);

        }
    }
}
