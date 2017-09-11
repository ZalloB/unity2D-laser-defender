using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    float damage = 50f;

    public float getDamage()
    {
        return damage;
    }

    public void hit()
    {
        Destroy(gameObject);
    }
}
