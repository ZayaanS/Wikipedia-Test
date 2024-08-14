using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Wikipedia_Testing.Page_Objects;

// Namespace for the test project
namespace Wikipedia_Testing.Tests
{
    // TestFixture attribute indicates that this class contains NUnit tests
    [TestFixture]
    public class SearchWikipediaForAutomationTesting_Test : IDisposable
    {
        private ChromeDriver driver; // ChromeDriver instance used to control the browser
        private WikipediaHomePage homePage; // Instance of WikipediaHomePage class for interacting with the Wikipedia homepage
        private WikipediaSearchResultsPage searchResultsPage; // Instance of WikipediaSearchResultsPage class for interacting with the search results page
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger(); // Logger instance for logging messages

        // SetUp method to run before each test
        [SetUp]
        public void Setup()
        {
            try // Try block to handle potential exceptions during setup
            {
                driver = new ChromeDriver(); // Initialize the ChromeDriver instance before each test runs
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Set an implicit wait globally for all elements
                homePage = new WikipediaHomePage(driver); // Initilize page objects with the ChromeDriver instance
                searchResultsPage = new WikipediaSearchResultsPage(driver); // Initilize page objects with the ChromeDriver instance
            }
            catch (WebDriverException ex) // Catch block to handle WebDriverException during setup
            { 
                Assert.Fail($"Failed to initialize WebDriver: {ex.Message}"); // Log the exception and fail the test using Assert.Fail
            }
        }

        // TearDown method to run after each test
        [TearDown]
        public void TearDown()
        {
            Dispose(); // Ensure resources are properly disposed of after each test
        }

        // Implement IDisposable function to properly clean up resources
        public void Dispose()
        {
            try // Try block to handle potential exceptions during cleanup
            {
                driver?.Quit(); // Quit the ChromeDriver session and close the browser
            }
            catch (WebDriverException ex) // Catch block to handle WebDriverException during cleanup
            {
                logger.Error(ex, "Error during WebDriver cleanup.");  // Log the exception but don't necessarily fail the test
            }
            finally  // Finally block to ensure the ChromeDriver instance is disposed of even if exceptions occur
            {
                driver?.Dispose(); // Dispose of the ChromeDriver instance to release unmanaged resources
            }
        }

        // Test method is Parameterized test using TestCase attribute
        // (Less complex for this example than using something like TestCaseData in a separate method)
        [TestCase("Automation testing", "Test automation - Wikipedia", "box-More_footnotes_needed", "Test automation can automate some repetitive but necessary tasks in a formalized testing process")]
        public void SearchWikipediaForAutomationTesting(string searchTerm, string expectedTitle, string tableClassName, string expectedText)
        {
            try // Try block to handle potential exceptions during the test execution
            {              
                homePage.NavigateToHomePage(); // Navigate to Wikipedia's homepage using the Page Object
                homePage.Search(searchTerm); // Perform search using the Page Object
                
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); // Wait for the page to load and display the search results              
                var actualTitle = searchResultsPage.GetPageTitle(expectedTitle);  // Retrieve the actual title of the web page       
                Assert.That(actualTitle, Is.EqualTo(expectedTitle), $"The page title does NOT match the expected title. Expected: '{expectedTitle}', but found: '{actualTitle}'.");  // Assert that the actual title matches the expected title 

                var resultText = searchResultsPage.GetResultText(tableClassName);  // Get the text inside the praragraph element
                Assert.That(resultText, Does.Contain(expectedText), $"The text '{expectedText}' was NOT found in the actual text."); // Assert that the text displayed contains the expected text 
            }
            catch (NoSuchElementException ex) // Catch block to handle `NoSuchElementException` exceptions
            {
                Assert.Fail($"Element not found: {ex.Message}"); // Fail the test with a descriptive message including the exception details
            }
            catch (WebDriverTimeoutException ex) // Catch block to handle `WebDriverTimeoutException` exceptions
            {
                Assert.Fail($"Timeout occurred: {ex.Message}"); // Fail the test with a descriptive message including the exception details
            }
            catch (Exception ex) // Catch block to handle any other unexpected exceptions
            {
                Assert.Fail($"An unexpected error occurred: {ex.Message}"); // Fail the test with a descriptive message including the exception details
            }
        }
    }
}