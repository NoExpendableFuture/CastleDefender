using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, Hazard
{
    public bool activeHazard = true;

    public float physicalDamage = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        hurtPlayer(other);
    }

    public void OnTriggerStay2D(Collider2D other) {
        hurtPlayer(other);
    }

    void hurtPlayer(Collider2D other) {
        if(other.tag == "Player" && other.GetComponent<ActorHealth>() != null){
            other.GetComponent<ActorHealth>().DamageWithHazard(this);
        }
    }

    public bool isActiveHazard() {
        return activeHazard;
    }
    
    public float getPhysicalDamage() {
        return physicalDamage;   
    }

}
