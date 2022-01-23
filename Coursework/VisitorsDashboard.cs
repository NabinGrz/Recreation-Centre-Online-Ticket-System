using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class VisitorsDashboard : Form
    {
        bool nameValid;
        bool phoneValid;
        bool grpValid;
        bool ageValid;

        public string UserName { get; set; }
        public string Password { get; set; }

        VisitorsDetail vis;

        public VisitorsDashboard()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //getting textbox data and assigning in an object
                string name = nameTB.Text;
                string phoneno = phonenoTB.Text;
                string agegroup = agegroupCB.SelectedItem.ToString();
                int groupno = int.Parse(grpTB.Text);
                DateTime indatetime = DateTime.Now;
                string outdatetime = null;
                DateTime Visitorsdate = dateTimePicker1.Value.Date;
                string day = dateTimePicker1.Value.Date.DayOfWeek.ToString();
                string month = dateTimePicker1.Value.Date.Month.ToString();


                vis = new VisitorsDetail();
                vis.VisitorsName = name;
                vis.PhoneNo = phoneno;
                vis.AgeGroup = agegroup;
                vis.GroupNo = groupno;
                vis.VisitorsDateTime = Visitorsdate;
                vis.indateTime = indatetime.TimeOfDay.ToString();
                vis.outdateTime = outdatetime;
                vis.Day = day;

                if (nameValid == true && phoneValid == true && grpValid == true && ageValid == true)
                {
                    //GETTING DATA FROM TEXT FILE AND ASSINIGNG IN "visitorsData"
                    string visitorsData = Utility.ReadToText(); 
                    List<VisitorsDetail> lstVisitors = new List<VisitorsDetail>();
                    if (visitorsData!= null & visitorsData!= "")
                    {
                            lstVisitors = JsonConvert.DeserializeObject<List<VisitorsDetail>>(visitorsData);
                    }
                    lstVisitors.Add(vis);
                    int id = lstVisitors.Count + 1;
                    vis.VisitorsID = id;
                    string str = JsonConvert.SerializeObject(lstVisitors);

                    Utility.WriteToText(str);

                    MessageBoxButtons buttons = MessageBoxButtons.OK;

                    DialogResult result = MessageBox.Show("Visitors detail has been recorded", "Message", buttons);
                    if (result == DialogResult.OK)
                    {
                        clearDetails();

                    }

                }
                else
                {
                    MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;

                    DialogResult result = MessageBox.Show("Please enter valid details", "Message", buttons);
                    if (result == DialogResult.OK)
                    {
                        clearDetails();

                    }
                }
            }
            catch (Exception)
            {

            }

        }
        private void clearDetails()
        {
            //setting empty data in textbox when method called
            nameTB.Text = "";
            phonenoTB.Text = "";
            agegroupCB.SelectedItem = "--Select--";
            grpTB.Text = "";
            dateTimePicker1.Value = DateTime.UtcNow;
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            reportChart reportObj = new reportChart();
            reportObj.ShowDialog();
        }

        //validating the data inserted when non digit key is pressed
        private void nameTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)  || nameTB.Text.Contains("[^0-9]") || string.IsNullOrWhiteSpace(nameTB.Text)) )
            {
                nameErrorMessage.Text = "Invalid Input"; nameValid = false;
                
            }
            else
            {
                nameErrorMessage.Text = ""; nameValid = true;
            }
        }


        private void phonenoTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || phonenoTB.Text.Contains("^[A-Z]?[a-z]*$ ")))
            {
                phoneValid = false;
                phonenoError.Text = "Invalid Input";
            }
            else
            {
                phonenoError.Text = ""; phoneValid = true;
            }
        }

        private void agegroupCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(agegroupCB.SelectedIndex == 0)
            {
                ageValid = false;
                ageGrpErrorMessage.Text = "Please select age category";
            }
            else
            {
                ageGrpErrorMessage.Text = ""; ageValid = true;
            }
        }


        private void grpTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || grpTB.Text.Contains("^[A-Z]?[a-z]*$ ")))
            {
                grpValid = false;
                grpnoErrorMessage.Text = "Invalid Input";
            }
            else
            {
                grpnoErrorMessage.Text = ""; grpValid = true;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearDetails();
        }
    }
}
