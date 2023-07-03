using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;

namespace WebApplication1.Pages
{
    public class RssModel : PageModel
    {
        public List<RssModelClass> RssList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            RssList = new List<RssModelClass>();
            var doc = new XmlDocument();
            using (var reader = new XmlTextReader("http://scripting.com/rss.xml"))
            {
                doc.Load(reader);
            }
            var ModelList = new RssModelClass();
            foreach (XmlNode node in doc.SelectNodes("/rss/channel/item"))
            {
                var ModelObject = new RssModelClass();
                ModelObject.Guid = node["guid"].InnerText;
                ModelObject.Description = node["description"].InnerText;
                ModelObject.PubDate = node["pubDate"].InnerText;
                ModelObject.Link = node["link"].InnerText;
                RssList.Add(ModelObject);
            }
            return Page();
        }
    }
    public class RssModelClass
    {
        public string Description { get; set; }
        public string PubDate { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string Text { get; set; }
        
    }
}
