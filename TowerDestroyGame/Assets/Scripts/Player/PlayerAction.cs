using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Player Settings")]
    public float health; 

    [Header("Cannon Settings")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject cannon, bulletPrefab;
    [SerializeField] private Transform shootPoint, rotatePoint;
    [SerializeField] private float shootDelay;

    [Header("Shield Settings")]
    [SerializeField] private GameObject shield;
    [SerializeField] private int shieldDelay, shieldHealth;

    private bool isStartShoot;
    private const float minRotateAngle = -30f, maxRotateAngle = 10f;
    private float touchPoint;
    private Ray ray;

    void Start()
    {
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

            float newAngle = Mathf.Clamp(angle - (touchPoint - ray.origin.y), minRotateAngle, maxRotateAngle);

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
    public int getShieldDelay() {
        return shieldDelay;
    }
}
