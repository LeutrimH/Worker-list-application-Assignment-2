using NUnit.Framework;

using WebApplication4.Models;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;

namespace Tests
{
    public class Tests
    {                          
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Chrome driver. Normally we want this path not hardcoded here
            // But we get better error messages like this.
            // Make sure you download the ChromeDriver compatible with your
            // currently installed Chrome and OS.
            // Website: https://chromedriver.chromium.org/downloads
            driver = new ChromeDriver("D:\\Projects\\ChromeDriver\\86.0.4240.22");
        }

        [TearDown]
        public void Cleanup()
        {
            // Closes the Chrome instance after all tests are done.
            driver.Close();
        }

        [Test]
        public void RemovePersonTest()
        {
            // Open the browser, navigate to URL, change to whatever URL your ISS is
            // running.
            driver.Navigate().GoToUrl("https://localhost:5001/");

            // Wait 10s or until the element with the id "#person_row_3" in our page
            // is loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("person_list")));

            // Find the Remove link, try to click it.            
            IWebElement person_row_3 = driver.FindElement(By.Id("person_row_3"));
            person_row_3.FindElement(By.TagName("a")).Click();

            // Wait 10s or until the element person_row_3 is removed from DOM. 
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try {
                    driver.FindElement(By.Id("person_row_3"));
                    return true;
                } catch (NoSuchElementException e) {
                    return true;
                }
            });

            // If no timeouts have happened so far, this test succeeds.
        }

        [Test]
        public void RemoveJobTest()
        {
            // Open the browser, navigate to URL, change to whatever URL your ISS is
            // running.
            driver.Navigate().GoToUrl("https://localhost:5001/Home/JobList");

            // Wait 10s or until the element with the id "#job_row_2" in our page
            // is loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("job_list")));

            // Find the Remove link, try to click it.            
            IWebElement job_row_2 = driver.FindElement(By.Id("job_row_2"));
            job_row_2.FindElement(By.TagName("a")).Click();

            // Wait 10s or until the element job_row_2 is removed from DOM. 
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try {
                    driver.FindElement(By.Id("job_row_2"));
                    return false;
                } catch (NoSuchElementException e) {
                    return true;
                }
            });

            // If no timeouts have happened so far, this test succeeds.
        }

        [Test]
        public void AddJobTest()
        {
            // Open the browser, navigate to URL, change to whatever URL your ISS is
            // running.
            driver.Navigate().GoToUrl("https://localhost:5001/Home/JobForm");

            // Wait 10s or until the element with the id "#job_row_2" in our page
            // is loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("job_form")));

            // Find the Elements we need, fill name then click submit.            
            IWebElement job_form_name = driver.FindElement(By.Id("job_form_name"));
            IWebElement job_form_submit = driver.FindElement(By.Id("job_form_submit"));
            job_form_name.SendKeys("Tosser");
            job_form_submit.Click();

            driver.Navigate().GoToUrl("https://localhost:5001/Home/JobList");

            // Wait 10s or until the new element is added. 
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try {
                    driver.FindElement(By.Id("job_row_3"));
                    return true;
                } catch (NoSuchElementException e) {
                    return false;
                }
                /* try {
                    IWebElement job_row_3 = driver.FindElement(By.Id("job_row_3"));
                    if(job_row_3.GetAttribute("Job").Equals("Tosser")) {
                        return true;
                    }
                    return false;
                } catch (NoSuchElementException e) {
                    return false;
                } */
            });

            // If no timeouts have happened so far, this test succeeds.
        }

        [Test]
        public void AddPersonTest()
        {
            // Open the browser, navigate to URL, change to whatever URL your ISS is
            // running.
            driver.Navigate().GoToUrl("https://localhost:5001/Home/PersonForm");

            // Wait 10s or until the element with the id "#job_row_2" in our page
            // is loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("person_form")));

            // Find the Elements we need, fill name then click submit.            
            IWebElement person_form_name = driver.FindElement(By.Id("person_form_name"));
            IWebElement person_form_salary = driver.FindElement(By.Id("person_form_salary"));
            IWebElement person_form_submit = driver.FindElement(By.Id("person_form_submit"));
            person_form_name.SendKeys("Joe");
            person_form_salary.SendKeys("100000");
            person_form_submit.Click();
            driver.Navigate().GoToUrl("https://localhost:5001");

            // Wait 10s or until the new element is added. 
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try {
                    driver.FindElement(By.Id("person_row_4"));
                    return true;
                } catch (NoSuchElementException e) {
                    return false;
                }
            });
            // If no timeouts have happened so far, this test succeeds.
        }
    }
}