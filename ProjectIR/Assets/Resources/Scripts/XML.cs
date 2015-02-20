using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
public class XML : MonoBehaviour {



  

     public void UpdateScore(int Score)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load("Info.xml");
        APP2 = APP1;
        APP2.HighScore = Score;
        APP2.save("Info.xml");
    }


   static public void UpdateControl(int Control)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load("Info.xml");
        APP2 = APP1;
        APP2.Control = Control;
        APP2.save("Info.xml");
    }

  static public void UpdateFirstPlay(int First)
    {
        ApplicationData APP1 = new ApplicationData();
        ApplicationData APP2 = new ApplicationData();
        APP1 = APP1.Load("Info.xml");
        APP2 = APP1;
        APP2.FirstTimePlay = First;
        APP2.save("Info.xml");
    }

  static public int ControlScheme()
        {
            ApplicationData APP1 = new ApplicationData();
            APP1 = APP1.Load("Info.xml");
            return APP1.Control;
        }

 public int HighScore()
  {
      int temp;
      ApplicationData APP1 = new ApplicationData();
      APP1 = APP1.Load("Info.xml");
      temp = APP1.HighScore;
      return temp;
     
  }




    public class ApplicationData
    {

        public int HighScore { get; set; }
        public int FirstTimePlay { get; set; }
        public int Control { get; set; }
       



      public void save(string FileName)
        {

            using (var Stream = new FileStream(FileName, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ApplicationData));
                XML.Serialize(Stream, this);
            //    Debug.Log("saved");
            }
        }


      public ApplicationData Load(string FileName)
      {
          using (var Stream = new FileStream(FileName, FileMode.Open))
          {
              XmlSerializer XML = new XmlSerializer(typeof(ApplicationData));
            //  Debug.Log("Open");
             return(ApplicationData)XML.Deserialize(Stream);
          }

      }
    }

   
    


}
