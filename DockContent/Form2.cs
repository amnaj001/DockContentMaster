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
    public partial class Form2 : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        readonly CommonUse ConndbAS = new CommonUse();
        readonly StringBuilder sb = new StringBuilder();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ShowTableOdoo();
        }
        private void ShowTableOdoo()
        {
            sb.Remove(0, sb.Length);
            sb.Append("SELECT id,name,street,vat FROM res_partner");
            string sqlInv = sb.ToString();
            dgvInv.DataSource = ConndbAS.ExecuteReaderOdoo(sqlInv);
            //FormatGridView();
        }
    }
}
