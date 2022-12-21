using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
