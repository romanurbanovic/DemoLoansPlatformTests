using OpenQA.Selenium;
using System.IO;


namespace DemoLoansPlatformTests.PageObjects
{
    class LoginPageOject
    {
        private IWebDriver driver;

        private readonly By _userNameInputTab = By.Id("eMail");
        private readonly By _passwordInputTab = By.Id("Password");
        private readonly By _submitButton = By.Id("submit_button");
        public static string expectedUserName = "Demo User300";
        public static readonly By logOutButton = By.Id("user_logout");

        public LoginPageOject(IWebDriver driver)
        {
           this.driver = driver;
        }

        public void SignIn()
        {
            // Aquire login data from file to protect private data

            // Create StreamReader object
            StreamReader sr = new(Helper.loginDataFileLocation);

            // Read separate lines from file and create variables "userName" and "password" to login
            string userName = sr.ReadLine();
            string password = sr.ReadLine();

            // Input username
            driver.FindElement(_userNameInputTab).SendKeys(userName);

            // Input password
            driver.FindElement(_passwordInputTab).SendKeys(password);

            // Click "Login" button
            driver.FindElement(_submitButton).Click();
        }

        public void LogOut()
        {
            // Click Menu button
            driver.FindElement(DashboardPageObject.loggedUserName).Click();

            // Click "Log out" button
            driver.FindElement(logOutButton).Click();
        }
    }
}
