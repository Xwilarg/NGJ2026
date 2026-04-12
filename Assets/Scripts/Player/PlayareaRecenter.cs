using System;
using NGJ2026.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayareaRecenter : MonoBehaviour {
    public Transform root;
    public Transform head;
    public GameManager gameManager; 

    private void Start() {
        Recenter();
    }

    private void Update() {
        if (Keyboard.current.pKey.wasPressedThisFrame && !gameManager.IsScorePanelOpen){
            Recenter();
        }
    }

    public void Recenter() {
        var headPos = head.position;
        headPos.y = 0;
        root.position -= headPos;
    }
}
