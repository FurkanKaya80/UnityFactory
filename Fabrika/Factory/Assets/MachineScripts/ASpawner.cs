using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpawner : MonoBehaviour
{
    public float delay;
    public GameObject Package; // Prefab olan
    Vector3 packagePosition = new Vector3(92f, 1.71f, 65.9f);




    // Start is called before the first frame update
    void Start()
    {
        delay = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay >= 4)
        {
            Instantiate(Package, packagePosition, Quaternion.identity);
            delay = 0;
        }
        delay += Time.deltaTime;
    }

}
