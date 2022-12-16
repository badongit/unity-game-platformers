using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] 
    private float 
        activeTime = 0.1f,
        alphaSet = 0.8f,
        alphaDecay = 0.85f;

    private float 
        timeActived,
        alpha;

    [SerializeField] private Transform player;

    private SpriteRenderer sr;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sr.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActived = Time.time;
    }

    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(1f, 1f, 1f, alpha);
        sr.color = color;

        if(Time.time >= (timeActived + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
