using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayareaRecenter : MonoBehaviour {
    public Transform root;
    public Transform head;

    private void Start() {
        Recenter();
    }

    private void Update() {
        if (Keyboard.current.pKey.wasPressedThisFrame){
            Recenter();
        }
    }

    public void Recenter() {
        var headPos = head.position;
        headPos.y = 0;
        root.position -= headPos;
    }
}
