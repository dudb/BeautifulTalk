using BeautifulTalkInfrastructure.Validator;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Models
{
    public class RequiredInfoModel : BindableBase, IDataErrorInfo
    {
        private string m_strId;
        private string m_strPassword;
        private string m_strNickName;
        public string Id
        {
            get { return this.m_strId; }
            set { SetProperty(ref this.m_strId, value); }
        }
        public string Password
        {
            get { return this.m_strPassword; }
            set { SetProperty(ref this.m_strPassword, value); }
        }
        public string NickName
        {
            get { return this.m_strNickName; }
            set { SetProperty(ref this.m_strNickName, value); }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string strErrorMsg = string.Empty;

                switch (columnName)
                {
                    case "Id":
                        {
                            if (false == IDvalidator.Validate(m_strId))
                            {
                                strErrorMsg = "Please input your id.\nId should be a kind of e-mail.\nex) service@shop.com"; 
                            }
                            break;
                        }
                    case "Password":
                        {
                            if (false == PasswordValidator.Validate(m_strPassword)) { strErrorMsg = "Please input your password."; }
                            break;
                        }
                    case "NickName":
                        {
                            if (string.IsNullOrEmpty(this.m_strNickName)) { strErrorMsg = "Please input your Nickname."; }
                            break;
                        }
                    default: break;
                }

                return strErrorMsg;
            }
        }
    }
}
