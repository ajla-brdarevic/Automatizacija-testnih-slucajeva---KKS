using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class ProductDetails
    {
        private IWebDriver driver = null!;

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
        public void NavigateToProductDetails_ShouldShowProductDetails()
        {
            // Navigacija na "oreabazaar.com"
            driver.Navigate().GoToUrl("https://www.oreabazaar.com");

            // Navigacija na "Božićni tanjuri"
            driver.FindElement(By.LinkText("Božićni tanjuri")).Click();

            // Klik na određeni proizvod
            driver.FindElement(By.LinkText("Božićni tanjuri")).Click();

            // Provjera očekivanog rezultata - provjera prisutnosti elemenata s detaljima o proizvodu
            bool areDetailsShown = driver.FindElement(By.ClassName("product-details")).Displayed;

            // Provjera očekivanog rezultata
            Assert.IsTrue(areDetailsShown, "Detalji proizvoda nisu prikazani!");
        }
    }
}
