using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkStatus.Utils;

namespace VkStatus.Models
{
    public class VkUser : Notifier
    {
        #region Fields
        private string url;
        private string status;
        #endregion

        #region Properties
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                Status = VkUtils.GetStatus(url);
                NotifyPropertyChanged();
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Functions

        #endregion
    }
}
