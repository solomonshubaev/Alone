using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(GameManagerEvent))]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    #region EVENTS VARIABLES
    private GameManagerEvent gameManagerEvent;
    [SerializeField] private GeneralLightEvent generalLightEvent;
    #endregion

    #region DAT/NIGHT CYCLE VARIABLES
    private float lastTimeAPeriodStarted;
    private float timeForATimePeriod;
    private int currentPeriodNumber;
    private int midFullDayPeriodNumber;
    private float lightIntensityChangeForPeriod;

    private readonly float maxGeneralLightIntensity = 1f;
    private readonly float aFullDayLength = 5f * 60;
    private readonly int aFullDayDifferentPeriods = 80;
    #endregion

    private void Awake()
    {
        this.gameManagerEvent = GetComponent<GameManagerEvent>();
    }

    void Start()
    {
        this.midFullDayPeriodNumber = (int)this.aFullDayDifferentPeriods / 2;
        this.timeForATimePeriod = this.aFullDayLength / aFullDayDifferentPeriods;
        this.lastTimeAPeriodStarted = Time.time;
        this.currentPeriodNumber = 1;
        this.lightIntensityChangeForPeriod = this.maxGeneralLightIntensity / this.midFullDayPeriodNumber;
        this.gameManagerEvent.UpdateDayTimeEvent(this.currentPeriodNumber, this.midFullDayPeriodNumber,
                this.lightIntensityChangeForPeriod, this.maxGeneralLightIntensity);
    }


    void Update()
    {
        this.UpdateDayNightCycle();

        // for testing, will be removed
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
        
    }

    private void UpdateDayNightCycle()
    {
        if (Time.time >= this.lastTimeAPeriodStarted + this.timeForATimePeriod)
        {
            this.currentPeriodNumber++;
            Debug.Log("Changing period number to: " + this.currentPeriodNumber);
            this.gameManagerEvent.UpdateDayTimeEvent(this.currentPeriodNumber, this.midFullDayPeriodNumber,
                this.lightIntensityChangeForPeriod, this.maxGeneralLightIntensity);
            this.lastTimeAPeriodStarted = Time.time;
            if (this.currentPeriodNumber == this.aFullDayDifferentPeriods)
            {
                this.SetNewDay();
            }
        }
    }

    private void SetNewDay()
    {
        this.currentPeriodNumber = 1;
    }

    #region VALIDATION
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (this.aFullDayDifferentPeriods % 2 != 0)
        {
            Debug.LogWarning("aFullDayDifferentPeriods should be an even because we divide it by 2");
        }
    }
#endif
    #endregion
}
