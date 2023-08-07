using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InformationUi : MonoBehaviour
{
    [SerializeField] private InformationUiEvent informationUiEvent;
    [SerializeField] private Image vitalityBar;
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
        this.UpdateVitalityBar(informationUiArgs.vitalityPercent);
        this.UpdateStaminaBar(informationUiArgs.staminaPercent);
    }

    private void UpdateVitalityBar(float percent)
    {
        this.vitalityBar.fillAmount = percent/100f;
    }

    private void UpdateStaminaBar(float percent)
    {
        this.staminaBar.fillAmount = percent/100f;
    }

    private void OnValidate()
    {
        HelperValidations.ValidateNotNull(this.informationUiEvent, "informationUiEvent");
        HelperValidations.ValidateNotNull(this.vitalityBar, "hungerBar");
        HelperValidations.ValidateNotNull(this.staminaBar, "staminaBar");
    }
}
