using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHealth : MonoBehaviour {
    public float maxHealth = 3f;
    private float health = 1f;
    
    public float invincibilityTime = 0.75f;
    private bool invincibilityFramesActive = false;
    private float invincibilityDuration = 0f;

    public bool physicalDamageImmune = false;

    void Start() {
        health = maxHealth;
    }

    void Update() {
        if(invincibilityFramesActive) {
            invincibilityDuration += Time.deltaTime;
            if(invincibilityDuration >= invincibilityTime) {
                invincibilityFramesActive = false;
            }
        }
    }

    public void DamageWithHazard(Hazard hazard) {
        if(!invincibilityFramesActive && health > 0f) {
            getPhysicalDamage(hazard);
            invincibilityFramesActive = true;
            invincibilityDuration = 0f;

            if (health <= 0f) {
                GetComponent<Actor>().Kill();
            }
        }
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetHealth() {
        return health;
    }

    private void getPhysicalDamage(Hazard hazard) {
        if(hazard.getPhysicalDamage() > 0f && !physicalDamageImmune) {
            // TODO: Animate the damage!
            health -= hazard.getPhysicalDamage();
            UnityEngine.Debug.Log("OW!");
        }
    }
}