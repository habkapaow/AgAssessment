using OpenQA.Selenium;
using log4net;

namespace UIAutomation.PageObjects
{
    public class HomePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(HomePage));

        private readonly IWebDriver driver;
        private readonly By solutionsMenu = By.LinkText("SOLUTIONS");
        private readonly By marketIntelligenceSubMenu = By.LinkText("Market Intelligence");

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver;
            Logger.Info("HomePage initialized.");
        }

        /// <summary>
        /// Clicks the Solutions menu on top navigation.
        /// </summary>
        public void ClickSolutionsMenu()
        {
            Logger.Info("Clicking on the 'Solutions' menu.");
            driver.FindElement(solutionsMenu).Click();
            Logger.Info("'Solutions' menu clicked successfully.");
        }

        /// <summary>
        /// Clicks on the Market Intelligence sub-menu from the Solutions menu.
        /// </summary>
        /// <returns>Market Intelligence Page</returns>
        public MarketIntelligencePage ClickMarketIntelligenceSubMenu()
        {
            Logger.Info("Clicking on the 'Market Intelligence' sub-menu.");
            driver.FindElement(marketIntelligenceSubMenu).Click();
            Logger.Info("'Market Intelligence' sub-menu clicked successfully.");
            return new MarketIntelligencePage(driver);
        }
    }
}
