using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UiManager : MonoBehaviour
{
    [SerializeField] private PlayerInformationEvent playerInfoEvent;
    [SerializeField] private GameManagerEvent gameManagerEvent;
    [SerializeField] private Image vitalityBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Text survivalDayText;

    private void OnEnable()
    {
        this.playerInfoEvent.updatePlayerInformation += this.PlayerInformationEvent_UpdatePlayerInfo;
        this.gameManagerEvent.newDay += this.UiManagerEvent_UpdateGameInfo;
    }

    private void OnDisable()
    {
        this.playerInfoEvent.updatePlayerInformation -= this.PlayerInformationEvent_UpdatePlayerInfo;
        this.gameManagerEvent.newDay -= this.UiManagerEvent_UpdateGameInfo;

    }

    private void PlayerInformationEvent_UpdatePlayerInfo(PlayerInformationEvent playerInformationEvent,
        PlayerInformationArgs playerInformationArgs)
    {
        this.UpdateVitalityBar(playerInformationArgs.vitalityPercent);
        this.UpdateStaminaBar(playerInformationArgs.staminaPercent);
    }

    private void UiManagerEvent_UpdateGameInfo(GameManagerEvent gameManagerEvent,
        GameManagerArgs_SurvivalDay gameManagerArgs)
    {
        this.survivalDayText.text = string.Format("Survival Day: {0}", gameManagerArgs.survivalDay);
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
        HelperValidations.ValidateNotNull(this.playerInfoEvent, "informationUiEvent");
        HelperValidations.ValidateNotNull(this.vitalityBar, "hungerBar");
        HelperValidations.ValidateNotNull(this.staminaBar, "staminaBar");
    }
}
