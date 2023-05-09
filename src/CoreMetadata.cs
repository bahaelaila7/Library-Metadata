using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Landis.Core;
using Landis.SpatialModeling;

namespace Landis.Library.Metadata
{
    public class CoreMetadata : IMetadata
    {
        public string Version { get; set; }

        public uint? Seed { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public List<string> InputFiles { get; set; }

        public List<CoreExtensionMetadata> Extensions { get; set; }
        //---------------------------------------------------------------------


        public CoreMetadata()
        {
            Extensions = new List<CoreExtensionMetadata>();
        }

        public CoreMetadata(string version, uint? seed, int start, int end, List<CoreExtensionMetadata> extensions, List<string> inputs)
        {
            Version = version;
            Seed = seed;
            Start = start;
            End = end;
            Extensions = new List<CoreExtensionMetadata>();
            Extensions.AddRange(extensions);
            InputFiles = new List<string>();
            InputFiles.AddRange(inputs);
        }

        public XmlNode Get_XmlNode(XmlDocument doc)
        {
            XmlNode node = doc.CreateElement("core");

            XmlAttribute nameAtt = doc.CreateAttribute("name");
            nameAtt.Value = "LANDIS-II v" + this.Version;
            node.Attributes.Append(nameAtt);

            if (this.Seed != null)
            {
                XmlAttribute seedAtt = doc.CreateAttribute("seed");
                seedAtt.Value = this.Seed.ToString();
                node.Attributes.Append(seedAtt);
            }

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

            //XmlNode srNode = doc.CreateElement("scenario-replication");
            XmlNode extensionsColl = doc.CreateElement("extensions");
            foreach (CoreExtensionMetadata em in Extensions)
                extensionsColl.AppendChild(em.Get_XmlNode(doc));
            node.AppendChild(extensionsColl);
            return node;
        }


    }
}
