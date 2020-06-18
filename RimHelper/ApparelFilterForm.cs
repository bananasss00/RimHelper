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
    public partial class ApparelFilterForm : Form
    {
        private TabApparel tab;

        public ApparelFilterForm(TabApparel tab)
        {
            InitializeComponent();
            this.tab = tab;

            comboBox2.DataSource = typeof(Apparel).GetProperties().Where(x => x.PropertyType == typeof(float?)).Select(x =>
            {
                var localized = (LocalizedDisplayNameAttribute)x
                    .GetCustomAttributes(true)
                    .Where(a => a is LocalizedDisplayNameAttribute)
                    .FirstOrDefault();

                return new ComboBoxColumn
                {
                    Name = x.Name,
                    DisplayName = localized?.DisplayName
                };
            }).ToList();

            string langName = File.Exists("Language") ? File.ReadAllText("Language") : "English";
            string langFileName = $"Languages\\{langName}.xml";
            if (!Localization.TryLoadFromFile(langFileName, out Exception ex))
            {
                MessageBox.Show($"Error: {ex.Message}", langFileName);
                Environment.Exit(1);
            }

            comboBox1.Items.Add("Combo1_Item_WithRecipe".Translate());
            comboBox1.Items.Add("Combo1_Item_All".Translate());

            comboBox3.Items.Add("Combo2_Item_SortByDescending".Translate());
            comboBox3.Items.Add("Combo2_Item_SortByAscending".Translate());

            label2.Text = "Label_HasRecipe".Translate();
            label3.Text = "Label_FilterByParam".Translate();
            label4.Text = "Label_SortOrder".Translate();
            label1.Text = "Label_Depth".Translate();

            button1.Text = "Button_ApplyFilter".Translate();

            Localization.Finalize();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int bodyResultsCount))
            {
                List<string> allowedStuff = checkedListBox1.CheckedItems
                    .Cast<string>()
                    .ToList();
                List<Apparel> allItems = tab.GetCachedList()
                    .Where(x => String.IsNullOrEmpty(x.Material) || allowedStuff.Contains(x.Material))
                    .ToList();

                string fieldName = (comboBox2.SelectedItem as ComboBoxColumn)?.Name;

                List<Apparel> filtered = ApparelFilter.FilterApparels(allItems, fieldName, comboBox3.SelectedIndex == 0,
                    comboBox1.SelectedIndex == 0, bodyResultsCount);

                if (checkBox1.Checked)
                {
                    tab.ClearCart();
                }
                tab.AddInToCart(filtered);
                tab.ActivateCart(true, true);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid number!");
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string stuffName = checkedListBox1.Items[e.Index].ToString();
            if (stuffName == null)
                throw new Exception("can't get checked stuff");
            TabApparel.StuffActiveState[stuffName] = e.NewValue == CheckState.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
    }

    internal class ApparelFilter
    {
        private PropertyInfo _field;
        private List<Apparel> _allItems;

        private Dictionary<string, HashSet<string>> _allBodysOnLayers;

        public ApparelFilter(List<Apparel> list, string fieldName)
        {
            var prop = typeof(Apparel).GetProperty(fieldName);
            _field = prop ?? throw new Exception("Can't find Apparel property");
            _allItems = new List<Apparel>(list);

            // Get sorted layers and body for better results
            foreach (var ap in _allItems)
            {
                ap.Layer = SortJoinedString(ap.Layer);
                ap.Body = SortJoinedString(ap.Body);
            }

            // Get unique layers
            var allLayers = new HashSet<string>(list
                .Select(x => x.Layer)
                .Distinct());
        
            // Get unique bodys per layer
            _allBodysOnLayers = new Dictionary<string, HashSet<string>>();
            foreach (var layer in allLayers)
            {
                if (!_allBodysOnLayers.ContainsKey(layer))
                {
                    var uniqueBodys = new HashSet<string>(list
                        .Where(x => x.Layer == layer)
                        .Select(x => x.Body)
                        .Distinct());

                    _allBodysOnLayers.Add(layer, uniqueBodys);
                }
            }
        }

        private string SortJoinedString(string str) => String.Join(",", str.Split(',').OrderBy(x => x).ToArray());

        internal void SortApparelsByDescending()
        {
            _allItems = _allItems.OrderByDescending(x => _field.TryGetFloat(x, out float val) ? val : 0f)
                .ThenBy(x => x.Layer)
                .ThenBy(x => x.Body)
                .ToList();
        }

        internal void SortApparels()
        {
            _allItems = _allItems.OrderBy(x => _field.TryGetFloat(x, out float val) ? val : 0f)
                .ThenBy(x => x.Layer)
                .ThenBy(x => x.Body)
                .ToList();
        }

        internal List<Apparel> Filter(bool onlyCraftable, int bodyResultsCount)
        {
            List<Apparel> filtered = new List<Apparel>();

            foreach (var allBodysOnLayer in _allBodysOnLayers)
            {
                string layer = allBodysOnLayer.Key;
                foreach (var bodyOnLayer in allBodysOnLayer.Value)
                {
                    List<Apparel> apparelsWithCurrentBody = _allItems
                        .Where(x => x.Layer == layer
                                    && x.Body == bodyOnLayer
                                    && (!onlyCraftable || x.CanCraft)
                                    && _field.TryGetFloat(x, out float val))
                        .ToList();

                    int end = Math.Min(bodyResultsCount, apparelsWithCurrentBody.Count);
                    for (int i = 0; i < end; ++i)
                        filtered.Add(apparelsWithCurrentBody[i]);
                }
            }

            return filtered;
        }

        public static List<Apparel> FilterApparels(List<Apparel> list, string fieldName, bool orderByDescending, bool onlyCraftable, int bodyResultsCount)
        {
            ApparelFilter filter = new ApparelFilter(list, fieldName);

            if (orderByDescending)
                filter.SortApparelsByDescending();
            else
                filter.SortApparels();

            return filter.Filter(onlyCraftable, bodyResultsCount);
        }
    }

    internal static class PropertyInfoExtensions
    {
        internal static bool TryGetFloat(this PropertyInfo prop, object getFrom, out float value)
        {
            float? _value = (float?)prop.GetValue(getFrom, null);

            if (_value == null)
            {
                value = 0f;
                return false;
            }

            value = (float)_value;
            return true;
        }
    }
}
