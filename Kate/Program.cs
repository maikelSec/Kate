using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace Kate
{
    class Program
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        /// <summary>
        /// Where all the magic happens !
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {


            // Print out of Welcome screen

            WelcomeScreen();
           

            // This will great the user in  the default voice
           
            Speak("Welcome to kate version one point o",VoiceGender.Female);

            #region My Performance Counters
            // This will pull the current CPU load in percentages
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information","% Processor Time","_Total");
            perfCpuCount.NextValue();

            // this will pull the current availale memory in Megabytes
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfMemCount.NextValue();

            // this will pull the system up time in secodns
            PerformanceCounter perfUpTimeCount = new PerformanceCounter("System", "System Up Time");
            perfUpTimeCount.NextValue();

            #endregion

            TimeSpan UpTimeSpan = TimeSpan.FromSeconds(perfUpTimeCount.NextValue());
            string systemUptimeMessage =
                string.Format(
                    "The current system uptime is {0} days {1} hours {2} minutes {3} seconds",
                    (int)UpTimeSpan.TotalDays,
                    (int)UpTimeSpan.Hours,
                    (int)UpTimeSpan.Minutes,
                    (int)UpTimeSpan.Seconds);

            Speak(systemUptimeMessage, VoiceGender.Male);
            bool isChromeOpenedAlready = false;

            //infinite while loop
            while (true)
            {

                // Get the current values for the performance counters
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
               int currentAvailableMemmory = (int)perfMemCount.NextValue();

                
                // print every 
                Console.WriteLine(" Cpu load : {0}% ", currentCpuPercentage);
                Console.WriteLine(" Mem load : {0}MB ", currentAvailableMemmory);
                //Console.WriteLine("Up Time : {0}MB ", perfUpTimeCount.NextValue());


                // only tell cpu above 80 percent
                if (currentCpuPercentage > 80)
                {
                    if (currentCpuPercentage == 100)
                    {
                        string CpuloadVocalMessage = string.Format("Warning Cpu is about to catch fire !");

                        if (isChromeOpenedAlready == false)
                        {
                            OpenWebsite("https://www.youtube.com/watch?v=dQw4w9WgXcQ&list=RDdQw4w9WgXcQ#t=0");
                            isChromeOpenedAlready = true;
                        }

                        Speak(CpuloadVocalMessage,VoiceGender.Female);
                    }
                    else
                    {
                        string CpuloadVocalMessage = string.Format("The current Cpu Load is {0} percent", currentCpuPercentage);
                        Speak(CpuloadVocalMessage, VoiceGender.Male);
                        
                    }
                   
                }

                //below 1 gig
                if (currentAvailableMemmory < 1024)
                {
                   string memAvailableVocalMessage = string.Format(
                 " You currently have {0} megabytes of memory available",
                 currentAvailableMemmory);

                    Speak(memAvailableVocalMessage, VoiceGender.Male);

                }
            
             
                Thread.Sleep(1000);
            } // end of loop

        }

        /// <summary>
        /// speaks with a selected voice
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voiceGender"></param>
        public static void Speak(String message, VoiceGender voiceGender)
        {
            synth.SelectVoiceByHints(voiceGender);
            synth.Speak(message);
        }

        /// <summary>
        /// Same but with speed
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voiceGender"></param>
        /// <param name="rate"></param>
        public static void Speak(String message, VoiceGender voiceGender, int rate)
        {
            synth.Rate = rate;
            Speak(message,voiceGender);

            
        }


        public static void OpenWebsite(string URL)
        {   Process p1 = new Process();
            p1.StartInfo.FileName = "chrome.exe";
            p1.StartInfo.Arguments = URL;
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            p1.Start();

        }

        public static void WelcomeScreen()
        {
            Console.WriteLine(" *******************************************************************");
            Console.WriteLine(" *                                                                 *");
            Console.WriteLine(" *                                                                 *");
            Console.WriteLine(" *                   Kate 1.0 by Maikel Ninaber                    *");
            Console.WriteLine(" *                                                                 *");
            Console.WriteLine(" *                                                                 *");
            Console.WriteLine(" *******************************************************************");

            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine(" The application will begin now");
            Console.WriteLine("");
            Thread.Sleep(3000);
        }

    }
}
