using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using System.Net.NetworkInformation;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {


    /// <summary>
    /// Interaction logic for ucSOP.xaml
    /// </summary>
    public partial class ucSOP : UserControl {

        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public ucSOP() {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            Thread t = new Thread(new ThreadStart(() => {
                while (true) {
                    string dir = string.Format(@"\\{0}\SOP\{1}\{2}", MyGlobal.MySetting.SOPServer, MyGlobal.MySetting.ProductName, MyGlobal.MySetting.StationName);
                    Dispatcher.Invoke(new Action(() => {
                        try {
                            lbl_videoDir.Content = lbl_documentDir.Content = dir;
                        } catch { }
                        
                    }));
                    PingReply r = null;
                    try {
                        r = new Ping().Send(MyGlobal.MySetting.SOPServer, 1000, Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
                    } catch { }
                    
                    if (r.Status == IPStatus.Success) {
                        Dispatcher.Invoke(new Action(() => {
                            try {
                                if (Directory.Exists(dir)) {
                                    //get lastest file
                                    lbl_documentFile.Content = new GetLatestFileInDirectory(dir, new string[] { "pdf" }).GetFileName();
                                    lbl_videoFile.Content = new GetLatestFileInDirectory(dir, new string[] { "mp4", "mkv", "avi" }).GetFileName(); ;
                                }
                                else {
                                    lbl_documentFile.Content = "null";
                                    lbl_videoFile.Content = "null";
                                }
                            } catch { }
                            
                        }));
                    }
                    else {
                        Dispatcher.Invoke(new Action(() => {
                            try {
                                lbl_documentFile.Content = "null";
                                lbl_videoFile.Content = "null";
                            } catch { }
                        }));
                    }

                    Thread.Sleep(1000);
                }
            }));
            t.IsBackground = true;
            t.Start();


        }


        private void timer_Tick(object sender, EventArgs e) {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider)) {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }


        private void TabItem_GotFocus(object sender, RoutedEventArgs e) {
            TabItem item = sender as TabItem;

            switch (item.Header) {
                case "PDF Viewer": {
                        if (lbl_documentFile.Content.ToString() != "null") {
                            pdfWebViewer.Navigate(string.Format(@"{0}\{1}", lbl_documentDir.Content, lbl_documentFile.Content));
                        }
                        else {
                            pdfWebViewer.Navigate(new Uri("about:blank"));
                        }

                        break;
                    }
            }
        }


        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mePlayer.Source = new Uri(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e) {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e) {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e) {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e) {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e) {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e) {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (lbl_videoFile.Content.ToString() != "null") {
                mePlayer.Source = new Uri(string.Format(@"{0}\{1}", lbl_videoDir.Content, lbl_videoFile.Content));
                mePlayer.Play();
            }
            else MessageBox.Show("File is not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
