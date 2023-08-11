using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Makine15 : MonoBehaviour
{
    public GameObject Character;
    public GameObject info;
    public TMP_Text Title;
    public TMP_Text Content;
    string title = "";
    string cont = "";
    int puanC = 0;
    string textC = "0";
    public float delay;
    public GameObject Package; // Prefab olan
    Vector3 packagePosition = new Vector3(103f, 1.71f, 79f);

    //Collision
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "PackageA")
        {
            
            puanC++;
            textC = puanC.ToString() + ' '+ 'A';
           // print("C ye giren ürün: " + textC);
            Destroy(collision.gameObject);
            guncelle();
            //Start();
        }
    }
    private void guncelle()
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();
        //WHERE id=15
        string query = "UPDATE \"makine\" SET material = '"+textC+"'  WHERE id=15";

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
            title = stringList.ElementAt(71).Trim(' ').Trim('"');
            cont += "Durumu: " + stringList.ElementAt(72).Trim(' ').Trim('"') + "\n";
            cont += stringList.ElementAt(73).Trim(' ').Trim('"') + "\n";
            cont += "Ham madde : " + stringList.ElementAt(74).Trim(" ] ".ToCharArray()).Trim('"');
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        title = string.Empty;
        cont= string.Empty;
 
        StartCoroutine(getData());

        /*NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=12345;Database=Deneme;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();
        //WHERE id=15
        string query = "UPDATE \"makine\" SET material="+puanC+" WHERE id=15";

        command.CommandText = query;
        command.ExecuteNonQuery();
        conn.Close();*/
        /*
        string query = "SELECT* FROM \"makine\" WHERE id=15";
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
        if (delay >= 6 && puanC > 0)
        {
            Instantiate(Package, packagePosition, Quaternion.identity);
            puanC--;
            if (puanC > 0)
            {
                textC = puanC.ToString() + " A";
                guncelle();
            }
            else if (puanC == 0)
            {
                textC = "0";
                guncelle();

            }
            delay = 0;


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
