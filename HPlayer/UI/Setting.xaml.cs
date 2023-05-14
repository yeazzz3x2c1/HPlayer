using ManagerTool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HPlayer
{
    /// <summary>
    /// Setting.xaml 的互動邏輯
    /// </summary>
    public partial class Setting : Window
    {
        string SettingName = "InfoSetting";
        Hashtable SettingTable = new Hashtable();
        Player Host;
        public Setting(Player Host)
        {
            this.Host = Host;
            Opacity = 0;
            InitializeComponent();
            Closing += (s, e) => { e.Cancel = true; CloseWindow(); };
            Loaded += (s, e) =>
            {
                Manager.LoadSetting(ref SettingTable, SettingName);
                foreach (string key in SettingTable.Keys)
                {
                    switch (key)
                    {
                        case "DontCheckUpdate":
                            DontCheckUpdate.IsChecked = bool.Parse(SettingTable[key].ToString());
                            break;
                        case "AutoUpdate":
                            AutoUpdate.IsChecked = bool.Parse(SettingTable[key].ToString());
                            break;
                    }
                }
                void OnCheckChange(object sender, EventArgs args)
                {
                    CheckBox ck = sender as CheckBox;
                    SettingTable[ck.Name] = ck.IsChecked;
                    Manager.SaveSetting(ref SettingTable, SettingName);
                    Host.connection.Send_Check_Version(DontCheckUpdate.IsChecked == true ? "false" : "true");
                }
                foreach (CheckBox i in SettingContainer.Children)
                {
                    i.Checked += OnCheckChange;
                    i.Unchecked += OnCheckChange;
                }
            };
            PowerOff.MouseDown += (s, e) => CloseWindow();
        }
        public void CloseWindow()
        {
            Opacity = 0;
            Hide();
        }
        public void OpenWindow()
        {
            Opacity = 1;
            Show();
        }
    }
}
