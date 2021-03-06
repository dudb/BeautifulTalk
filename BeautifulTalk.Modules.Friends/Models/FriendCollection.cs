﻿using BeautifulTalkInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Models
{
    public class FriendCollection : ObservableCollection<Friend>, ITotalUnReadCount
    {
        private Int32 m_nTotalUnReadCount;
        public Int32 TotalUnReadCount
        {
            get { return this.m_nTotalUnReadCount; }
            set
            {
                this.m_nTotalUnReadCount = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("TotalUnReadCount"));
            }
        }
    }
}
