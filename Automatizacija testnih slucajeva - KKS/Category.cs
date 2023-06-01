using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class CategoryNavigation
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
        }

        [Test]
        public void CategoryNavigation_ShouldNavigateToClothing()
        {
            // Pronađi link za kategorije i klikni na njega
            IWebElement categoriesLink = driver.FindElement(By.CssSelector("a#categories-menu-btn"));
            categoriesLink.Click();

            // Pričekaj da se otvori padajući meni
            System.Threading.Thread.Sleep(1000);

            // Pronađi link za odjeću i klikni na njega
            IWebElement clothingLink = driver.FindElement(By.LinkText("Odjeća"));
            clothingLink.Click();

            // Provjeri da li se nalaziš na stranici sa odjećom
            Assert.IsTrue(driver.Url.Contains("https://www.oreabazaar.com/bs/category/1/odjeca"), "Nije uspješno navigirano na stranicu sa odjećom.");
        }
    }
}
