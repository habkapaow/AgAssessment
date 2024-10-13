using NUnit.Framework;
using OpenQA.Selenium;
using UIAutomation.PageObjects;
using AgData.Utilities;
using UIAutomation.Utilities;

namespace UIAutomation.Tests
{
    public class UITests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            string baseUrl = BaseUtils.GetParameter("baseUrlUi");

            // Initialize the driver with the URL from the .runsettings file
            driver = WebDriverUtils.InitializeDriver(baseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            WebDriverUtils.QuitDriver(driver);
        }

        [Test]
        public void ValidateMarketIntelligenceFlow()
        {
            // Pass the driver to HomePage object
            HomePage homePage = new HomePage(driver);

            // Navigate to Market Intelligence page
            homePage.ClickSolutionsMenu();
            MarketIntelligencePage marketPage = homePage.ClickMarketIntelligenceSubMenu();

            // Get "Ways You Benefit" headings and assert each one is visible
            marketPage.VerifyWaysYouBenefitHeadings();

            // Click "Let's Get Started" button
            ContactPage contactPage = marketPage.ClickLetsGetStartedButton();

            // Validate Contact Page is loaded
            contactPage.IsContactPageLoaded();
        }
    }
}
