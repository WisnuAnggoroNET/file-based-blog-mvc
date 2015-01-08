using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlogEngine.Web.Models
{
    public class BlogFileSystemManager
    {
        private string filePathToBlogPosts;

        public BlogFileSystemManager(string dirPath)
        {
            filePathToBlogPosts = dirPath;
        }

        public List<BlogListing> GetBlogListings(int limit)
        {
            var blogListings = new List<BlogListing>();
            foreach (var blogListing in GetPublishedAndValidFiles().OrderByDescending(i => i.PostDate).Take(limit))
            {
                blogListings.Add(blogListing);
            }
            return blogListings;
        }

        public List<BlogListing> GetPublishedAndValidFiles()
        {
            var allFileNames = Directory.GetFiles(filePathToBlogPosts, "*.md").ToList();
            var blogListings = new List<BlogListing>();
            foreach (var fileName in allFileNames.OrderByDescending(i => i).ToList())
            {
                // Check for valid file convention
                var r = new Regex(@"\d{4}-\d{2}-\d{2}_(.*)", RegexOptions.IgnoreCase);
                var m = r.Match(Path.GetFileName(fileName));
                if (m.Success)
                {
                    string fileData = File.ReadAllText(fileName);
                    var newBlogListing = GetBlogHeader(fileName, fileData);

                    if (newBlogListing != null)
                    {
                        if (newBlogListing.Title != "" &&
                            newBlogListing.UrlSlug != "")
                        {
                            blogListings.Add(newBlogListing);
                        }
                    }
                }
            }

            return blogListings;
        }

        public BlogListing GetBlogHeader(string filepath, string filecontent)
        {
            var yamlHeader = filecontent.YamlHeader();
            if (yamlHeader.ContainsKey("published") && yamlHeader["published"].ToString().ToLower() == "true")
            {
                return new BlogListing
                {
                    Title = yamlHeader.ContainsKey("title") ? yamlHeader["title"].ToString() : "",
                    Description = yamlHeader.ContainsKey("description") ? yamlHeader["description"].ToString() : "",
                    UrlSlug = yamlHeader.ContainsKey("slug") ? yamlHeader["slug"].ToString() : "",
                    PostDate = yamlHeader.ContainsKey("date") ? DateTime.Parse(yamlHeader["date"].ToString()) : GetFileDateStamp(filepath),
                    Tags = yamlHeader.ContainsKey("tags") ? (IList<string>)yamlHeader["tags"] : null,
                    Published = true
                };
            }
            else
            {
                return null;
            }
        }

        public DateTime GetFileDateStamp(string filepath)
        {
            var fileName = Path.GetFileName(filepath);
            var dateTime = fileName.Substring(0, 10);
            DateTime timestamp;
            return DateTime.TryParse(dateTime, out timestamp) ? timestamp : DateTime.Now;
        }

        public BlogPost GetBlogPostByTitleForUrl(string titleForUrl)
        {
            var files = Directory.GetFiles(filePathToBlogPosts, string.Format("*{0}*.md", titleForUrl));
            var r = new Regex(@"\d{4}-\d{2}-\d{2}_" + titleForUrl, RegexOptions.IgnoreCase);
            var fileName = files.Where(f => r.IsMatch(f)).FirstOrDefault();
            if (String.IsNullOrEmpty(fileName))
            {
                return null;
            }

            string fileData = File.ReadAllText(fileName);
            return new BlogPost(GetBlogHeader(fileName, fileData), fileData.ExcludeHeader());
        }
    }
}