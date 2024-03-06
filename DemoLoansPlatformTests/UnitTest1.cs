using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;



namespace DemoLoansPlatformTests
{
    // Create additional class to run SetUp and TearDown methods once for all tests
    public class BaseTest
    {
        public static IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            // Launch ChromeDriver  
            driver = new ChromeDriver("C:\\Users\\Asus\\Downloads\\chromedriver_win32\\chromedriver.exe");

            // Maximize browser window
            driver.Manage().Window.Maximize();

            // Wait for window to maximize
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            // Go to the login page
            driver.Navigate().GoToUrl("https://demo.loansplatform.com/lms/");

         // Aquire login data from file to protect private data

            // Create StreamReader object
            StreamReader sr = new("C:\\Users\\Asus\\Desktop\\Lendstream_login_data.txt");

            // Read separate lines from file and create variables "userName" and "password" to login
            string userName = sr.ReadLine();
            string password = sr.ReadLine();

            // Input username
            driver.FindElement(By.XPath("//input[@name='Username']")).SendKeys(userName);

            // Input password
            driver.FindElement(By.Id("Password")).SendKeys(password);

            // Click "Login" button
            driver.FindElement(By.Id("submit_button")).Click();

            // Check current user is logged
            Assert.AreEqual("Demo User300", driver.FindElement(By.Id("user_profile")).Text);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
//            driver.Quit();
        }
    }

    public class TestMethods: BaseTest
    {
        public static void FillActionsForm()
        {
            driver.FindElement(By.XPath("//div[contains(text(), 'Leads, New: Contacted')]")).Click();
            driver.FindElement(By.XPath("//span[contains(text(), 'Request rejected')]")).Click();

            driver.FindElement(By.XPath("//div[contains(text(), 'User020')]")).Click();
            driver.FindElement(By.Id("bs-select-5-2")).Click();

            driver.FindElement(By.XPath("//div[contains(text(), 'Low')]")).Click();
            driver.FindElement(By.XPath("//div[contains(text(), 'High")).Click();

            driver.FindElement(By.Id("DeadlineDate")).SendKeys("10102024");

            driver.FindElement(By.Id("notes")).SendKeys("Smoke test");
        }
    }
    public class Tests : TestMethods
    {
        [Test]
        public void DashboardWindowsOpeningTest()
        {
            // Open "Dashboard" section
            driver.FindElement(By.XPath("//*[@id=\"appbody\"]/div[1]/ul/li[1]/a/i")).Click();
            
            // Create a List of WebElements from "Loan Origination" section windows
            IList<IWebElement> loanOriginationList = driver.FindElements(By.XPath("//div[@title='Title']"));

            Assert.Multiple(() =>
            {

                // Iterate through the WebElements List of "Loan Origination" section windows
                foreach (IWebElement window in loanOriginationList)
                {
                    // Wait for the element to be clickable
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.ElementToBeClickable(window));

                    // Open each element
                    window.Click();

                    // Check element opens without "ERROR: 500"
                    Assert.IsFalse(driver.PageSource.Contains("ERROR: 500"), "'Loan Origination' section element is not opening correctly. Internal Server Error detected.");

                    // Return to the "Loan Origination" section
                    driver.Navigate().Back();
                }

                // Switch to Loan Servicing section
                driver.FindElement(By.XPath("//a[contains(text(),'Loan Servicing')]")).Click();

                // Create a List of WebElements from "Loan Servicing" section windows
                IList<IWebElement> loanServisingList = driver.FindElements(By.XPath("//div[@title='Title']"));

                // Iterate through the WebElements List of "Loan Origination" section windows
                foreach (IWebElement window in loanServisingList)
                {
                    // Wait for the element to be clickable
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.ElementToBeClickable(window));

                    // Open each element
                    window.Click();

                    // Check element opens without "ERROR: 500"
                    Assert.IsFalse(driver.PageSource.Contains("ERROR: 500"), "'Loan Servicing' section element is not opening correctly. Internal Server Error detected.");

                    // Return to the "Loan Servicing" section
                    driver.Navigate().Back();
                }
            });

            Assert.Pass();
        }

        [Test]
        public void SideBarMenuElementsOpeningTest()
        {
        // Check the sidebear menu items are openeing

            // Iterating through sidebar menu clicking each item 
            for (int i =1; i < 13; i++)
            {
                IWebElement element = driver.FindElement(By.XPath("//*[@id='appbody']/div[1]/ul/li[" + i + "]/a/i"));
                
                // Implementing JavaScriptExecuter to click non visible menu elements                
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);

                // Cheking the correct opening of the page
                Assert.IsFalse(driver.PageSource.Contains("ERROR: 500"), "Internal Server Error detected while openeing SideBar menu item");
            }

        // Check sidebar menu toggle/collapse function

            // Locate the sidebar menu element
            IWebElement texsElementDashboard = driver.FindElement(By.XPath("//span[contains(text(), 'Dashboard')]"));

            // Locate the sidebar toggle/collapse menu element
            IWebElement sideBarToggleCollapseButton = driver.FindElement(By.ClassName("slider"));

            // Click on the sidebar sideBarToggleCollapseButton to toggle menu
            sideBarToggleCollapseButton.Click();

            // Check if the sidebar menu is expanded by verifying visibility of "Dashboard" word in menu
            bool isExpanded = texsElementDashboard.Displayed;

            // Use multiple assertions block to prevent test stop after assertion failure
            Assert.Multiple(() =>
            {
                // Assertion to check if the sidebar menu is expanded
                Assert.IsTrue(isExpanded, "Sidebar menu should be expanded after clicking");

                // Click on the sidebar sideBarToggleCollapseButton to collapse menu
                sideBarToggleCollapseButton.Click();

                // Wait for the sidebar menu to become collapsed
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[contains(text(), 'Dashboard')]")));

                // Check if the sidebar menu is collapsed
                bool isCollapsed = !texsElementDashboard.Displayed;

                // Assertion to check if the sidebar menu is collapsed
                Assert.IsTrue(isCollapsed, "Sidebar menu should be collapsed after clicking");

            });
        }

        [Test]
        public void LoanApplicationTest()
        {
            // Applications, Submitted, New" tab
            driver.FindElement(By.XPath("//*[@class='content']/div/div[3]")).Click();

            // Checking Loans filters function
            driver.FindElement(By.XPath("//button[@title='Loan Origination']")).Click();
            driver.FindElement(By.XPath("//button[@title='All Users']")).Click();
            driver.FindElement(By.XPath("//span[text()='All Users']")).Click();
            driver.FindElement(By.XPath("//button[@title='Applications, Submitted']")).Click();
            driver.FindElement(By.XPath("//span[text()='Applications, Submitted']")).Click();

            // Checking functioning of "Actions" butons for the separate loan          
            driver.FindElement(By.XPath("//a[contains(text(),'Other')]")).Click();

            // Implementing JavaScriptExecuter to click overlapped "close" button
            IWebElement closeButton = driver.FindElement(By.XPath("//button[@class='close']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", closeButton);

            driver.FindElement(By.XPath("//a[contains(text(),'App Ready')]")).Click();
            IWebElement closeButton1 = driver.FindElement(By.XPath("//button[@class='close']"));
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", closeButton1);

            driver.FindElement(By.XPath(" //div[contains(text(),'Correction Needed: Return for correction')]")).Click();
            IWebElement closeButton2 = driver.FindElement(By.XPath("//button[@class='close']"));
            IJavaScriptExecutor js2 = (IJavaScriptExecutor)driver;
            js2.ExecuteScript("arguments[0].click();", closeButton2);

            driver.FindElement(By.XPath("//a[@title='Reassign...']")).Click();
            IWebElement closeButton3 = driver.FindElement(By.XPath("//button[@class='close']"));
            IJavaScriptExecutor js3 = (IJavaScriptExecutor)driver;
            js3.ExecuteScript("arguments[0].click();", closeButton3);

            driver.FindElement(By.XPath("//a[@title='Actions...']")).Click();
            IWebElement closeButton4 = driver.FindElement(By.XPath("//button[@class='close']"));
            IJavaScriptExecutor js4 = (IJavaScriptExecutor)driver;
            js4.ExecuteScript("arguments[0].click();", closeButton4);

            //FillActionsForm();
            //driver.FindElement(By.XPath("//button[contains(text(),'Submit')]")).Click();
            
            //driver.FindElement(By.Id("submitBtn")).Click();
        }

    }
}