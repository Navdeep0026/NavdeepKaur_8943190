using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

class InstagramAutomation
{
    static void Main(string[] args)
    {
        // Replace with your Instagram credentials
        string username = "nav_kauurr";
        string password = "Mind@123";
        string targetUser = "nav_kauurr";

        // Setting up the Chrome driver
        IWebDriver driver = new ChromeDriver();
        Thread.Sleep(2000);

        // Navigating to Instagram login page
        driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
        Thread.Sleep(5000);

        // Logging into Instagram
        IWebElement usernameField = driver.FindElement(By.Name("username"));
        IWebElement passwordField = driver.FindElement(By.Name("password"));
        IWebElement loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));

        usernameField.SendKeys(username);
        passwordField.SendKeys(password);
        loginButton.Click();

        // Waiting for the home page to load
        Thread.Sleep(5000);

        // Closing the "Save Your Login Info?" popup if it appears
        try
        {
            IWebElement notNowButton = driver.FindElement(By.XPath("//button[contains(text(), 'Not Now')]"));
            notNowButton.Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("No 'Not Now' button found.");
        }

        // Closing the "Turn on Notifications" popup if it appears
        try
        {
            IWebElement turnOffNotificationsButton = driver.FindElement(By.XPath("//button[contains(text(), 'Not Now')]"));
            turnOffNotificationsButton.Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("No 'Turn on Notifications' button found.");
        }

        // Navigating to the target user's profile
        driver.Navigate().GoToUrl($"https://www.instagram.com/{targetUser}/");
        Thread.Sleep(5000);

        // Follow or Unfollow the user
        try
        {
            IWebElement followButton = driver.FindElement(By.XPath("//button[contains(text(), 'Follow')]"));
            followButton.Click();
            Console.WriteLine("Followed the user.");
        }
        catch (NoSuchElementException)
        {
            try
            {
                IWebElement unfollowButton = driver.FindElement(By.XPath("//button[contains(text(), 'Following')]"));
                unfollowButton.Click();
                Thread.Sleep(2000);
                IWebElement confirmUnfollowButton = driver.FindElement(By.XPath("//button[contains(text(), 'Unfollow')]"));
                confirmUnfollowButton.Click();
                Console.WriteLine("Unfollowed the user.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No follow/unfollow button found.");
            }
        }

        // Liking the first post
        try
        {
            IWebElement firstPost = driver.FindElement(By.XPath("//article//a"));
            firstPost.Click();
            Thread.Sleep(2000);

            IWebElement likeButton = driver.FindElement(By.XPath("//span[@aria-label='Like']"));
            likeButton.Click();
            Console.WriteLine("Liked the first post.");
            Thread.Sleep(2000);

            IWebElement closePostButton = driver.FindElement(By.XPath("//button[contains(text(), 'Close')]"));
            closePostButton.Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("No post or like button found.");
        }

        // Verifying actions
        try
        {
            driver.Navigate().GoToUrl($"https://www.instagram.com/{username}/");
            Thread.Sleep(5000);

            // Verify profile loaded
            IWebElement profileName = driver.FindElement(By.XPath($"//h2[contains(text(), '{username}')]"));
            if (profileName.Displayed)
            {
                Console.WriteLine("Profile page loaded successfully.");
            }

            // Verify liked post
            driver.Navigate().GoToUrl($"https://www.instagram.com/{targetUser}/");
            Thread.Sleep(5000);

            IWebElement firstPost = driver.FindElement(By.XPath("//article//a"));
            firstPost.Click();
            Thread.Sleep(2000);

            IWebElement likedButton = driver.FindElement(By.XPath("//span[@aria-label='Unlike']"));
            if (likedButton.Displayed)
            {
                Console.WriteLine("Post was successfully liked.");
            }

            // Close the post
            IWebElement closePostButton = driver.FindElement(By.XPath("//button[contains(text(), 'Close')]"));
            closePostButton.Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Verification elements not found.");
        }

        // Closing the webdriver
        driver.Quit();
    }
}