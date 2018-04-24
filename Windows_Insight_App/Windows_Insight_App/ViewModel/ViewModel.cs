using System;
using System.Text;
using System.Windows.Threading; // Timer
using System.Diagnostics; // Debug on-off
using System.ComponentModel; // INotify
using System.Windows.Input; // ICommand
using System.Collections.ObjectModel; // ObservableCollections

namespace Windows_Insight_App
{
    /// <summary>
    /// Serves as the inbetween for the View and the Model.
    /// Creates cases for which buttons do what.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        #region VARIABLES
        public static bool inDebugMode = false;
        public bool debugMode
        {
            get { return inDebugMode; }
        }

        private static ViewModel _instance;

        public string windowTitle
        {
            get;
            set;
        }


        #region QUICK_BTNS
        // OneBtn quick-action
        //private string _oneContent = string.Empty; // Button text content.
        public string oneContent
        {
            get
            {
                return Properties.Settings.Default.oneContent;
            }
            set
            {
                Properties.Settings.Default.oneContent = value;
                oneVisible = GetButtonVisibility(1).ToString();
                OnPropertyChanged("oneContent");
            }
        }
        public string oneProcName
        {
            get { return Properties.Settings.Default.oneProcName; }
            set
            {
                Properties.Settings.Default.oneProcName = value;
                oneVisible = GetButtonVisibility(1).ToString();
                OnPropertyChanged("oneProcName");
            }
        }
        public string oneProcTitle
        {
            get { return Properties.Settings.Default.oneProcTitle; }
            set
            {
                Properties.Settings.Default.oneProcTitle = value;
                oneVisible = GetButtonVisibility(1).ToString();
                OnPropertyChanged("oneProcTitle");
            }
        }
        public string oneVisible
        {
            get
            {
                return Properties.Settings.Default.oneVisible;
            }
            set
            {
                Properties.Settings.Default.oneVisible = value;
                OnPropertyChanged("oneVisible");
            }
        }
        // TwoBtn quick-action
        public string twoContent
        {
            get
            {
                return Properties.Settings.Default.twoContent;
            }
            set
            {
                Properties.Settings.Default.twoContent = value;
                twoVisible = GetButtonVisibility(2).ToString();
                OnPropertyChanged("twoContent");
            }
        }
        public string twoProcName
        {
            get { return Properties.Settings.Default.twoProcName; }
            set
            {
                Properties.Settings.Default.twoProcName = value;
                twoVisible = GetButtonVisibility(2).ToString();
                OnPropertyChanged("twoProcName");
            }
        }
        public string twoProcTitle
        {
            get { return Properties.Settings.Default.twoProcTitle; }
            set
            {
                Properties.Settings.Default.twoProcTitle = value;
                twoVisible = GetButtonVisibility(2).ToString();
                OnPropertyChanged("twoProcTitle");
            }
        }
        public string twoVisible
        {
            get
            {
                return Properties.Settings.Default.twoVisible;
            }
            set
            {
                Properties.Settings.Default.twoVisible = value;
                OnPropertyChanged("twoVisible");
            }
        }
        // ThreeBtn quick-action
        public string threeContent
        {
            get
            {
                return Properties.Settings.Default.threeContent;
            }
            set
            {
                Properties.Settings.Default.threeContent = value;
                threeVisible = GetButtonVisibility(3).ToString();
                OnPropertyChanged("threeContent");
            }
        }
        public string threeProcName
        {
            get { return Properties.Settings.Default.threeProcName; }
            set
            {
                Properties.Settings.Default.threeProcName = value;
                threeVisible = GetButtonVisibility(3).ToString();
                OnPropertyChanged("threeProcName");
            }
        }
        public string threeProcTitle
        {
            get { return Properties.Settings.Default.threeProcTitle; }
            set
            {
                Properties.Settings.Default.threeProcTitle = value;
                threeVisible = GetButtonVisibility(3).ToString();
                OnPropertyChanged("threeProcTitle");
            }
        }
        public string threeVisible
        {
            get
            {
                return Properties.Settings.Default.threeVisible;
            }
            set
            {
                Properties.Settings.Default.threeVisible = value;
                OnPropertyChanged("threeVisible");
            }
        }
        public string fourContent
        {
            get
            {
                return Properties.Settings.Default.fourContent;
            }
            set
            {
                Properties.Settings.Default.fourContent = value;
                fourVisible = GetButtonVisibility(4).ToString();
                OnPropertyChanged("fourContent");
            }
        }
        public string fourProcName
        {
            get { return Properties.Settings.Default.fourProcName; }
            set
            {
                Properties.Settings.Default.fourProcName = value;
                fourVisible = GetButtonVisibility(4).ToString();
                OnPropertyChanged("fourProcName");
            }
        }
        public string fourProcTitle
        {
            get { return Properties.Settings.Default.fourProcTitle; }
            set
            {
                Properties.Settings.Default.fourProcTitle = value;
                fourVisible = GetButtonVisibility(4).ToString();
                OnPropertyChanged("fourProcTitle");
            }
        }
        public string fourVisible
        {
            get
            {
                return Properties.Settings.Default.fourVisible;
            }
            set
            {
                Properties.Settings.Default.fourVisible = value;
                OnPropertyChanged("fourVisible");
            }
        }
        #endregion // QUICK_BTNS


        #region NAME_TITLE
        public static string manualNameStatic
        {
            get { return _instance.manualName; }
            set
            {
                _instance.manualName = value;
            }
        }
        // Manually-acquired-process-field text
        private string _manualName = "<process name>";
        public string manualName
        {
            get { return _manualName; }
            set
            {
                _manualName = value;
                //VmOnPropertyChanged("manualName");
                OnPropertyChanged("manualName");
            }
        }
        public static string manualTitleStatic
        {
            get { return _instance.manualTitle; }
            set
            {
                _instance.manualTitle = value;
            }
        }
        private string _manualTitle = "<process title>";
        public string manualTitle
        {
            get { return _manualTitle; }
            set
            {
                _manualTitle = value;
                VmOnPropertyChanged("manualTitle");
            }
        }
        #endregion // NAME_TITLE


        #region PERFORMANCE_VALUES
        public static string processPerfStatic
        {
            get { return _instance.processPerf; }
            set
            {
                _instance.processPerf = value;
            }
        }
        private string _processPerf;
        public string processPerf
        {
            get { return _processPerf; }
            set
            {
                _processPerf = value;
                _instance.OnPropertyChanged("processPerf");
            }
        }
        public static string cpuPerfStatic
        {
            get { return _instance.cpuPerf; }
            set
            {
                _instance.cpuPerf = value;
            }
        }
        private string _cpuPerf;
        public string cpuPerf
        {
            get { return _cpuPerf; }
            set
            {
                _cpuPerf = value;
                _instance.OnPropertyChanged("cpuPerf");
            }
        }
        public static string ramPerfStatic
        {
            get { return _instance.ramPerf; }
            set
            {
                _instance.ramPerf = value;
            }
        }
        private string _ramPerf;
        public string ramPerf
        {
            get { return _ramPerf; }
            set
            {
                _ramPerf = value;
                _instance.OnPropertyChanged("ramPerf");
            }
        }
        public static string tramPerfStatic
        {
            get { return _instance.tramPerf; }
            set
            {
                _instance.tramPerf = value;
            }
        }
        private string _tramPerf;
        public string tramPerf
        {
            get { return _tramPerf; }
            set
            {
                _tramPerf = value;
                _instance.OnPropertyChanged("tramPerf");
            }
        }
        public static string netPerfStatic
        {
            get { return _instance.netPerf; }
            set
            {
                _instance.netPerf = value;
            }
        }
        private string _netPerf;
        public string netPerf
        {
            get { return _netPerf; }
            set
            {
                _netPerf = value;
                OnPropertyChanged("netPerf");
            }
        }
        #endregion // PERFORMANCE_VALUES


        #region HEIGHT_WIDTH
        // http://stackoverflow.com/questions/3491510/how-to-hookup-textboxs-textchanged-event-and-command-in-order-to-use-mvvm-patte
        public static string heightStatic
        {
            get { return _instance.height; }
            set
            {
                _instance.height = value;
            }
        }
        private string _height = "0";
        public string height
        {
            get { return _height; }
            set
            {
                if (Model.HelpManager.IsValidPositiveInt(value))
                {
                    _height = value; // System.Windows.SystemParameters.PrimaryScreenHeight.ToString();
                    VmOnPropertyChanged("publicStaticHeight");
                }
            }
        }
        public static string widthStatic
        {
            get { return _instance.width; }
            set
            {
                _instance.width = value;
            }
        }
        private string _width = "0";
        public string width
        {
            get { return _width; }
            set
            {
                if (Model.HelpManager.IsValidPositiveInt(value))
                {
                    _width = value; // System.Windows.SystemParameters.PrimaryScreenWidth.ToString();
                    VmOnPropertyChanged("publicStaticWidth");
                }
            }
        }
        public static string whContentStatic
        {
            get { return _instance.whContent; }
            set
            {
                _instance.whContent = value;
            }
        }
        private string _whContent;
        public string whContent
        {
            get { return _whContent; }
            set
            {
                _whContent = value;
                VmOnPropertyChanged("whContent");
                //_instance.OnPropertyChanged("whContent");
            }
        }
        public static bool whCBCheckedStatic
        {
            get { return _instance.whCBChecked; }
            set { _instance.whCBChecked = value; }
        }
        private bool _whCBChecked = false;
        public bool whCBChecked
        {
            get { return _whCBChecked; }
            set
            {
                _whCBChecked = value;
                OnPropertyChanged("whCBChecked");
            }
        }
        #endregion // HEIGHT_WIDTH


        public static ObservableCollection<Model.TrimmedProcess> trimmedProcessesStatic
        {
            get { return _instance.trimmedProcesses; }
            set { _instance.trimmedProcesses = value; }
        }
        public ObservableCollection<Model.TrimmedProcess> trimmedProcesses
        {
            get { return _trimmedProcesses; }
            set
            {
                _trimmedProcesses = value;
                OnPropertyChanged("trimmedProcesses");
                OnPropertyChanged("procNum");
            }
        }
        private ObservableCollection<Model.TrimmedProcess> _trimmedProcesses;
        /*
        public static void setOneTrimmedProcStatic(int procNum, Model.TrimmedProcessStuct tProc)
        {
            _instance.trimmedProcs[procNum - 1] = tProc;
        }
        public static Model.TrimmedProcessStuct[] trimmedProcsStatic
        {
            get { return _instance.trimmedProcs; }
            set { _instance.trimmedProcs = value; }
        }
        private Model.TrimmedProcessStuct[] _trimmedProcs;
        public Model.TrimmedProcessStuct[] trimmedProcs
        {
            get { return _trimmedProcs; }
            set
            {
                _trimmedProcs = value;
                OnPropertyChanged("trimmedProcs");
            }
        }
        */


        #region CONTROL_COMMANDS
        //public ICommand buttonCmd = Model.ICommandManager.SetupICommand("button");
        //public ICommand checkboxCmd = Model.ICommandManager.SetupICommand("checkbox");
        //public ICommand dropdownCmd = Model.ICommandManager.SetupICommand("dropdown");
        //public ICommand radiobuttonCmd = Model.ICommandManager.SetupICommand("radiobutton");
        private ICommand _buttonCmd;
        public ICommand buttonCmd
        {
            get
            {
                return _buttonCmd;
            }
            set
            {
                _buttonCmd = value;
            }
        }
        private ICommand _checkboxCmd;
        public ICommand checkboxCmd
        {
            get
            {
                return _checkboxCmd;
            }
            set
            {
                _checkboxCmd = value;
            }
        }
        private ICommand _dropdownCmd;
        public ICommand dropdownCmd
        {
            get
            {
                return _dropdownCmd;
            }
            set
            {
                _dropdownCmd = value;
            }
        }
        private ICommand _radiobuttonCmd;
        public ICommand radiobuttonCmd
        {
            get
            {
                return _radiobuttonCmd;
            }
            set
            {
                _radiobuttonCmd = value;
            }
        }
        #endregion // CONTROL_COMMANDS


        #region LISTVIEW_VARS
        private int _sIndex;
        public int sIndex
        {
            set
            {
                _sIndex = value;
                if (_sIndex != -1)
                {
                    Model.ProcessManager.UpdateTargetProcess(_sIndex);
                    Model.PerformanceManager.okForPerf = true;
                    // Telemetry.log(TelemetryKey.processlist_click, manualTitle);
                }
            }
        }

        System.Threading.Thread getProcessesThread = new System.Threading.Thread(Model.ProcessManager.CheckProcessesThread);
        #endregion // LISTVIEW_VARS


        #region SETTING_VARS
        public static bool showAllCBCheckedStatic
        {
            get { return _instance.showAllCBChecked; }
            set { _instance.showAllCBChecked = value; }
        }
        private bool _showAllCBChecked;
        public bool showAllCBChecked
        {
            get { return _showAllCBChecked; }
            set
            {
                _showAllCBChecked = value;
            }
        }

        public static bool runConstCBCheckedStatic
        {
            get { return _instance.runConstCBChecked; }
            set { _instance.runConstCBChecked = value; }
        }
        private bool _runConstCBChecked;
        public bool runConstCBChecked
        {
            get { return _runConstCBChecked; }
            set
            {
                _runConstCBChecked = value;
            }
        }
        #endregion // SETTING_VARS


        #endregion // VARIABLES

        /// <summary>
        /// Initialize the View Model.
        /// </summary>
        public ViewModel()
        {
            DebugMode();

            #region TELEMETRY
            string data = string.Format(System.Globalization.CultureInfo.CurrentCulture, "name = '{0}', script = '{1}'", "Sample Script", "cmd.exe ipconfig /?");

            StringBuilder version = new StringBuilder();
            version.Append(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major).Append(".")
                .Append(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor).Append(".")
                .Append(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);

            // Session begins in Telemetry.  It initializes the start.
            // Telemetry.log(TelemetryKey.project, "Quantum");
            // Telemetry.log(TelemetryKey.version, version.ToString());
            // Telemetry.log(TelemetryKey.hostname, System.Net.Dns.GetHostEntry("localhost").HostName);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            System.Windows.Application.Current.Exit += new System.Windows.ExitEventHandler(this.OnApplicationExit);
            #endregion // TELEMETRY

            InitializeVariables();

            windowTitle = "Windows Insight App " + version.ToString();
            this.OnPropertyChanged("windowTitle");

            getProcessesThread.Start();

            Model.TimerManager.InitializeTimers();

            while (!getProcessesThread.IsAlive) ;
        }

        /// <summary>
        /// Initialize all VM variables.
        /// </summary>
        private void InitializeVariables()
        {
            #region COMMANDS_SETUP
            this.buttonCmd = Model.ICommandManager.SetupICommand("button");
            this.checkboxCmd = Model.ICommandManager.SetupICommand("checkbox");
            this.dropdownCmd = Model.ICommandManager.SetupICommand("dropdown");
            this.radiobuttonCmd = Model.ICommandManager.SetupICommand("radiobutton");
            #endregion // COMMANDS_SETUP

            // Use for external class reference to the View Model's methods/variables.
            _instance = this;

            height = System.Windows.SystemParameters.PrimaryScreenHeight.ToString();
            width = System.Windows.SystemParameters.PrimaryScreenWidth.ToString();
        }

        private Model.Visibility GetButtonVisibility(int button)
        {
            switch (button)
            {
                case 1:
                    if (Properties.Settings.Default.oneContent != "" & Properties.Settings.Default.oneProcName != "" & Properties.Settings.Default.oneProcTitle != "")
                        return Model.Visibility.Visible;
                    break;
                case 2:
                    if (Properties.Settings.Default.twoContent != "" & Properties.Settings.Default.twoProcName != "" & Properties.Settings.Default.twoProcTitle != "")
                        return Model.Visibility.Visible;
                    break;
                case 3:
                    if (Properties.Settings.Default.threeContent != "" & Properties.Settings.Default.threeProcName != "" & Properties.Settings.Default.threeProcTitle != "")
                        return Model.Visibility.Visible;
                    break;
                case 4:
                    if (Properties.Settings.Default.fourContent != "" & Properties.Settings.Default.fourProcName != "" & Properties.Settings.Default.fourProcTitle != "")
                        return Model.Visibility.Visible;
                    break;
                default:
                    break;
            }
            return Model.Visibility.Hidden;
        }

        /// <summary>
        /// Determine if the program is running in debug (testing) mode.
        /// </summary>
        [Conditional("DEBUG")]
        private static void DebugMode()
        {
            inDebugMode = true;
        }

        #region PROPERTY_CHANGE_METHODS
        /// <summary>
        /// Used to call the OnPropertyChanged event from other classes.
        /// </summary>
        /// <param name="name">The name of the control variable to update.</param>
        public static void VmOnPropertyChanged(string name)
        {
            _instance.OnPropertyChanged(name);
        }

        /// <summary>
        /// Update control variables visible to the user.
        /// </summary>
        /// <param name="name">The name of the control variable to update.</param>
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion // PROPERTY_CHANGE_METHODS

        #region APP_CLOSING_METHODS
        /// <summary>
        /// Log that the application is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            // TODO : Remove this line once Telemetry is reenabled.
            Model.ProcessManager.keepThreadAlive = false;
            // Telemetry.logClose();
        }

        /// <summary>
        /// Catch any exception anywhere that causes the plugin to crash.
        /// Then, send final telemetry before the application closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The exception we care about.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            // Include message and stacktrace.
            // Telemetry.log(TelemetryKey.session_unhandled_exception, string.Format( "{0}\n\n{1}", ex.Message, ex.StackTrace ) );
            // Telemetry.logClose();
            // TODO : Remove this line once Telemetry is reenabled.
            Model.ProcessManager.keepThreadAlive = false;
        }
        #endregion // APP_CLOSING_METHODS
    }
}
