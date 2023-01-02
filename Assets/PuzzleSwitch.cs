using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{
    public bool holdToActivate = false;

    // Set this from SwitchPuzzle 
    public SwitchPuzzle parentSwitchPuzzle;

    public bool Pressed {get{return pressed;}}
    private bool pressed = false;

    private GameObject activatedBy = null;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!pressed && (other.GetComponent<Player>() != null || other.GetComponent<Block>() != null)) {
            // Debug.Log("Switch trigger enter - pressed = " + pressed);
            press(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(!pressed && (other.gameObject.GetComponent<Player>() != null || other.gameObject.GetComponent<Block>() != null)) {
            // Debug.Log("Switch trigger stay - pressed = " + pressed);
            press(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (holdToActivate && other.gameObject != null && other.gameObject == activatedBy) {
            // Debug.Log("Switch trigger exit");
            depress();
        }
    }

    private void press(GameObject pressedBy) {
        // Debug.Log("Switch pressed");
        pressed = true;
        activatedBy = pressedBy;
        parentSwitchPuzzle.NotifySwitchUpdate(this);
        if(!audioSource.isPlaying){
            // Debug.Log("Switch playing sound");
            audioSource.Play();
        }
        GetComponent<Animator>().SetBool("pressed", true);
    }

    private void depress () {
        // Debug.Log("Switch de-pressed");
        pressed = false;
        activatedBy = null;
        parentSwitchPuzzle.NotifySwitchUpdate(this);
        if(!audioSource.isPlaying){
            // Debug.Log("Switch playing sound");
            audioSource.Play();
        }
        GetComponent<Animator>().SetBool("pressed", false);        
    }
}
