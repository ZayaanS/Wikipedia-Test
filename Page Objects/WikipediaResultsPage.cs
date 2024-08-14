using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Wikipedia_Testing.Page_Objects
{
    internal class WikipediaSearchResultsPage
    {
        private readonly ChromeDriver driver;

        // Constructor to initialize the driver
        public WikipediaSearchResultsPage(ChromeDriver driver)
        {
            this.driver = driver;
        }

        // Method to get the page title
        public string GetPageTitle()
        {
            return driver.Title;
        }

        // Method to get the result paragraph text
        public string GetResultText(string tableClassName)
        {
            IWebElement table = driver.FindElement(By.ClassName(tableClassName));
            IWebElement textElement = driver.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(table));
            return textElement.Text;
        }
    }
}
