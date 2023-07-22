using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperValidations
{
    public static bool ValidateNotNull(object value, string variableName)
    {
        if(value == null)
        {
            Debug.LogWarning(string.Format("Variable's value is null. variable's name: {0}", variableName));
            return false;
        }
        return true;
    }
}
