using System;
using Application;
using UnityEngine;
using UnityEngine.Networking;

public class WayfarerInputHandler : NetworkBehaviour{

    public GameObject bombPrefab;
    public GameObject bulletPrefab;
    private Animator animator;

    [SerializeField]
    private float speed = 2.4f;
    [SyncVar(hook = "OnChangeDirection")]
    Vector2 moveDirection;
    public NetworkChest NearbyTriggerable;
    public IWeapon WeaponSlotA;
    public IWeapon WeaponSlotB;
    public IWeapon WeaponSlotC;
    private PlayerMotor motor;

    private void Start()
    {
        InitialiseComponents();
    }

    private void InitialiseComponents()
    {
        WeaponSlotA = gameObject.AddComponent<Fists>();
        WeaponSlotB = gameObject.AddComponent<Blunderbuss>();
        WeaponSlotC = gameObject.AddComponent<BombBag>();
        motor = GetComponent<PlayerMotor>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        ProcessButtonInput();
        ProcessDirectionInput();
        UpdateAnimation();
    }

    private void ProcessButtonInput()
    {
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

    private void ProcessDirectionInput()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

        moveDirection = inputDirection;
        motor.Move(moveDirection);
    }

    private void UpdateAnimation()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            animator.SetFloat("FaceX", moveDirection.x);
            animator.SetFloat("FaceY", moveDirection.y);
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    void OnChangeDirection(Vector2 newDirection)
    {
        moveDirection = newDirection;
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
        Rigidbody2D bulletBody = projectile.GetComponent<Rigidbody2D>();
        Debug.Log("b vel:"+bulletBody.velocity);
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
    public void SetNearbyTriggerable(NetworkChest nearbyTriggerable){
        NearbyTriggerable = nearbyTriggerable;
    }
    public void LeaveNearbyTriggerable(NetworkChest nearbyTriggerable)
    {
        if(NearbyTriggerable ==nearbyTriggerable){
            NearbyTriggerable = null; 
        }
    }
}
