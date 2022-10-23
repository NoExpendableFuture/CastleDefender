using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndPopUp : MonoBehaviour
{
    void Start() {
        HidePopup();
    }

    public void ShowPopup() {
        gameObject.SetActive(true);
    }

    public void HidePopup() {
        gameObject.SetActive(false);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
