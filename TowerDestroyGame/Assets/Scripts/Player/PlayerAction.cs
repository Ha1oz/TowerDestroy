using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public int health;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject cannon, bulletPrefab, shield;
    [SerializeField] private Transform shootPoint;//, shieldPoint;
    [SerializeField] private float shootDelay;
    [SerializeField] private int shieldDelay,shieldHealth;

    private bool isStartShoot, isShieldActive;
    //private float callDown = 0f;
    private Ray ray;
    private float touchPoint;

    void Start()
    {
        isStartShoot = true;
        isShieldActive = false;
    }

    void Update()
    {
        RotateCannon();
        Shoot();
        ActivateShield();
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

            cannon.transform.Rotate(0f, 0f, (touchPoint - ray.origin.y) * rotateSpeed * Time.deltaTime);

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
            
        if (Input.GetMouseButtonDown(1) && 
            !shield.activeInHierarchy && !isShieldActive) { //UI button
            shield.GetComponent<Shield>().Create(shieldHealth);
            isShieldActive = true;
            StartCoroutine(ShieldCooldown());
        }
    }
    IEnumerator ShieldCooldown()
    {
        //timer
        yield return new WaitForSeconds(shieldDelay);
        isShieldActive = false;
    }

    public void HitDamage(int damage = 1)
    {
        health -= damage;
        Debug.Log("Health: " + health);
        //GameManager.Instance.UpdateHPBar();

        if (health <= 0)
        {
            //GameManager.Instance.GameOver();
        }
    }
}
