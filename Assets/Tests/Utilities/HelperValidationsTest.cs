using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Moq;
using UnityEngine.TestTools;

public class HelperValidationsTest
{
    
    [Test]
    public void ValidateNotNull_Null()
    {
        //Arrange
        string x = null;

        //Act
        bool is_valid = HelperValidations.ValidateNotNull(x, nameof(x));

        //Assert
        Assert.IsFalse(is_valid, "Should be false");
    }    

    [Test]
    public void ValidateNotNull_NotNull()
    {
        //Arrange
        string x = "some value";
        
        //Act
        bool is_valid = HelperValidations.ValidateNotNull(x, nameof(x));

        //Assert
        Assert.IsTrue(is_valid, "Should be true");
    }
}
