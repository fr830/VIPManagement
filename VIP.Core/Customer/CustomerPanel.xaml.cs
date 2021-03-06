﻿using Easy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VIP.Core.Customer
{
    /// <summary>
    /// CustomerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerPanel : UserControl
    {
        ICustomerService service;
        public CustomerPanel()
        {
            service = Loader.CreateInstance<ICustomerService>();
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            listPanel.Service(service);
            listPanel.EditClick += (s, ee) =>
            {
                var wind = new ModelWindow();
                wind.Height = 220;
                wind.Service(service);
                wind.Model = s;
                wind.Title = (wind.Model as CustomerEntity).FirstName;
                if (wind.ShowDialog() ?? false)
                {
                    listPanel.Reload();
                }
            };
            listPanel.DeleteClick += (s, ee) =>
            {
                service.Delete((s as CustomerEntity).ID);
            };
            Button add = new Button();
            add.Template = FindResource("ButtonIcon") as ControlTemplate;
            add.DataContext = new { Source = new Uri("/VIP.Core;component/Images/plus.png", UriKind.Relative) };
            add.Content = "添加";
            add.Foreground = new SolidColorBrush(Colors.White);
            add.Click += (s, ee) =>
            {
                var wind = new ModelWindow();
                wind.Title = "添加联系人";
                wind.Service(service);
                wind.ModelType = typeof(CustomerEntity);
                wind.Height = 220;
                if (wind.ShowDialog() ?? false)
                {
                    listPanel.Reload();
                }
            };
            listPanel.AddToToolBar(add);
        }
    }
}
