using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Puzzle linkedOrb;
    public Actor spawnPrefab;
    public int maxActiveSpawns = 3;
    public int activeSpawns = 0;
    public float spawnCooldownDuration = 2f;   
    public float spawnCooldownTimeElapsed = 2f;
    
    // private bool active = true;

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
            Destroy(this);
        } else {
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
}
