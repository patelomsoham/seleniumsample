using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace TestingTool.Testing
{
    [TestClass]
    public class WikipediaSearchText
    {
        private IWebDriver driver;

        //Run 1 time BEFORE each test run
        [TestInitialize]
        public void TestInit()
        {
            #region Chrome Specific            
            //ChromeOptions chromeOptions = new ChromeOptions();
            ////This is the location where you have installed Firefox on your machine
            //chromeOptions.BinaryLocation = (@"C:\Program Files\Google\Chrome\Application\chrome.exe");
            //driver = new ChromeDriver(chromeOptions);
            #endregion

            #region Firefox Specific 
            driver = new FirefoxDriver();
            //FirefoxOptions firefoxOptions = new FirefoxOptions();
            ////This is the location where you have installed Firefox on your machine
            //firefoxOptions.BrowserExecutableLocation = (@"C:\Program Files\Mozilla Firefox\firefox.exe");
            //firefoxOptions.AddArgument("-silent");
            //driver = new FirefoxDriver(firefoxOptions);
            #endregion

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://www.wikipedia.org/");
        }

        //Run 1 time AFTER each test run
        [TestCleanup]
        public void TestCleanUp()
        {
            driver.Quit();
        }


        [TestMethod]
        public void Search_Find_ResultPageIsShown()
        {
            var searchTerm = "Narendra Modi";
            SearchFor(searchTerm);
            IWebElement firstHeading = GetByElement("firstHeading");

            var expected = searchTerm;
            var actual = firstHeading.Text;
            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void Search_Find_ResultPageIsNotFound()
        {
            SearchFor("abcxyz");
            IWebElement createLinkMessage = GetByClassName("mw-search-createlink");

            var expected = "The page \"Abcxyz\" does not exist";
            //var actual = createLinkMessage.Text;
            Assert.IsTrue(createLinkMessage.Text.Contains(expected));
        }

        #region Helper Methods
        private IWebElement GetByClassName(string className)
        {
            return driver.FindElement(By.ClassName(className));
        }

        private IWebElement GetByElement(string elementID)
        {
            return driver.FindElement(By.Id(elementID));
        }

        private void SearchFor(string searchTerm)
        {
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys(searchTerm);
            searchInput.SendKeys(Keys.Enter);
        }
        #endregion
    }
}
