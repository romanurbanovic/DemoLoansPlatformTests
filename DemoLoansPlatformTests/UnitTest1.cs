using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace DemoLoansPlatformTests
{
    public class Tests
    {

        private IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver("C:\\Users\\Asus\\Downloads\\chromedriver_win32\\chromedriver.exe");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Navigate().GoToUrl("https://demo.loansplatform.com/lms/dashboard");
        }
         

        [Test]
        public void SmokeTest()
        {

///         Loging to the application
///         
            StreamReader srUserName = new("C:\\Users\\Asus\\Desktop\\Lendstream_userName.txt");
            String userName = srUserName.ReadLine();

            StreamReader srPassword = new("C:\\Users\\Asus\\Desktop\\Lendstream_password.txt");
            String password = srPassword.ReadLine();


            driver.FindElement(By.XPath("//input[@name='Username']")).SendKeys(userName);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("submit_button")).Click();

///         Creating List of Loan Origination windows
            IList<IWebElement> loanOriginationList = driver.FindElements(By.XPath("//div[@title='Title']"));

///         Iterating through the Loan Origination List and clicking each window
            foreach (IWebElement window in loanOriginationList)
            {
                Thread.Sleep(500);
                window.Click();
                driver.Navigate().Back();
            }

///         Switching to Loan Origination tab
            driver.FindElement(By.XPath("//a[contains(text(),'Loan Servicing')]")).Click();

///         Creating List of Loan Servicing windows
            IList<IWebElement> loanServisingList = driver.FindElements(By.XPath("//div[@title='Title']"));

///         Iterating through the Loan Origination List and clicking each window
            foreach (IWebElement window2 in loanServisingList)
            {
                Thread.Sleep(500);
                window2.Click();
                driver.Navigate().Back();
            }

            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}