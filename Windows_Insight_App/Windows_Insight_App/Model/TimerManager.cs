using System;
using System.Windows.Threading;
using System.Windows;

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handle all timers and their actions.
    /// </summary>
    class TimerManager
    {
        // TODO : Find if PerformanceTimer is ever used to effect anywhere.
        public static DispatcherTimer PerformanceTimer;
        public static DispatcherTimer WHTimer;

        /// <summary>
        /// Create timers.
        /// </summary>
        public static void InitializeTimers()
        {
            try
            {
                WHTimer.Stop();
            }
            catch
            {
                WHTimer = new DispatcherTimer();
            }
            finally
            {
                WHTimer.Tick += WHTimer_Tick;
                WHTimer.Interval = new TimeSpan(0, 0, 0, 0, 250); // Every quarter of a second
            }
        }

        // May wish to use the performance timer to add performance data to a log every 5 seconds.
        /// <summary>
        /// Not used.
        /// </summary>
        public static void StartPerformanceTimer()
        {
            //PerformanceTimer = new DispatcherTimer();
            //PerformanceTimer.Tick += PerformanceTimer_Tick;
            //PerformanceTimer.Interval = new TimeSpan(0, 0, 5);
            //PerformanceTimer.Start();
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PerformanceTimer_Tick(object sender, EventArgs e) // JMC // need to pass in these labels
        {
            // Take a snapshot of perf data and add that snap to a list.

            // Depreciated
            #region PERFORMANCE_GETTING
            //try
            //{
            //    ViewModel.processPerfStatic = PerformanceManager.GetPerformance("PROC");

            //    ViewModel.cpuPerfStatic = PerformanceManager.GetPerformance("CPU");

            //    ViewModel.ramPerfStatic = PerformanceManager.GetPerformance("RAM");

            //    ViewModel.tramPerfStatic = PerformanceManager.GetPerformance("TRAM");
            //}
            //catch
            //{
            //    PerformanceTimer.Stop();
            //    PerformanceManager.ResetPerformanceData();
            //    System.Windows.Message Box.Show("The target process has stopped.");
            //    System.Console.WriteLine("Process has stopped.");
            //    try
            //    {
            //        WHTimer.Stop();
            //        //ViewModel.whContentStatic = "";
            //        //ViewModel.whCBCheckedStatic = false;
            //    }
            //    catch { }
            //}
            #endregion // PERFORMANCE_GETTING

        }

        /// <summary>
        /// Get and update the display of a process's width and height.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WHTimer_Tick(object sender, EventArgs e)
        {
            Rect myRect = new Rect(0, 0, 0, 0);
            try
            {
                myRect = ProcessManager.UnsafeRect();
            }
            catch
            {
                // TODO : Add better error handling here.
                Console.Write("// TODO : FIX ME");
            }

            ViewModel.whContentStatic = "W/H (" + myRect.Width + "x" + myRect.Height + ")";
        }
    }
}
