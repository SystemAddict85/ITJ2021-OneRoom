using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PuzzleComponent : MonoBehaviour
{
    [FormerlySerializedAs("onPuzzleComponentStart")] public UnityEvent<bool> onPuzzleComponentToggle;
    
    public bool puzzleSatisfied = false;

    public void SetPuzzle(bool isSatisified)
    {
        puzzleSatisfied = isSatisified;
        onPuzzleComponentToggle?.Invoke(puzzleSatisfied);
    }

    public void TogglePuzzle() => SetPuzzle(!puzzleSatisfied);

    private void Start()
    {
        onPuzzleComponentToggle?.Invoke(puzzleSatisfied);
    }
}