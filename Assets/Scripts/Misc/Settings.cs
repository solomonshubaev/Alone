using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ANIMATOR PARAMETERS - PLAYER
    public static int lookUp = Animator.StringToHash("lookUp");
    public static int lookSide = Animator.StringToHash("lookSide");
    public static int lookDown = Animator.StringToHash("lookDown");
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isWalking = Animator.StringToHash("isWalking");
    #endregion
}
