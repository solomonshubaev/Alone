using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperValidations
{
    public static bool ValidateNotNull(object value, string variableName, string message = null)
    {
        if(value == null)
        {
            if (message != null)
            {
                Debug.LogWarning(string.Format("{0}. variable's name: {1}", message ,variableName));
                return false;
            }
            else
            {
                Debug.LogWarning(string.Format("Variable's value is null. variable's name: {0}", variableName));
                return false;
            }
        }
        return true;
    }
}
