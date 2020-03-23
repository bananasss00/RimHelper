using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using IPCInterface;
using IPCInterface.HarmonyBrowser;

namespace RimHelper
{
    public partial class HarmonyBrowserForm : Form
    {
        const string ReportPatchesName = "HarmonyPatches.txt";
        const string ReportProfilingName = "HarmonyProfiling.txt";

        public HarmonyBrowserForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string functionResult = IPC.StateCallback(State.GetAllHarmonyPatches, () => IPC.StringBuf);
            File.WriteAllText(ReportPatchesName, functionResult);

            if (String.IsNullOrEmpty(textBox2.Text))
                Process.Start(ReportPatchesName);
            else
                Process.Start(textBox2.Text, ReportPatchesName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.StartHarmonyProfiling, () => MessageBox.Show("harmony profiling initialised"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<ReportRecord> functionResult = IPC.StateCallback(State.GetHarmonyProfilingResults, () => IPC.GetObjectBuf<List<ReportRecord>>());
            new HarmonyBrowserResults(functionResult).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var hi = new HarmonyInstances();
            foreach (var item in listBox2.Items)
            {
                hi.List.Add((string)item);
            }

            hi.SkipMethodsInGenericClass = checkBox1.Checked;
            IPC.StateCallback(State.StartHarmonyPatchesProfiling, () => MessageBox.Show("harmony patches profiling initialised"), hi);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var hi = IPC.StateCallback(State.GetHarmonyInstances, () => IPC.GetObjectBuf<HarmonyInstances>());
            listBox1.DataSource = hi.List;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox1.SelectedItems)
            {
                listBox2.Items.Add(item);
            }
            
            //listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox1.Items)
            {
                listBox2.Items.Add(item);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.ResetHarmonyProfilingResults, () => MessageBox.Show("harmony profiling results resetted"));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.HarmonyProfilingUnpatchAll, () => MessageBox.Show("all unpatched!"));
        }


        public class Settings
        {
            public string openWithPath;
        }

        public static readonly string SettingsFile = "DEBUG_HarmonyBrowser.xml";

        private void HarmonyBrowserForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(SettingsFile))
            {
                using (FileStream fs = new FileStream(SettingsFile, FileMode.Open))
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(Settings));
                    var obj = (Settings)_xSer.Deserialize(fs);
                    textBox2.Text = obj.openWithPath;
                }
            }
        }

        private void HarmonyBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FileStream fs = new FileStream(SettingsFile, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(Settings));
                xSer.Serialize(fs, new Settings { openWithPath = textBox2.Text });
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var functions = textBox1.Text.Split(new []{ Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            IPC.StateCallback(State.StartGameProfiling, () => MessageBox.Show("game functions profiling initialised"), functions);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            IPC.StateCallback(State.StartGameProfilingTickerList, () => MessageBox.Show("ticker list functions profiling initialised"));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var hi = IPC.StateCallback(State.GetHarmonyInstances, () => IPC.GetObjectBuf<HarmonyInstances>());
            listBox3.DataSource = hi.List;
            listBox4.DataSource = null;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var unpatchInstances = new List<HarmonyUnpatch>();
            if (listBox4.SelectedItems.Count == 0)
            {
                foreach (string item in listBox3.SelectedItems)
                {
                    unpatchInstances.Add(new HarmonyUnpatch {instance = item});
                }
            }
            else
            {
                foreach (Pair item in listBox4.SelectedItems)
                {
                    var inst = unpatchInstances.FirstOrDefault(i => i.instance == item.Key);
                    if (inst != null)
                    {
                        inst.patches.Add(item.Value);
                    }
                    else
                    {
                        unpatchInstances.Add(new HarmonyUnpatch { instance = item.Key, patches = new List<string>(new [] {item.Value})});
                    }
                }
            }

            IPC.StateCallback(State.HarmonyUnpatchInstances, () => MessageBox.Show("unpatched"), unpatchInstances);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            List<string> instances = listBox3.SelectedItems.Cast<string>().ToList();
            listBox4.DataSource = IPC.StateCallback(State.GetHarmonyPatchesForInstances, () => IPC.GetObjectBuf<List<Pair>>(), instances);
        }
    }
}
