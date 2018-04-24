using System;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Windows_Insight_App
{
    /// <summary>
    /// Used to call into Session and remove code from the ViewModel.
    /// </summary>
    class Telemetry
    {
        /// <summary>
        /// Keep track of the user's session.
        /// </summary>
        public static Session sessionTelem = new Session();

        /// <summary>
        /// Send different events to SQL.
        /// </summary>
        /// <param name="key">What is being sent.</param>
        /// <param name="val">What's being sent's value.</param>
        public static void log(TelemetryKey key, string val)
        {
            sessionTelem.SendEvent(key.ToString(), val);
        }

        /// <summary>
        /// Observe that the program is closing.
        /// </summary>
        public static void logClose()
        {
            // Turn-off threads that may be running.
            Model.ProcessManager.keepThreadAlive = false;
            if (!ViewModel.inDebugMode)
                sessionTelem.SendEvent(TelemetryKey.session_end.ToString(),
                        System.Security.Principal.WindowsIdentity.GetCurrent().Name).Wait(1000);
        }
    }

    /// <summary>
    /// Tony's Tool Telemetry Session
    /// </summary>
    public class Session
    {
        #region VARIABLES
        /// <summary>
        /// Session GUID
        /// </summary>
        public readonly Guid SessionGUID;

        /// <summary>
        /// SQL insert command
        /// </summary>
        private const string InsertCommand = "insert into [event_table] ([application], [session_guid], [timestamp], [key], [value]) values ";

        /// <summary>
        /// SQL Server name
        /// </summary>
        private readonly string server;

        /// <summary>
        /// SQL Database name
        /// </summary>
        private readonly string database;

        /// <summary>
        /// SQL User login name
        /// </summary>
        private readonly string user;

        /// <summary>
        /// SQL User password
        /// </summary>
        private readonly string password;

        /// <summary>
        /// Application name
        /// </summary>
        private readonly string application;

        /// <summary>
        /// Database connection string
        /// </summary>
        private readonly string connectionString;

        // TODO : Fix warning - Warning CA1001  Implement IDisposable on 'Session' because
        // it creates members of the following IDisposable types: 'SqlConnection'.
        // If 'Session' has previously shipped, adding new members that implement IDisposable
        // to this type is considered a breaking change to existing
        // consumers.Windows_Insight_App C:\Users\Rowman Sailor\documents\visual studio 2015\
        // Projects\Windows_Insight_App\Windows_Insight_App\Telemetry.cs   53	Active
        /// <summary>
        /// SQL database connection
        /// </summary>
        private readonly SqlConnection connection;
        #endregion

        /// <summary>
        /// Initializes a new instance of the Session class
        /// </summary>
        /// <param name="application">Application name</param>
        /// <param name="server">SQL Server name</param>
        /// <param name="database">SQL database name</param>
        /// <param name="user">SQL user</param>
        /// <param name="password">SQL password</param>
        // TODO : Update SQL values.
        public Session(
            string application = "Windows Insight App",
            string server = "", // Server\\Name,port?
            string database = "", // Tools_Telemetry
            string user = "", // Telemetry_Event_Writer
            string password = "")
        {
            this.SessionGUID = Guid.NewGuid();
            this.server = server;
            this.database = database;
            this.user = user;
            this.password = password;
            this.application = application;
            this.connectionString = "Data Source=" + server + ";Initial Catalog=" + database + ";User Id=" + user + ";Password=" + password + ";MultipleActiveResultSets=true";

            // connect to server
            try
            {
                this.connection = new SqlConnection(this.connectionString);
                this.connection.Open();
            }
            catch
            {
                if (this.connection != null)
                {
                    try
                    {
                        this.connection.Close();
                    }
                    finally
                    {
                        this.connection = null;
                    }
                }
            }

            this.SendEvent(TelemetryKey.session_start.ToString(), System.Security.Principal.WindowsIdentity.GetCurrent().Name);
        }

        /// <summary>
        /// Sends an event 
        /// </summary>
        /// <param name="key">Event key</param>
        /// <param name="value">Event value</param>
        /// <returns>Task to wait on</returns>
        public Task SendEvent(string key, string value)
        {
            if (!ViewModel.inDebugMode)
                return this.SendEventInternal(key, value);
            else
                return null;
        }

        /// <summary>
        /// Sends an event asynchronously
        /// </summary>
        /// <param name="key">Dictionary key</param>
        /// <param name="value">Dictionary value</param>
        /// <returns>Async Task</returns>
        private async Task SendEventInternal(string key, string value)
        {
            try
            {
                if (this.connection != null)
                {
                    var command = InsertCommand + string.Format("(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}')", this.application, this.SessionGUID.ToString(), DateTime.UtcNow, key, value);
                    using (var cmd = new SqlCommand(command, this.connection))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// List of all the pieces of telemetry data that can be sent.
    /// </summary>
    public enum TelemetryKey
    {
        project,                // Quantum
        version,                // #.#.#.#
        hostname,               // <pc name>.<region>.corp.microsoft.com
        session_start,          // Tool Started
        session_end,            // Tool Closed
        session_unhandled_exception,          // Tool Crashed

        quick_btn,              // 1, 2, 3, 4
        //see_all_processes_btn,  // Clicked
        set_hw_btn,             // Clicked
        screenshot_btn,         // Clicked
        hw_timer,               // 1,0
        //copy_btn,               // Clicked
        processlist_click,      // Value of process title
        manual_btn              // Clicked

        // Tool Feature Options

    }
}