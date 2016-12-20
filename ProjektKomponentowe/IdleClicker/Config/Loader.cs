using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace IdleClicker
{
    class Loader
    {

        public static bool Loading { get; private set; }
        public static bool Loaded { get; private set; }

        public static void LoadConfig()
        {

        }
        /*
        public static void LoadWebConfig()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Version));
            var subReq = new Version(1, 2, 3);
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString(); // Your XML
                }
            }
            System.Windows.Forms.MessageBox.Show(xml);
        }*/
    }
}
