using System;
using System.Windows; // Messagebox

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handles the decisions of all errors that occur.
    /// </summary>
    class ErrorManager
    {
        private static string[] errorMessages =
        {
            "Unknown error!\n\nPlease contact the PDX Tools team at:\nExperis-PDX-Tools@microsoft.com",
            "No {0}.\n\nCheck process name and title\nunder the Settings tab.",
            "The target process has stopped.", //"Error - Process not running.\n\nPlease restart the process and proceed again.",
            "No process found.",
            "Unassigned call to {0}.",
            "No processes found running at all!",
            "Please enter a name and title for the process you want to connect to.",
            "Invalid positive integer."
        };

        /// <summary>
        /// Calls the corresponding error message from the list of messages.
        /// </summary>
        /// <param name="err">The error message to use.</param>
        /// <param name="detail1">Additional text that can be applied to the message.</param>
        public static void ManageError(PossibleErrors err, string detail1 = "")
        {
            MessageBox.Show(String.Format(errorMessages[(int)err], detail1));
        }

        /// <summary>
        /// List of possible errors and the corresponding index of the error message text list.
        /// </summary>
        public enum PossibleErrors
        {
            Unknown = 0,
            No_Process_Found_w_Title = 1,
            Process_Stopped = 2,
            No_Process_Found = 3,
            Unassigned_Call = 4,
            No_Processes_Running = 5,
            No_NameTitle_Entered = 6,
            Invalid_Int = 7
        }

    }
}
