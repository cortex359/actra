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
                () => focusChanges.Run(this)
            );

            Console.WriteLine("Okay");
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list_events.ItemsSource = this.data.eventList.showEvents("");
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
            this.data.SaveEvents();
        }

        private void btn_end_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void list_activities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void list_events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
