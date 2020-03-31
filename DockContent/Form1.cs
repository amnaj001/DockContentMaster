using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DockContentMaster
{
    public partial class Form1 : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        readonly CommonUse ConndbAS = new CommonUse();
        readonly StringBuilder sb = new StringBuilder();
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
            ShowTableOdoo();
        }
        private void ShowTable()
        {
            sb.Remove(0, sb.Length);
            sb.Append("SELECT * FROM tbInvoice");
            string sqlInv = sb.ToString();
            dgvInv.DataSource = ConndbAS.ExecuteReader(sqlInv);
            FormatGridView();
        }
        private void FormatGridView()
        {
            if (dgvInv.RowCount > 0)
            {
                dgvInv.Columns[0].HeaderText = "Invoice No.";
                dgvInv.Columns[0].Width = 100;
            }
        }

        private void ShowTableOdoo()
        {
            sb.Remove(0, sb.Length);
            sb.Append("SELECT * FROM account_invoice");
            string sqlInv = sb.ToString();
            dgvInv.DataSource = ConndbAS.ExecuteReaderOdoo(sqlInv);
            //FormatGridView();
        }
    }
}
