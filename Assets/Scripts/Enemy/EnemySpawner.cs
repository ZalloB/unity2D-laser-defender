using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float speed = 5f;

    public float width = 10f;
    public float height = 5f;

    private bool movingRight = false;

    private float xmax;
    private float xmin;

    // Use this for initialization
    void Start () {

        enemyInstantiate();
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {
        moveManagement();

        if (AllMembersDead())
        {
            enemyInstantiate();
        }
	}

    private void moveManagement()
    {
        if (movingRight)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else
            transform.position += Vector3.left * speed * Time.deltaTime;

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
    }

    private void enemyInstantiate()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        xmax = rightBoundary.x;
        xmin = leftBoundary.x;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }


    bool AllMembersDead(){

        foreach (Transform childPositionGameObject in transform) {

            if(childPositionGameObject.childCount > 0)
                return false;
        }

        return true;
    }
}
