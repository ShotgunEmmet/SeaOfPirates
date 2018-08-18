using UnityEngine;
using System.Collections;
using Application;
using UnityEngine.Networking;

public class BombBag : MonoBehaviour, IWeapon
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject CreateBomb(GameObject ammoPrefab)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 offset = rb.velocity.normalized * 2f;//blank for the host
        GameObject instance = Instantiate(ammoPrefab);
        instance.transform.position = transform.position + new Vector3(offset.x, offset.y, 0);
        instance.transform.rotation = Quaternion.identity;

        return instance;
    }

    public GameObject Fire(GameObject ammoPrefab)
    {
        return CreateBomb(ammoPrefab);
    }
}
