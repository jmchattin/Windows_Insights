using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

using System.Collections.ObjectModel; // Observable collection

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handle interactions, inquiries, and the control of a target process(es).
    /// </summary>
    class ProcessManager
    {

        #region VARIABLES
        public static Process runningProc;
        private static Process[] runningProcs;

        public static volatile bool keepThreadAlive = true;


        [DllImport("user32.dll")] // JMC
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [StructLayout(LayoutKind.Sequential)]
        public struct CustRect // JMC
        {
            public int Left, Top, Right, Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, // JMC
            out CustRect lpRect);
        #endregion // VARIABLES

        /// <summary>
        /// Initializes the data fields if a specific process can be found.
        /// </summary>
        /// <param name="procName">The process name.</param>
        /// <param name="procTitle">The process title.</param>
        public static void StartProcessManager(string procName, string procTitle)
        {
            //System.Console.WriteLine(
            //    "Working Area: "+System.Windows.SystemParameters.WorkArea);
            //System.Console.WriteLine(
            //    "Virtual Scrn: " + System.Windows.SystemParameters.VirtualScreenWidth);

            if (procName == "" || procTitle == "")
            {
                ErrorManager.ManageError(ErrorManager.PossibleErrors.Unassigned_Call, "StartProcessManager");
                return;
            }

            GetProcessByNameTitle(procName, procTitle);
        }

        /// <summary>)
        /// Get a known process for its handles and such.
        /// </summary>
        /// <param name="procName">The process name.</param>
        /// <param name="procTitle">The process title.</param>
        /// <returns>The sought-for process, if currently running.</returns>
        public static void GetProcessByNameTitle(string procName, string procTitle)
        {
            runningProc = null;

            Process[] procs = Process.GetProcessesByName(procName);

            // Check if at least one process was found.
            try
            {
                procs[0].ToString();
            }
            catch
            {
                ErrorManager.ManageError(ErrorManager.PossibleErrors.No_Process_Found_w_Title, procTitle);
                return;
            }

            // Get the first matching process - useful for cycling through browser tabs.
            foreach (Process proc in procs)
            {
                if (proc.MainWindowTitle.ToLower().Contains(procTitle.ToLower()))
                {
                    runningProc = proc;
                    return;
                }
            }

            // Why would this be hit?
            if (runningProc == null)
                ErrorManager.ManageError(ErrorManager.PossibleErrors.Unknown);
            //System.Console.WriteLine("No app " + procTitle);
        }

        /// <summary>
        /// Get a process by its known ID.
        /// </summary>
        /// <param name="procId">Process ID.</param>
        public static void GetProcessById(int procId)
        {
            runningProc = null;

            Process proc;

            proc = Process.GetProcessById(procId);

            try
            {
                proc.ToString();
                runningProc = proc;
                return;
            }
            catch
            {
                ErrorManager.ManageError(ErrorManager.PossibleErrors.No_Process_Found);
                return;
            }
        }

        #region WRITE_PROCS_TO_FILE
        // No-longer in use.
        public static void WriteOutProcesses()
        {
            int index = 1;

            string name, title;
            StringBuilder sb = new StringBuilder();

            foreach (Process proc in runningProcs)
            {
                title = proc.MainWindowTitle;
                name = proc.ProcessName;

                sb.Append("#" + index + " : " + proc +
                    "\r\nName: " + name +
                    "\r\nTitle: " + title + "\r\n-\r\n");
                index++;
            }

            WriteToProcessFile(sb.ToString());
        }

        // No-longer in use.
        private static void WriteToProcessFile(string text) // JMC
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"C:/Users/" + Environment.UserName + "/Documents/Running Processes.txt");
            string tempPath = sb.ToString();

            File.Delete(tempPath);

            FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);

            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Time {0}", DateTime.Now.ToString());
                writer.Write(text);
                writer.Close();
            }

            Process.Start("notepad.exe", tempPath);
        }
        #endregion // WRITE_PROCS_TO_FILE

        /// <summary>
        /// Get all the appropriate processes running on the current machine.
        /// </summary>
        public static void GetAllProcesses()
        {
            Process[] procs;

            procs = Process.GetProcesses();

            // Check if at least one process is available.
            try
            {
                procs[0].ToString();
            }
            catch
            {
                ErrorManager.ManageError(ErrorManager.PossibleErrors.No_Processes_Running);
                //Message Box.Show("No processes running.");
                return;
            }

            List<Process> tempProcs = new List<Process>();

            foreach (Process proc in procs)
            {
                // Remove background processes (or include them if the user has indicated).
                if (proc.MainWindowTitle != "" || ViewModel.showAllCBCheckedStatic)
                {
                    tempProcs.Add(proc);
                }
            }

            runningProcs = tempProcs.ToArray();

            ViewModel.trimmedProcessesStatic = GetAllProcessesCollection();
        }

        /// <summary>
        /// Get a collection of the running processes, only including process Index, Name, Title, and ID.
        /// </summary>
        /// <returns></returns>
        private static ObservableCollection<TrimmedProcess> GetAllProcessesCollection()
        {
            ObservableCollection<TrimmedProcess> tempProcs = new ObservableCollection<TrimmedProcess>();
            TrimmedProcess tempProc;
            int index = 1;
            foreach (Process proc in runningProcs)
            {
                tempProc = new TrimmedProcess(index, proc.ProcessName, proc.MainWindowTitle, proc.Id);
                tempProcs.Add(tempProc);
                index++;
            }
            return tempProcs;
        }

        /// <summary>
        /// Halts internal timers.
        /// Updates the user that the monitored process has stopped.
        /// </summary>
        private static void ProcessStopped()
        {
            TimerManager.WHTimer.Stop();
            TimerManager.PerformanceTimer.Stop();
            ViewModel.whCBCheckedStatic = false;
            ErrorManager.ManageError(ErrorManager.PossibleErrors.Process_Stopped);
        }

        /// <summary>
        /// Re-target a process from the list of processes.
        /// </summary>
        /// <param name="procIndex">The process to acquire from the list of running processes.</param>
        public static void UpdateTargetProcess(int procIndex)
        {
            Process proc = runningProcs[procIndex];
            ViewModel.manualNameStatic = proc.ProcessName;
            ViewModel.manualTitleStatic = proc.MainWindowTitle;
            GetProcessById(proc.Id);
        }

        /// <summary>
        /// Method used to keep the list of processes up-to-date in a separate thread.
        /// </summary>
        public static void CheckProcessesThread()
        {
            while (keepThreadAlive)
            {
                try
                {
                    GetAllProcesses();

                    if (PerformanceManager.okForPerf)
                        PerformanceManager.PerformanceManagingThreadChild();

                    // How much of the system the tool will use.
                    if (ViewModel.runConstCBCheckedStatic)
                    {
                        // Keeps CPU usage at 0-2%
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        // Keeps CPU usage at ~8%
                        // Without this, CPU usage is at ~30-35%
                        System.Threading.Thread.Sleep(100);
                    }
                }
                catch
                {
                    // TODO : Add better error catching.
                    Console.Write("// TODO : FIX ME");
                }
            }
        }

        /// <summary>
        /// Handles information coming from outside the program (not contained, thus unsafe).
        /// </summary>
        /// <returns>Returns a reference to the dimensions of the sought-for window.</returns>
        unsafe public static Rect UnsafeRect()
        {
            // TODO : Check if injection could ever happen here - such as manipulating the -
                // parameters of a window?
            Rect sendRect = new Rect(0, 0, 0, 0);

            CustRect simpleRect = new CustRect();

            HandleRef hRef = new HandleRef(runningProc,
                runningProc.MainWindowHandle);

            // TODO : Put this into a Try/Catch
            if (!GetWindowRect(hRef, out simpleRect))
            {
                ProcessStopped();
                return sendRect;
            }

            sendRect = new Rect(Convert.ToDouble(simpleRect.Left),
                Convert.ToDouble(simpleRect.Top),
                Convert.ToDouble(simpleRect.Right - simpleRect.Left),
                Convert.ToDouble(simpleRect.Bottom - simpleRect.Top)
                );

            return sendRect;
        }
    }
}
