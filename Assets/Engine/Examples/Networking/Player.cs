﻿using System;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    private void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(transform.name + " has " + currentHealth + " health");

        if(currentHealth <= 0)
        {

        }
    }
}
