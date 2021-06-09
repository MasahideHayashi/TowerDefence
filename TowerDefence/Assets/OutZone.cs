using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutZone : MonoBehaviour
{
    [SerializeField] Image[] hpImage;
    [SerializeField] Sprite sprite;
    [SerializeField] GameManager gameManager;


    int hp = 3;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if (hp > 0)
        {
        hp--;
        hpImage[2 - hp].sprite = sprite;
        }

        if (hp <= 0)
        {
            gameManager.GameOver();
        }
    }

    
}
