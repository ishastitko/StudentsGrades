using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using StudentsGrades.Controllers;
using StudentsGrades.Models;
using StudentsGrades.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudentsGrades.Tests
{
    public class GradeControllerTests
    {
        private readonly Mock<IGradeService> _mockGradeService;
        private readonly GradeController _controller;

        public GradeControllerTests()
        {
            _mockGradeService = new Mock<IGradeService>();
            _controller = new GradeController(_mockGradeService.Object);
        }

        [Fact]
        public async Task GetGrades_ReturnListOfGrades()
        {
            // Arrange
            var student = new Student("John", "Doe");
            var subject = new Subject("Math");
            var grades = new List<Grade>
            {
                new Grade(1, student.StudentId, subject.SubjectId)
            };
            _mockGradeService.Setup(s => s.GetAllGradesAsync()).ReturnsAsync(grades);

            // Act
            var result = await _controller.GetGrades();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetGradeById_GradeExists_ReturnsOk()
        {
            //Arrange
            var student = new Student("John", "Doe");
            var subject = new Subject("Math");
            var grade = new Grade(2, student.StudentId, subject.SubjectId);
            _mockGradeService.Setup(s => s.GetGradeByIdAsync(grade.GradeId)).ReturnsAsync(grade);

            // Act
            var result = await _controller.GetGradeById(grade.GradeId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedGrade = Assert.IsType<Grade>(actionResult.Value);
            Assert.Equal(grade.GradeId, returnedGrade.GradeId);
        }

        [Fact]
        public async Task GetGradeById_GradeNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockGradeService.Setup(s => s.GetGradeByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Grade)null);

            // Act
            var result = await _controller.GetGradeById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateGrade_ValidData_ReturnsCreated()
        {
            // Arrange
            var student = new Student("John", "Doe");
            var subject = new Subject("Math");
            var grade = new Grade(2, student.StudentId, subject.SubjectId);
            _mockGradeService
                .Setup(s => s.DataValidation(grade.GradeGot, student.FirstName, student.LastName, subject.SubjectName))
                .Returns(true);
            _mockGradeService
                .Setup(s => s.CreateGradeAsync(grade.GradeGot, student.FirstName, student.LastName, subject.SubjectName))
                .ReturnsAsync(grade);

            // Act
            var result = await _controller.CreateGrade(2, "John", "Doe", "Math");

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdGrade = Assert.IsType<Grade>(actionResult.Value);
            Assert.Equal(grade.GradeId, createdGrade.GradeId);
        }
    }
}
