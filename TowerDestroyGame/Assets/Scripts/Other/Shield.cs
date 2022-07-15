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
        UIManager.Instance.ShieldIsBroken();
        gameObject.SetActive(false);
    }

    public virtual void HitDamage(int damage = 1)
    {
        HEALTH -= damage;

        if (HEALTH <= 0)
        {
            Break();
        }
    }
}
