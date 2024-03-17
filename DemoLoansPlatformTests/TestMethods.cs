using OpenQA.Selenium;
using System;

namespace DemoLoansPlatformTests
{
    // Class for common test methods in project
    public class TestMethods
    {
        // Method to check failed assertions
         public static string CatchFailedAssertion(IWebElement element, By locator)
        {
            string failedAssertion = null;
            string menuElementName = null;

            try
            {
                // Wait for the element to be active
                WaitUntil.WaitElementIsActive(BaseTest.driver, locator);

                // Create JavaScriptExecutor to interact with element in case it's non-visible
                IJavaScriptExecutor js = (IJavaScriptExecutor)BaseTest.driver;

                // Get element name to write into assertion in case it fails
                menuElementName = (string)js.ExecuteScript("return arguments[0].textContent;", element);

                // Click element
                js.ExecuteScript("arguments[0].click();", element);

                // Wait until the page loads completely
                WaitUntil.WaitToLoadPage(BaseTest.driver);

                // Check the correct opening of the page
                if (!BaseTest.driver.PageSource.Contains("Demo User300") || BaseTest.driver.PageSource.Contains("Error: 500"))
                {
                    // Save assertion with current element name if page don't opens correctly
                    failedAssertion = "\"" + menuElementName + "\"" + " element is not opening correctly.";
                }

                // Handle over exceptions
            }
            catch (NoSuchElementException)
            {
                failedAssertion = "\"" + menuElementName + "\"" + "element not found.";
            }
            catch (Exception ex)
            {
                failedAssertion = "An unexpected error occurred while openeing" + "\"" + menuElementName + "\"" + " element: " + ex.Message;
            }
            return failedAssertion;
        }
    }
}
