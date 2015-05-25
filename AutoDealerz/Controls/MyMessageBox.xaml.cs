using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AutoDealerz.Controls
{
    public partial class MyMessageBox : UserControl
    {
        public delegate void CancelDelegate();
        public event CancelDelegate CancelAction;
        public delegate void OKDelegate();
        public event OKDelegate OKAction;

        public bool IsShow = false;
        string KeyCode = "";
        public MyMessageBox()
        {
            InitializeComponent();
            Story_hide.Completed += Story_hide_Completed;
        }

        public void Hide()
        {
            KeyCode = "";
            Story_hide.Begin();
        }
        public void Show(string title, string content, string str_cancel, string str_ok, int type)
        {
            KeyCode = "";
            IsShow = true;

            TB_title.Text = title;
            TB_content.Text = content;
            BT_cancel.Content = str_cancel;
            BT_ok.Content = str_ok;
            if(type==0)
            {
                BT_cancel.Visibility = System.Windows.Visibility.Collapsed;
                BT_ok.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            if (type == 1) BT_cancel.Visibility = System.Windows.Visibility.Collapsed;
            else
                BT_cancel.Visibility = System.Windows.Visibility.Visible;
            background.Visibility = System.Windows.Visibility.Visible;
            Story_show.Begin();

        }

        private void BT_ok_Click(object sender, RoutedEventArgs e)
        {
            Story_hide.Begin();
            KeyCode = "OK";
        }

        void Story_hide_Completed(object sender, EventArgs e)
        {
            IsShow = false;
            background.Visibility = System.Windows.Visibility.Collapsed;
            if (OKAction != null && KeyCode == "OK")
                OKAction();
            else if (CancelAction != null && KeyCode == "Cancel")
                CancelAction();
        }

        private void BT_cancel_Click(object sender, RoutedEventArgs e)
        {
            Story_hide.Begin();
            KeyCode = "Cancel";
        }

        private void background_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Story_hide.Begin();
            KeyCode = "Cancel";
        }
    }
}
