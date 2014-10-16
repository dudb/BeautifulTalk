using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BeautifulTalk.Modules.Chatting.Controls
{
    public class ChattingInputBoxControl : ContentControl
    {
        private TextBox m_InputTextBox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_InputTextBox = GetTemplateChild("InputTextBox") as TextBox;

            if (null == m_InputTextBox) throw new NullReferenceException("InputTextBox");
        }

        public void ClearInput()
        {
            if (null != m_InputTextBox) m_InputTextBox.Clear();
        }
    }
}
