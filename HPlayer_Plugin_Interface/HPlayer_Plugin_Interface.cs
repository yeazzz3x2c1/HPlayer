using System.Windows.Controls;

namespace HPlayer_Plugin_Manager
{
    public interface HPlayer_Plugin_Interface
    {
        string Plugin_Name();
        void On_Enabled();
        void On_Disabled();
        void On_Now_Song_Name_Update(string Song_Name);
        void On_Total_Time_Update(string Total_Time);
        void On_Now_Time_Update(string Now_Time);

        UserControl Get_Control();
    }
}
