using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Warhammer.Tests.Smoke
{
    class SmokeTests
    {
        private IWebDriver _driver;
        private readonly TestSettings _settings = new TestSettings();
        private WebDriverWait _wait;
        [TestFixtureSetUp]
        public void SetUp()
        {
            Assert.IsTrue(_settings.LoadSettings(), "If we can't load the settings we're not going to get far...");
            _driver = new FirefoxDriver();
            _wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            _driver.Navigate().GoToUrl(_settings.UpdateUrl);

            _wait.Until(ExpectedConditions.ElementExists(By.Id("Email")));
            IWebElement email = _driver.FindElement(By.Id("Email"));
            IWebElement password = _driver.FindElement(By.Id("Password"));
            email.SendKeys(_settings.Username);
            password.SendKeys(_settings.Password);
            password.Submit();

            Assert.IsTrue(_driver.PageSource.Contains("Did"));

            _driver.Navigate().GoToUrl(_settings.BaseUrl);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void HitHomePage()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl);
            Assert.IsTrue(_driver.PageSource.Contains("Warhammer"));
        }

        [Test]
        public void HitSessions()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/Home/Sessions");
            Assert.IsTrue(_driver.PageSource.Contains("Sessions"));
        }

        [Test]
        public void HitGraveyard()
        {
            _driver.Navigate().GoToUrl(_settings.BaseUrl + "/Home/Graveyard");
            Assert.IsTrue(_driver.PageSource.Contains("The Graveyard"));
        }
    }
}
