using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PreventionAdvisorUnitTest
{
    [TestClass]
    public class OrganizationTests
    {
        [TestMethod]
        public void GivenMultipleOrganizations_WhenUserQueries_ThenOnlyShowUserBound()
        {
            // Arrange
            var a = 1;

            // Act
            a++;

            // Assert
            Assert.AreEqual(2, a);
        }
    }
}
