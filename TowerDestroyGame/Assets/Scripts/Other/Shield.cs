using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int HEALTH;

    public virtual void Create(int shieldHP) 
    {
        HEALTH = shieldHP;
        gameObject.SetActive(true);
    }

    public virtual void Break()
    {
        HEALTH = 0;
        gameObject.SetActive(false);
    }

    public virtual void HitDamage(int damage = 1)
    {
        HEALTH -= damage;

        if (HEALTH <= 0)
        {
            //GameManager.Instance.UpdateScore(score);
            Break();
        }
    }
}
