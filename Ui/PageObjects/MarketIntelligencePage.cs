using OpenQA.Selenium;
using log4net;
using NUnit.Framework;
using System.Collections.Generic;

namespace UIAutomation.PageObjects
{
    public class MarketIntelligencePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MarketIntelligencePage));

        private readonly IWebDriver driver;
        private readonly By waysYouBenefitHeadings = By.CssSelector(".services_target_markets h3");
        private readonly By letsGetStartedButton = By.XPath("//*[@id=\"prefooter\"]/a[text()=\"Let's Get Started\"]");

        public MarketIntelligencePage(IWebDriver webDriver)
        {
            driver = webDriver;
            Logger.Info("MarketIntelligencePage initialized.");
        }

        /// <summary>
        /// Verifies headers displayed for Ways You Benefit section
        /// </summary>
        public void VerifyWaysYouBenefitHeadings()
        {
            Logger.Info("Verifying 'Ways You Benefit' section headings.");
            try
            {
                IList<IWebElement> headings = driver.FindElements(waysYouBenefitHeadings);
                Logger.Info($"Found {headings.Count} headings in 'Ways You Benefit' section.");

                // Assert that each heading is displayed
                foreach (IWebElement heading in headings)
                {
                    Assert.That(heading.Displayed, Is.True, $"The heading '{heading.Text}' is not visible.");
                    Logger.Info($"Heading '{heading.Text}' is displayed.");
                }
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to find 'Ways You Benefit' headings.", ex);
                throw;
            }
        }

        /// <summary>
        /// Click on Let's Get Started button
        /// </summary>
        /// <returns>Contact Page</returns>
        public ContactPage ClickLetsGetStartedButton()
        {
            Logger.Info("Clicking 'Let's Get Started' button.");
            try
            {
                driver.FindElement(letsGetStartedButton).Click();
                Logger.Info("'Let's Get Started' button clicked successfully.");
                return new ContactPage(driver);
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to find 'Let's Get Started' button.", ex);
                throw;
            }
        }
    }
}
