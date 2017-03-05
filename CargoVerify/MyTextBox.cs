using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace CargoVerify
{
    public class MyTextBox : TextBox
    {
        /// <summary>
        /// 重写获取焦点事件：全部选择文本
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (string.IsNullOrEmpty(this.Text))
                return;
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Tick += delegate(object obj, EventArgs args)
            {
                SelectAll();
                timer.Dispose();
            };
        }

    }
}
