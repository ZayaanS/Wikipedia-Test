using System;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Wikipedia_Testing.Custom_Exceptions;

namespace Wikipedia_Testing.Page_Objects
{
    internal class WikipediaSearchResultsPage
    {
        private readonly ChromeDriver driver;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        // Constructor to initialize the driver
        public WikipediaSearchResultsPage(ChromeDriver driver)
        {
            this.driver = driver;
            // Add implicit wait globally for all elements
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // Method to get the page title
        public string GetPageTitle(string expectedPageTitle)
        {
            try
            {
                // Wait for the page to load and display the search results
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.Until(ExpectedConditions.TitleContains(expectedPageTitle));
                return driver.Title;
            }
            catch (WebDriverTimeoutException ex)
            {
                // Log the exception and rethrow or return a default value
                logger.Error(ex, $"Timeout waiting for page title to contain '{expectedPageTitle}'.");
                throw new PageTitleNotFoundException($"Expected page title '{expectedPageTitle}' not found.", ex);
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions and rethrow or return a default value
                logger.Error(ex, "Unexpected error while getting the page title.");
                throw;
            }
        }

        // Method to get the result paragraph text
        public string GetResultText(string tableClassName)
        {
            try
            {
                // Use explicit wait to ensure the table is present
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(tableClassName)));
                IWebElement textElement = driver.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(table));
                return textElement.Text;
            }
            catch (WebDriverTimeoutException ex)
            {
                // Log the exception and rethrow or return a default value
                logger.Error(ex, $"Timeout waiting for the table with class '{tableClassName}' to be visible.");
                throw new ElementNotVisibleException($"Table with class '{tableClassName}' not visible.", ex);
            }
            catch (NoSuchElementException ex)
            {
                // Log the exception and rethrow or return a default value
                logger.Error(ex, "Element not found.");
                throw new ElementNotFoundException("Expected element not found.", ex);
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions and rethrow or return a default value
                logger.Error(ex, "Unexpected error while getting the result text.");
                throw;
            }
        }
    }
}
