using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using libSMARTMultiTouch.Table;

namespace TouchApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // This method initializes both the touch input and the default set of resources for the table.
            // The LayoutRoot is our root grid defined in Windows1.xaml
            TableManager.Initialize(this, LayoutRoot);

            //TableControl: see the TableControl class file that is part of this project
            LayoutRoot.Children.Add(new TableControl());

            // Specifies that the application is to be displayed in full screen mode
            TableManager.IsFullScreen = true;
        }
    }
}
