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
using System.Windows.Shapes;

namespace FinalTest2
{
    /// <summary>
    /// Interaction logic for showShared.xaml
    /// </summary>
    public partial class showShared : Window
    {
        protected List<string> games = new List<string>();
        public showShared(List<string> shared)
        {
            InitializeComponent();
            games = shared;
            foreach (string Current in games)
            {
                shareBox.Items.Add(Current);
            }
            shareBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Ascending));
        }
    }
}
