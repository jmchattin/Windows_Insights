using System.Diagnostics; // Needed for PerformanceCounter

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handle the getting and setting performance variables.
    /// </summary>
    class PerformanceManager
    {
        public static bool okForPerf = false;

        /// <summary>
        /// Method called by the second app thread.
        /// Updates performance variables if the app is available.
        /// </summary>
        public static void PerformanceManagingThreadChild()
        {
            try
            {
                ViewModel.processPerfStatic = GetPerformance("PROC");
                ViewModel.cpuPerfStatic = GetPerformance("CPU");
                ViewModel.ramPerfStatic = GetPerformance("RAM");
                ViewModel.tramPerfStatic = GetPerformance("TRAM");
                ViewModel.netPerfStatic = GetPerformance("NET");
            }
            catch
            {
                // Stop the process thread from calculating performance metrics.
                okForPerf = false;
                ResetPerformanceData();
                ErrorManager.ManageError(ErrorManager.PossibleErrors.Process_Stopped);
                //System.Windows.Message Box.Show("The target process has stopped.");
            }
        }

        // TODO : Consider turning this into a call-once method with -
        // - a return of a string array (1,2,3,4,etc) that is then -
        // - accessed by the caller (no need to pass in values).
        /// <summary>
        /// Acquire an app's given resource value.
        /// </summary>
        /// <param name="param">What app parameter to look for.</param>
        /// <returns>The string value of the given parameter.</returns>
        public static string GetPerformance(string param)
        {
            string returnStr;

            switch (param)
            {
                case "PROC":
                    returnStr = ProcessManager.runningProc.MainWindowTitle;
                    break;
                case "CPU": // Process CPU
                    returnStr = ProcessManager.runningProc.UserProcessorTime.ToString();
                    break;
                case "RAM": // Process RAM
                    PerformanceCounter PC = new PerformanceCounter("Process",
                        "Working Set - Private", ProcessManager.runningProc.ProcessName);
                    returnStr = HelpManager.TurnKBIntoMB((PC.RawValue / 1024), 2).ToString() + " MB";
                    break;
                case "TRAM": // Total RAM
                    PerformanceCounter ram = new PerformanceCounter("Memory",
                        "Available MBytes");
                    returnStr = ram.NextValue() + " MB";
                    break;
                case "NET": // Total MB/s used in communicating outside of the app
                    #region NOTES
                    // https://msdn.microsoft.com/en-us/library/ms804621.aspx
                    //var readOpSec = new PerformanceCounter("Process", "IO Read Operations/sec", ProcessManager.runningProc.ProcessName).RawValue;
                    //var writeOpSec = new PerformanceCounter("Process", "IO Write Operations/sec", ProcessManager.runningProc.ProcessName).RawValue;
                    //var dataOpSec = new PerformanceCounter("Process", "IO Data Operations/sec", ProcessManager.runningProc.ProcessName).RawValue;
                    //var readBytesSec = new PerformanceCounter("Process", "IO Read Bytes/sec", ProcessManager.runningProc.ProcessName).RawValue;
                    //var writeByteSec = new PerformanceCounter("Process", "IO Write Bytes/sec", ProcessManager.runningProc.ProcessName).RawValue;
                    #endregion // NOTES
                    // Total bytes written and read from files, network, and other devices.
                    long dataBytesSec = new PerformanceCounter("Process", "IO Data Bytes/sec", ProcessManager.runningProc.ProcessName).RawValue;
                    returnStr = HelpManager.TurnByteIntoMB(dataBytesSec, 2).ToString() + " MB/s";
                    break;
                default:
                    returnStr = "Unable to get performance.";
                    break;
            }

            return returnStr;
        }

        /// <summary>
        /// Reset all performance variables for display.
        /// </summary>
        private static void ResetPerformanceData()
        {
            ViewModel.processPerfStatic = "";
            ViewModel.cpuPerfStatic = "";
            ViewModel.ramPerfStatic = "";
            ViewModel.tramPerfStatic = "";
            ViewModel.netPerfStatic = "";
        }

    }
}
