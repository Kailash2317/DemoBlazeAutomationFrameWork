using Newtonsoft.Json;


namespace Demo_Project.Utilities
{
    public class TestData
    {
        public BrowserType browser { get; set; }
        public WebsiteUrl websiteUrl { get; set; }
        public LoginData login { get; set; }
        public SignupData signup { get; set; }
        public CheckoutData checkoutData { get; set; }

        public class WebsiteUrl
        {
            public string url { get; set; }
        }
        public class BrowserType
        {
            public string chrome { get; set; }
            public string edge { get; set; }
            public string firefox { get; set; }
        }
        public class LoginData
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class SignupData
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class CheckoutData
        {
            public string name { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string creditCard { get; set; }
            public string month { get; set; }
            public string year { get; set; }
        }

        // Method to load the JSON data from the file
        public static TestData LoadData(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<TestData>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from {filePath}: {ex.Message}");
                return null;
            }
        }
    }
}