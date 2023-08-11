using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Makine6 : MonoBehaviour
{
    public GameObject Character;
    public GameObject info;
    public TMP_Text Title;
    public TMP_Text Content;
    string title = "";
    string cont = "";
    int puanC = 0;
    string textC = "0";
    int puanB = 0;
    string textB = "0";
    string textToplam = "";
    public float delay;
    public GameObject Package; // Prefab olan
    Vector3 packagePosition = new Vector3(137.8f, 1.71f, 82.5f);
    //Collision
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "PackageB")
        {

            puanB++;
            textB = puanB.ToString() + " B ";
            textToplam = textB +"   " +textC;
           // print("D ye giren ürün: " + textC);
            Destroy(collision.gameObject);
            guncelle();
            Start();
        }
        else if(collision.collider.tag == "PackageC")
        {
            puanC++;
            textC = puanC.ToString() + " C ";
            textToplam = textB + "  " + textC;
            Destroy(collision.gameObject);
            guncelle();


        }
    }
    private void guncelle()
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();
        //WHERE id=15
        string query = "UPDATE \"makine\" SET material = '" + textToplam + "'  WHERE id=6";

        command.CommandText = query;
        command.ExecuteNonQuery();
        conn.Close();
        Start();

    }

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
            title = stringList.ElementAt(26).Trim(' ').Trim('"');
            cont += "Durumu: " + stringList.ElementAt(27).Trim(' ').Trim('"') + "\n";
            cont += stringList.ElementAt(28).Trim(' ').Trim('"') + "\n";
            cont += "Ham madde : " + stringList.ElementAt(29).Trim(" ] ".ToCharArray()).Trim('"');


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        title = string.Empty;
        cont = string.Empty;
        StartCoroutine(getData());
        /*
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();

        string query = "SELECT* FROM \"makine\" WHERE id=6";
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
        if (delay >= 10 && puanC >= 2 && puanB >= 1)
        {
            Instantiate(Package, packagePosition, Quaternion.identity);
            delay = 0;
            puanC += -2;
            puanB--;
            if (puanC != 0)
            {
                textC = puanC.ToString() + " C";
                guncelle();

            }
            else
            {
                textC = "0";
                guncelle();

            }

            if (puanB != 0)
            {
                textB = puanB.ToString() + " B";
                guncelle();

            }
            else
            {
                textB = "0";
                guncelle();

            }

        }
        delay += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float uzaklik = Vector3.Distance(Character.transform.position, transform.position);

        if (uzaklik < 8f)
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
