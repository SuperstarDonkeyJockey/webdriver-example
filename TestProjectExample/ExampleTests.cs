using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace WebDriverExample
{
    [TestClass]
    public class WebDriverTests
    {
        IWebDriver driver;

        By searchQuery = By.Name("q");
        By searchSubmit = By.Name("btnG");
        By searchResults = By.CssSelector("#ires");
        By searchResultLinks = By.CssSelector("#ires .r a");

        public void waitForElement(By by, int seconds = 5)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementExists(by));
        }

        [TestInitialize]
        public void StartDriver()
        {
            driver = new ChromeDriver(@"C:\WebDriver");
        }

        [TestCleanup]
        public void CloseDriver()
        {
            driver.Close();
        }

        [TestMethod]
        public void TopResultForGoogleSearch()
        {
            driver.Navigate().GoToUrl("https://www.google.co.uk");
            driver.FindElement(searchQuery).SendKeys("Wayne Peacock");
            driver.FindElement(searchSubmit).Click();
            waitForElement(searchResults);
            var resultLinks = driver.FindElements(searchResultLinks);
            Assert.AreEqual("Wayne Peacock profiles - United Kingdom | LinkedIn", resultLinks[0].Text);
        }
    }
}
