using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;

    public float hp;
    [SerializeField] int speed;
    [SerializeField] int score;
    [SerializeField] int coin;

    private void Start()
    {

    }

    private void Update()
    {
        transform.position += new Vector3(0.001f, 0, 0) * speed;

        if (hp <= 0)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.UpdateInfo(score, coin);
            Destroy(this.gameObject);
        }
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp > 0)
        {
            transform.DOShakePosition(0.3f, 0.2f);
        }

    }

}
