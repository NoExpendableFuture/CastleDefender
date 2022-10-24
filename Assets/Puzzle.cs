using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour {
    
    public PuzzleDoor door;

    public abstract bool PuzzleComplete();
}
