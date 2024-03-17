using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;


namespace DemoLoansPlatformTests
{
    // Class for various waiting methods
    public static class WaitUntil
    {
        // Method to wait until an element becomes active
        public static void WaitElementIsActive(IWebDriver driver, By locator, int seconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(locator));
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        // Method to wait until an element becomes invisible
        public static void WaitElementIsInvisible(IWebDriver driver, By locatar, int seconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.InvisibilityOfElementLocated(locatar));
        }

        // Method to wait for a page to load completely
        public static void WaitToLoadPage(IWebDriver driver)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
