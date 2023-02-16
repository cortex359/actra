using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace actra
{
    internal class Data
    {
        private string saveFile = "actra-storage.csv";

        public EventList eventList = new EventList();

        public Data(string sf = "actra-storage.csv")
        {
            if (File.Exists(sf))
            {
                this.saveFile = sf;
            }
            else
            {
                try
                {
                    File.Create(sf).Close();
                    this.saveFile = sf;
                } catch (IOException)
                {
                    Console.WriteLine("Konnte Datei nicht anlegen.");
                }
            }
            eventList = this.LoadEvents();
        }

        public void SaveEvents()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(saveFile))
                {

                    foreach (Event e in this.eventList.getNext())
                        sw.WriteLine($"{e.Timestamp};{e.Type};{e.Description}");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Konnte Datei nicht schreiben!");
            }
        }

        public EventList LoadEvents()
        {
            EventList eventList = new EventList();
            try
            {
                string[] allLine = File.ReadAllLines(this.saveFile);
                foreach (string line in allLine)
                {
                    string[] cols = line.Split(';');
                    Event e = new Event(cols[1], cols[2], DateTime.Parse(cols[0]));
                    eventList.add(e);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Konnte Datei nicht lesen!");
            }
            return eventList;
        }
    }
}
