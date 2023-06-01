using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    internal class IncorrectLogin
    {
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com/bs/end_user/auth/login");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
        }

        [Test]
        public void LoginWithIncorrectCredentials_ShouldFail()
        {
            string email = "brdarevicajla@gmail.com";
            string password = "123sifra";

            driver.FindElement(By.Id("username")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(password);

            driver.FindElement(By.CssSelector(".green.login-register-button")).Click();

            // Provjera pojave poruke o neuspješnoj prijavi
            bool isErrorMessageDisplayed = driver.FindElement(By.ClassName("help-block")).Displayed;

            // Provjera očekivanog rezultata
            Assert.IsTrue(isErrorMessageDisplayed, "Poruka o neuspješnoj prijavi se ne prikazuje!");
        }
    }
}
