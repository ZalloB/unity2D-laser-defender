using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject projectile;
    private float projectileSpeed = 10f;
    private float health = 150f;
    private float shotsPerSeconds = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        manageFire();
    }

    void manageFire()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability)
            fire();
    }

    void fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;

        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.getDamage();
            missile.hit();

            if (health<= 0)
            {
                Destroy(gameObject);
            }
           
        }
        
    }

}
