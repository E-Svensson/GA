using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public PlayerMovement playerMovement;

    void Update()
    {
        var StaminaStats = playerMovement.GetStamina();
        slider.value = StaminaStats.x;
        slider.maxValue = StaminaStats.y;
    }
}
    
