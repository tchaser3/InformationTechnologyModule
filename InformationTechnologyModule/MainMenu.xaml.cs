/* Title:           Main Menu
 * Date:            4-114-17
 * Author:          Terry Holmes
 * 
 * Description:     This is used as the main menu */

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

namespace InformationTechnologyModule
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this will close the program
            TheMessagesClass.CloseTheProgram();
        }

        private void btnCreateEditProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProject CreateProject = new CreateProject();
            CreateProject.Show();
            Close();
        }
    }
}
