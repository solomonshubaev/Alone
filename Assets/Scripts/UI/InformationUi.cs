using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InformationUi : MonoBehaviour
{
    [SerializeField] private InformationUiEvent informationUiEvent;
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image staminaBar;

    private void OnEnable()
    {
        this.informationUiEvent.onInformation += this.InformationUiEvent_UpdateUI;
    }

    private void OnDisable()
    {
        this.informationUiEvent.onInformation -= this.InformationUiEvent_UpdateUI;
    }

    private void InformationUiEvent_UpdateUI(InformationUiEvent informationUiEvent, InformationUiArgs informationUiArgs)
    {
        this.UpdateHungerBar(informationUiArgs.hungerPercent);
        this.UpdateStaminaBar(informationUiArgs.staminaPercent);
    }

    private void UpdateHungerBar(float percent)
    {
        this.hungerBar.fillAmount = percent/100f;
    }

    private void UpdateStaminaBar(float percent)
    {
        this.staminaBar.fillAmount = percent/100f;
    }

    private void OnValidate()
    {
        HelperValidations.ValidateNotNull(this.informationUiEvent, "informationUiEvent");
        HelperValidations.ValidateNotNull(this.hungerBar, "hungerBar");
        HelperValidations.ValidateNotNull(this.staminaBar, "staminaBar");
    }
}
