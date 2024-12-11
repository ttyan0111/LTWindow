using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LTWINDOW_
{
    public partial class Form1 : Form
    {
        SQL sql;
        public Form1()
        {
            InitializeComponent();
            sql = new SQL();
            sql.OpenConnection();
            LoadRevenueChart();
        }



        private void LoadRevenueChart()
        {
            
            DataTable data = sql.GetOrderSummary();

            // Cấu hình biểu đồ
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Ngày";
            chart1.ChartAreas[0].AxisY.Title = "Tổng Tiền";

            // Tạo series
            Series series = new Series
            {
                Name = "Doanh Thu",
                ChartType = SeriesChartType.StackedBar, // Dạng cột
                XValueType = ChartValueType.Date
            };

            chart1.Series.Add(series);

            // Thêm dữ liệu vào series
            foreach (DataRow row in data.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                double tongTien = Convert.ToDouble(row["TongTien"]);
                series.Points.AddXY(ngay, tongTien);
            }
        }





    
    }
}
