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
    public partial class Form2 : Form
    {
        bool weekdays1;
        bool weekdays2;
        bool weekend1;
        bool weekend2;
        PriceDetails p;
        public Form2()
        {
            InitializeComponent();
        }

        private void saveBTN_Click(object sender, EventArgs e)
        {
            try
            {
                //getting textbox data and assigning in an object
                


                if (weekend1 == false || weekend2 == false || weekdays1 == false || weekdays2 == false)
                {
                    Console.WriteLine(weekdays1);
                    Console.WriteLine(weekdays2);
                    Console.WriteLine(weekend1);
                    Console.WriteLine(weekend2);
                    MessageBoxButtons msg = MessageBoxButtons.RetryCancel;

                    DialogResult dialog = MessageBox.Show("Invalid Input", "Message", msg);
                    if (dialog == DialogResult.Retry)
                    {
                        clearDetails();

                    }
                }
                else
                {
                    Console.WriteLine(weekdays1);
                    Console.WriteLine(weekdays2);
                    Console.WriteLine(weekend1);
                    Console.WriteLine(weekend2);
                    int weekDays1 = int.Parse(weekdays1TB.Text);
                    int weekDays2 = int.Parse(weekdays2TB.Text);
                    int weekEnds1 = int.Parse(weekends1TB.Text);
                    int weekEnds2 = int.Parse(weekends2TB.Text);
                    string group = groupTB.Text;
                    int duration = int.Parse(durationCB.SelectedItem.ToString());

                    p = new PriceDetails();
                    p.WeekDays1 = weekDays1;
                    p.WeekDays2 = weekDays2;
                    p.WeekEnds1 = weekEnds1;
                    p.WeekEnds2 = weekEnds2;
                    p.Group = int.Parse(group);
                    p.Duration = duration;
                    string data = Utility.ReadToTextPrice(); //GETTING DATA FROM TEXT FILE AND ASSINIGNG IN "data"
                    List<PriceDetails> lstPrice = new List<PriceDetails>();
                    if (data != null & data != "")
                    {
                        //deserialzing the data from pricedetails class
                        lstPrice = JsonConvert.DeserializeObject<List<PriceDetails>>(data);
                    }
                    lstPrice.Add(p);

                    string str = JsonConvert.SerializeObject(lstPrice);// object to string 

                    Utility.WriteToTextPrice(str);
                    MessageBoxButtons msg = MessageBoxButtons.OK;

                    DialogResult dialog = MessageBox.Show("Price Detail has been inserted successfully", "Message", msg);
                    if (dialog == DialogResult.OK)
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
            weekdays1TB.Text = "";
            weekdays2TB.Text = "";
            weekends1TB.Text = "";
            weekends2TB.Text = "";
            groupTB.Text = "--Select--";
            durationCB.SelectedItem = "--Select--";
        }

        private void viewdetailsBTN_Click(object sender, EventArgs e)
        {
            //displaying form when clicked
            AdminReport admin = new AdminReport();
            admin.ShowDialog();
        }


        private void reportBtn_Click(object sender, EventArgs e)
        {
            reportChart rp = new reportChart();
            rp.ShowDialog();
        }
        //validating the data inserted when non digit key is pressed

        private void weekdays1TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                childWeekDaysErrorMessage.Text = "Invalid Input";
                weekdays1 = false;
            }
            else
            {
                childWeekDaysErrorMessage.Text = "";
                weekdays1 = true;
            }
        }

        private void weekends1TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                childWeekEndErrorMessage.Text = "Invalid Input"; weekend1 = false;
            }
            else
            {
                childWeekEndErrorMessage.Text = "";
                weekend1=true;
            }
        }

        private void weekdays2TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                adultWeekDayErrorMessage.Text = "Invalid Input";weekdays2 = false;
            }
            else
            {
                adultWeekDayErrorMessage.Text = "";
                weekdays2=  true;
            }
        }

        private void weekends2TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                adultWeekEndErrorMessage.Text = "Invalid Input"; weekend2 = false;
            }
            else
            {
                adultWeekEndErrorMessage.Text = ""; weekend2 = true;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearDetails();
        }
    }
}
