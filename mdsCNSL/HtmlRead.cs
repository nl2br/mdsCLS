using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using HtmlAgilityPack;

namespace mdsCNSL
{

    public class ProduitHtml
    {
        public string Name;
        public string Description;
        public string Price;

        public ProduitHtml()
        {

        }
    }

    public class HtmlRead
    {
        public void Read()
        {
            var request = WebRequest.Create("http://store.demoqa.com/products-page/product-category/");
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();

            var htmlFile = new HtmlDocument();
            htmlFile.Load(dataStream);

            HtmlNodeCollection nodes = htmlFile.DocumentNode.SelectNodes("//div[contains(@class,'default_product_display')]");

            var products = new List<ProduitHtml>();

            foreach(HtmlNode node in nodes)
            {
                ProduitHtml product = new ProduitHtml();

                // . = parcourir current node    // = parcourir tous les "critères apres //"  
                product.Name = node.SelectSingleNode(".//a[contains(@class,'wpsc_product_title')]").InnerText;
                product.Description = node.SelectSingleNode(".//div[contains(@class,'wpsc_description')]/p").InnerText;
                product.Price = node.SelectSingleNode(".//span[contains(@class,'currentprice')]").InnerText;

                products.Add(product);
            }
            SaveXML(products);
        }

        public void SaveXML(List<ProduitHtml> produits)
        {
            string file = "Produits.xml";
            var xs = new XmlSerializer(typeof(List<ProduitHtml>));
            using (var sw = new StreamWriter(file))
            {
                xs.Serialize(sw, produits);
            }
        }

    }
}
