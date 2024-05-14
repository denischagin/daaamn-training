using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace govno.Navigations
{
    public static class Navigation
    {
        public static void Navigate (Page page, Page pageTo)
        {
            MainWindow mainWindow = Window.GetWindow(page) as MainWindow;
            mainWindow.navigationFrame.Navigate(pageTo);
        }
    }
}
