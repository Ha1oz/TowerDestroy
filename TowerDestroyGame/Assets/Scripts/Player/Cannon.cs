using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;

    private bool isStartShoot;
    private Ray ray;
    private float touchPoint;

    void Start()
    {
        isStartShoot = true;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            touchPoint = ray.origin.y;
        }

        if (Input.GetMouseButton(0)) {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.Rotate(0f, 0f, (touchPoint - ray.origin.y) * rotateSpeed * Time.deltaTime);

        }
        
        if (Input.GetMouseButtonUp(0)) {

            touchPoint = 0;
            Shoot();
        }


    }

    private void Shoot() {
        if (isStartShoot)
        {
            Instantiate(bullet, shootPoint.position, shootPoint.transform.rotation);
            isStartShoot = false;
            StartCoroutine(ShootDelay());
        } 
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        isStartShoot = true;
    }


}

