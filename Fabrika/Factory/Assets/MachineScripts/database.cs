using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;

public class database : MonoBehaviour
{
    public GameObject Tehlike;
    public GameObject Panel;
    public TMP_Text work;
    public TMP_Text stop;
    public TMP_Text danger;
    public TMP_Text mainstate;
    public TMP_Text workstate;
    public TMP_Text stopstate;
    public TMP_Text dangerstate;
    int work1 = 0;
    int stop1 = 0;
    int danger1 = 0;
    string workid = "";
    string stopid = "";
    string dangerid = "";
    string workstr = "";
    string stopstr = "";
    string dangerstr = "";
    //CORS


    //ENUMERATOR
    [Serializable]
    public class DataModel
    {
        public int id;
        public string title;
        public string state;
        public string content;
        // Diðer sütunlarý buraya ekleyebilirsiniz
    }

    IEnumerator getData()
    {
        string hostName = "localhost";//buraya ip girersem yerel að için
        string userID = "postgres";
        string password = "12345";
        string databaseName = "Deneme";
        string tableName = "makine";
        string columnName = "id";
        string value = "1";
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("hostName =" + hostName +
                                               "&userID =" + userID +
                                               "&password =" + password +
                                        "&databaseName =" + databaseName +
                                              "&tableName =" + tableName +
                                            "&columnName =" + columnName +
                                                "&value =" + value));

        string uri = "http://127.0.0.1:5000/getData";
        UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            print(webRequest.error);
        }
        else
        {

            //string tra = JsonUtility.ToJson(webRequest.downloadHandler.text, true);
            //DataModel[] dataList = JsonUtility.FromJson<DataModel[]>(data);

             // Her bir veri öðesini iþleme
           /* foreach (RootObject data in myObject)
             {
                 Debug.Log("ID: " + data.id);
                 Debug.Log("Title: " + data.title);
                 Debug.Log("State: " + data.state);
                 Debug.Log("Content: " + data.content);
             }*/
            // DataModel data = JsonUtility.FromJson<DataModel>(webRequest.downloadHandler.text);
            // Debug.Log("Gelen Veri: ID = " + data.id + ", Title = " + data.title + ", State = " + data.state + ", Content = " + data.content);
           // Debug.Log("Ýstek baþarýlý: " );
                string data = webRequest.downloadHandler.text;//web verisini string çekiyoz
                data = data.Replace("\n", "");
               // data = data.Replace(" ", "");

            List<string> stringList = new List<string>(data.Split(','));//liste yapýyoz
            // Liste elemanlarýný yazdýrma
            workid = "";
            stopid = "";
            dangerid = "";
            workstr = "";
            stopstr = "";
            dangerstr = "";
            work1 = 0;
            stop1 = 0;
            danger1 = 0;
            int lenght = 0;
            foreach (string item in stringList)
            {
                if(item.Contains("Working"))
                {
                    work1++;
                }
                else if(item.Contains("Stopped"))
                {
                    stop1++;
                }
                else if (item.Contains("Defective"))
                {
                    danger1++;
                }
               // Debug.Log(item);
                 lenght++;
            }
           // Debug.Log(lenght);
            for (int i =0; i < lenght; i++)
            {
                if (stringList.ElementAt(i).Contains("Working"))
                {

                    //workid += stringList.ElementAt(i - 2).TrimStart("[") + " - "; 
                     workid += stringList.ElementAt(i - 2).Trim(" [ ".ToCharArray()) + " - ";// kýrpýp kesiyoruz indexle alýyoruz fln
                     workstr += stringList.ElementAt(i - 1).Trim(' ').Trim('"') + "\t";
                }
                else if (stringList.ElementAt(i).Contains("Stopped"))
                {
                    stopid += stringList.ElementAt(i - 2).Trim(" [ ".ToCharArray()) + " - ";
                    stopstr += stringList.ElementAt(i - 1).Trim(' ').Trim('"') + "\t";
                }
                else if (stringList.ElementAt(i).Contains("Defective"))
                {
                    dangerid += stringList.ElementAt(i - 2).Trim(" [ ".ToCharArray()) + " - ";
                    dangerstr += stringList.ElementAt(i - 1).Trim(' ').Trim('"') + "\t";
                }

            }
           /* var rast = JsonUtility.ToJson(stringList);
            
            DataModel[] dataList = JsonUtility.FromJson<DataModel[]>(rast);
            foreach (DataModel veri in dataList)
            {
                
                Debug.Log("ID: " + veri.id);
                Debug.Log("Title: " + veri.title);
                Debug.Log("State: " + veri.state);
                Debug.Log("Content: " + veri.content);
            }*/
        }
    }
    private void update()
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();
        //WHERE id=15
        string query = "UPDATE \"makine\" SET material = '0' ";

        command.CommandText = query;
        command.ExecuteNonQuery();
        conn.Close();

    }

    // Start is called before the first frame update
    void Start()
    { 
        update();
        StartCoroutine(getData());

        /* NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
         conn.Open();
         NpgsqlCommand command = conn.CreateCommand();

         string query = "SELECT* FROM \"makine\" ORDER BY id ASC";
         command.CommandText = query;
         NpgsqlDataReader reader = command.ExecuteReader();
         while (reader.Read())
         {
             if (reader.GetValue(2).ToString() == "Working")
             {
                 workid += reader.GetValue(0).ToString()+" - ";
                 workstr += reader.GetValue(1).ToString() + " \t ";
                 ++work1;
             }
             else if (reader.GetValue(2).ToString() == "Stopped")
             {
                 stopid += reader.GetValue(0).ToString() + " - ";
                 stopstr += reader.GetValue(1).ToString() + " \t ";
                 ++stop1;
             }
             else
             {
                 dangerid += reader.GetValue(0).ToString() + " - ";
                 dangerstr += reader.GetValue(1).ToString() + " \t ";
                 ++danger1;
             }
         }
         conn.Close();*/

        }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("k"))
        {
            Start();
            FixedUpdate();
        }
        else
        {

        }

        if (Input.GetKeyDown("escape"))
        {
            if(Panel.activeInHierarchy == false)
            {
                Panel.SetActive(true);
            }
            else
            {
                Panel.SetActive(false);
            }
        }
        else
        {

        }

    }
    private void FixedUpdate()
    {
        work.text = "Çalýþan Makine Sayýsý: " + work1;
        stop.text = "Durmuþ Makine Sayýsý: " + stop1;

        if (danger1 > 0)
        { 
            danger.text = "Arýzalý Makine Sayýsý: " + danger1;
            Tehlike.SetActive(true);
        }
        else
        {
            Tehlike.SetActive(false);
        }

        mainstate.text = "Çalýþan Makineler: \n" + workid + "\n\n" + "Durmuþ Makineler:" + "\n" + stopid + "\n\n" + "Arýzalý Makineler: \n" + dangerid;
        workstate.text = "Çalýþan Makineler: \n\n" + workstr;
        stopstate.text = "Durmuþ Makineler: \n\n" + stopstr;
        dangerstate.text = "Arýzalý Makineler: \n\n" + dangerstr;
    }
}
