using System;
using System.Windows.Forms;

namespace RimHelper
{
    public partial class ColumnFilter : Form
    {
        public static string btnAllText { get; set; }
        public static string btnNothingText { get; set; }

        private readonly DataGridView _dgv;

        class CheckboxColumn
        {
            public string Name;
            public string DisplayName;
            public override string ToString() => DisplayName;
        }

        public ColumnFilter(DataGridView dgv)
        {
            InitializeComponent();
            this._dgv = dgv;

            checkedListBox1.Items.Clear();
            foreach (DataGridViewColumn c in dgv.Columns)
                checkedListBox1.Items.Add(new CheckboxColumn {DisplayName = c.HeaderText, Name = c.Name}, c.Visible);

            checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;

            выделитьВсеToolStripMenuItem.Text = btnAllText;
            снятьВыделениеСоВсехToolStripMenuItem.Text = btnNothingText;
        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = ((CheckboxColumn)checkedListBox1.Items[e.Index]).Name;//checkedListBox1.GetItemText();
            _dgv.Columns[item].Visible = e.NewValue == CheckState.Checked;
        }

        private void снятьВыделениеСоВсехToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; ++i)
                checkedListBox1.SetItemChecked(i, false);
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; ++i)
                checkedListBox1.SetItemChecked(i, true);
        }
    }
}
