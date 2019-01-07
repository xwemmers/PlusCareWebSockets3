using Microsoft.ServiceModel.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PlusCareWebSockets3
{
    class SocketApplicationContext : ApplicationContext
    {

        // Add a reference to System.ServiceModel
        // Installeer vanuit NuGet the package Microsoft.WebSockets

        WebSocketHost<MyWebSocketService> host = null;
        NotifyIcon notifyIcon = new NotifyIcon();

        public SocketApplicationContext()
        {
            MenuItem showLogMenuItem = new MenuItem("Show log", ShowLogHandler);
            MenuItem configMenuItem = new MenuItem("Config", ConfigHandler);
            MenuItem exitMenuItem = new MenuItem("Exit", ExitHandler);

            notifyIcon.Icon = PlusCareWebSockets3.Properties.Resources.AppIcon;
            notifyIcon.ContextMenu = new ContextMenu(new[] { showLogMenuItem, configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

            LogMessage("The application context was initialized.");

            // Initializing the WebSocket Server
            
            logForm.Visible = true; // nice for debugging... turn off for production

            host = new WebSocketHost<MyWebSocketService>(new Uri("ws://localhost:8080/testsocket"));
            var endpoint = host.AddWebSocketEndpoint();
            LogMessage("Opening the host...");
            host.Open();
            LogMessage("Host open!");

        }

        LoggingForm logForm = new LoggingForm();
        string logging = "";

        public void LogMessage(string str)
        {
            logging += str + Environment.NewLine;
            logForm.ShowLog(logging);
        }

        private void ShowLogHandler(object sender, EventArgs e)
        {
            if (logForm == null || logForm.IsDisposed)
                logForm = new LoggingForm();

            logForm.ShowLog(logging);
            logForm.Visible = true;
        }

        private void ConfigHandler(object sender, EventArgs e)
        {
            MessageBox.Show("This is a nice piece of Configuration!");
        }

        private void ExitHandler(object sender, EventArgs e)
        {
            host?.Close();
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
