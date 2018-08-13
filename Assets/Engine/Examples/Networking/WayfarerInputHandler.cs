using System;
using Application;
using UnityEngine;
using UnityEngine.Networking;

public class WayfarerInputHandler : NetworkBehaviour{

    public GameObject bombPrefab;
    public GameObject bulletPrefab;

    public IWeapon WeaponSlotA;
    public IWeapon WeaponSlotB;
    public IWeapon WeaponSlotC;
    private void Start()
    {
        WeaponSlotA = gameObject.AddComponent<Fists>();
        WeaponSlotB = gameObject.AddComponent<Blunderbuss>();
        WeaponSlotC = gameObject.AddComponent<BombBag>();
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            CmdFireWeaponA();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            CmdFireWeaponB();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            CmdFireWeaponC();
        }
    }


    [Command]
    private void CmdFireWeaponA()
    {
        var projectile = WeaponSlotA.Fire(null);
        CreateOnServer(projectile);
    }
    [Command]
    private void CmdFireWeaponB()
    {
        var projectile = WeaponSlotB.Fire(bulletPrefab);
        CreateOnServer(projectile);
    }
    [Command]
    private void CmdFireWeaponC()
    {
        var projectile = WeaponSlotC.Fire(bombPrefab);
        CreateOnServer(projectile);
    }
    private static void CreateOnServer(GameObject projectile)
    {
        Debug.Log(projectile);
        if (projectile != null)
        {
            NetworkServer.Spawn(projectile);
        }
    }
}
