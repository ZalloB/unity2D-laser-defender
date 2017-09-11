using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float firingRate = 0.2f;

    public float speed = 5f;
    public float padding = 1f;
    float xmin;
    float xmax;

    public float health = 100f;

    // Use this for initialization
    void Start () {
        setMinimunLayout();
    }
	
	// Update is called once per frame
	void Update () {
        setMovement();
        manageFire();
    }

    private void setMinimunLayout() {
        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 righttmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftmost.x + padding;
        xmax = righttmost.x - padding;
    }

    private void setMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void manageFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("fire", 0.000000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("fire");
        }
    }

    private void fire()
    {
        Vector3 offset = transform.position + new Vector3(0, 1, 0);
        GameObject bean = Instantiate(projectile, offset, Quaternion.identity) as GameObject;
        bean.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.getDamage();
            missile.hit();

            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }

    }
}
