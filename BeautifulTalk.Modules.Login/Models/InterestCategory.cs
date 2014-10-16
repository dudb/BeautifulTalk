using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Login.Models
{
    public class InterestCategory : BindableBase
    {
        private bool m_bIsSelected;
        public bool IsSelected
        {
            get { return this.m_bIsSelected; }
            set { SetProperty(ref m_bIsSelected, value); }
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] BaseThumbnail { get; set; }

        public InterestCategory() { }
        public InterestCategory(string strTitle, string strDescription, byte[] baseThumbnail)
        {
            this.Title = strTitle;
            this.Description = strDescription;
            this.BaseThumbnail = baseThumbnail;
        }
    }
}
