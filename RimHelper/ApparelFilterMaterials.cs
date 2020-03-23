using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using IPCInterface;
using IPCInterface.Rows;

namespace RimHelper
{
    public partial class ApparelFilterMaterials : Form
    {
        public ApparelFilterMaterials(TabApparel tab)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            checkBox1.Checked = TabApparel.ShowDefStuff;
            // get materials list
            {
                var stuffs = tab.GetCachedList()
                    .Where(x => !String.IsNullOrEmpty(x.Material))
                    .Select(x => x.Material)
                    .OrderBy(x => x)
                    .Distinct();

                checkedListBox1.DataSource = stuffs.ToList();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    string stuffName = (string) checkedListBox1.Items[i];
                    if (!TabApparel.StuffActiveState.TryGetValue(stuffName, out bool state))
                    {
                        TabApparel.StuffActiveState[stuffName] = state = true;
                    }
                    checkedListBox1.SetItemChecked(i, state);
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string stuffName = checkedListBox1.Items[e.Index].ToString();
            if (stuffName == null)
                throw new Exception("can't get checked stuff");
            TabApparel.StuffActiveState[stuffName] = e.NewValue == CheckState.Checked;
        }

        private void всеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; ++i)
                checkedListBox1.SetItemChecked(i, true);
        }

        private void ничегоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; ++i)
                checkedListBox1.SetItemChecked(i, false);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TabApparel.ShowDefStuff = checkBox1.Checked;

            checkedListBox1.Enabled = !checkBox1.Checked;
        }
    }
}
