using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region singleton


public class GameManager : MonoBehaviour
{
    static public Transform target; //Target the enemies will look for
    public Transform _target;

    [Header("Enemies")]
    public GameObject[] _EnemyDeathPrefabs;
    static public GameObject[] EnemyDeathPrefabs;

    [Header("Objects")]
    public GameObject bulletImpact;
    static public GameObject BulletImpact;
    
    // Start is called before the first frame update
    void Awake()
    {
        target = _target;
        EnemyDeathPrefabs = _EnemyDeathPrefabs;
        BulletImpact = bulletImpact;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#endregion
