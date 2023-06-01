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
    public class Availability
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
        public void FilterProductsByCategoryAndAvailability_ShouldDisplayAvailableProducts()
        {
            // Provjeri da li se nalazimo na odgovarajućoj stranici
            Assert.AreEqual("https://www.oreabazaar.com/bs/category/1/odjeca", driver.Url);

            // Pronađi element padajućeg menija za dostupnost
            IWebElement availabilityDropdown = driver.FindElement(By.Name("is_available"));

            // Selektuj opciju "Proizvodi na stanju" iz padajućeg menija
            SelectElement selectAvailability = new SelectElement(availabilityDropdown);
            selectAvailability.SelectByText("Proizvodi na stanju");

            // Pričekaj da se proizvodi ažuriraju nakon filtriranja
            System.Threading.Thread.Sleep(2000);

            // Provjeri da li su se proizvodi filtrirali na osnovu dostupnosti
            ReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".product-list-item"));
            foreach (IWebElement product in products)
            {
                IWebElement availabilityElement = product.FindElement(By.CssSelector(".product-availability"));
                string availability = availabilityElement.Text;

                Assert.IsTrue(availability.Contains("Na stanju"), $"Proizvod {product.Text} nije dostupan na stanju.");
            }
        }
    }
}
