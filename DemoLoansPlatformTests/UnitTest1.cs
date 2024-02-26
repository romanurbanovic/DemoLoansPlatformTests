using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace DemoLoansPlatformTests
{
    // Creating additional class to run SetUp and TearDown methods once for all tests
    public class BaseTest
    {
        public static IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            // Launching ChromeDriver and loadin Login page  
            driver = new ChromeDriver("C:\\Users\\Asus\\Downloads\\chromedriver_win32\\chromedriver.exe");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Navigate().GoToUrl("https://demo.loansplatform.com/lms/dashboard");

            // Aquiring login data from file         
            StreamReader sr = new("C:\\Users\\Asus\\Desktop\\Lendstream_login_data.txt");
            String userName = sr.ReadLine();
            String password = sr.ReadLine();

            // Loging to the application
            driver.FindElement(By.XPath("//input[@name='Username']")).SendKeys(userName);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("submit_button")).Click();
        }

        [OneTimeTearDown]
        public void TearDown()
        {

        }
    }
    public class Tests : BaseTest
    {

        [Test]
        public void SmokeTest()
        {

            driver.FindElement(By.XPath("//*[@id=\"appbody\"]/div[1]/ul/li[1]/a/i")).Click();
            
            // Creating List of Loan Origination windows
            IList<IWebElement> loanOriginationList = driver.FindElements(By.XPath("//div[@title='Title']"));

            // Iterating through the Loan Origination List and clicking each window
            foreach (IWebElement window in loanOriginationList)
            {
                Thread.Sleep(500);
                window.Click();
                Assert.IsFalse(driver.PageSource.Contains("500 Internal Server Error"), "Page is not opening correctly. Internal Server Error detected.");
                driver.Navigate().Back();
            }

            // Switching to Loan Origination tab
            driver.FindElement(By.XPath("//a[contains(text(),'Loan Servicing')]")).Click();

            // Creating List of Loan Servicing windows
            IList<IWebElement> loanServisingList = driver.FindElements(By.XPath("//div[@title='Title']"));

            // Iterating through the Loan Origination List and clicking each window
            foreach (IWebElement window2 in loanServisingList)
            {
                Thread.Sleep(500);
                window2.Click();
                Assert.IsFalse(driver.PageSource.Contains("500 Internal Server Error"), "Page is not opening correctly. Internal Server Error detected.");
                driver.Navigate().Back();
            }

            Assert.Pass();
        }

        [Test]
        public void SideBarTest()
        {

            // Iterating through SideBar menu items
            for (int i =1; i < 13; i++)
            {
                Thread.Sleep(500);
                IWebElement element = driver.FindElement(By.XPath("//*[@id=\"appbody\"]/div[1]/ul/li[" + i + "]/a/i"));
                
                // Implementing JavaScriptExecuter to click non visible menu elements                
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);
                Assert.IsFalse(driver.PageSource.Contains("500 Internal Server Error"), "Page is not opening correctly. Internal Server Error detected.");
            }
        }


    }
}