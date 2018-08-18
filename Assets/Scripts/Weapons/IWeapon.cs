using System;
using UnityEngine;
using System.Collections;

namespace Application
{
    public interface IWeapon
    {
        GameObject Fire(GameObject ammoPrefab);
    }
}
