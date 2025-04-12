using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WPFIconsApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IconItem? _selectedIconItem;
        private string _currentBigImage = string.Empty;

        public ObservableCollection<IconItem> IconItems { get; set; }

        public IconItem? SelectedIconItem
        {
            get => _selectedIconItem;
            set
            {
                if (_selectedIconItem != value)
                {
                    _selectedIconItem = value;
                    OnPropertyChanged(nameof(SelectedIconItem));
                    if (_selectedIconItem != null)
                    {
                        CurrentBigImage = _selectedIconItem.AvatarImage;
                    }
                }
            }
        }

        public string CurrentBigImage
        {
            get => _currentBigImage;
            set
            {
                if (_currentBigImage != value)
                {
                    _currentBigImage = value;
                    OnPropertyChanged(nameof(CurrentBigImage));
                }
            }
        }

        public ICommand ImageClickCommand { get; set; }

        public MainViewModel()
        {
            IconItems = new ObservableCollection<IconItem>
            {
                new IconItem
                {
                    Title = "Белые котята",
                    AvatarImage = "Images/avatar1.png",
                    Description = "Описание белых котят",
                    IconImages = new ObservableCollection<string>
                    {
                        "Images/img1_1.png",
                        "Images/img1_2.png",
                        "Images/img1_3.png",
                        "Images/img1_4.png"
                    }
                },
                new IconItem
                {
                    Title = "Серые котята",
                    AvatarImage = "Images/avatar2.png",
                    Description = "Описание серых котят",
                    IconImages = new ObservableCollection<string>
                    {
                        "Images/img2_1.png",
                        "Images/img2_2.png",
                        "Images/img2_3.png",
                        "Images/img2_4.png"
                    }
                },
                new IconItem
                {
                    Title = "Рыжие котята",
                    AvatarImage = "Images/avatar3.png",
                    Description = "Описание рыжих котят",
                    IconImages = new ObservableCollection<string>
                    {
                        "Images/img3_1.png",
                        "Images/img3_2.png",
                        "Images/img3_3.png"
                    }
                },
                new IconItem
                {
                    Title = "Чёрные котята",
                    AvatarImage = "Images/avatar4.png",
                    Description = "Описание чёрных котят",
                    IconImages = new ObservableCollection<string>
                    {
                        "Images/img4_1.png",
                        "Images/img4_2.png",
                        "Images/img4_3.png"
                    }
                }
            };

            ImageClickCommand = new RelayCommand(param =>
            {
                if (param is string imagePath)
                {
                    foreach (var item in IconItems)
                    {
                        if (item.AvatarImage == imagePath || item.IconImages.Contains(imagePath))
                        {
                            SelectedIconItem = item;
                            break;
                        }
                    }
                    CurrentBigImage = imagePath;
                }
            });

            if (IconItems.Count > 0)
            {
                SelectedIconItem = IconItems[0];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
