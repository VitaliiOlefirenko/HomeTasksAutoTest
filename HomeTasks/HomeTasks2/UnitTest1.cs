using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HomeTasks
{
    [TestFixture]
    public static class TestCaseDataSource
    {
        public static IEnumerable<object[]> userCredentials()
        {
            yield return new object[] { "JohnDoe", "passwOrd" };
            yield return new object[] { "lilyaJY", "isNotMe" };
            yield return new object[] { "GoingTo", "BeAuto" };
        }
    }

    public class Test
    {
        private IWebDriver driver;
        
        [OneTimeSetUp]
        public void BeforeClass()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test, TestCaseSource(typeof(TestCaseDataSource), "userCredentials")]
        public void Test1(string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.XPath("//*[contains(text(),'Sign in')]")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id='SubmitLogin']/span")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(),'Invalid email address')]")));

            Assert.Multiple(()=>
            {
                Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(),'Invalid email address')]")).Displayed, "Text differs from expected!");
                Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(),'Invalid email address')]")).Displayed, "Text differs from expected!");
                Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(),'Invalid email address')]")).Displayed, "Text differs from expected!");
            });
        }


        [Test, TestCase("GoingTo", "BeAuto")]
        [TestCase("lilyaJY", "isNotMe")]
        [TestCase("GoingTo", "BeAuto")]
        public void Test2(string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.XPath("//*[contains(text(),'Sign in')]")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id='SubmitLogin']/span")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(),'Invalid email address')]")));

            Assert.Multiple(() =>
            {
                Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(),'Invalid email address')]")).Displayed, "Text differs from expected!");
                Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(),'Invalid email address')]")).Displayed, "Text differs from expected!");
                Assert.IsTrue(driver.FindElement(By.XPath("//*[contains(text(),'Invalid email address')]")).Displayed, "Text differs from expected!");
            });
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            driver.Quit();
        }
    }
}