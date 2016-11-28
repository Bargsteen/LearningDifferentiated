﻿using ADL.Controllers;
using ADL.Models;
using ADL.Models.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ADLTest2
{
    public class AssignmentTest
    {
        [Fact]
        public void Can_List_Assignments()
        {

            // Arrange
            Mock<IAssignmentRepository> mockAssigntment = new Mock<IAssignmentRepository>();
            Mock<ILocationRepository> mockLocation = new Mock<ILocationRepository>();

            mockAssigntment.Setup(m => m.Assignments).Returns(new Assignment[]
            {
                new Assignment {Headline = "h1", Question = "Hej"},
                new Assignment {Headline = "h2", Question = "hej2"},
                new Assignment {Headline = "h3", Question = "hej3"}
            });

            AssignmentController controller = new AssignmentController(mockAssigntment.Object, mockLocation.Object);
            // Act
            IEnumerable<Assignment> result =
                   ((AssignmentAndLocationListViewModel)controller.List().Model).Assignments as IEnumerable<Assignment>;

            // Assert

            Assignment[] assignArray = result.ToArray();
            Assert.Equal("h1", assignArray[0].Headline);
            Assert.Equal("h2", assignArray[1].Headline);
        }
    }
}