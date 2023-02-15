using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
using Microsoft.Toolkit.Uwp.Notifications;


namespace ToastNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            var Notification1 = new ToastContentBuilder()
                .SetToastScenario(ToastScenario.Reminder)
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
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
            Notification1.Show();
            Console.WriteLine("Notification triggerd.");
        }
    }
}
