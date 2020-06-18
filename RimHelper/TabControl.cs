using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IPCInterface;
using IPCInterface.Extensions;

namespace RimHelper
{
    // Very shit stuff :D

    public interface ITab
    {
        string ToString();

        void Bind();

        void EquinFiltering(float min, float max);
        void EquinFiltering(string containStrings);
        void ResetFilters();
        void HideNullColumns();
        void ClearCache();

        bool? SelectedColumnIsNumeric();

        SelectedObject GetSelectedDescription();

        bool CartIsActive();
        void ActivateCart(bool state, bool forceUpdate = false);
        void AddSelectedInToCart();
        void RemoveSelectedFromCart();
        void ClearCart();

        void Debug();
    }

    class ComboBoxColumn
    {
        public string Name;
        public string DisplayName;
        public override string ToString() => DisplayName;
    }

    public class SelectedObject
    {
        public string Description;
        public string IconPath;
    }

    public class BindingListViewWithEvent<T> : BindingListView<T>
    {
        public BindingListViewWithEvent(IList list) : base(list)
        {
        }

        public BindingListViewWithEvent(IContainer container) : base(container)
        {
        }

        public delegate void OnListResetFn();

        public event OnListResetFn OnListReset;

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            // only for view tables. small optimize for select rows in big tables
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemMoved:
                    return;
            }

            base.OnListChanged(e);

            if (OnListReset != null && e.ListChangedType == ListChangedType.Reset)
            {
                OnListReset();
            }
        }

        public bool TryFind(string propertyName, object key, out int index)
        {
            index = Find(propertyName, key);
            return index != -1;
        }
    }

    // T - must have Property: Label, Description, TexturePath
    public class TabControl<T> : ITab
    {
        protected bool _cartActive;
        protected readonly List<T> _cartData;
        protected List<T> _cachedData;
        protected BindingListViewWithEvent<T> _bindingList;
        protected readonly string _tabName;
        protected readonly Dictionary<string, ColorizeOrderOption> _colorizeRules;
        protected readonly DataGridView _dgv;
        protected readonly ComboBox _cb;
        protected TextBox _tbDesc;
        protected PictureBox _pb;
        protected readonly Func<List<T>> _listGetterFn;

        public TabControl(string tabName, Func<List<T>> listGetterFn, DataGridView dataGridViewOutput,
            ComboBox comboBoxColumns, TextBox textBoxDescription, PictureBox pbIcon)
        {
            _tabName = tabName;
            _listGetterFn = listGetterFn;
            _dgv = dataGridViewOutput;
            _cb = comboBoxColumns;
            _tbDesc = textBoxDescription;
            _pb = pbIcon;
            _colorizeRules = GetColorizeRules();
            _cartData = new List<T>();
        }

        // Interface implement //
        public override string ToString() => _tabName;

        #region Cart

        public bool CartIsActive() => _cartActive;

        public void ActivateCart(bool state, bool forceUpdate = false)
        {
            bool reset = _cartActive != state;
            _cartActive = state;
            if (reset || forceUpdate) Bind();
        }

        public void AddSelectedInToCart()
        {
            if (!_cartActive)
            {
                IEnumerable<T> items = SelectedObjects;
                if (items != null)
                {
                    _cartData.AddRange(items);
                }
            }
        }

        public void AddInToCart(List<T> items)
        {
            _cartData.AddRange(items);
        }

        public void RemoveSelectedFromCart()
        {
            if (_cartActive)
            {
                IEnumerable<T> items = SelectedObjects;
                if (items != null)
                {
                    _cartData.RemoveAll(i => items.Contains(i));
                }
            }

            _bindingList.Refresh();
        }

        public void ClearCart()
        {
            _cartData.Clear();
            _bindingList.Refresh();
        }

        #endregion

        public void Bind()
        {
            if (_cachedData == null)
            {
                try
                {
                    _cachedData = new List<T>(_listGetterFn());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Tab parse error. Check RimWorld console!");
                    return;
                }
            }

            _bindingList = new BindingListViewWithEvent<T>(_cartActive ? _cartData : _cachedData);
            BindToUI();
        }

        public virtual void BindToUI()
        {
            _dgv.DataSource = _bindingList; //new BindingSource { DataSource = bindingList };
            _dgv.Columns["Id"].Visible = false;
            _dgv.Columns[0].Frozen = true;

            _cb.DataSource = _dgv.Columns.Cast<DataGridViewColumn>().Where(i => i.Name != "Id").Select(c => new ComboBoxColumn
            {
                Name = c.Name,
                DisplayName = c.HeaderText
            }).ToList();

            _bindingList.OnListReset += () =>
            {
                ColorizeDataGridView();
                //HideNullColumns();
            };

            ColorizeDataGridView();
        }

        public List<T> GetCachedList()
        {
            return _cachedData;
        }

        #region Filtering

        public virtual void EquinFiltering(float min, float max)
        {
            var columnName = (_cb.SelectedItem as ComboBoxColumn)?.Name;
            if (columnName == null)
                return;
            var prop = typeof(T).GetProperty(columnName);
            _bindingList.ApplyFilter(k =>
                prop.GetValue(k, null) != null && (float) prop.GetValue(k, null) >= min &&
                (float) prop.GetValue(k, null) <= max);
        }

        public virtual void EquinFiltering(string containStrings)
        {
            var columnName = (_cb.SelectedItem as ComboBoxColumn)?.Name;
            if (columnName == null)
                return;

            var prop = typeof(T).GetProperty(columnName);
            var strs = containStrings.Split(',').Select(s => s.ToLower());
            _bindingList.ApplyFilter(k => strs.Any(s =>
            {
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

        public List<T> GetFilteredList()
        {
            var filteredList = new List<T>();

            foreach (var item in _bindingList)
            {
                filteredList.Add(item);
            }

            return filteredList;
        }

        public virtual void ResetFilters()
        {
            _bindingList.RemoveFilter();
        }

        public void HideNullColumns()
        {
            for (int i = 0; i < _dgv.Columns.Count; i++)
            {
                bool columnIsNull = _dgv.Rows.Cast<DataGridViewRow>().Count(r => r.Cells[i].Value != null) == 0;
                if (columnIsNull) _dgv.Columns[i].Visible = false;
            }
        }

        public void ClearCache()
        {
            if (_cachedData != null)
            {
                _cachedData.Clear();
                _cachedData = null;
            }
        }

        #endregion

        public bool? SelectedColumnIsNumeric()
        {
            var columnName = (_cb.SelectedItem as ComboBoxColumn)?.Name;
            if (columnName == null)
                return null;
            return _dgv.Columns[columnName].ValueType == typeof(float?);
        }

        public SelectedObject GetSelectedDescription()
        {
            var obj = SelectedObject;
            if (obj == null)
                return null;

            return new SelectedObject
            {
                Description = (string) typeof(T).GetProperty("Description")?.GetValue(obj, null),
                IconPath = (string) typeof(T).GetProperty("TexturePath")?.GetValue(obj, null)
            };
        }

        public void Debug()
        {
            MessageBox.Show("SelectedObjects: " + SelectedObjects.ToList().Count);
        }


        /////////////////////
        // private members //
        /////////////////////
        private T SelectedObject
        {
            get
            {
                if (_dgv.CurrentRow == null ||
                    !_bindingList.TryFind("Id", _dgv.CurrentRow.Cells["Id"].Value, out int index))
                    return default;
                return _bindingList[index].Object;
            }
        }

        private IEnumerable<T> SelectedObjects
        {
            get
            {
                foreach (var row in _dgv.Rows.Cast<DataGridViewRow>().Where(r => r.Selected).OrEmptyIfNull())
                {
                    if (_bindingList.TryFind("Id", row.Cells["Id"].Value, out int index))
                        yield return _bindingList[index].Object;
                }
            }
        }

        protected void ColorizeDataGridView()
        {
            for (int i = 0; i < _dgv.ColumnCount; ++i)
            {
                var columnName = _dgv.Columns[i].Name;
                if (_dgv.Columns[i].ValueType == typeof(float?) && _colorizeRules[columnName] != ColorizeOrderOption.None)
                {
                    var columnCells = _dgv.Rows.Cast<DataGridViewRow>().Select(r => r.Cells[i]).ToList();
                    //try
                    {
                        var cellsNotNull = columnCells.Where(c => c.Value != null).ToList();
                        float? min = cellsNotNull.Min(c => (float?) c.Value);
                        float? max = cellsNotNull.Max(c => (float?) c.Value);
                        if (min != null && max != null)
                        {
                            foreach (var cell in columnCells)
                            {
                                var val = (float?) cell.Value;
                                if (val != null)
                                {
                                    if (_colorizeRules[columnName] == ColorizeOrderOption.Positive)
                                    {
                                        cell.Style.BackColor = val > 0 ? Color.Green : Color.Red;
                                    }
                                    else
                                    {
                                        int percent = max - min == 0 ? 100 : (int) ((100f * (val - min)) / (max - min));
                                        cell.Style.BackColor =
                                            GetColorFromPercentage(percent,
                                                _colorizeRules[columnName] == ColorizeOrderOption.Descending);
                                    }
                                }
                            }
                        }
                    }
                    //catch
                    {
                    }
                }
                else if (
                    _dgv.Columns[i].ValueType ==
                    typeof(bool) /* && colorizeRules[columnName] != ColorizeOrderOption.None*/)
                {
                    var columnCells = _dgv.Rows.Cast<DataGridViewRow>().Select(r => r.Cells[i]);
                    foreach (var cell in columnCells)
                    {
                        var val = (bool) cell.Value;
                        Color trueColor = Color.Green, falseColor = Color.Red;

                        if (_colorizeRules[columnName] == ColorizeOrderOption.Descending)
                        {
                            trueColor = Color.Red;
                            falseColor = Color.Green;
                        }

                        cell.Style.BackColor = val ? trueColor : falseColor;
                    }
                }
                else
                {
                    _dgv.Rows.Cast<DataGridViewRow>().Select(r => r.Cells[i].Style.BackColor = Color.White);
                }
            }
        }

        private static Dictionary<string, ColorizeOrderOption> GetColorizeRules()
        {
            var result = new Dictionary<string, ColorizeOrderOption>();
            var propsBrowsable = typeof(T).GetProperties().Where(prop =>
            {
                var attrs = prop.GetCustomAttributes(true);
                return attrs.Count(attr => (attr as BrowsableAttribute) != null) == 0;
            });

            foreach (var prop in propsBrowsable)
            {
                var colorizeAttr = prop.GetCustomAttributes(true)
                        .FirstOrDefault(attr => (attr as ColorizeOrderAttribute) != null)
                    as ColorizeOrderAttribute;
                result.Add(prop.Name, colorizeAttr?.Order ?? ColorizeOrderOption.None);
            }

            return result;
        }

        private static Color GetColorFromPercentage(int percentage, bool inversed = false)
        {
            const int rgbMax = 255; // Reduce this for a darker range
            const int rgbMin = 0; // Increase this for a lighter range
            // Work out the percentage of red and green to use (i.e. a percentage
            // of the range from RGB_MIN to RGB_MAX)
            var redPercent = Math.Min(200 - (percentage * 2), 100) / 100f;
            var greenPercent = Math.Min(percentage * 2, 100) / 100f;

            // Now convert those percentages to actual RGB values in the range
            // RGB_MIN - RGB_MAX
            var red = (int) (rgbMin + ((rgbMax - rgbMin) * redPercent));
            var green = (int) (rgbMin + ((rgbMax - rgbMin) * greenPercent));

            return !inversed ? Color.FromArgb(red, green, rgbMin) : Color.FromArgb(green, red, rgbMin);
        }
    }
}