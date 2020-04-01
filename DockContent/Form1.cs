using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Npgsql;

namespace DockContentMaster
{
    public partial class Form1 : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        readonly CommonUse ConndbAS = new CommonUse();
        readonly StringBuilder sb = new StringBuilder();
        NpgsqlConnection conn;
        NpgsqlDataReader dr;
        NpgsqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sb.Remove(0, sb.Length);
            sb.Append("SELECT * FROM tbInvoice");
            string sqlInv = sb.ToString();
            comboBox1.BeginUpdate();
            comboBox1.DisplayMember = "InvNo";
            comboBox1.ValueMember = "InvNo";
            comboBox1.DataSource = ConndbAS.ExecuteReader(sqlInv);
            comboBox1.EndUpdate();
            //ShowTable();
            //ShowTableOdoo();
            SumInvoice();
        }
        private void ShowTable()
        {
            //sb.Remove(0, sb.Length);
            //sb.Append("SELECT * FROM tbInvoice");
            //string sqlInv = sb.ToString();
            //dgvInv.DataSource = ConndbAS.ExecuteReader(sqlInv);
            //FormatGridView();
        }

        private void FormatGridView()
        {
            //if (dgvInv.RowCount > 0)
            //{
            //    dgvInv.Columns[0].HeaderText = "Invoice No.";
            //    dgvInv.Columns[0].Width = 100;
            //}
        }
     
        private void SumInvoice()
        {
            sb.Remove(0, sb.Length);
            sb.Append("SELECT to_char(date(date_invoice),'yyyy') as year_sales, SUM(amount_total) as sales_value");
            sb.Append(" FROM account_invoice");
            sb.Append(" WHERE date_invoice is not null");
            sb.Append(" GROUP BY 1");
            sb.Append(" ORDER BY 1");
            string sqlSum = sb.ToString();
            //dgvInv.DataSource = ConndbAS.ExecuteReaderOdoo(sqlSum);

            //Chart
            conn = ConndbAS.PgConnect();
            cmd = new NpgsqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = sqlSum,
                Connection = conn
            };
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                chart1.Series.Clear();
                try
                {
                    string newSeries = "year_sales";
                    Series series = chart1.Series.Add(newSeries);
                    while (dr.Read())
                    { 
                        series.Points.AddXY(dr["year_sales"].ToString(),dr["sales_value"].ToString());
                        series.ChartType = SeriesChartType.Column;
                        series.IsValueShownAsLabel = true;
                        series.LabelFormat = "#,##0.00";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dr.Close();
        }

    }
}
