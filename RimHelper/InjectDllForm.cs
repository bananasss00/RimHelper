using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using IPCInterface;
using Mono.Cecil;

namespace RimHelper
{
    public partial class InjectDllForm : Form
    {
        public InjectDllForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string newFileName = Guid.NewGuid().ToString();//Path.GetRandomFileName()
            string pathToDll = Path.GetDirectoryName(textBox1.Text) + "\\" + newFileName;
            string pathToReportTxt = pathToDll + ".txt";

            var asm = AssemblyDefinition.ReadAssembly(textBox1.Text);
            asm.Name = new AssemblyNameDefinition(newFileName, Version.Parse("1.0.0.0"));
            asm.Write(pathToDll);
            asm.Dispose();

            string functionResult = IPC.StateCallback(State.InjectDll, () => IPC.StringBuf, new InjectParameters
            {
                dllPath = pathToDll,
                dllClass = "DebugLibrary.__Class",
                dllFunction = "__Func"
            });

            try { File.Delete(pathToDll); } catch {}

            if (functionResult == "null")
            {
                MessageBox.Show("Finished with 'null' result");
                return;
            }

            File.WriteAllText(pathToReportTxt, functionResult);

            if (String.IsNullOrEmpty(textBox2.Text))
                Process.Start(pathToReportTxt);
            else
                Process.Start(textBox2.Text, pathToReportTxt);
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

        private void InjectDllForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(SettingsFile))
            {
                using (FileStream fs = new FileStream(SettingsFile, FileMode.Open))
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(Settings));
                    var obj = (Settings) _xSer.Deserialize(fs);
                    textBox1.Text = obj.dllPath;
                    textBox2.Text = obj.openWithPath;
                }
            }
        }

        private void InjectDllForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FileStream fs = new FileStream(SettingsFile, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(Settings));
                xSer.Serialize(fs, new Settings {dllPath = textBox1.Text, openWithPath = textBox2.Text});
            }
        }

        public class Settings
        {
            public string dllPath;
            public string openWithPath;
        }

        public static readonly string SettingsFile = "DEBUG_InjectDll.xml";
    }
}
