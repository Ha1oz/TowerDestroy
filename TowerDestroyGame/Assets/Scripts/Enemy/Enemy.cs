using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    [SerializeField] private Transform shootPoint;//, shieldPoint;
    [SerializeField] private float shootDelay;
    [SerializeField] private GameObject bulletPrefab, shield;
    [SerializeField] private int shieldHealth;

    private int shieldDelay;

    private void Start()
    {
        StartCoroutine(Shoot());
        StartCoroutine(ActivateShield());
    }

    IEnumerator Shoot()
    {

        yield return new WaitForSeconds(shootDelay);

        Instantiate(bulletPrefab, shootPoint.position, shootPoint.transform.rotation);
        
        StartCoroutine(Shoot());

    }
    IEnumerator ActivateShield()
    {
        if (!shield.activeInHierarchy) {

            shieldDelay = Random.Range(2, 10);
            yield return new WaitForSeconds(shieldDelay);

            shield.GetComponent<Shield>().Create(shieldHealth);

        }
        yield return new WaitForSeconds(1);
        StartCoroutine(ActivateShield());

    }


    public virtual void HitDamage(float damage = 1f)
    {
        health -= damage;

        UIManager.Instance.UpdateEnemyHPBar(health);

        if (health <= 0)
        {
            UIManager.Instance.GameOver();
        }
    }
}
