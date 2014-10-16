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
    public class LoginModel : BindableBase, IDataErrorInfo
    {
        private string m_strId;
        private string m_strPassword;
        private bool m_bIsAutoLogin;
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
        public bool IsAutoLogin 
        {
            get { return this.m_bIsAutoLogin; }
            set { SetProperty(ref this.m_bIsAutoLogin, value); }
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
                            if (false == IDvalidator.Validate(this.m_strId)) 
                            {
                                strErrorMsg = "Please input id of e-mail type."; 
                            }
                            break;
                        }
                    case "Password":
                        {
                            if (false == PasswordValidator.Validate(this.m_strPassword)) { strErrorMsg = "Please Input your password."; }
                            break;
                        }

                    default: break;
                }

                return strErrorMsg;
            }
        }
    }
}
