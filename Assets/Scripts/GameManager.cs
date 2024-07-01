using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region singleton


public class GameManager : MonoBehaviour
{
    static public Transform target; //Target the enemies will look for
    public Transform _target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#endregion
