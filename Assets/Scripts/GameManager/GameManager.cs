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
    private readonly int periodsPerADay = 80;
    #endregion

    private int survivalDay;

    protected override void Awake()
    {
        base.Awake();
        this.gameManagerEvent = GetComponent<GameManagerEvent>();
    }

    void Start()
    {
        this.midFullDayPeriodNumber = (int)this.periodsPerADay / 2;
        this.timeForATimePeriod = this.aFullDayLength / periodsPerADay;
        this.lastTimeAPeriodStarted = Time.time;
        this.currentPeriodNumber = 1;
        this.lightIntensityChangeForPeriod = this.maxGeneralLightIntensity / this.midFullDayPeriodNumber;
        this.gameManagerEvent.UpdateDayTimeEvent(this.currentPeriodNumber, this.midFullDayPeriodNumber,
                this.lightIntensityChangeForPeriod, this.maxGeneralLightIntensity);
        this.survivalDay = 1;
        this.gameManagerEvent.NewDay(this.survivalDay);
    }


    void Update()
    {
        this.UpdateDayNightCycle();

        // for testing, will be removed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.PassTimeToPeriod(10); // test only
        }

        // for testing, will be removed
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
        
    }

    private void PassTimeToPeriod(int periodToPass)
    {
        int newPeriodNumber = this.currentPeriodNumber + periodToPass;
        if (newPeriodNumber > this.periodsPerADay)
            this.SetNewDay(newPeriodNumber % this.periodsPerADay);
        else
            this.currentPeriodNumber = newPeriodNumber;
        Debug.Log("New period number is: " + this.currentPeriodNumber);
        this.lastTimeAPeriodStarted = Time.time;
        this.gameManagerEvent.UpdateDayTimeEvent(newPeriodNumber, this.midFullDayPeriodNumber, this.lightIntensityChangeForPeriod, this.maxGeneralLightIntensity);
    }

    private void UpdateDayNightCycle()
    {
        if (Time.time >= this.lastTimeAPeriodStarted + this.timeForATimePeriod)
        {
            this.currentPeriodNumber++;
            this.gameManagerEvent.UpdateDayTimeEvent(this.currentPeriodNumber, this.midFullDayPeriodNumber,
                this.lightIntensityChangeForPeriod, this.maxGeneralLightIntensity);
            this.lastTimeAPeriodStarted = Time.time;
            if (this.currentPeriodNumber == this.periodsPerADay)
            {
                this.SetNewDay();
            }
        }
    }

    private void SetNewDay(int periodInDay = 1)
    {
        Debug.Log("New survival day");
        this.currentPeriodNumber = periodInDay;
        this.survivalDay++;
        this.gameManagerEvent.NewDay(this.survivalDay);
    }

    #region VALIDATION
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (this.periodsPerADay % 2 != 0)
        {
            Debug.LogWarning("aFullDayDifferentPeriods should be an even because we divide it by 2");
        }
    }
#endif
    #endregion
}
