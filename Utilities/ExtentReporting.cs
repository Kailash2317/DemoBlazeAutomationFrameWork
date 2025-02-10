using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Demo_Project.Reports
{
    public class ExtentReporting
    {
        private static ExtentReports extentReports;
        public static ExtentTest extentTest;

        private static ExtentReports StartReporting()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"..\..\..\..\results\";
            if (extentReports == null)
            {
                Directory.CreateDirectory(path);
                extentReports = new ExtentReports();
                var htmlReporter = new ExtentHtmlReporter(path);
                extentReports.AttachReporter(htmlReporter);
            }
            return extentReports;
        }

        public static void CreateTest(string testName)
        {
            extentTest = StartReporting().CreateTest(testName);
        }

        public static void EndReporting()
        {
            StartReporting().Flush();
        }
        public static void LogInfo(string info)
        {
            extentTest.Info(info);
        }
        public static void LogPass(string info)
        {
            extentTest.Pass(info);
        }
        public static void LogFail(string info)
        {
            extentTest.Fail(info);
        }

        public static void LogScreenshot(string info, string image)
        {
            extentTest.Info(info, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }
    }
}
