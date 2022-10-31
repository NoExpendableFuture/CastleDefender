using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public ActorHealth playerHealth;
    private float previousPlayerHealth = 3f;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.GetHealth() != previousPlayerHealth) {
            previousPlayerHealth = playerHealth.GetHealth();
            text.text =  "Health: " + previousPlayerHealth;
        }
    }
}
