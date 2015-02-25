using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
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

	static public void UpdateControl(int Control)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.Control = Control;
        APP2.save(final);
    }

  	static public void UpdateFirstPlay(int First)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load(final);
        APP2 = APP1;
        APP2.FirstTimePlay = First;
        APP2.save(final);
    }

	static public int ControlScheme()
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
        ApplicationData APP1 = new ApplicationData();
        APP1 = APP1.Load(final);
    }



    public class ApplicationData
    {
    	public int HighScore { get; set; }
        public int FirstTimePlay { get; set; }
        public int Control { get; set; }


      	public void save(string FileName)
        {

            using (var Stream = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ApplicationData));
                XML.Serialize(Stream, this);
            //    Debug.Log("saved");
            }
        }


      	public ApplicationData Load(string FileName)
      	{
            using (var Stream = new FileStream(FileName, FileMode.OpenOrCreate))
          	{
              	XmlSerializer XML = new XmlSerializer(typeof(ApplicationData));
            //  Debug.Log("Open");
             	return(ApplicationData)XML.Deserialize(Stream);
          	}
      	}
    }

}
