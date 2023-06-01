using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class Price
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
        public void FilterProductsByPrice_ShouldDisplayProductsInRange()
        {
            // Pronađi klizač cijene
            IWebElement priceSlider = driver.FindElement(By.CssSelector("#slider"));

            // Dobavi koordinate klizača
            int xCoordinate = priceSlider.Location.X;
            int yCoordinate = priceSlider.Location.Y;

            // Dohvati širinu klizača
            int width = priceSlider.Size.Width;

            // Izračunaj x-koordinatu za donju granicu cijene (4 KM)
            decimal minPrice = 4;
            int minPriceXCoordinate = xCoordinate + (int)Math.Round(width * ((minPrice - 4) / (902 - 4)), MidpointRounding.AwayFromZero);

            // Izračunaj x-koordinatu za gornju granicu cijene (80 KM)
            decimal maxPrice = 80;
            int maxPriceXCoordinate = xCoordinate + (int)Math.Round(width * ((maxPrice - 4) / (902 - 4)), MidpointRounding.AwayFromZero) - 1;

            // Koristi akcije za postavljanje donje granice klizača
            Actions actions = new Actions(driver);
            actions.ClickAndHold(priceSlider)
                   .MoveByOffset(minPriceXCoordinate - xCoordinate, yCoordinate)
                   .Release()
                   .Perform();

            // Pričekaj da se proizvodi ažuriraju nakon filtriranja
            Thread.Sleep(2000);

            // Pronađi klizač cijene ponovo jer se može promijeniti nakon ažuriranja proizvoda
            priceSlider = driver.FindElement(By.CssSelector("#slider"));

            // Ponovo dobavi koordinate klizača
            xCoordinate = priceSlider.Location.X;

            // Koristi akcije za postavljanje gornje granice klizača
            actions.ClickAndHold(priceSlider)
                   .MoveByOffset(maxPriceXCoordinate - xCoordinate, yCoordinate)
                   .Release()
                   .Perform();

            // Pronađi gumb "Ažuriraj"
            IWebElement updateButton = driver.FindElement(By.CssSelector("button.btn.white"));

            // Klikni na gumb "Ažuriraj"
            updateButton.Click();

            // Pričekaj da se proizvodi ažuriraju nakon filtriranja
            System.Threading.Thread.Sleep(2000);

            // Provjeri da li se prikazuju samo proizvodi u odgovarajućem rasponu cijene
            ReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".product-list-item"));
            foreach (IWebElement product in products)
            {
                IWebElement priceElement = product.FindElement(By.CssSelector(".product-price"));
                string priceText = priceElement.Text;
                decimal price = decimal.Parse(priceText.Replace("KM", "").Trim());

                Assert.IsTrue(price >= minPrice && price <= maxPrice, $"Proizvod {product.Text} ima cijenu izvan raspona ({minPrice} KM - {maxPrice} KM).");
            }
        }


    }
}

