using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Stats")]
    public int ID = 0;
    public float health = 100;
    public float Damage = 5f;
    public float ReloadingTime = 1f;
    public float FireRate = 0.15f;  //How much time it takes to fire the gun again.
    public float StoppingPower = 50; //The power it has to slow the gouls advance;
    public bool Automatic = false;
    public float AimSpeed = 10;
 
    [Header("Ammo")]
    public int currAmmo = 0;
    public int maxAmmo = 0;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;

    public Vector3 NormalPosition;
    public Vector3 AimPosition;
}
