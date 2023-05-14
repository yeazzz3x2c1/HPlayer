
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HPlayer
{
    /// <summary>
    /// PlayerList.xaml 的互動邏輯
    /// </summary>
    public delegate void ItemDoubleClickHandler(object sender, PlayerItemEventArgs e);
    public class PlayerItemEventArgs : EventArgs
    {
        public PlayerListItem Item;
        public PlayerItemEventArgs(PlayerListItem Item) => this.Item = Item;
    }
    public partial class PlayerList : UserControl
    {
        public bool CanSelected { get; set; } = true;
        public bool MultiSelected { get; set; } = false;
        public event ItemDoubleClickHandler ItemDoubleClick;
        public List<PlayerListItem> Items { get; } = new List<PlayerListItem>();
        public List<PlayerListItem> AllSelected { get; } = new List<PlayerListItem>();
        Brush _DefaultItemForeground = new SolidColorBrush(Color.FromArgb(0xFF, 0xCA, 0xC9, 0xCF));
        public PlayerListItem LastDoubleClickItem = null;
        public PlayerListItem this[int index] { get { return index == -1 ? null : Items[index]; } }
        public Brush DefaultItemForeground
        {
            get => _DefaultItemForeground;
            set
            {
                if (value == _DefaultItemForeground) return;
                _DefaultItemForeground = value;
            }
        }
        Brush _DefaultItemBackground = new SolidColorBrush(Colors.Transparent);
        public Brush DefaultItemBackground
        {
            get => _DefaultItemBackground;
            set
            {
                if (value == _DefaultItemBackground) return;
                _DefaultItemBackground = value;
            }
        }
        public double ScrollMinimumHeight
        {
            get => Scroll.MinimumHeight;
            set => Scroll.MinimumHeight = value;
        }
        public Brush ScrollColor
        {
            get => Scroll.ScrollBackground;
            set => Scroll.ScrollBackground = value;
        }
        public Brush ScrollBackground
        {
            get => Scroll.Background;
            set => Scroll.Background = value;
        }
        public double ScrollSpeed
        {
            get => Scroll.ScrollSpeed;
            set => Scroll.ScrollSpeed = value;
        }

        public PlayerList()
        {
            InitializeComponent();
            Scroll.Visibility = Visibility.Collapsed;
            Scroll.Value = 0;
            Loaded += new RoutedEventHandler(OnLoaded);
            Scroll.OnValueChange += new ValueChangeHandler(ScrollValueChange);
            ListContainer.MouseWheel += new MouseWheelEventHandler(OnMouseWheel); //Lock Mouse Wheel
        }
        public void Add_Scroll_Force(int Force)
        {
            Scroll.AddForce(Force);
        }
        void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            Scroll.AddForce(e.Delta);
        }
        void ScrollValueChange(object sender, EventArgs e)
        {
            ListContainer.Margin = new Thickness(0, -Scroll.Value, 0, 0);
        }
        void OnLoaded(object sender, RoutedEventArgs e)
        {
        }
        PlayerListItem GetNewItem(string Path, bool Replace_Display_Path, double Height)
        {
            if (Path == "") return null;
            PlayerListItem s = new PlayerListItem(Path, Replace_Display_Path) { Height = Height, Foreground = _DefaultItemForeground, Background = _DefaultItemBackground, Margin = new Thickness(0, -1, 0, 0), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Top, BorderThickness = new Thickness(0, 1, 0, 1), BorderBrush = new SolidColorBrush(Color.FromArgb(255, 220, 220, 221)) };
            s.MouseDoubleClick += (ss, ee) =>
            {
                LastDoubleClickItem = s;
                ItemDoubleClick?.Invoke(this, new PlayerItemEventArgs(s));
            };
            s.PreviewMouseDown += (ss, ee) =>
            {
                if (!MultiSelected) ClearSelected(ss as PlayerListItem);
                if (CanSelected) s.Selected = !s.Selected;
            };
            return s;
        }

        bool ShowScroll = false;
        void AddSelected(PlayerListItem Item)
        {
            if (!AllSelected.Contains(Item))
                AllSelected.Add(Item);
        }
        void RemoveSelected(PlayerListItem Item)
        {
            if (AllSelected.Contains(Item))
                AllSelected.Remove(Item);
        }
        public void Add(string Path)
        {
            Add(Path, true, 45.298);
        }
        public void Add(string Path, bool Replace_Display_Path)
        {
            Add(Path, Replace_Display_Path, 45.298);
        }
        public void Add(string Path, bool Replace_Display_Path, double Height)
        {
            if (Path == "") return;
            PlayerListItem s = GetNewItem(Path , Replace_Display_Path, Height);
            s.OnSelectedChange += (sender, arg) =>
            {
                if (arg)
                    AddSelected(sender);
                else
                    RemoveSelected(sender);
            };
            Items.Add(s);
            ListContainer.Children.Add(s);
            UpdateUI();
        }
        public void Insert(int index, string Path)
        {
            PlayerListItem s = GetNewItem(Path , true, 45.298);
            Items.Insert(index, s);
            ListContainer.Children.Insert(index, s);
            UpdateUI();
        }
        void UpdateUI()
        {
            ListContainer.UpdateLayout();
            ShowScroll = ListContainer.ActualHeight > main.ActualHeight;
            Scroll.MaxValue = ListContainer.ActualHeight - main.ActualHeight;
            Scroll.HeightPercent = main.ActualHeight / ListContainer.ActualHeight;
            Scroll.Visibility = ShowScroll ? Visibility.Visible : Visibility.Collapsed;
        }
        public void Remove(PlayerListItem Item)
        {
            if (Item == null) return;
            if (Items.Contains(Item))
            {
                RemoveSelected(Item);
                ListContainer.Children.Remove(Item);
                Items.Remove(Item);
                if (LastDoubleClickItem == Item) LastDoubleClickItem = null;
            }
            GC.Collect();
            UpdateUI();
        }
        public void Remove(string path)
        {
            if (path == "") return;
            foreach (PlayerListItem i in Items)
            {
                if (i.Path == path)
                {
                    RemoveSelected(i);
                    ListContainer.Children.Remove(i);
                    Items.Remove(i);
                    if (LastDoubleClickItem == i) LastDoubleClickItem = null;
                    break;
                }
            }
            GC.Collect();
            UpdateUI();
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Items.Count) return;
            if (LastDoubleClickItem == Items[index]) LastDoubleClickItem = null;
            RemoveSelected(Items[index]);
            ListContainer.Children.RemoveAt(index);
            Items.RemoveAt(index);
            GC.Collect();
            UpdateUI();
        }
        public void Clear()
        {
            Scroll.Value = 0;
            for (int i = Items.Count - 1; i > -1; i--)
            {
                RemoveSelected(Items[i]);
                ListContainer.Children.RemoveAt(i);
                Items.RemoveAt(i);
            }
            GC.Collect();
            Scroll.Visibility = Visibility.Hidden;
        }
        public void ClearSelected(PlayerListItem Ignore = null)
        {
            List<PlayerListItem> Buffer = new List<PlayerListItem>();
            foreach (PlayerListItem i in AllSelected)
                if (i != Ignore) Buffer.Add(i);
            foreach (PlayerListItem i in Buffer)
                i.Selected = false;
            Buffer.Clear();
            Buffer = null;
        }
    }
}
