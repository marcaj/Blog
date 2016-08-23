using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace Blog.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }

        public static ICollection<Tag> GetListByString(string tags, BlogContext context)
        {
            ICollection<Tag> tagCollection = new List<Tag>();

            List<string> tagList = tags.Split(',').ToList();
            tagList.ForEach(tagName =>
            {
                Tag tag = GetByString(tagName, context);
                tagCollection.Add(tag);
            });

            return tagCollection;
        }

        private static Tag GetByString(string tagName, BlogContext context)
        {
            string tagNameClear = tagName.Trim();
            Tag tag = context.Tags.FirstOrDefault(t => t.Name == tagName);
            
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = tagName
                };
                context.Tags.Add(tag);
            }

            return tag;
        }
    }
}
