using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] private PlayerDetailsSO playerDetails;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // for testing will be removed
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
        
    }

    #region VALIDATION
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperValidations.ValidateNotNull(this.playerDetails, nameof(this.playerDetails));
    }
#endif
    #endregion
}
