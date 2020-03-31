using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.ThemeVS2015;

namespace DockContentMaster
{
    public partial class FrmMain : Form
    {      
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {   
            ShowDockContent();
        }

        public void ShowDockContent()
        {
            var dockContent = new Form1();
            var dockForm2 = new Form2();
            dockContent.Show(dockPanel1,DockState.Document);
            dockForm2.Show(dockPanel1, DockState.DockRight);

        }

        public void ShowForm2()
        {
            var dockForm2 = new Form2();
            dockForm2.Show(dockPanel1, DockState.DockRight);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowDockContent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowForm2();
        }
    }
}
