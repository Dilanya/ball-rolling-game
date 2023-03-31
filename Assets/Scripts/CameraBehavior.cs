using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{   
    public Transform Player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
       offset = Player.position - transform.position; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
       transform.position = Player.position - offset;
    }
}
