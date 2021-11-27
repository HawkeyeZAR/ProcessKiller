using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace RunningProcessKiller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Call method to create a combox box of system processes
            createComboBox();
        }

        ComboBox comboBoxProcess = new ComboBox();
        private void createComboBox()
        {
            // Creating and setting the properties of comboBox
            comboBoxProcess.Location = new Point(13, 15);
            comboBoxProcess.Size = new Size(460, 24);
            comboBoxProcess.DropDownStyle = ComboBoxStyle.DropDownList;
            //get list of processes and add to combobox
            getProcesses();
            // Add this ComboBox to the form
            this.Controls.Add(comboBoxProcess);
        }

        // Create list to store all processes id's
        List<int> pID = new List<int>();
        private void getProcesses()
        {
            // Get all processes running on the local computer.
            Process[] localAll = Process.GetProcesses();
            foreach(Process process in localAll)
            {
                string name = $"{ process.ProcessName}";
                string windowTitle = $"{ process.MainWindowTitle }";
                //add the processes to the combobox
                comboBoxProcess.Items.Add(name + "  " + windowTitle);
                pID.Add(process.Id);
            }
        }

        private void buttonKill_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBoxProcess.Text))
            {
                MessageBox.Show("You need to select a process first");
            }
            else
            {
                // Get index of selected item in combobox
                int index = comboBoxProcess.SelectedIndex;
                // Get the process ID of process to be deleted
                int processtoDelete = pID.ElementAt(index);

                //Get array of running processes
                Process[] runningProcess = Process.GetProcesses();
                //Loop through array, if process id found, kill process.
                foreach (Process prs in runningProcess)
                {
                    if (prs.Id == processtoDelete)
                    {
                        prs.Kill();
                        break;
                    }
                }
                // Remove selected item from combobox as process has been deleted:  
                comboBoxProcess.Items.Remove(comboBoxProcess.SelectedItem);
                // Remove killed process from the pID list
                pID.RemoveAt(index);

            }
        }
    }
}
