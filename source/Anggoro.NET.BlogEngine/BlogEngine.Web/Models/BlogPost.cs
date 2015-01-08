namespace BlogEngine.Web.Models
{
    public class BlogPost : BlogListing
    {

        public string Body { get; set; }

        public BlogPost(BlogListing bloglisting, string body)
            :base(
            bloglisting.Title,
            bloglisting.Description,
            bloglisting.UrlSlug,
            bloglisting.PostDate,
            bloglisting.Tags,
            bloglisting.Published
            )
        {
            Body = body;
        }
    }
}