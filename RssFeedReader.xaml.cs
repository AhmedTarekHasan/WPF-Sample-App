using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.ServiceModel.Syndication;
using WPFRssFeedReader.Model;
using WPFRssFeedReader.ViewModel;

namespace WPFRssFeedReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RssFeedReader : Window
    {
        #region Fields
        private ManageRssFeedsViewModel viewModel = new ManageRssFeedsViewModel();
        #endregion

        #region Properties
        public ManageRssFeedsViewModel ViewModel
        {
            get
            {
                return viewModel;
            }
        }
        #endregion

        #region Constructors
        public RssFeedReader()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
        #endregion
    }
}
