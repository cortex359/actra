﻿using System;
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
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Andrew sent you a picture")
                .AddText("Check this out, The Enchantments in Washington!")
                // Text box for replying
                .AddInputTextBox("tbReply", placeHolderContent: "Type a response")

                // Buttons
                .AddButton(new ToastButton()
                    .SetContent("Reply")
                    .AddArgument("action", "reply")
                    .SetBackgroundActivation())

                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like")
                    .SetBackgroundActivation())

                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

        }
    }
}
