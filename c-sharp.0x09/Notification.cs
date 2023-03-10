using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace actra
{
    internal class Notification
    {

        public ToastContentBuilder Notification1;


        public Notification()
        {
            Notification1 = new ToastContentBuilder()
                .SetToastScenario(ToastScenario.Reminder)
                .AddText("Inaktivität erkannt!")
                .AddText("Wie haben Sie die letzten 37 Min. verbracht?")
                // Text box for replying
                .AddInputTextBox("tbReply", placeHolderContent: "Aktivität angeben...")

                // Buttons
                .AddButton(new ToastButton()
                    .SetContent("Pause")
                    .AddArgument("action", "reply")
                    .SetBackgroundActivation())

                .AddButton(new ToastButton()
                    .SetContent("Arbeit")
                    .AddArgument("action", "like")
                    .SetBackgroundActivation());

            Console.WriteLine("Notification build.");
            Console.WriteLine("Notification triggerd.");
        }

    }
}
