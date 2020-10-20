using System;
using System.Windows;
using VkStatus.ViewModels;

namespace VkStatus.Views
{
    public partial class VkUserView : Window
    {
        public VkUserView()
        {
            InitializeComponent();
            DataContext = new VkUserViewModel();
        }
    }
}
