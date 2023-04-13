using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Landis.Core;
using Landis.SpatialModeling;

namespace Landis.Library.Metadata
{
    public class CoreExtensionMetadata : IMetadata
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public List<string> InputFiles { get; set; }

        //---------------------------------------------------------------------


        public CoreExtensionMetadata()
        {
            InputFiles = new List<string>();
        }

        public CoreExtensionMetadata(string name, string version, List<string> inputs)
        {
            Name = name;
            Version = version;
            InputFiles = new List<string>();
            InputFiles.AddRange(inputs);
        }

        public XmlNode Get_XmlNode(XmlDocument doc)
        {
            XmlNode node = doc.CreateElement("extension");

            XmlAttribute nameAtt = doc.CreateAttribute("name");
            nameAtt.Value = this.Name;
            node.Attributes.Append(nameAtt);

            XmlAttribute versionAtt = doc.CreateAttribute("version");
            versionAtt.Value = this.Version;
            node.Attributes.Append(versionAtt);

            XmlNode inputsColl = doc.CreateElement("inputs");
            int x = 1;
            foreach (string file in InputFiles)
            {
                XmlAttribute fileAtt = doc.CreateAttribute("inputFile" + x);
                fileAtt.Value = file;
                inputsColl.Attributes.Append(fileAtt);
                x++;
            }

            node.AppendChild(inputsColl);
            return node;
        }


    }
}
