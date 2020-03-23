using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IPCInterface.Rows;

namespace RimHelper
{
    public class TabApparel : RimHelper.TabControl<Apparel>
    {
        public TabApparel(string tabName, Func<List<Apparel>> listGetterFn, DataGridView dataGridViewOutput, ComboBox comboBoxColumns, TextBox textBoxDescription, PictureBox pbIcon) : base(tabName, listGetterFn, dataGridViewOutput, comboBoxColumns, textBoxDescription, pbIcon)
        {
        }

        public static bool ShowDefStuff { get; set; } = true;

        public static Dictionary<string, bool> StuffActiveState = new Dictionary<string, bool>();

        public override void BindToUI()
        {
            //_bindingList.RemoveFilter();

            if (!StuffActiveState.Any())
            {
                var stuffs = GetCachedList()
                    .Where(x => !String.IsNullOrEmpty(x.Material))
                    .Select(x => x.Material)
                    .Distinct().ToList();
                var defStuffs = GetCachedList()
                    .Where(x => !String.IsNullOrEmpty(x.DefMaterial))
                    .Select(x => x.DefMaterial)
                    .Distinct().ToList();

                StuffActiveState = stuffs.ToDictionary(x => x, y => defStuffs.Contains(y));
                
            }

            ResetFilters();

            base.BindToUI();
        }
        public override void ResetFilters()
        {
            _bindingList.ApplyFilter(k =>
            {
                if (ShowDefStuff)
                {
                    if (k.Material?.Equals(k.DefMaterial) ?? true)
                        return true;
                }
                else
                {
                    if (k.Material == null || StuffActiveState[k.Material])
                        return true;
                }

                return false;
            });
        }

        public override void EquinFiltering(float min, float max)
        {
            var columnName = (_cb.SelectedItem as ComboBoxColumn)?.Name;
            if (columnName == null)
                return;
            var prop = typeof(Apparel).GetProperty(columnName);
            _bindingList.ApplyFilter(k =>
            {
                if (ShowDefStuff)
                {
                    if (k.Material?.Equals(k.DefMaterial) ?? false)
                        return false;
                }
                else
                {
                    if (k.Material != null && !StuffActiveState[k.Material])
                        return false;
                }

                if (prop.GetValue(k, null) != null && (float) prop.GetValue(k, null) >= min &&
                    (float) prop.GetValue(k, null) <= max) return true;
                return false;
            });
        }

        public override void EquinFiltering(string containStrings)
        {
            var columnName = (_cb.SelectedItem as ComboBoxColumn)?.Name;
            if (columnName == null)
                return;

            var prop = typeof(Apparel).GetProperty(columnName);
            var strs = containStrings.Split(',').Select(s => s.ToLower());
            _bindingList.ApplyFilter(k => strs.Any(s =>
            {
                if (ShowDefStuff)
                {
                    if (k.Material?.Equals(k.DefMaterial) ?? false)
                        return false;
                }
                else
                {
                    if (k.Material != null && !StuffActiveState[k.Material])
                        return false;
                }

                try
                {
                    string rowString = (string) prop.GetValue(k, null);
                    return rowString.ToLower().Contains(s);
                }
                catch
                {
                    return false;
                }
            }));
        }
    }
}