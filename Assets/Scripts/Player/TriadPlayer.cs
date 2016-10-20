using UnityEngine;

public abstract class TriadPlayer : MonoBehaviour {

    protected bool draggingCard;

    private void Start() {
        InitializeTriadPlayer();
    }

    private void Update() {
        UpdateTriadPlayer();
    }

    private void LateUpdate() {
        UpdateTriadPlayerControls();
    }

    protected abstract void InitializeTriadPlayer();

    protected abstract void UpdateTriadPlayer();

    protected abstract void UpdateTriadPlayerControls();
}
