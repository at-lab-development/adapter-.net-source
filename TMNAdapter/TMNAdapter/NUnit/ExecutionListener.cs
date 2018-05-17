using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Engine;
using NUnit.Engine.Extensibility;


namespace TMNAdapter.NUnit
{
    [Extension(Description = "Test Reporter Extension")]
    class ExecutionListener : ITestEventListener
    {
        //IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
        public void OnTestEvent(string report)
        {
            var file = new FileInfo("C:\\data\\rezult.xml");

            using (StreamWriter writer = file.AppendText())
            {
                writer.WriteLine(report);
            }           

           // Console.WriteLine(report);
           // throw new NotImplementedException();
        }
    }
}
