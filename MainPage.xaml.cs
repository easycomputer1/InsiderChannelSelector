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
        RegistryKey keyExa = Registry.CurrentUser;
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
            messageShow();
            
        }
       private void dev()
        {
            setIns("Dev", "Dev Channel",2);
            Doing.IsActive = false;
          
        }
        private void beta()
        {
            setIns("Beta", "Beta Channel",4);
        }
        private void rp()
        {
            setIns("ReleasePreview", "Release Preview Channel",8);
        }
        private void none()
        {

        }
        private async void messageShow()
        {
            var msgS = new Windows.UI.Popups.MessageDialog("配置完成！请重启电脑") { Title = "notice!" };
            await msgS.ShowAsync();
        }
        private void setIns(string channelSet,string Fancy,int BRL)
        {
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Orchestrator", true).SetValue("EnableUUPScan", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\SLS\\Programs\\Ring%Ring%", true).SetValue("Enabled", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\SLS\\Programs\\WUMUDCat", true).SetValue("WUMUDCATEnabled", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("EnablePreviewBuilds", "2", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("BranchName", channelSet, RegistryValueKind.String);
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
            key.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate", true).SetValue("BranchReadinessLevel", BRL, RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Visibility", true).SetValue("UIHiddenElements_Rejuv", "65534", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Visibility", true).SetValue("UIDisabledElements_Rejuv", "65535", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("UIRing", "External", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("UIContentType", "Mainline", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("UIBranch",Channel, RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("UIOptin", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("RingBackup", "External", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("RingBackupV2", "External", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("BranchBackup", Channel, RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Cache", true).SetValue("PropertyIgnoreList", "AccountsBlob;;CTACBlob;FlightIDBlob;ServiceDrivenActionResults", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Cache", true).SetValue("RequestedCTACAppIds", "WU;FSS", RegistryValueKind.String);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Account", true).SetValue("SupportedTypes", "3", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Account", true).SetValue("Status", "8", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\Applicability", true).SetValue("UseSettingsExperience", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("AllowFSSCommunications", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("UICapabilities", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("IgnoreConsolidation", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("MsaUserTicketHr", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("MsaDeviceTicketHr", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("SValidateOnlineHr", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("LastHR", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("ErrorState", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("PilotInfoRing", "3", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("RegistryAllowlistVersion", "4", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\ClientState", true).SetValue("FileAllowlistVersion", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI", true).SetValue("UIControllableState", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("UIDialogConsent", "0", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("UIUsage", "26", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("OptOutState", "25", RegistryValueKind.DWord);
            key.OpenSubKey("SOFTWARE\\Microsoft\\WindowsSelfHost\\UI\\Selection", true).SetValue("AdvancedToggleState", "24", RegistryValueKind.DWord);
            key.OpenSubKey("SYSTEM\\Setup\\WindowsUpdate", true).SetValue("AllowWindowsUpdate", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SYSTEM\\Setup\\MoSetup", true).SetValue("AllowUpgradesWithUnsupportedTPMOrCPU", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SYSTEM\\Setup\\LabConfig", true).SetValue("BypassRAMCheck", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SYSTEM\\Setup\\LabConfig", true).SetValue("BypassSecureBootCheck", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SYSTEM\\Setup\\LabConfig", true).SetValue("BypassStorageCheck", "1", RegistryValueKind.DWord);
            key.OpenSubKey("SYSTEM\\Setup\\LabConfig", true).SetValue("BypassTPMCheck", "1", RegistryValueKind.DWord);
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PCHC", true).SetValue("UpgradeEligibility", "1", RegistryValueKind.DWord);
            
        }

    }
}
