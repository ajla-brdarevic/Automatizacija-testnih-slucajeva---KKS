using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    internal class Country
    {
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com/bs/category/1/odjeca");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
        }

        [Test]
        public void FilterProductsByCategoryAndLocation_ShouldDisplayFilteredProducts()
        {
            // Provjeri da li se nalazimo na odgovarajućoj stranici
            Assert.AreEqual("https://www.oreabazaar.com/bs/category/1/odjeca", driver.Url);

            // Pronađi element padajućeg menija za državu
            IWebElement countryDropdown = driver.FindElement(By.Name("country"));

            // Selektuj Bosnu i Hercegovinu iz padajućeg menija
            SelectElement selectCountry = new SelectElement(countryDropdown);
            selectCountry.SelectByText("Bosna i Hercegovina");

            // Pričekaj da se proizvodi ažuriraju nakon filtriranja
            System.Threading.Thread.Sleep(2000);

            // Provjeri da li su se proizvodi filtrirali na osnovu lokacije
            ReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".product-list-item"));
            foreach (IWebElement product in products)
            {
                IWebElement locationElement = product.FindElement(By.CssSelector(".product-location"));
                string location = locationElement.Text;

                Assert.AreEqual("Bosna i Hercegovina", location, $"Proizvod {product.Text} nije iz Bosne i Hercegovine.");
            }
        }
    }
}
