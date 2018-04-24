using System.Windows; // msgbox

namespace Windows_Insight_App
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {

        public View()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }
    }
}
