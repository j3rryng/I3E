using UnityEngine;
using UnityEngine.InputSystem;

public class GiftBox : MonoBehaviour
{
    [SerializeField] private int requiredPresses = 3;

    private int pressCount = 0;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            pressCount++;
            Debug.Log($"Gift box pressed {pressCount}/{requiredPresses}");

            if (pressCount >= requiredPresses)
                DestroyBox();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void DestroyBox()
    {
        Destroy(gameObject);
    }
}