using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    internal class CorrectMailIncorrectPass
    {
        private IWebDriver driver = null!;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Postavljanje WebDrivera i otvaranje stranice samo jednom prije početka svih testova
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com/bs/end_user/auth/login");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Zatvaranje preglednika nakon završetka svih testova
            driver.Quit();
        }

        [Test]
        public void LoginWithCorrectEmailInccorectPass_ShouldFail()
        {
            // Unos ispravnih kredencijala za prijavu
            string email = "ajlabrdarevic@gmail.com";
            string password = "123sifra";

            // Unos emaila i lozinke
            driver.FindElement(By.Id("username")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(password);

            // Klik na gumb za prijavu
            driver.FindElement(By.CssSelector(".green.login-register-button")).Click();

            // Provjera pojave poruke o neuspješnoj prijavi
            bool isErrorMessageDisplayed = driver.FindElement(By.ClassName("help-block")).Displayed;

            // Provjera očekivanog rezultata
            Assert.IsTrue(isErrorMessageDisplayed, "Poruka o neuspješnoj prijavi se ne prikazuje!");
        }
    }
}
