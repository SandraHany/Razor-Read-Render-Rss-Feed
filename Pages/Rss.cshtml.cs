using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace WebApplication1.Pages
{
    public class RssModel : PageModel
    {
        public List<RssModelClass> RssList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            RssList = new List<RssModelClass>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://scripting.com/rss.xml");
                if (response.IsSuccessStatusCode)
                {
                    var xmlContent = await response.Content.ReadAsStringAsync();
                    var doc = new XmlDocument();
                    doc.LoadXml(xmlContent);

                    foreach (XmlNode node in doc.SelectNodes("/rss/channel/item"))
                    {
                        var modelObject = new RssModelClass();
                        modelObject.Guid = node["guid"].InnerText;
                        modelObject.Description = node["description"].InnerText;
                        modelObject.PubDate = node["pubDate"].InnerText;
                        modelObject.Link = node["link"].InnerText;
                        RssList.Add(modelObject);
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
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
