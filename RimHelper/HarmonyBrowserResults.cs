using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IPCInterface.HarmonyBrowser;

namespace RimHelper
{
    public partial class HarmonyBrowserResults : Form
    {
        private BindingListView<ReportRecord> bindList;

        public HarmonyBrowserResults(List<ReportRecord> report)
        {
            InitializeComponent();

            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgv,
                new object[] { true });

            bindList = new BindingListView<ReportRecord>(report);
            dgv.DataSource = bindList;
            bindList.ApplySort("avgTimeTick DESC");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                bindList.RemoveFilter();
            }
            else if (int.TryParse(textBox1.Text, out int ticks))
            {
                bindList.ApplyFilter(r => r.num >= ticks);
            }
        }
    }
}
