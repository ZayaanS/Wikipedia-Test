using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Wikipedia_Testing.Custom_Exceptions;

namespace Wikipedia_Testing.Page_Objects
{
    // Internal class to represent the Wikipedia search results page
    internal class WikipediaSearchResultsPage
    {
        private readonly ChromeDriver driver; // ChromeDriver instance used to control the browser
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger(); // Logger instance for logging messages

        // Constructor to initialize the driver instance
        public WikipediaSearchResultsPage(ChromeDriver driver)
        {
            this.driver = driver; // Assign the passed driver instance to the private field
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Add implicit wait globally for all elements
        }

        // Method to get the page title
        public string GetPageTitle(string expectedPageTitle)
        {
            try // Try block to handle potential exceptions during title retrieval
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15)); // Wait for the page to load and display the search results
                wait.Until(ExpectedConditions.TitleContains(expectedPageTitle)); // Wait for the page title to contain the expected text 
                return driver.Title; // Return the actual page title retrieved from the driver
            }
            catch (WebDriverTimeoutException ex) // Catch block to handle WebDriverException
            { 
                logger.Error(ex, $"Timeout waiting for page title to contain '{expectedPageTitle}'."); // Log the exception
                throw new PageTitleNotFoundException($"Expected page title '{expectedPageTitle}' not found.", ex); // Throw a custom PageTitleNotFoundException with details
            }
            catch (Exception ex) // Catch block to handle any other unexpected exceptions
            {
                logger.Error(ex, "Unexpected error while getting the page title."); // Log unexpected exceptions
                throw; // Rethrow for further handling
            }
        }

        // Method to get the result paragraph text
        public string GetResultText(string tableClassName)
        {
            try // Try block to handle potential exceptions during text retrieval
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15)); // Use explicit wait to ensure the table is present
                IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(tableClassName))); // Wait for the table with the specified class name to be visible
                IWebElement textElement = driver.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(table)); // Locate the paragraph element
                return textElement.Text; // Return the text within the paragraph element
            }
            catch (WebDriverTimeoutException ex) // Catch block to handle WebDriverTimeoutException
            {
                logger.Error(ex, $"Timeout waiting for the table with class '{tableClassName}' to be visible."); // Log the exception and rethrow or return a default value
                throw new ElementNotVisibleException($"Table with class '{tableClassName}' not visible.", ex);  // Throw a custom ElementNotVisibleException with details
            }
            catch (NoSuchElementException ex) // Catch block to hanndle NoSuchElementException
            {
                logger.Error(ex, "Element not found."); // Log the exception
                throw new ElementNotFoundException("Expected element not found.", ex); // Throw a custom ElementNotFoundExeption
            }
            catch (Exception ex) // Catch block to handle any other unexpected exceptions
            {
                logger.Error(ex, "Unexpected error while getting the result text."); // Log unexpected exceptions
                throw; // Rethrow or return a default value
            }
        }
    }
}
