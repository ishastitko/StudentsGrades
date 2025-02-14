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
            var student = new Student("John", "Smith");
            var subject = new Subject("Mathematics");
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
    }
}
