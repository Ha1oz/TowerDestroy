using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float health; 
    public int timer;
    
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject cannon, bulletPrefab, shield;
    [SerializeField] private Transform shootPoint, rotatePoint;//, shieldPoint;
    [SerializeField] private float shootDelay;
    [SerializeField] private int shieldDelay,shieldHealth;

    private bool isStartShoot;
    //private float callDown = 0f;
    private Ray ray;
    private float touchPoint;

    void Start()
    {
        timer = shieldDelay;
        isStartShoot = true;
    }

    void Update()
    {
        RotateCannon();
        Shoot();
    }

    private void RotateCannon() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            touchPoint = ray.origin.y;
        }

        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 right = cannon.transform.right;
            
            float angle = Vector3.Angle(Vector3.right, right); 

            angle *= Vector3.Cross(Vector3.right, right).z > 0 ? 1 : -1; 

            float newAngle = Mathf.Clamp(angle - (touchPoint - ray.origin.y), -30f, 10f);

            float deltaAngle = newAngle - angle;

            cannon.transform.RotateAround(rotatePoint.position, Vector3.forward, deltaAngle);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonUp(0))
        {
            touchPoint = 0;

            if (isStartShoot)
            {
                Instantiate(bulletPrefab, shootPoint.position, shootPoint.transform.rotation);
                isStartShoot = false;
                StartCoroutine(ShootDelay());
            }
        }
        
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        isStartShoot = true;
    }

    public void ActivateShield() 
    {
        shield.GetComponent<Shield>().Create(shieldHealth);

    }

    public void HitDamage(float damage = 1)
    {
        health -= damage;

        UIManager.Instance.UpdateMyHPBar(health);

        if (health <= 0)
        {
            UIManager.Instance.GameOver();
        }

    }
}
