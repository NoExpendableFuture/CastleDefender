using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour, Hazard
{
    public Puzzle linkedOrb;
    public Actor spawnPrefab;
    public int maxActiveSpawns = 3;
    public int activeSpawns = 0;
    public float spawnCooldownDuration = 2f;   
    private float spawnCooldownTimeElapsed = 0f;
    
    private bool active = true;
    public float physicalDamage = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if(linkedOrb == null) {
            UnityEngine.Debug.LogWarning(gameObject.name + " not linked to orb!");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if(linkedOrb.PuzzleComplete()) {
            active = false;
            // TODO: Animate the portal turning off
            // Destroy(this);
        } 
        
        if (active){
            spawnCooldownTimeElapsed += Time.deltaTime;
            CheckSpawn();
        }
    }

    void CheckSpawn() {
        if(activeSpawns < maxActiveSpawns && spawnCooldownTimeElapsed >= spawnCooldownDuration) {
            Actor newlySpawned = Instantiate(spawnPrefab, transform.position, UnityEngine.Quaternion.identity);
            newlySpawned.spawnFromPoint = this;
            activeSpawns++;
            spawnCooldownTimeElapsed = 0f;
            
            if(newlySpawned.GetComponent<ActorInputPlayerChase>() != null) {
                newlySpawned.GetComponent<ActorInputPlayerChase>().target = FindObjectOfType<Player>();
            }
        }
    }

    public void SignalSpawnedActorDestroyed(Actor destroyed) {
        activeSpawns--;
    }

    public bool isActiveHazard() {
        return active;
    }
    
    public float getPhysicalDamage() {
        return physicalDamage;   
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
}
