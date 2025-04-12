using System.Collections.ObjectModel;

namespace WPFIconsApp
{
    public class IconItem
    {
        public string Title { get; set; } = string.Empty;
        public string AvatarImage { get; set; } = string.Empty;
        public ObservableCollection<string> IconImages { get; set; } = new ObservableCollection<string>();
        public string Description { get; set; } = string.Empty;
    }
}
