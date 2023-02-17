using System;
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

// Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
using Microsoft.Toolkit.Uwp.Notifications;


namespace actra
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data data;

        public MainWindow()
        {
            InitializeComponent();
            this.data = new Data();
            EventSource focusChanges = new EventSource(this.data);
            Task eventWatcher = Task.Run(
                () => focusChanges.Run()
            );
        }

        public async void UpdateSales()
        {
            var collection = await Task.Run(() =>
            {
                return this.data.eventList.showEvents("");
            });
            list_events.ItemsSource = collection;
            list_events.Items.Refresh();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSales();
            // list_events.ItemsSource = this.data.eventList.showEvents("");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void inp_searchNode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void list_nodes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_addToNode_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_pause_Click(object sender, RoutedEventArgs e)
        {
            if (btn_pause.Content.ToString().Equals("Pause"))
            {
                btn_end.Visibility = Visibility.Hidden;
                btn_pause.Content = "Pause beenden";

            } else
            {
                btn_end.Visibility = Visibility.Visible;
                btn_pause.Content = "Pause";
                Notification note = new Notification();
                note.Notification1.Show();
            }
        }

        private void btn_end_Click(object sender, RoutedEventArgs e)
        {
            btn_start.Visibility = Visibility.Visible;
            btn_end.Visibility = Visibility.Hidden;
            btn_pause.Visibility = Visibility.Hidden;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            btn_start.Visibility = Visibility.Hidden;
            btn_end.Visibility = Visibility.Visible;
            btn_pause.Visibility = Visibility.Visible;
        }

        private void list_activities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void list_events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_exportCSV_Click(object sender, RoutedEventArgs e)
        {
            this.data.SaveEvents();
        }
    }
}
