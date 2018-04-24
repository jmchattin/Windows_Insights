namespace Windows_Insight_App.Model
{
    // TODO : Consider making this a struct.
    // TODO : Is a list of trimmed processes needed?
    /// <summary>
    /// Handles limiting the details of what processes are running for easy access.
    /// </summary>
    public class TrimmedProcess
    {
        /// <summary>
        /// Initialize the class instance.
        /// </summary>
        /// <param name="pnum">Index number of the process.</param>
        /// <param name="pname">Name of the process.</param>
        /// <param name="ptitle">Title of the process.</param>
        /// <param name="pid">ID of the process.</param>
        public TrimmedProcess(int pnum, string pname, string ptitle, int pid)
        {
            num = pnum;
            name = pname;
            title = ptitle;
            id = pid;
        }

        public int num
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string title
        {
            get;
            set;
        }

        public int id
        {
            get;
            set;
        }
    }
}
