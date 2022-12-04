using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Win32;


// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace InsiderChannelSelector
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        RegistryKey key = Registry.LocalMachine;
        int build = Environment.OSVersion.Version.Build;

        private void Start(object sender, RoutedEventArgs e)
        {
            Doing.IsActive = true;
            int get = Channel.SelectedIndex;
            if (get == 0)
            {
                dev();
            }
            else if (get == 1)
            {
                beta();
            }
            else if (get == 2)
            {
                rp();
            }
            else if (get == 3)
            {
                none();
            }
            
            
        }
       private void dev()
        {
            setIns("dev");
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("BranchName", "Dev", RegistryValueKind.String);
        }
        private void beta()
        {
            setIns("beta");
        }
        private void rp()
        {
            setIns("release preview");
        }
        private void none()
        {

        }
        private void setIns(string channelSet)
        {
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Orchestrator", true).SetValue("EnableUUPScan", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\SLS\\Programs\\Ring%Ring%", true).SetValue("Enabled", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\SLS\\Programs\\WUMUDCat", true).SetValue("WUMUDCATEnabled", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("EnablePreviewBuilds", "2", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("IsBuildFlightingEnabled", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("IsConfigSettingsFlightingEnabled", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("IsConfigExpFlightingEnabled", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("TestFlagsTestFlags", "32", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("RingId", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("Ring", "External", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("ContentType", "Mainline", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\SLS\\Programs\\Ring%Ring%", true).SetValue("Enabled", "1", RegistryValueKind.DWord);
            if (build < 21990)
            {
                key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Strings", true).SetValue("StickyXaml", "<StackPanel xmlns=\"^\"\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"^\"\">"+
                    "<TextBlock Text=\"^\"\"当前预览体验计划\"^\"\" Margin=\"^\"\"0,20,0,10\"^\"\" Style=\"^\"\"{StaticResource SubtitleTextBlockStyle}\"^\"\" />" +
                        "<TextBlock Style=\"^\"\"{StaticResource BodyTextBlockStyle }\"^\"\" Margin=\"^\"\"0,0,0,5\"^\"\">" +
                            "<Run FontFamily=\"^\"\"Segoe MDL2 Assets\"^\"\">&#xECA7;</Run> " +
                            "<Span FontWeight=\"^\"\"SemiBold\"^\"\">" + channelSet +"</Span>" +
                        "</TextBlock>" +
                        "<TextBlock Text=\"^\"\"Telemetry settings notice\"^\"\" Margin=\"^\"\"0,20,0,10\"^\"\" Style=\"^\"\"{StaticResource SubtitleTextBlockStyle}\"^\"\" />" +
                        "<TextBlock Style=\"^\"\"{StaticResource BodyTextBlockStyle }\"^\"\">当前更新由<Hyperlink NavigateUri=\"^\"\"https://github.com/abbodi1406/offlineinsiderenroll\"^\"\" TextDecorations=\"^\"\"None\"^\"\">InsiderChannelSelector</Hyperlink>控制，您可以通过InsiderChannelSelector更改渠道。Windows预览体验计划要求您的电脑诊断数据 <Span FontWeight=\"^\"\"SemiBold\"^\"\">完全打开</Span>！" +
                        "点击下面的按钮以查看更多消息</TextBlock>" +
                    "<Button Command=\"^\"\"{StaticResource ActivateUriCommand}\"^\"\" CommandParameter=\"^\"\"ms-settings:privacy-feedback\"^\"\" Margin=\"^\"\"0,10,0,0\"^\"\">" +
                        "<TextBlock Margin=\"^\"\"5,0,5,0\"^\"\">诊断与反馈</TextBlock>" +
                    "</Button>" +
                    "</StackPanel>", RegistryValueKind.String);

            }
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Visibility", true).SetValue("UIHiddenElements", "65535", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Visibility", true).SetValue("UIDisabledElements", "65535", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Visibility", true).SetValue("UIServiceDrivenElementVisibility", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Visibility", true).SetValue("UIErrorMessageVisibility", "192", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection", true).SetValue("AllowTelemetry", "3", RegistryValueKind.DWord);
        }

    }
}
