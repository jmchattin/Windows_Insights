namespace Windows_Insight_App.Model
{
    // TODO : Make it so only Visible and Hidden values can be used for a control's visibility.
    public enum Visibility
    {
        Visible,
        Hidden
    }

    public struct TrimmedProcessStuct
    {
        private int _procNumber;
        public int btnNumber
        {
            get { return _procNumber; }
            set { _procNumber = value; }
        }

        private string _procName;
        public string procName
        {
            get { return _procName; }
            set { _procName = value; }
        }

        private string _procTitle;
        public string procTitle
        {
            get { return _procTitle; }
            set { _procTitle = value; }
        }

        private string _procId;
        public string procId
        {
            get { return _procId; }
            set { _procId = value; }
        }
    }

    public struct SpeedButton
    {
        #region VARIABLES
        private int _btnNumber;
        public int btnNumber
        {
            get { return _btnNumber; }
            set { _btnNumber = value; }
        }
        private string _content;
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }
        private string _procName;
        public string procName
        {
            get { return _procName; }
            set { _procName = value; }
        }
        private string _procTitle;
        public string procTitle
        {
            get { return _procTitle; }
            set { _procTitle = value; }
        }
        private string _visibility;
        public string visibility
        {
            get { return _visibility; }
            set { _visibility = value; }
        }
        #endregion // VARIABLES


    }
}
