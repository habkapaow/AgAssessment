using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;

namespace UIAutomation.Utilities
{
    public class WebDriverUtils
    {
        public static IWebDriver InitializeDriver(string url)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = new ChromeOptions();

            // Add all the necessary Chrome options
            options.AddArgument("test-type");
            options.AddArgument("--js-flags=--expose-gc");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-default-apps");
            options.AddArgument("--enable-automation");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--log-level=3");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--ignore-urlfetcher-cert-requests");
            options.AddArgument("--allow-cross-origin-auth-prompt");
            //options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            IWebDriver driver = new ChromeDriver(options);
            //driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);

            return driver;
        }

        public static void QuitDriver(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
