using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System;
using System.IO;
using UnityEngine.UI;

public class XML : MonoBehaviour {

    string filepath;
    string file;
    static string final;
    void Start()
    {
        file = "/info.xml";
        filepath = Application.persistentDataPath;
        final = filepath + file;
        FirstTime();
    }


    public void UpdateScore(int Score)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.HighScore = Score;
        APP2.save(final);


    }

     public void UpdateControl(int Control)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.Control = Control;
        APP2.save(final);
    }

    public void UpdateFirstPlay(int First)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.FirstTimePlay = First;
        APP2.save(final);
    }

     public int ControlScheme()
    {
        ApplicationData APP1 = new ApplicationData();
        APP1 = APP1.Load(final);
        return APP1.Control;
    }

    public int HighScore()
    {
        int temp;
        ApplicationData APP1 = new ApplicationData();
        APP1 = APP1.Load(final);
        temp = APP1.HighScore;
        return temp;
    }

    public void FirstTime()
    {
        DirectoryInfo root = new DirectoryInfo(filepath);
        FileInfo[] listfiles = root.GetFiles("info.*");
        if (listfiles.Length > 0)
        {
            //File exists
            foreach (FileInfo file in listfiles)
            {
                //Debug.Log("i exist");
            }
        }
        else
        {
            //Debug.Log("i am created");
            XElement srcTree = new XElement("ApplicationData",
                                                new XElement("HighScore", 0),
                                                new XElement("FirstTimePlay", 0),
                                                new XElement("Control", 0),
                                                new XElement("music", 0.0f),
                                                new XElement("TilteEnabled", false)
                                            );

            srcTree.Save(final);
        }
    }

    public float MusicRead()
    {
        float temp;
        ApplicationData APP1 = new ApplicationData();
        APP1 = APP1.Load(final);
        temp = APP1.music;
        return temp;
    }

    public void MusicWrite(float IN)
{
         ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.music = IN; ;
        APP2.save(final);

}

    public bool TilteEnabledRead()
    {
        bool b1;
        ApplicationData APP1 = new ApplicationData();
        APP1 = APP1.Load(final);
        b1 = APP1.TilteEnabled;
        return b1;
    }

    public void TilteEnabledWrite(bool b1)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.TilteEnabled = b1; ;
        APP2.save(final);
    }


    public class ApplicationData
    {
        public int HighScore { get; set; }
        public int FirstTimePlay { get; set; }
        public int Control { get; set; }
        public float music { get; set; }
        public bool TilteEnabled { get; set; }

        public void save(string FileName)
        {

            using (var Stream = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ApplicationData));
                XML.Serialize(Stream, this);
            }
        }


        public ApplicationData Load(string FileName)
        {
            using (var Stream = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ApplicationData));
                return (ApplicationData)XML.Deserialize(Stream);
            }
        }
    }

}






