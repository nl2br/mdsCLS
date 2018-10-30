using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace mdsTST
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            var request = WebRequest.Create("http://www.demoqa.com");
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();
            var sr = new StreamReader(dataStream);

            var result = sr.ReadToEnd();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var request = WebRequest.Create("http://store.demoqa.com/products-page/product-category/");
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();

            var htmlFile = new HtmlDocument();
            htmlFile.Load(dataStream);

            var node = htmlFile.DocumentNode.SelectSingleNode("//*[@id=\"post - 1\"]/header/h2/a");
            Console.WriteLine(node.InnerText);
        }
    }
}
