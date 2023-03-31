using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    private float x, z;
    private Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void FixedUpdate(){
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        force = Vector3.forward * z + Vector3.right * x;
        rb.AddForce(force * speed);
    }

    void OnTriggerEnter(Collider otherObj)
    {
      
      if(otherObj.GetComponent<SphereData>() == null)return;
      else if(otherObj.GetComponent<MeshRenderer>().enabled == false)return;

      otherObj.GetComponent<SphereData>().emitParticles();
      otherObj.GetComponent<MeshRenderer>().enabled = false;

      GameManager.Instance.AddScore();

      GameManager.Instance.playAudio();
    }
} 
