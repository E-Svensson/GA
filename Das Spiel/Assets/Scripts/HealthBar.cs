using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerMovement playerMovement;
    public Enemy enemy;
    void Update()
    {
        if(playerMovement != null)
        {
            var HealthStats = playerMovement.GetHealth();
            slider.value = HealthStats.x;
            slider.maxValue = HealthStats.y;
        }

        else if(enemy != null)
        {
            var HealthStats = enemy.GetHealth();
            slider.value = HealthStats.x;
            slider.maxValue = HealthStats.y;
        }
    }
}
