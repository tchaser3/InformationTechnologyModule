/* Title:           Main Window - Information Technology Module
 * Date:            4-13-17
 * Author:          Terry Holmes
 * 
 * Description:     This is the sign in for Information Techology Module */

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
using DataValidationDLL;
using NewEmployeeDLL;
using NewEventLogDLL;

namespace InformationTechnologyModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();

        //setting up data
        public static VerifyLogonDataSet TheVerifyLogonDataSet = new VerifyLogonDataSet();

        //global variables
        int gintNoOfMisses;
        public static int gintEmployeeID;

        public MainWindow()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gintNoOfMisses = 0;

            pbxPassword.Focus();
        }
        private void LogonFailed()
        {
            gintNoOfMisses++;

            if(gintNoOfMisses == 3)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "There Have Been Three Attemps To Sign into the Information Technology Module");

                TheMessagesClass.ErrorMessage("You Have Tried to Sign In Three Times\nThe Application Will Shut Down");

                Application.Current.Shutdown();
            }
            else
            {
                TheMessagesClass.InformationMessage("You Have Failed The Sign In Procedure");
            }
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strValueForValidation;
            string strLastName;
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            string strErrorMessage = "";
            int intRecordsReturned;

            //data validation
            strValueForValidation = pbxPassword.Password.ToUpper();
            blnThereIsAProblem = TheDataValidationClass.VerifyIntegerData(strValueForValidation);
            if(blnThereIsAProblem == true)
            {
                blnFatalError = true;
                strErrorMessage += "The Employee ID is not an Integer\n";
            }
            else
            {
                gintEmployeeID = Convert.ToInt32(strValueForValidation);
            }
            strLastName = txtLastName.Text;
            if(strLastName == "")
            {
                blnFatalError = true;
                strErrorMessage += "The Last Name Was Not Entered\n";
            }
            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(strErrorMessage);
                return;
            }

            //getting the employee information
            TheVerifyLogonDataSet = TheEmployeeClass.VerifyLogon(gintEmployeeID, strLastName);

            intRecordsReturned = TheVerifyLogonDataSet.VerifyLogon.Rows.Count;

            if(intRecordsReturned == 0)
            {
                LogonFailed();
            }
            else
            {
                if(TheVerifyLogonDataSet.VerifyLogon[0].EmployeeGroup != "ADMIN")
                {
                    LogonFailed();
                }
                else
                {
                    MainMenu MainMenu = new MainMenu();
                    MainMenu.Show();
                    Hide();
                }
            }
        }
    }
}
