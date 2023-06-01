using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class CategoryNavigation
    {
        //Može sadržavati null vrijednost
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Postavljanje WebDrivera i otvaranje stranice samo jednom prije početka svih testova
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Zatvaranje preglednika nakon završetka svih testova
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
            IWebElement clothingLink = driver.FindElement(By.CssSelector("nav.bmenu a[href='https://www.oreabazaar.com/bs/category/1/odjeca']"));

            // Dovesti element u vidljivo područje
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", clothingLink);

            // Klikni na link za odjeću
            clothingLink.Click();

            // Provjeri da li se nalaziš na stranici sa odjećom
            Assert.IsTrue(driver.Url.Contains("https://www.oreabazaar.com/bs/category/1/odjeca"), "Nije uspješno navigirano na stranicu sa odjećom.");
        }
    }
}
