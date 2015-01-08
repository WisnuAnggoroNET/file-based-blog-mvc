using System;
using System.Collections.Generic;

namespace BlogEngine.Web.Models
{
    public class BlogListing
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public DateTime PostDate { get; set; }
        public IList<string> Tags { get; set; }
        public bool Published { get; set; }

        public BlogListing()
        {

        }

        public BlogListing(
            string title,
            string desc,
            string url,
            DateTime date,
            IList<string> tags,
            bool published
            )
        {
            Title = title;
            Description = desc;
            UrlSlug = url;
            PostDate = date;
            Tags = tags;
            Published = published;
        }
    }
}