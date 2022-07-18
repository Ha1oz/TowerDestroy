using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health;

    [Header("Cannon Settings")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootDelay;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Shield Settings")]
    [SerializeField] private GameObject shield;
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
