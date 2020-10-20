using VkStatus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VkStatus.ViewModels
{
    public class VkUserViewModel : Notifier
    {
        private string url;
        public ObservableCollection<VkUser> Users { get; set; }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                NotifyPropertyChanged();
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      var user = new VkUser()
                      {
                          Url = this.Url
                      };
                      Users.Add(user);
                      Url = "";
                  },
                 (obj) => !string.IsNullOrEmpty(Url)));
            }
        }

        public VkUserViewModel()
        {
            Users = new ObservableCollection<VkUser>();
        }
    }
}
