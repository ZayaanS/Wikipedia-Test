using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Wikipedia_Testing.Page_Objects;
using NLog;

// Namespace for the test project
namespace Wikipedia_Testing.Tests
{
    // TestFixture attribute indicates that this class contains NUnit tests
    [TestFixture]
    public class SearchWikipediaForAutomationTesting_Test : IDisposable
    {
        // ChromeDriver instance used to control the browser
        private ChromeDriver driver;
        private WikipediaHomePage homePage;
        private WikipediaSearchResultsPage searchResultsPage;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        // SetUp method to run before each test
        [SetUp]
        public void Setup()
        {
            try
            {
                // Initialize the ChromeDriver instance before each test runs
                driver = new ChromeDriver();
                // Set an implicit wait globally for all elements
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                // Initilize page objects
                homePage = new WikipediaHomePage(driver);
                searchResultsPage = new WikipediaSearchResultsPage(driver);
            }
            catch (WebDriverException ex)
            {
                Assert.Fail($"Failed to initialize WebDriver: {ex.Message}");
            }
        }

        // TearDown method to run after each test
        [TearDown]
        public void TearDown()
        {
            // Ensure resources are properly disposed of after each test
            Dispose();
        }

        // Implement IDisposable function to properly clean up resources
        public void Dispose()
        {
       
            try
            {
                // Quit the ChromeDriver session and close the browser
                driver?.Quit();
            }
            catch (WebDriverException ex)
            {

                logger.Error(ex, "Error during WebDriver cleanup.");
            }
            finally
            {
                // Dispose of the ChromeDriver instance to release unmanaged resources
                driver?.Dispose();
            }
        }

        // Test method is Parameterized test using TestCase attribute
        // (Less complex for this example than using something like TestCaseData in a separate method)
        [TestCase("Automation testing", "Test automation - Wikipedia", "box-More_footnotes_needed", "Test automation can automate some repetitive but necessary tasks in a formalized testing process")]
        public void SearchWikipediaForAutomationTesting(string searchTerm, string expectedTitle, string tableClassName, string expectedText)
        {
            try
            {
                // Navigate to Wikipedia's homepage using the Page Object
                homePage.NavigateToHomePage();
                // Perform search using the Page Object
                homePage.Search(searchTerm);

                // Wait for the page to load and display the search results
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                // Retrieve the actual title of the web page
                var actualTitle = searchResultsPage.GetPageTitle(expectedTitle);
                // Assert that the actual title matches the expected title 
                Assert.That(actualTitle, Is.EqualTo(expectedTitle), $"The page title does NOT match the expected title. Expected: '{expectedTitle}', but found: '{actualTitle}'.");

                // Get the text inside the praragraph element
                var resultText = searchResultsPage.GetResultText(tableClassName);
                // Assert that the text displayed contains the expected text 
                Assert.That(resultText, Does.Contain(expectedText), $"The text '{expectedText}' was NOT found in the actual text.");
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Element not found: {ex.Message}");
            }
            catch (WebDriverTimeoutException ex)
            {
                Assert.Fail($"Timeout occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
