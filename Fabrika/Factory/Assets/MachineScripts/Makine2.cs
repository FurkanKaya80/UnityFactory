using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Makine2 : MonoBehaviour
{
    public GameObject Character;
    public GameObject info;
    public TMP_Text Title;
    public TMP_Text Content;
    string title = "";
    string cont = "";
    int puanD = 0;
    string textD = "0";
    public float delay;
    public GameObject Package; // Prefab olan
    Vector3 packagePosition = new Vector3(141f, 1.71f, 103.3f);
    //Collision
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "PackageD")
        {

            puanD++;
            textD = puanD.ToString() + ' ' + 'D';
          //  print("D ye giren �r�n: " + textD);
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
        string query = "UPDATE \"makine\" SET material = '" + textD + "'  WHERE id=2";

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
            title = stringList.ElementAt(6).Trim(' ').Trim('"');
            cont += "Durumu: " + stringList.ElementAt(7).Trim(' ').Trim('"') + "\n";
            cont += stringList.ElementAt(8).Trim(' ').Trim('"') + "\n";
            cont += "Ham madde : " + stringList.ElementAt(9).Trim(" ] ".ToCharArray()).Trim('"');

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

        string query = "SELECT* FROM \"makine\" WHERE id=2";
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
        if (delay >= 10 && puanD >= 3)
        {
            Instantiate(Package, packagePosition, Quaternion.identity);
            delay = 0;
            puanD -= 3;
            if (puanD != 0)
            {
                textD = puanD.ToString() + " A";
                guncelle();

            }
            else
            {
                textD = "0";
                guncelle();

            }

        }
        delay += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float uzaklik = Vector3.Distance(Character.transform.position, transform.position);

        if (uzaklik < 5f)
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
