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
    public partial class AdminReport : Form
    {

        public AdminReport()
        {
            InitializeComponent();

            //Calling different methods
            BindGridDayWeeklyReport();
            BindGridAgeDailyReport();
            LoadBarChart();
        }

        private void BindGridAgeDailyReport()
        {

            DateTime timePick = dateTimePicker1.Value.Date;

            //creating an object of VisitorsDetails class
            VisitorsDetail obj = new VisitorsDetail();

            //assigning as List data in "listVisitorsDetail"
            List<VisitorsDetail> listVisitorsDetail = obj.List();

            //checking if "listVisitorsDetail" is null or not
            if (listVisitorsDetail != null)
            {

                //using link to filter "VisitorsDetail" data and get specific data
                var groupByAgeData = listVisitorsDetail
                    .Where(n => n.VisitorsDateTime == timePick)
                    .GroupBy(n => n.AgeGroup)
                    .Select(n => new
                    {
                        AgeGroup = n.Key,
                        //getting group no and summing them
                        Visitor = n.Sum(n1 => n1.GroupNo).ToString()
                    }).ToList();

                //assigning "groupBNyAgeData" as data source for grid view
                dataGridViewAdmin.DataSource = groupByAgeData;

            }
        }
        private void BindGridDayWeeklyReport()
        {

            //getting start date and end date of the week as
            DateTime date = dateTimePicker2.Value;
            int year = date.Date.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DayOfWeek day = date.DayOfWeek;
            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekNo = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int days = (weekNo - 1) * 7;
            DateTime dt1 = firstDay.AddDays(days);
            DayOfWeek dow = dt1.DayOfWeek;
            DateTime startDateOfWeek = dt1.AddDays(-(int)dow);
            DateTime endDateOfWeek = startDateOfWeek.AddDays(6);


            PriceDetails pp = new PriceDetails();
            VisitorsDetail obj = new VisitorsDetail();
            String listVisitorsDetail = Utility.ReadToText();

           //getting a month from datetimepicker
            String timePick = dateTimePicker1.Value.Date.Month.ToString();
            
            if (listVisitorsDetail != null)
            {
                var lstPeople = JsonConvert.DeserializeObject<List<VisitorsDetail>>(listVisitorsDetail);
                var groupByDayData = lstPeople
                   .Where(n => n.VisitorsDateTime >= startDateOfWeek && n.VisitorsDateTime <= endDateOfWeek)
                    .GroupBy(n => n.Day)
                    .Select(n => new
                    {
                        Day = n.Key,
                        //getting visitors no,price and summing them
                        Visitor = n.Sum(n1 => n1.GroupNo).ToString(),
                        Price = n.Sum(n1 => n1.Price).ToString(),
                    }).ToList();
                //assigning "groupByDayData" as data source for grid view
                dataGridViewDayReport.DataSource = groupByDayData;
            }
        }

        private void sortByPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            int year = date.Date.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DayOfWeek day = date.DayOfWeek;
            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekNo = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int days = (weekNo - 1) * 7;
            DateTime dt1 = firstDay.AddDays(days);
            DayOfWeek dow = dt1.DayOfWeek;
            DateTime startDateOfWeek = dt1.AddDays(-(int)dow);
            DateTime endDateOfWeek = startDateOfWeek.AddDays(6);

            //   Visitors obj = new Visitors();
            string readData = Utility.ReadToText();
            var listVisitorsDetail = JsonConvert.DeserializeObject<List<VisitorsDetail>>(readData);
            // var list = obj.Sort(check);
            var sortData = listVisitorsDetail
                    .Where(n => n.VisitorsDateTime >= startDateOfWeek && n.VisitorsDateTime <= endDateOfWeek)
                    .GroupBy(n => n.Day)
                    .Select(n => new
                    {
                        Day = n.Key,
                        Visitor = n.Sum(n1 => n1.GroupNo).ToString(),
                        Price = n.Sum(n1 => n1.Price).ToString(),
                    }).ToList();
            //getting filtered data
            //after getting data, data is sort as

            if (sortByPicker.SelectedItem.ToString().Equals("Sort By Ascending"))
            {
                var finalList = AscendingSort(sortData);
                dataGridViewDayReport.DataSource = finalList;

            }
          else  if (sortByPicker.SelectedItem.ToString().Equals("Sort By Descending"))
            {

                var finalList = DescendingSort(sortData);
                dataGridViewDayReport.DataSource = finalList;

            }
            else
            {
                dataGridViewDayReport.DataSource = sortData;
            }

        }
        
        //method to sort weekly report descendingly
        public List<WeeklyReports> DescendingSort(dynamic VisitorsList)
        {

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(VisitorsList);

            List<WeeklyReports> list = JsonConvert.DeserializeObject<List<WeeklyReports>>(JsonString);

            if (JsonString != null)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (list[i].Price < list[j].Price)
                        {
                            WeeklyReports tempCustomer = list[i];
                            list[i] = list[j];
                            list[j] = tempCustomer;
                        }
                    }
                }
                return list;
            }
            return null;

        }

        //method to sort weekly report ascendingly
        public List<WeeklyReports> AscendingSort(dynamic VisitorsList)
        {

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(VisitorsList);

            List<WeeklyReports> list = JsonConvert.DeserializeObject<List<WeeklyReports>>(JsonString);

            if (JsonString != null)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (list[i].Price > list[j].Price)
                        {
                            WeeklyReports tempCustomer = list[i];
                            list[i] = list[j];
                            list[j] = tempCustomer;
                        }
                    }
                }
                return list;
            }
            return null;

        }

        //method to sort daily report of total earning ascendingly
        public List<DailyReports> AscendingSortDailyReport(dynamic VisitorsList)
        {

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(VisitorsList);

            List<DailyReports> list = JsonConvert.DeserializeObject<List<DailyReports>>(JsonString);

            if (JsonString != null)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (list[i].Visitor > list[j].Visitor)
                        {
                            DailyReports tempCustomer = list[i];
                            list[i] = list[j];
                            list[j] = tempCustomer;
                        }
                    }
                }
                return list;
            }
            return null;

        }

        //method to sort daily report of total earning descendingly
        public List<DailyReports> DescendingSortDailyReport(dynamic VisitorsList)
        {

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(VisitorsList);

            List<DailyReports> list = JsonConvert.DeserializeObject<List<DailyReports>>(JsonString);

            if (JsonString != null)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (list[i].Visitor < list[j].Visitor)
                        {
                            DailyReports tempCustomer = list[i];
                            list[i] = list[j];
                            list[j] = tempCustomer;
                        }
                    }
                }
                return list;
            }
            return null;

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //calling method whne value changed
            BindGridAgeDailyReport();

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //calling method whne value changed
            BindGridDayWeeklyReport();
        }
        private void LoadBarChart()
        {
            DateTime date = dateTimePicker3.Value;
            int year = date.Date.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DayOfWeek day = date.DayOfWeek;
            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekNo = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int days = (weekNo - 1) * 7;
            DateTime dt1 = firstDay.AddDays(days);
            DayOfWeek dow = dt1.DayOfWeek;
            DateTime startDateOfWeek = dt1.AddDays(-(int)dow);
            DateTime endDateOfWeek = startDateOfWeek.AddDays(6);

            string data1 = Utility.ReadToText();
            //   String timePick = dateTimePicker2.Value.Date.Month.ToString();
            var lstPeople = JsonConvert.DeserializeObject<List<VisitorsDetail>>(data1);
            var groupByAgeData = lstPeople
                    .Where(n => n.VisitorsDateTime >= startDateOfWeek && n.VisitorsDateTime <= endDateOfWeek)
                    .GroupBy(n => n.Day)
                    .Select(n => new
                    {
                        Day = n.Key,
                        Visitor = n.Sum(n1 => n1.GroupNo).ToString(),
                        Price = n.Sum(n1 => n1.Price).ToString(),
                    }).ToList();
            reportCHT.Series[0].LegendText = "Visitors Graph";
            reportCHT.Series[1].LegendText = "Earning Graph";
            reportCHT.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            reportCHT.Series[0].IsValueShownAsLabel = true;
            reportCHT.Series[0].XValueMember = "Day";
            reportCHT.Series[0].YValueMembers = "Visitor";

            reportCHT.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            reportCHT.Series[1].IsValueShownAsLabel = true;
            reportCHT.Series[1].XValueMember = "Day";
            reportCHT.Series[1].YValueMembers = "Price";
            reportCHT.DataSource = groupByAgeData;
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            //calling method whne value changed
            LoadBarChart();
        }

        private void sortingDailyReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sorting data according to the combo box data selected

            DateTime timePick = dateTimePicker1.Value.Date;
            VisitorsDetail obj = new VisitorsDetail();
            List<VisitorsDetail> listVisitorsDetail = obj.List();
            if (listVisitorsDetail != null)
            {
                var dailyReport = listVisitorsDetail
                    .Where(n => n.VisitorsDateTime == timePick)
                    .GroupBy(n => n.AgeGroup)
                    .Select(n => new
                    {
                        AgeGroup = n.Key,
                        Visitor = n.Sum(n1 => n1.GroupNo).ToString()
                    }).ToList();
                dataGridViewAdmin.DataSource = dailyReport;

            if (sortingDailyReport.SelectedItem.ToString().Equals("Sort By Ascending"))
            {
                var finalList = AscendingSortDailyReport(dailyReport);
                    dataGridViewAdmin.DataSource = finalList;

            }
            else if (sortingDailyReport.SelectedItem.ToString().Equals("Sort By Descending"))
            {
                var finalList = DescendingSortDailyReport(dailyReport);
                    dataGridViewAdmin.DataSource = finalList;
            }
            else
            {
                    dataGridViewAdmin.DataSource = dailyReport;
            }
        }
    }
    }
    public class WeeklyReports
    {
        public string Day { get; set; }
        public string Visitor { get; set; }
        public int Price { get; set; }
    }
    public class DailyReports
    {
        public string AgeGroup { get; set; }
        public int Visitor { get; set; }
    }

}
