using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    [SerializeField] private bool isItPlayersBullet;

    void FixedUpdate()
    {
        transform.Translate(Vector2.right.normalized * speed * Time.deltaTime);

        if (transform.position.x > 15 || transform.position.x < -15)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {

            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null) 
                enemy.HitDamage(damage);

            Destroy(gameObject);
        }
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerAction>().HitDamage(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("PlayerShield"))
        {
            if (!isItPlayersBullet) {
                collision.GetComponent<Shield>().HitDamage(); //damage
                Destroy(gameObject);
            }

        }

        if (collision.CompareTag("EnemyShield"))
        {
            if (isItPlayersBullet) {
                collision.GetComponent<Shield>().HitDamage(); // damage
                Destroy(gameObject);
            }
        }

    }
}