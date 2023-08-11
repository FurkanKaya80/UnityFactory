using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionC : MonoBehaviour
{
    int puanC = 0;
   // public GameObject Package; // Prefab olan
    //public GameObject Levha;
    //Vector3 packagePosition = new Vector3(98f, 2.201f, 70.461f);
  //  bool one = false;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "PackageA")
        {
           // Levha.transform.eulerAngles = new Vector3(0, 90, 90);
            puanC++;
            print("C ye giren ürün: " + puanC);
            Destroy(collision.gameObject);
            //Instantiate(Package, packagePosition, Quaternion.identity);
           // one = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

       /* if (one == true)
        {
            Levha.transform.eulerAngles = new Vector3(0, 90, 90);
            //Vector3 rotation = transform.eulerAngles;
            one = false;
        }*/
    }
}
