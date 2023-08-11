using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Makine10 : MonoBehaviour
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
            string data = webRequest.downloadHandler.text;//web verisini string çekiyoz
            data = data.Replace("\n", "");
            // data = data.Replace(" ", "");

            List<string> stringList = new List<string>(data.Split(','));//liste yapýyoz
                                                                        // Liste elemanlarýný yazdýrma

            int lenght = 0;
            foreach (string item in stringList)
            {
                lenght++;
            }
            // Debug.Log(lenght);
            title = stringList.ElementAt(46).Trim(' ').Trim('"');
            cont += "Durumu: " + stringList.ElementAt(47).Trim(' ').Trim('"') + "\n";
            cont += stringList.ElementAt(48).Trim(' ').Trim('"') + "\n";
            cont += "Ham madde : " + stringList.ElementAt(49).Trim(" ] ".ToCharArray()).Trim('"');


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

        string query = "SELECT* FROM \"makine\" WHERE id=10";
        command.CommandText = query;
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            title += (reader.GetValue(1));
            cont += "Durumu : " + reader.GetValue(2) + "\n";
            cont += reader.GetValue(3);
        }
        conn.Close();*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float uzaklik = Vector3.Distance(Character.transform.position, transform.position);

        if (uzaklik < 7f)
        {
            Title.text = title;
            Content.text = cont;
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
    }
}
