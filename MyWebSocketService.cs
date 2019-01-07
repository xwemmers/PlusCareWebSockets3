using Microsoft.ServiceModel.WebSockets;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Windows.Forms;
using System;

namespace PlusCareWebSockets3
{
    class MyWebSocketService : WebSocketService
    {
        // Add a reference to System.Web.Extensions
        JavaScriptSerializer ser = new JavaScriptSerializer();

        public override void OnOpen()
        {
            this.Send("The connection was opened!");
        }

        public override void OnMessage(string message)
        {
            var msg = ser.DeserializeObject(message) as Dictionary<string, object>;

            string fn = (string)msg["fn"];

            if (fn == "openexcel")
            {
                // Een voorbeeld met een externe library (Excel)
                Type type = Type.GetTypeFromProgID("Excel.Application");
                dynamic app = Activator.CreateInstance(type);
                app.visible = true;
                app.Workbooks.Add();
                var sheet1 = app.Worksheets[1];
                sheet1.Cells[2, 2] = "Hello";
                sheet1.Cells[2, 3] = "World";
            }
            else
            {
                // Een voorbeeld met het dynamisch aanroepen van een functie middels Reflection.
                var q = from m in msg
                        where m.Key != "fn"
                        select m.Value;

                var parameters = q.ToArray();

                var c = new Calculator();
                var type = c.GetType();
                var method = type.GetMethod(fn);
                object result = method.Invoke(c, parameters);
                this.Send(ser.Serialize(result));
            }
        }

    }
}