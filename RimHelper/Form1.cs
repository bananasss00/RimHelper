using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using IPCInterface;
using IPCInterface.Rows;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace RimHelper
{
    public partial class Form1 : Form
    {
        private const string SelectedLanguageFileName = "Language";
        private string LanguageChangedMessage;

        public Form1()
        {
            InitializeComponent();

            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgv,
                new object[] {true});

            foreach (var lang in Directory.GetFiles("Languages", "*.xml"))
            {
                string name = Path.GetFileNameWithoutExtension(lang);
                языкToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(name, null, delegate
                {
                    File.WriteAllText(SelectedLanguageFileName, name);
                    MessageBox.Show($"{LanguageChangedMessage}");
                    Environment.Exit(0);
                }));
            }

            string langName = "English";
            if (File.Exists(SelectedLanguageFileName))
                langName = File.ReadAllText(SelectedLanguageFileName);

            InitializeGui($"Languages\\{langName}.xml");
        }

        void InitializeGui(string langFileName)
        {
            if (!Localization.TryLoadFromFile(langFileName, out Exception ex))
            {
                MessageBox.Show($"Error: {ex.Message}", langFileName);
                Environment.Exit(1);
            }

            LanguageChangedMessage = "Message_LangChanged".Translate();
            инцидентыToolStripMenuItem.Text = "Menu_Incidents".Translate();
            pawnHediffsToolStripMenuItem.Text = "Menu_PawnHediffs".Translate();
            отображениеКолонокToolStripMenuItem.Text = "Menu_VisibleColumns".Translate();
            скрытьПустыеСтолбцыToolStripMenuItem.Text = "Menu_HideNullColumns".Translate();
            корзинаToolStripMenuItem.Text = toolStripMenuItem1.Text = "Menu_Cart".Translate();
            добавитьВКорзинуToolStripMenuItem.Text = "Menu_AddInCart".Translate();
            удалитьИзКорзиныToolStripMenuItem.Text = "Menu_RemoveFromCart".Translate();
            очиститьКорзинуToolStripMenuItem.Text = "Menu_ClearCart".Translate();
            языкToolStripMenuItem.Text = "Menu_Language".Translate();
            exportToolStripMenuItem.Text = "Menu_Export".Translate();
            exportTabToolStripMenuItem.Text = "Menu_ExportTab".Translate();
            exportAllTabsToolStripMenuItem.Text = "Menu_ExportAllTabs".Translate();
            injectDllAndRunToolStripMenuItem.Text = "Menu_InjectDllAndRun".Translate();

            new ToolTip().SetToolTip(btnResetCache, "Button_ClearTabsCache_Tooltip".Translate());

            ColumnFilter.btnAllText = "Button_All".Translate();
            ColumnFilter.btnNothingText = "Button_Nothing".Translate();

            button2.Text = "Button_OpenFilterForm".Translate();

            var tabs = cbTabs.Items;
            // Materials
            tabs.Add(new RimHelper.TabControl<Material>("Tab_Materials".Translate(), () =>
                    IPC.StateCallback(State.GetMaterials, () => IPC.GetObjectBuf<List<Material>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // WeaponsRanged
            tabs.Add(new RimHelper.TabControl<WeaponRanged>("Tab_WeaponsRanged".Translate(), () =>
                    IPC.StateCallback(State.GetWeaponsRanged, () => IPC.GetObjectBuf<List<WeaponRanged>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // WeaponsMelee
            tabs.Add(new RimHelper.TabControl<WeaponMelee>("Tab_WeaponsMelee".Translate(), () =>
                    IPC.StateCallback(State.GetWeaponsMelee, () => IPC.GetObjectBuf<List<WeaponMelee>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Apparels
            tabs.Add(new RimHelper.TabApparel("Tab_Apparel".Translate(), () =>
                    IPC.StateCallback(State.GetApparels, () => IPC.GetObjectBuf<List<Apparel>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Buildings From Materials
            tabs.Add(new RimHelper.TabControl<BuildingsFromMaterial>("Tab_BuildingsFromMaterial".Translate(), () =>
                    IPC.StateCallback(State.GetBuildingsFromMaterials,
                        () => IPC.GetObjectBuf<List<BuildingsFromMaterial>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Animals
            tabs.Add(new RimHelper.TabControl<Animal>("Tab_Animals".Translate(), () =>
                    IPC.StateCallback(State.GetAnimals, () => IPC.GetObjectBuf<List<Animal>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Debuffs
            tabs.Add(new RimHelper.TabControl<Debuff>("Tab_Debuffs".Translate(), () =>
                    IPC.StateCallback(State.GetDebuffs, () => IPC.GetObjectBuf<List<Debuff>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Drugs
            tabs.Add(new RimHelper.TabControl<Drug>("Tab_Drugs".Translate(), () =>
                    IPC.StateCallback(State.GetDrugs, () => IPC.GetObjectBuf<List<Drug>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // BodyParts
            tabs.Add(new RimHelper.TabControl<BodyPart>("Tab_BodyParts".Translate(), () =>
                    IPC.StateCallback(State.GetBodyParts, () => IPC.GetObjectBuf<List<BodyPart>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Facilities
            tabs.Add(new RimHelper.TabControl<Facility>("Tab_Facilities".Translate(), () =>
                    IPC.StateCallback(State.GetFacilities, () => IPC.GetObjectBuf<List<Facility>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Plants
            tabs.Add(new RimHelper.TabControl<Plant>("Tab_Plant".Translate(), () =>
                    IPC.StateCallback(State.GetPlants, () => IPC.GetObjectBuf<List<Plant>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Backstorys
            tabs.Add(new RimHelper.TabControl<Backstory>("Tab_Backstorys".Translate(), () =>
                    IPC.StateCallback(State.GetBackstorys, () => IPC.GetObjectBuf<List<Backstory>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Traits
            tabs.Add(new RimHelper.TabControl<Trait>("Tab_Traits".Translate(), () =>
                    IPC.StateCallback(State.GetTraits, () => IPC.GetObjectBuf<List<Trait>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // CEAmmos
            tabs.Add(new RimHelper.TabControl<CEAmmo>("Tab_CEAmmos".Translate(), () =>
                    IPC.StateCallback(State.GetCEAmmos, () => IPC.GetObjectBuf<List<CEAmmo>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            // Tools
            tabs.Add(new RimHelper.TabControl<ST_Tool>("Tab_Tools".Translate(), () =>
                    IPC.StateCallback(State.GetTools, () => IPC.GetObjectBuf<List<ST_Tool>>()),
                dgv, comboBox1, textBox4, pictureBox1));

            Localization.LoadColumns();
            Localization.Finalize();
        }

        #region IPC_GUI

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            IPC.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool active = IPC.Active;
            if (!active)
                IPC.Connect();

            label1.Text = active ? "Online" : "Offline";
            label1.ForeColor = active ? Color.Green : Color.Red;
            cbTabs.Enabled = active;
            инцидентыToolStripMenuItem.Enabled = active;
            pawnHediffsToolStripMenuItem.Enabled = active;
            дампМатериаловИХарактеристикToolStripMenuItem.Enabled = active;
            дампОружияИОдеждыToolStripMenuItem.Enabled = active;
            injectDllAndRunToolStripMenuItem.Enabled = active;
            gCCollectToolStripMenuItem.Enabled = active;
        }

        void GetActiveIncidents()
        {
            IPC.StateCallback(State.GetActiveIncidents, () => MessageBox.Show(IPC.StringBuf));
        }

        ITab GetCurrentTab() => cbTabs.SelectedItem as ITab;

        #endregion

        private void cbTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCurrentTab()?.Bind();
            toolStripMenuItem1.Checked = false;
            добавитьВКорзинуToolStripMenuItem.Visible = !toolStripMenuItem1.Checked;
            удалитьИзКорзиныToolStripMenuItem.Visible = toolStripMenuItem1.Checked;
            GetCurrentTab()?.ActivateCart(toolStripMenuItem1.Checked);

            button2.Visible = GetCurrentTab() is TabApparel;
            button4.Visible = GetCurrentTab() is TabApparel;
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            textBox4.Text = GetCurrentTab()?.GetSelectedDescription()?.Description;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetCurrentTab()?.SelectedColumnIsNumeric() ?? false)
            {
                tbFilter1.Text = "0";
                tbFilter2.Text = "9999";
                tbFilter2.Visible = true;
                tbFilter1.Size = new Size(55, 20);
            }
            else
            {
                tbFilter1.Text = "";
                tbFilter2.Text = "";
                tbFilter2.Visible = false;
                tbFilter1.Size = new Size(103, 20);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool? isNumeric = GetCurrentTab()?.SelectedColumnIsNumeric();
            if (isNumeric == null)
                return;

            if ((bool) isNumeric)
            {
                if (float.TryParse(tbFilter1.Text, out float min) && float.TryParse(tbFilter2.Text, out float max))
                    GetCurrentTab()?.EquinFiltering(min, max);
            }
            else
            {
                GetCurrentTab()?.EquinFiltering(tbFilter1.Text);
            }
        }

        private void отображениеКолонокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ColumnFilter(dgv).ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            добавитьВКорзинуToolStripMenuItem.Visible = !toolStripMenuItem1.Checked;
            удалитьИзКорзиныToolStripMenuItem.Visible = toolStripMenuItem1.Checked;
            GetCurrentTab()?.ActivateCart(toolStripMenuItem1.Checked);
        }

        private void добавитьВКорзинуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTab()?.AddSelectedInToCart();
        }

        private void удалитьИзКорзиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTab()?.RemoveSelectedFromCart();
        }

        private void очиститьКорзинуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTab()?.ClearCart();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GetCurrentTab()?.Debug();
        }

        private void дампМатериаловИХарактеристикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.BuildingStuffDump, () => MessageBox.Show("Finished"));
        }

        private void скрытьПустыеСтолбцыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentTab()?.HideNullColumns();
        }

        private void очиститьКешToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GetCurrentTab()?.ClearCache();
        }

        private void btnResetCache_Click(object sender, EventArgs e)
        {
            foreach (var item in cbTabs.Items)
            {
                (item as ITab).ClearCache();
            }
        }

        private void дампОружияИОдеждыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.WeaponApparelDump, () => MessageBox.Show(IPC.StringBuf));
        }

        private void injectDllAndRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new InjectDllForm().ShowDialog();
        }

        void AddExcelTab(ExcelPackage p, string tabName)
        {
            ExcelWorksheet ws = p.Workbook.Worksheets.Add(tabName);

            //Write header
            int col = 1, row = 1;
            foreach (DataGridViewColumn dgvColumn in dgv.Columns)
            {
                if (!dgvColumn.Visible)
                    continue;

                ExcelRange cell = ws.Cells[1, col];
                cell.Value = dgvColumn.HeaderText.Replace(" ", Environment.NewLine);

                col++;
            }


            //Write rows
            row = 2;
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                col = 1;

                for (int i = 0; i < dgvRow.Cells.Count; ++i)
                {
                    if (!dgv.Columns[i].Visible)
                        continue;

                    ExcelRange cell = ws.Cells[row, col];
                    cell.Value = dgvRow.Cells[i].Value;
                    if (cell.Value is float)
                    {
                        cell.Value = Math.Round((float) cell.Value, 2); // auto precision
                        //cell.Style.Numberformat.Format = "#,##0.00";
                    }

                    if (col == 1)
                    {
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }

                    col++;
                }

                row++;
            }

            ws.Cells.AutoFitColumns(10f);
            ws.View.FreezePanes(2, 2);

            int rowCount = ws.Dimension.End.Row;
            int colCount = ws.Dimension.End.Column;

            // Create table style
            {
                //ExcelRange headerRow = ws.Cells[1, 1, 1, colCount];
                //headerRow.AutoFilter = true;
                //headerRow.Style.Font.Bold = true;
                //headerRow.Style.WrapText = true;
                //headerRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //headerRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //headerRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //headerRow.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                //headerRow.Style.Indent = 3;

                ExcelTable table = ws.Tables.Add(ws.Cells[1, 1, rowCount, colCount], tabName);
                table.HeaderRowCellStyle = "HeaderStyle";
                table.TableStyle = TableStyles.Medium27; //Light16,Light21
                table.ShowTotal = true;

                // Centered values
                ws.Cells[2, 2, rowCount, colCount].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Add databars
            if (false)
            {
                for (int i = 2; i <= colCount; ++i)
                {
                    var rule = ws.ConditionalFormatting.AddDatabar(ws.Cells[2, i, rowCount, i], Color.Blue);
                    rule.StopIfTrue = true;
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void инцидентыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetActiveIncidents();
        }

        private void pawnHediffsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            IPC.StateCallback(State.GetPawnsHeddifs, () => MessageBox.Show(IPC.StringBuf));
        }

        private void exportTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tab = GetCurrentTab();
            if (tab == null)
                return;

            using (var p = new ExcelPackage())
            {
                var headerStyle = p.Workbook.Styles.CreateNamedStyle("HeaderStyle");
                headerStyle.Style.Font.Bold = true;
                headerStyle.Style.WrapText = true;
                headerStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                headerStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerStyle.Style.Fill.BackgroundColor.SetColor(Color.DarkSlateGray);
                headerStyle.Style.Font.Color.SetColor(Color.White);

                var tabName = GetSafeFilename(GetCurrentTab().ToString()).Replace(" ", "_");//GetCurrentTab().ToString().Replace(" ", "_");
                AddExcelTab(p, tabName);

                string curtDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string fileName = $"{curtDir}\\table_{tabName}_{DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss")}.xlsx";

                p.SaveAs(new FileInfo(fileName));
                System.Diagnostics.Process.Start(fileName);
            }
        }

        private void exportAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var p = new ExcelPackage())
            {
                var headerStyle = p.Workbook.Styles.CreateNamedStyle("HeaderStyle");
                headerStyle.Style.Font.Bold = true;
                headerStyle.Style.WrapText = true;
                headerStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                headerStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerStyle.Style.Fill.BackgroundColor.SetColor(Color.DarkSlateGray);
                headerStyle.Style.Font.Color.SetColor(Color.White);

                for (int k = 0; k < cbTabs.Items.Count; ++k)
                {
                    cbTabs.SelectedIndex = k;
                    GetCurrentTab()?.Bind();

                    var tabName = GetSafeFilename(GetCurrentTab().ToString()).Replace(" ", "_");//GetCurrentTab().ToString().Replace(" ", "_");
                    AddExcelTab(p, tabName);
                }

                string curtDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string fileName = $"{curtDir}\\table_{DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss")}.xlsx";

                p.SaveAs(new FileInfo(fileName));
                System.Diagnostics.Process.Start(fileName);
            }
        }

        public string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GetCurrentTab() is TabApparel tab)
            {
                new ApparelFilterForm(tab).ShowDialog();

                bool cartIsActive = tab.CartIsActive();
                toolStripMenuItem1.Checked = cartIsActive;
                добавитьВКорзинуToolStripMenuItem.Visible = !cartIsActive;
                удалитьИзКорзиныToolStripMenuItem.Visible = cartIsActive;
            }
        }

        private void gCCollectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.GcCollect, () =>
            {
                //MessageBox.Show("Finish");
                return 0;
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetCurrentTab()?.ResetFilters();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (GetCurrentTab() is TabApparel tab)
            {
                var form = new ApparelFilterMaterials(tab);
                if (form.ShowDialog() == DialogResult.OK)
                    tab.ResetFilters();
            }
        }
    }
}