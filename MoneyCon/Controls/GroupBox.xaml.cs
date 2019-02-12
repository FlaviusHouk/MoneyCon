using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MoneyCon.Controls
{
    public class GroupBox : UserControl
    {
        public static readonly AvaloniaProperty HeaderProperty = AvaloniaProperty.Register<GroupBox, string>("Header", "Header");

        public string Header
        {
            get
            {
                return GetValue(HeaderProperty) as string;
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public GroupBox()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
