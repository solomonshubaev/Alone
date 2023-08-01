using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{

    void Start()
    {

    }

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

    }
#endif
    #endregion
}
