﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicecontrolhub.logger
{
    internal class Logger : ILogger
    {
        #region const        
        string logFolder = "logs";
        #endregion

        #region vars        
        Queue<LogMessage> logMessages = new Queue<LogMessage>();
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer clearTimer = new System.Timers.Timer();
        string filePath;
        #endregion

        public Logger(string subdir, string filename)
        {
            var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), logFolder, subdir);
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            filePath = Path.Combine(fileDirectory, $"{filename}.log");

            if (File.Exists(filePath))
                File.Delete(filePath);

            timer.AutoReset = true;
            timer.Interval = 10 * 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            clearTimer.AutoReset = true;
            clearTimer.Interval = 48 * 60 * 60 * 1000;
            clearTimer.Elapsed += ClearTimer_Elapsed;
            clearTimer.Start();
        }

        #region private
        private void ClearTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

            }
            catch (Exception ex)
            {

            }
        }

        void appendLogFile()
        {
            try
            {

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    while (logMessages.Count > 0)
                    {
                        LogMessage message = logMessages.Dequeue();
                        if (message != null)
                            sw.WriteLine(message.ToString());
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            appendLogFile();
        }
        #endregion

        #region helpers
        void post(LogMessage message)
        {
            
            logMessages.Enqueue(message);
        }
        #endregion

        #region public
        public void dbg(string tag, string text)
        {
            var message = new LogMessage(LogMessageType.dbg, tag, text);
            post(message);
            Console.WriteLine(message.ToString());

        }

        public void err(string tag, string text)
        {
            var message = new LogMessage(LogMessageType.err, tag, text);
            post(message);
            Console.WriteLine(message.ToString());
        }

        public void inf(string tag, string text)
        {
            var message = new LogMessage(LogMessageType.inf, tag, text);
            post(message);
            Console.WriteLine(message.ToString());
        }

        public void inf_urgent(string tag, string text)
        {
            var message = new LogMessage(LogMessageType.inf_urgent, tag, text);
            post(message);
            Console.WriteLine(message.ToString());
        }
        #endregion
    }
}
