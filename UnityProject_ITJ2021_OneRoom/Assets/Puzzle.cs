using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public UnityEvent onPuzzleSatisfied;

    [SerializeField] private List<PuzzleComponent> puzzleComponents;


    private void Awake()
    {
        onPuzzleSatisfied.AddListener(DisablePuzzlePieces);
    }

    public void CheckPuzzleComplete()
    {
        bool isSatisfied = true;
        foreach (var puzz in puzzleComponents)
            if (puzz.puzzleSatisfied == false)
            {
                isSatisfied = false;
                break;
            }

        if (isSatisfied)
            onPuzzleSatisfied?.Invoke();
    }

    private void DisablePuzzlePieces()
    {
        foreach (var puzz in puzzleComponents)
        {
            var use = puzz.GetComponent<UseInteraction>();
            if (use.IsUnityNull() == false)
            {
                use.ToggleReadyToInteract(false);
            }
        }
    }
}