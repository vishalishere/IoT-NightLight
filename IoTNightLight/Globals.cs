﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTNightLight
{
    // TODO: Probably don't need this anymore
    public class currentPage
    {
        public static currentPage _mainPage  = new currentPage();
        public static currentPage _lightPage = new currentPage();
        public static currentPage _tempPage  = new currentPage();
        public static currentPage _logPage   = new currentPage();
    }

    public static class Globals
    {
        private static Frame     rootFrame;
        private static MainPage  mainPage;
        private static LightPage lightPage;
        private static TempPage  tempPage;
        private static LogPage   logPage;

 
        //private static Frame tempPage = new Frame();

        private static currentPage _currentPage;

        static Globals()
        {
            // Grab window, then cast to specific page as necessary
            rootFrame = Window.Current.Content as Frame;
            mainPage  = (MainPage )rootFrame.Content;

            //lightPage = (LightPage)rootFrame.Content;
            //if (tempPage == null) {
            //    tempPage = (tempPage = new TempPage());
            //}
            //logPage   = (LogPage  )rootFrame.Content;

            _currentPage = new currentPage();        
        }


        /// <summary>
        /// TODO: have this return a different value based on the string. Just demo right now
        /// </summary>
        /// <param name="msg"></param>
        private static string stringValFromMsg(string msg)
        {
            string[] separators = new string[] { ",", ".", "!", "\'", " ", "\'s" };
            string text         = msg;
            string newMsg       = "";

            foreach (string word in text.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                Debug.WriteLine(word);

                if (word.Contains("temp"))
                {
                    return newMsg;
                }
            }
            return newMsg;
        }


        /// <summary>
        /// Interpreets commands from console app and relays to the correct function
        /// </summary>
        /// <param name="msg">Command sent from console app</param>
        public static void parseMsg(string msg)
        {
            int    intInMsg        = GetIntVal(msg); //TODO: May not need this
            string convertToString = intInMsg.ToString();
            object convertToObj    = (Object)convertToString;

            switch (msg)
            {
                // ---------------------------------------- TEMPERATURE
                case "temp 10":
                    Debug.WriteLine("temp 10");
                    mainPage.Goto(10);
                    break;
                case "temp 30":
                    Debug.WriteLine("temp 30");
                    mainPage.Goto(30);
                    break;
                case "temp 70":
                    Debug.WriteLine("temp 70");
                    mainPage.Goto(70);
                    break;
                case "temp 100":
                    Debug.WriteLine("temp 100");
                    mainPage.Goto(100);
                    break;
                // ---------------------------------------- LIGHT
                case "light 10":
                    break;
                case "light 30":
                    break;
                case "light 70":
                    break;
                case "light 100":
                    break;
                // ---------------------------------------- NAVIGATION
                case "nav to log":
                    Debug.WriteLine("navigating to log page");
                    rootFrame.Navigate((typeof (LogPage)), null);
                    break;
                case "nav to main":
                    Debug.WriteLine("navigating to main page");
                    mainPage.ChangeTitleText("Main Page");
                    //rootFrame.Navigate((typeof(MainPage)), null);
                    break;
                case "nav to temp":
                    Debug.WriteLine("navigating to temp page");
                    mainPage.ChangeTitleText("Temperature");
                    //rootFrame.Navigate((typeof (TempPage)), null);
                    break;
                case "nav to light":
                    Debug.WriteLine("navigating to light page");
                    rootFrame.Navigate((typeof (LightPage)), null);
                    break;
            }
        }


        /// <summary>
        /// Parses int from msg str which is sent to IoT device for increase / decrease temp, etc.
        /// </summary>
        /// <param name="msg">What do you want to the IoT device to do?</param>
        /// <returns>Integer used to change values in IoT device</returns>
        private static int GetIntVal(string msg)
        {
            int intInMsg     = 0;
            string[] numbers = Regex.Split(msg, @"\D");

            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    intInMsg = i;
                }
            }
            return intInMsg;
        }




        /* NAVIGATION
         * ==========================================================*/

        public static void Nav_To_Temp(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate((typeof (TempPage)), null);
            Debug.WriteLine("Navigating to a new page");
        }

        public static void Nav_To_Main(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate((typeof (MainPage)), null);
            Debug.WriteLine("Navigating to a new page");
        }

        public static void Nav_To_Light(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate((typeof (LightPage)), null);
            Debug.WriteLine("Navigating to a new page");
        }

        public static void Nav_To_Log(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate((typeof (LogPage)), null);
            Debug.WriteLine("Navigating to a new page");
        }

    }
}
