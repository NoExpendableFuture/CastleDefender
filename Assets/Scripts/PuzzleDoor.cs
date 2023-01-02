using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    public void Open() {
        // TODO: Animate pls
        gameObject.SetActive(false);
    }
    public void Close() {
        // TODO: Animate pls
        gameObject.SetActive(true);
    }
}
