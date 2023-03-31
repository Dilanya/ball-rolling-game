using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereData : MonoBehaviour
{
    
    public ParticleSystem particles;
    public int count = 10;
    public void emitParticles(){
        particles.Emit(count);
    }
    void Update(){
        transform.Rotate(transform.up);
    }
}
