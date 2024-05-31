using UnityEngine;
using TMPro;

public class Interactions : MonoBehaviour
{
    public PlayerAimController playerAim;
    public bool playerIsClose;
    public string Action;
    public string Key;
    public TextMeshProUGUI Interact;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) 
        {
            playerIsClose = true;
            Interact.text = $"Use {Key} to {Action}";
            playerAim.CanFire = false;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            playerIsClose = false;
            Interact.text = "";
            playerAim.CanFire = true;
        }
    }
}
