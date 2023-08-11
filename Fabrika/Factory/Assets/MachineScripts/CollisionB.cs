using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    int puanB = 0;
    //public GameObject Package; // Prefab olan
  //  public GameObject Levha;
   // Vector3 packagePosition = new Vector3(98f, 2.201f, 70.461f);
    bool two = false;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "PackageA") 
        {
            puanB++;
            print("B ye giren ürün: "+puanB);
            Destroy(collision.gameObject);
           // Instantiate(Package, packagePosition, Quaternion.identity);     
          /*  if(puanB % 2 == 0 && puanB != 0) 
            {
                two = true;
            }
            if (two == true)
            {
                Levha.transform.eulerAngles = new Vector3(0, 50, 90);
                //Vector3 rotation = transform.eulerAngles;
                two = false;

            }*/
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
