using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    //public bool isEnemyBullet;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > 15 || transform.position.x < -15)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("DEAD");

        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Shield"))
        {
            //
        }

        if (collision.CompareTag("Enemybullet"))
        {
            Destroy(gameObject);
        }
    }
}