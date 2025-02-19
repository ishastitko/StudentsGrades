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
            _mockGradeService.Setup(s => s.GetGradeByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Grade?)null);

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

        [Fact]
        public async Task CreateGrade_InvalidFirstName_ReturnsBadRequest()
        {
            // Arrange
            _mockGradeService.Setup(s => s.DataValidation(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = await _controller.CreateGrade(1, "123", "Doe", "Math");

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task CreateGrade_InvalidLastName_ReturnsBadRequest()
        {
            // Arrange
            _mockGradeService.Setup(s => s.DataValidation(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = await _controller.CreateGrade(1, "John", "123", "Math");

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task CreateGrade_InvalidGrade_ReturnsBadRequest()
        {
            // Arrange
            _mockGradeService.Setup(s => s.DataValidation(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = await _controller.CreateGrade(123, "John", "Doe", "Math");

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UpdateGrade_ValidData_ReturnsOk()
        {
            // Arrange
            var gradeId = Guid.NewGuid();
            _mockGradeService.Setup(s => s.UpdateGradeAsync(gradeId, It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateGrade(gradeId, 2);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateGrade_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var gradeId = Guid.NewGuid();
            _mockGradeService.Setup(s => s.UpdateGradeAsync(gradeId, It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateGrade(gradeId, 6);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallService()
        {
            // Arrange
            var gradeId = Guid.NewGuid();
            _mockGradeService.Setup(s => s.DeleteGradeAsync(gradeId)).Returns(Task.CompletedTask);

            // Act
            await _controller.DeleteAsync(gradeId);

            // Assert
            _mockGradeService.Verify(s => s.DeleteGradeAsync(gradeId), Times.Once);
        }
    }
}
