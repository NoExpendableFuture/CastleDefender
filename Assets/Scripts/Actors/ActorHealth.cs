using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHealth : MonoBehaviour {
    public float maxHealth = 3f;
    private float health = 1f;
    
    public float invincibilityDuration = 0.75f;
    private bool invincibilityFramesActive = false;
    private float invincibilityTimeElapsed = 0f;

    public bool damagedByPlayer = true;
    public bool damagedByEnemy = true;

    public bool physicalDamageImmune = false;

    void Start() {
        health = maxHealth;
    }

    void Update() {
        if(invincibilityFramesActive) {
            invincibilityTimeElapsed += Time.deltaTime;
            if(invincibilityTimeElapsed >= invincibilityDuration) {
                invincibilityFramesActive = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(!invincibilityFramesActive && health > 0f) {
            MeleeAttack meleeAttack = other.GetComponent<MeleeAttack>();
            if(other.GetComponent<MeleeAttack>() != null) {
                if(damagedByPlayer && meleeAttack.attackerType == ActorType.Player) {
                    applyMeleeAttackDamage(meleeAttack);
                }
                if(damagedByEnemy && meleeAttack.attackerType == ActorType.Enemy) {
                    applyMeleeAttackDamage(meleeAttack);
                }
            }
        }
    }

    public void DamageWithHazard(Hazard hazard) {
        if(!invincibilityFramesActive && health > 0f) {
            applyPhysicalDamage(hazard);
            invincibilityFramesActive = true;
            invincibilityTimeElapsed = 0f;

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

    private void applyMeleeAttackDamage(MeleeAttack meleeAttack) {
        applyPhysicalDamage(meleeAttack);
        invincibilityFramesActive = true;
        invincibilityTimeElapsed = 0f;
        if (health <= 0f) {
            GetComponent<Actor>().Kill();
        }
    }

    private void applyPhysicalDamage(Hazard hazard) {
        if(hazard.getPhysicalDamage() > 0f && !physicalDamageImmune) {
            // TODO: Animate the damage!
            health -= hazard.getPhysicalDamage();
            UnityEngine.Debug.Log("OW! Entered hazard");
        }
    }
    
    private void applyPhysicalDamage(MeleeAttack meleeAttack) {
        if(meleeAttack.getPhysicalDamage() > 0f && !physicalDamageImmune) {
            // TODO: Animate the damage!
            health -= meleeAttack.getPhysicalDamage();
            UnityEngine.Debug.Log("OW! Hit by attack " + meleeAttack.name);
        }
    }
}