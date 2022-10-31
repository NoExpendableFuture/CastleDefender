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
    public bool knockbackDamageImmune = false;

    private Actor ac;

    void Start() {
        health = maxHealth;
        ac = GetComponent<Actor>();
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
        
        bool physicalDamageApplied = applyPhysicalDamage(meleeAttack);
        bool knockbackDamageApplied = applyKnockbackDamage(meleeAttack);
        
        if(physicalDamageApplied || knockbackDamageApplied) {
            
            invincibilityFramesActive = true;
            invincibilityTimeElapsed = 0f;

            if (health <= 0f) {
                GetComponent<Actor>().Kill();
            }
        }
    }

    private bool applyPhysicalDamage(Hazard hazard) {
        if(hazard.getPhysicalDamage() > 0f && !physicalDamageImmune) {
            // TODO: Animate the damage!
            health -= hazard.getPhysicalDamage();
            UnityEngine.Debug.Log("OW! Entered hazard");
            return true;
        }
        return false;
    }
    
    private bool applyPhysicalDamage(MeleeAttack meleeAttack) {
        if(meleeAttack.getPhysicalDamage() > 0f && !physicalDamageImmune) {
            // TODO: Animate the damage!
            health -= meleeAttack.getPhysicalDamage();
            UnityEngine.Debug.Log("OW! Hit by attack " + meleeAttack.name);
            return true;
        }
        return false;    
    }
    
    private bool applyKnockbackDamage(MeleeAttack meleeAttack) {
        if(meleeAttack.getKnockBackDamage() > 0f && !knockbackDamageImmune) {
            // TODO: Animate the damage!
            Vector2 knockbackDirectionAndForce = (transform.position - meleeAttack.attacker.transform.position) * meleeAttack.getKnockBackDamage();
            ac.StartKnockback(knockbackDirectionAndForce, meleeAttack.getKnockBackDuration());
            // UnityEngine.Debug.Log("OW! Knocked back by " + meleeAttack.name);
            return true;
        }
        return false;
    }
}