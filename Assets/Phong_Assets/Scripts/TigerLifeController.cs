using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerLifeController : MonoBehaviour
{
    public Transform tigerTip;
    public GameObject tiger;
    float fireRate = 1f;
    float nextTiger = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - nextTiger>fireRate)
        {
            nextTiger = Time.time + fireRate;
            Instantiate(tiger, tigerTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }

    }
}
