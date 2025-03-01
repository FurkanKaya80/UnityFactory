using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Npgsql;
using Unity.VisualScripting;
using System;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine.Networking;

public class MesafeKontrol : MonoBehaviour
{
    public GameObject Character;
    public GameObject info;
    public TMP_Text Title;
    public TMP_Text Content;
    string title = "";
    string cont = "";

    //ENUMERATOR
    IEnumerator getData()
    {
        string hostName = "localhost";
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
            string data = webRequest.downloadHandler.text;//web verisini string �ekiyoz
            data = data.Replace("\n", "");
            // data = data.Replace(" ", "");

            List<string> stringList = new List<string>(data.Split(','));//liste yap�yoz
            // Liste elemanlar�n� yazd�rma

            int lenght = 0;
            foreach (string item in stringList)
            {
                lenght++;
            }
            // Debug.Log(lenght);
            title = stringList.ElementAt(1).Trim(' ').Trim('"');
            cont += "Durumu: " + stringList.ElementAt(2).Trim(' ').Trim('"') + "\n";
            cont += stringList.ElementAt(3).Trim(' ').Trim('"') + "\n";
            cont += "Ham madde : " + stringList.ElementAt(4).Trim(" ] ".ToCharArray()).Trim('"');


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
        /*
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();

        string query = "SELECT* FROM \"makine\" WHERE id=1";
        command.CommandText = query;
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            title += (reader.GetValue(1));
            cont += "Durumu : " + reader.GetValue(2) + "\n";
            cont += reader.GetValue(3);
        }
        conn.Close(); 
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float uzaklik = Vector3.Distance(Character.transform.position, transform.position);

        if (uzaklik < 5f)
        {
            Title.text = title;      //"Makine 1:";
            Content.text = cont;    //"Makine 1 nin i�eri�i buras� isteid�ini yazabilirsin";
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
    }
}
