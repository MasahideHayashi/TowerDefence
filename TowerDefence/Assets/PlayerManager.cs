using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    Animator animator;

    [SerializeField] float at;
    [SerializeField] float atTime;
    float atStartTime = 10;




    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator attack(EnemyManager enemy)
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.4f);
        enemy.Damage(at);
        //yield return new WaitForSeconds(0.2f);
        animator.SetBool("isAttack", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();
        atStartTime += Time.deltaTime;
        if (atTime <= atStartTime)
        {
            StartCoroutine(attack(enemy));
            //attack(enemy);
            atStartTime = 0;
        }
    }

    public void LevelUp()
    {
        at += at * 0.1f;
        atTime -= atTime * 0.1f;
    }

    public void UpdataText(Text text)
    {
        text.text = string.Format("AT:{0}\nSPEED:{1}", at.ToString("N0"), atTime.ToString("N0"));
    }

}
