using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class reportChart : Form
    {
        //declaring a variable that will get selected cell id
        int indexRow;
        public reportChart()
        {
            InitializeComponent();
            BindGridToCheckOut();
        }
        private void BindGridToCheckOut()
        {
            VisitorsDetail obj = new VisitorsDetail();
            String listReview = Utility.ReadToText();

            //adding a button in data grid view
            DataGridViewButtonColumn db = new DataGridViewButtonColumn();
            db.FlatStyle = FlatStyle.Popup;
            db.HeaderText = "Checkout";
            db.Name = "Checkout";
            db.UseColumnTextForButtonValue = true;
            db.Text = "Checkout";
            db.Width = 80;
            dataGridViewInOut.Columns.Add(db);
            if (listReview != null)
            {
                var lstPeople = JsonConvert.DeserializeObject<List<VisitorsDetail>>(listReview);
                dataGridViewInOut.DataSource = lstPeople;
            }
        }
       
        private void dataGridViewInOut_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //getting selected cell data and assigning
                indexRow = e.RowIndex;
                DataGridViewRow d = dataGridViewInOut.Rows[indexRow];
                DateTime Visitorsdate = DateTime.Now;
               string agegroup = d.Cells[5].Value.ToString();
                String groupno = d.Cells[7].Value.ToString();

                VisitorsIDTB.Text = d.Cells[1].Value.ToString();
                VisitorsNameTB.Text = d.Cells[2].Value.ToString();
                inTimeTB.Text = d.Cells[9].Value.ToString();
               
                //calculating duration
                DateTime inTime = DateTime.Parse(d.Cells[9].Value.ToString());
                DateTime outTime = DateTime.Now;
                int duration = outTime.Hour - inTime.Hour;
                Console.WriteLine("||||||||||||||||||||||||||||");
                Console.WriteLine(duration);
                //filtering duration data and using in diiferent way
                if (duration == 0)
               {
                   duration = 1;
                }

                if (duration < 0)
                {
                    duration = duration - (duration * 2);
                    Console.WriteLine("less than 0"+duration);
                    if (duration > 4)
                    {
                        duration = 24;
                        Console.WriteLine("more than 4:"+duration);
                    }

                }
                if (duration > 4)
                {
                    duration = 24;
                    Console.WriteLine("more than 4:" + duration);
                }

                //getting datas and assigning in textboxes
                string timeperiod = duration.ToString();
                Console.WriteLine(PriceTB.Text);
                durationTB.Text = timeperiod;
                outTimeTB.Text = outTime.TimeOfDay.ToString();

                //getting data from PriceSearch method and assigning in price text box
                PriceTB.Text = PriceSearch(agegroup, groupno, duration, Visitorsdate).ToString();
                string data1 = Utility.ReadToText();
                var lstPeople = JsonConvert.DeserializeObject<List<VisitorsDetail>>(data1);
                List<VisitorsDetail> myList = lstPeople;
            }
            catch (Exception)
            {

            }
        }

        //method data return price
        private int PriceSearch(string age, string groupOf, int duration, DateTime week)
        {
          
            try
            {
                string PriceData = Utility.ReadToTextPrice();
                if (PriceData != null)
                {
                   var lstTicket = JsonConvert.DeserializeObject<List<PriceDetails>>(PriceData);  // desearilizing the data
                    Console.WriteLine("D CHECK");

                    //checking age
                    if (age== "Child")
                        {
                        //checking weekend or weekdays
                        if (week.DayOfWeek.ToString() != "Saturday")
                        {
                            PriceDetails groupAgeData = lstTicket.Where(n => n.Group == int.Parse(groupOf) && n.Duration == duration).FirstOrDefault();
                             return groupAgeData.WeekDays1;
                        }
                        else
                        {
                            PriceDetails groupAgeData = lstTicket.Where(n => n.Group == int.Parse(groupOf) && n.Duration == duration).FirstOrDefault();
                            return groupAgeData.WeekEnds1;
                        }

                    }
                    else if (age == "Adult")
                    {
                        if (week.DayOfWeek.ToString() != "Saturday")
                        {
                            PriceDetails groupAgeData = lstTicket.Where(n => n.Group == int.Parse(groupOf) && n.Duration == duration).FirstOrDefault();
                            return groupAgeData.WeekDays2;
                        }
                        else
                        {
                            PriceDetails groupAgeData = lstTicket.Where(n => n.Group == int.Parse(groupOf) && n.Duration == duration).FirstOrDefault();
                            return groupAgeData.WeekEnds2;
                        }

                    }

                }
                else
                {
                    Console.WriteLine("CHECK");
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private void update()
        {
            DataGridViewRow d = dataGridViewInOut.Rows[indexRow];

            //assigining agegroup and groupof data from cells
            string ageGroup = d.Cells[5].Value.ToString();
            string groupOf = d.Cells[7].Value.ToString();

            //getting textbox datas and assining in new variables
            string id = VisitorsIDTB.Text;
            string VisitorsName = VisitorsNameTB.Text;
            string inTime = inTimeTB.Text;
            DateTime outTime = DateTime.Parse(outTimeTB.Text);
            int duration = int.Parse(durationTB.Text);
            string vid = d.Cells[1].Value.ToString();

            string data1 = Utility.ReadToText();
            var lstPeople = JsonConvert.DeserializeObject<List<VisitorsDetail>>(data1);
            List<VisitorsDetail> myList = lstPeople;

            VisitorsDetail updData = lstPeople
            .Where(n => n.VisitorsID == int.Parse(vid)).FirstOrDefault();

            int Price = PriceSearch(ageGroup, groupOf, duration, outTime);

            //after getting updtaed datas, making new object of VisitorsDetail and assigning the data in it
            VisitorsDetail temp = new VisitorsDetail();
            temp.VisitorsName = updData.VisitorsName;
            temp.indateTime = updData.indateTime;
            temp.VisitorsDateTime = updData.VisitorsDateTime;
            temp.GroupNo = int.Parse(groupOf);
            temp.PhoneNo = updData.PhoneNo;
            temp.AgeGroup = updData.AgeGroup;
            temp.VisitorsID = updData.VisitorsID;
            temp.PhoneNo = updData.PhoneNo;
            temp.Price = Price;
            temp.outdateTime = outTime.TimeOfDay.ToString();
            temp.Duration = duration.ToString();
            temp.Day = updData.Day;

         //REMOVING OLD DATA
            myList.Remove(updData);
            //ADDING NEW DATA
            myList.Add(temp);

            string myData = JsonConvert.SerializeObject(myList);
            Utility.WriteToText(myData);
            BindGridToCheckOut();
        }


        private void reportBtn_Click(object sender, EventArgs e)
        {
            //calling method 
            update();
        }
    }
}
