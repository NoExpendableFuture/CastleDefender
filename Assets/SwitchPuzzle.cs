using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : Puzzle
{
    private PuzzleSwitch[] puzzleSwitches;

    private bool allSwitchesActivated = false;

    public void Start() {
        puzzleSwitches = this.GetComponentsInChildren<PuzzleSwitch>(false);
        foreach (PuzzleSwitch s in puzzleSwitches)
            s.parentSwitchPuzzle = this;
    }

    public void NotifySwitchUpdate(PuzzleSwitch puzzleSwitch)
    {
        // Check if all child switches are pressed - if yes puzzle is complete. If no, puzzle incomplete
        bool complete = true;

        foreach (PuzzleSwitch ps in puzzleSwitches) {
            if (!ps.Pressed) {
                complete = false;
            }
        }
        
        allSwitchesActivated = complete;

        if(complete) {
            door.Open();
        } else {
            door.Close();
        }         
    }

    public override bool PuzzleComplete() {
        return allSwitchesActivated;
    }

}
