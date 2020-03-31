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
        Form1 F1 = null;
        Form2 F2 = null;
        public FrmMain()
        {
            InitializeComponent();
            var theme = new VS2015LightTheme();
            dockPanel1.Theme = theme;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //ShowDockContent();
            ShowForm1();
            ShowForm2();
        }

        public void ShowDockContent()
        {
            var dockContent = new Form1();
            var dockForm2 = new Form2();
            dockContent.Show(dockPanel1,DockState.Document);
            dockForm2.Show(dockPanel1, DockState.DockRight);

        }

        public void ShowForm1()
        {
            if (F1 == null || F1.Text == "")
            {
                F1 = new Form1();
                F1.Show(dockPanel1, DockState.Document);
            }
            else if (CheckOpened(F1.Text))
            {
                F1.Show(dockPanel1, DockState.Document);
                F1.Focus();
            }
           
        }

        public void ShowForm2()
        {
            if (F2 == null || F2.Text == "")
            {
                F2 = new Form2();
                F2.Show(dockPanel1, DockState.DockRight);
            }
            else if (CheckOpened(F2.Text))
            {
                F2.Show(dockPanel1, DockState.DockRight);
                F2.Focus();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowForm1();        
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if(frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowForm2();
        }
    }
}
