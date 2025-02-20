﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace AutoSignRobot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private UserControl activeWindow;
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
        }

        //Methods
        private string SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return color;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    string color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    BrushConverter conv = new BrushConverter();
                    Brush bru = conv.ConvertFromInvariantString(color) as Brush;
                    currentButton.Background = bru;
                    currentButton.Foreground = Brushes.White;
                    GridTopPanel.Background = bru;
                    
                    // Get TextBlock's text behind currentButton
                    // and Give it to HomeLabel
                    WrapPanel stp = currentButton.Content as WrapPanel;
                    TextBlock blk = stp.Children[1] as TextBlock;
                    LblHomeTile.Content = blk.Text.Trim();
                }
            }
        }

        private void DisableButton()
        {
            foreach (object previousBtn in StpColumn.Children)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    currentButton = (Button)previousBtn;
                    currentButton.Background = Brushes.Transparent;
                    currentButton.Foreground = Brushes.White;
                }
            }
        }

        private void OpenChildWindow(UserControl childWindow, object btnSender)
        {
            if (activeWindow != null)
            {
                this.DesktopGrid.Children.Remove(activeWindow);
            }
            ActivateButton(btnSender);
            activeWindow = childWindow;
            childWindow.Margin = new Thickness(0, 0, 0, 0);
            childWindow.VerticalAlignment = VerticalAlignment.Stretch;
            childWindow.HorizontalAlignment= HorizontalAlignment.Stretch;
            this.DesktopGrid.Children.Add(childWindow);
            this.DesktopGrid.Tag = childWindow;
            childWindow.BringIntoView();
        }

        private void BtnPartyWork_Click(object sender, RoutedEventArgs e)
        {
            OpenChildWindow(new PartyWork(), sender);
        }

        private void BtnProfessionalSign_Click(object sender, RoutedEventArgs e)
        {
            OpenChildWindow(new ProfessionalSignWork(), sender);
        }
        private void BtnTrainTime_Click(object sender, RoutedEventArgs e)
        {
            OpenChildWindow(new TrainTimeWork(), sender);
        }
    }
}
