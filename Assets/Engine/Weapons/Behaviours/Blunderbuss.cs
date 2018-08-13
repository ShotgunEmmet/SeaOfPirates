using UnityEngine;
using System.Collections;
using Application;

public class Blunderbuss : MonoBehaviour, IWeapon
{
    public GameObject Fire(GameObject ammoPrefab)
    {
        return CreateBullet(ammoPrefab);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private GameObject CreateBullet(GameObject ammoPrefab)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            ammoPrefab,
            transform.position + new Vector3(1, 1, 0),
            Quaternion.AngleAxis(45, Vector3.forward));

        var bullet2D = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, 45) * Vector2.down);
        bullet2D.velocity = dir * 2f;

        Destroy(bullet, 20);


        return bullet;
    }

}
