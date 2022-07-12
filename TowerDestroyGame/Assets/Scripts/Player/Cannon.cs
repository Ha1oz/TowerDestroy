using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public float speed;//rotateSpeed, defaultSpeed, direction, ;
    private float turnSmoothVelocity;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;

    private bool isFire;
    private bool isStartShoot;

    private float timeNextShoot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
        isStartShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();


    }

    private void Move() {
        //float vert = Input.GetAxisRaw("Vertical");//change to slider
        //Vector3 direction = new Vector3(0f, 0f, vert).normalized;
        //if (direction.magnitude >= 0.1f) {
        //    float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
        //        ref turnSmoothVelocity, turnSmoothTime);
        //    transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //}
        float vert = Input.GetAxisRaw("Vertical");//change to slider
        transform.Rotate(0f, 0f, vert * speed * Time.deltaTime);//need to add <= and >=

        
    }

    private void Shoot() {
        if (Input.GetMouseButtonDown(0)) {
            if (isStartShoot)
            {
                Instantiate(bullet, shootPoint.position, shootPoint.transform.rotation);
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
    public void Fire(bool fire)
    {
        isFire = fire;
    }

}
