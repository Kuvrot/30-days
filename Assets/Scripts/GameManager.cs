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
    
    // Start is called before the first frame update
    void Start()
    {
        target = _target;
        EnemyDeathPrefabs = _EnemyDeathPrefabs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#endregion
