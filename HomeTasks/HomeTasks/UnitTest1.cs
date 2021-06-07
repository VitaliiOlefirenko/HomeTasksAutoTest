using System;
using System.Reflection;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HomeTasks
{
    [TestFixture]
    public class Test
    {
        private IWebDriver driver;
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly By loginLink = By.CssSelector("#login2");
        private readonly By loginImput = By.Id("loginusername");
        private readonly By passwordImput = By.XPath("//*[@id='loginpassword']");
        private readonly By loginButton = By.CssSelector("[onclick='logIn()']");
        private readonly By validationUserName = By.XPath("//*[@id='nameofuser']");

        private const string user = "VitaliiOlefirenko";
        private const string password = "123QAz";
        private const string expectedLogin = "Welcome " + user;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
          
        }

        [Test]
        public void Test1()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(loginLink));

            var logIn = driver.FindElement(loginLink);
            logIn.Click();

            var userNameField = driver.FindElement(loginImput);
            userNameField.SendKeys(user);
            var userPasswordField = driver.FindElement(passwordImput);
            userPasswordField.SendKeys(password);

            var formLoginButton = driver.FindElement(loginButton);
            formLoginButton.Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(validationUserName));

            var actualLogin = driver.FindElement(validationUserName).Text;
            Assert.AreEqual(expectedLogin, actualLogin, "loginLink unsuccessful");
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            driver.Quit();
        }
    }
}