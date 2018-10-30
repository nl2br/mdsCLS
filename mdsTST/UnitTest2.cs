using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace mdsTST
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("TypesCategories.xml");

            XmlNode node = doc.SelectSingleNode("/root/type/categories/categorie[2]/nom");
            Assert.AreEqual("Jet dirigé", node.InnerText);
        }

        [TestMethod]
        public void TestMethod2()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("TypesCategories.xml");

            XmlNode node = doc.SelectSingleNode("//categorie[@id=2]/nom");
            Assert.AreEqual("Jet dirigé", node.InnerText);
        }

        [TestMethod]
        public void TestMethod3()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("TypesCategories.xml");

            XmlNode node = doc.SelectSingleNode("//nom[text()='Jet dirigé']");
            Assert.AreEqual("Jet dirigé", node.InnerText);
        }

        [TestMethod]
        public void TestMethod4()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("TypesCategories.xml");

            // cherche le noeud nom qui contient jet dirigé puis remonte 
            XmlNode node = doc.SelectSingleNode("//nom[text()='Jet dirigé']/../valeurs");
            Assert.AreEqual(3, node.ChildNodes.Count);
        }
    }
}
