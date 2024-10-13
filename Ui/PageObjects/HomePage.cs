using OpenQA.Selenium;

namespace UIAutomation.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly By solutionsMenu = By.LinkText("SOLUTIONS");
        private readonly By marketIntelligenceSubMenu = By.LinkText("Market Intelligence");

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        /// <summary>
        /// Clicks the solution menu on top navigation
        /// </summary>
        public void ClickSolutionsMenu()
        {
            driver.FindElement(solutionsMenu).Click();
        }

        /// <summary>
        /// Cliks on Market Intelligence sub-menu from Soluions menu
        /// </summary>
        /// <returns>Market Intelligence Page</returns>
        public MarketIntelligencePage ClickMarketIntelligenceSubMenu()
        {
            driver.FindElement(marketIntelligenceSubMenu).Click();
            return new MarketIntelligencePage(driver);
        }
    }
}
