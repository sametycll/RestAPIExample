using Microsoft.AspNetCore.Mvc;
using RestAPIExample.Models;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace RestAPIExample.Controllers
{
    public class Bank : Controller
    {
        public async Task<IActionResult> Index()
        {

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri("https://www.tcmb.gov.tr/kurlar/today.xml"));
            httpRequest.ContentType = "application/xml";
            httpRequest.Method = "Get";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string xml = (new StreamReader(stream)).ReadToEnd(); //get the XML response

                    //Using XMLSerializer to convert the XML response to the object.
                    XmlSerializer serializer = new XmlSerializer(typeof(Tarih_Date));
                    StringReader rdr = new StringReader(xml);
                    var result = (Tarih_Date)serializer.Deserialize(rdr);
                    return View(result);
                }
            }

            
        }
    }
}