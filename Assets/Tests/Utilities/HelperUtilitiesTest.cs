using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Moq;

public class HelperUtilitiesTest
{
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

    #region CalculatePercent
    [Test]
    public void CalculatePercentTest()
    {
        float maxValue = 1600f;
        float currentValue = 800f;
        Assert.AreEqual(50.0f, HelperUtilities.CalculatePercent(maxValue, currentValue));
    }
    #endregion

    #region GetLookDirection
    [Test]
    public void CalculateLookingDirection_Right()
    {
        Vector2 vector = new Vector2(1, 0);
        Assert.AreEqual(LookDirection.Right, HelperUtilities.GetLookDirection(vector));
    }
    [Test]
    public void CalculateLookingDirection_Left()
    {
        Vector2 vector = new Vector2(-1, 0);
        Assert.AreEqual(LookDirection.Left, HelperUtilities.GetLookDirection(vector));
    }
    [Test]
    public void CalculateLookingDirection_Up()
    {
        Vector2 vector = new Vector2(0, 1);
        Assert.AreEqual(LookDirection.Up, HelperUtilities.GetLookDirection(vector));
    }
    [Test]
    public void CalculateLookingDirection_Down()
    {
        Vector2 vector = new Vector2(0, -1);
        Assert.AreEqual(LookDirection.Down, HelperUtilities.GetLookDirection(vector));
    }
    #endregion
}
