using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{  
    [SerializeField] private GameObject healthLine; // The element that contain image of current health amount

    private Image healthBarImage;   // Image that show current health amount
    private float healthAmount;

    private float defaultHealthAmount = 100;

    private void Awake()
    {
        healthAmount = 100;
        healthBarImage = healthLine.GetComponent<Image>();
    }

    public void PlayerSetDamage(float damageAmount)
    {
        ValidateDamageAmount(damageAmount);

        healthAmount -= damageAmount;
        float healthLineLength = healthAmount / defaultHealthAmount;    // Calculate health bar length

        healthBarImage.fillAmount = healthLineLength;   // Change length of health bar
    }

    private void ValidateDamageAmount(float amount)
    {
        if (amount > defaultHealthAmount)
        {
            throw new System.Exception("DamageAmountGreaterThanFullHeathAmount");
        }
    }
}
