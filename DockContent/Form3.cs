using Npgsql;
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
    public partial class Form3 : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        readonly CommonUse ConndbAS = new CommonUse();
        readonly StringBuilder sb = new StringBuilder();
        NpgsqlConnection conn;
        NpgsqlDataReader dr;
        NpgsqlCommand cmd;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ShowTableOdoo();
        }
        private void ShowTableOdoo()
        {
            sb.Remove(0, sb.Length);
            sb.Append("SELECT acc_inv.date_invoice,acc_inv.number,rp.name,acc_inv.amount_untaxed,acc_inv.amount_tax,acc_inv.amount_total");
            sb.Append(" FROM account_invoice acc_inv");
            sb.Append(" LEFT JOIN res_partner rp");
            sb.Append(" ON acc_inv.partner_id = rp.id");
            sb.Append(" WHERE acc_inv.type='out_invoice' AND acc_inv.state<>'cancel'");
            sb.Append(" ORDER BY acc_inv.date_invoice DESC");
            string sqlInv = sb.ToString();
            dgvInv.DataSource = ConndbAS.ExecuteReaderOdoo(sqlInv);
            //FormatGridView();
        }
    }
}
