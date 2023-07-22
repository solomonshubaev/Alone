using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Moq;

public class HelperUtilitiesTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test()
    {
        Assert.AreEqual(0, 0);
    }

    #region GetAngleFromVector
    [Test]
    public void GetAngleFromVector_Up()
    {
        Vector3 vector = new Vector3(0f, 1f, 0f);
        float degrees = HelperUtilities.GetAngleFromVector(vector);
        Assert.AreEqual(90f, degrees);
    }

    [Test]
    public void GetAngleFromVector_Down()
    {
        Vector3 vector = new Vector3(0f, -1f, 0f);
        float degrees = HelperUtilities.GetAngleFromVector(vector);
        Assert.AreEqual(-90f, degrees);
    }

    [Test]
    public void GetAngleFromVector_Right()
    {
        Vector3 vector = new Vector3(1f, 0f, 0f);
        float degrees = HelperUtilities.GetAngleFromVector(vector);
        Assert.AreEqual(0f, degrees);
    }
    #endregion

    #region GetAimDirection
    [Test]
    public void GetAimDirection_Up()
    {
        float angleDegrees = 90f;
        AimDirection aimDirection = HelperUtilities.GetAimDirection(angleDegrees);
        Assert.AreEqual(AimDirection.Up, aimDirection);
    }

    [Test]
    public void GetAimDirection_Left_Positive()
    {
        float angleDegrees = 170f;
        AimDirection aimDirection = HelperUtilities.GetAimDirection(angleDegrees);
        Assert.AreEqual(AimDirection.Side, aimDirection);
    }

    [Test]
    public void GetAimDirection_Left_Negative()
    {
        float angleDegrees = -170f;
        AimDirection aimDirection = HelperUtilities.GetAimDirection(angleDegrees);
        Assert.AreEqual(AimDirection.Side, aimDirection);
    }

    [Test]
    public void GetAimDirection_Right()
    {
        float angleDegrees = 30f;
        AimDirection aimDirection = HelperUtilities.GetAimDirection(angleDegrees);
        Assert.AreEqual(AimDirection.Side, aimDirection);
    }

    [Test]
    public void GetAimDirection_Down()
    {
        float angleDegrees = -90f;
        AimDirection aimDirection = HelperUtilities.GetAimDirection(angleDegrees);
        Assert.AreEqual(AimDirection.Down, aimDirection);
    }
    #endregion
}
