using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SixBeeHealthTech.Components.DBContext;
using SixBeeHealthTech.Components.Models;
using SixBeeHealthTech.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace SixBeeHealthTech.Tests
{
    public class AppointmentsControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private Mock<DbSet<Appointment>> _mockSet;
        private AppointmentsController _controller;
        private List<Appointment> _appointments;

        public AppointmentsControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockSet = new Mock<DbSet<Appointment>>();
            _controller = new AppointmentsController(_mockContext.Object);

            _appointments = new List<Appointment>
        {
            new Appointment { Id = 1, Name = "First Appointment", ContactNumber = "1234567890", AppointmentDate = DateTime.Today.AddDays(1), AppointmentTime = new TimeOnly(1, 0), EmailAddress="one@one.com" },
            new Appointment { Id = 2, Name = "Second Appointment", ContactNumber = "098764321", AppointmentDate = DateTime.Today.AddDays(2), AppointmentTime = new TimeOnly(2, 0), EmailAddress="two@two.com" },
            new Appointment { Id = 3, Name = "Third Appointment", ContactNumber = "1111111111", AppointmentDate = DateTime.Today.AddDays(3), AppointmentTime = new TimeOnly(3, 0), EmailAddress="three@three.com" }
        };

            var queryableAppointments = _appointments.AsQueryable();
            _mockSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(queryableAppointments.Provider);
            _mockSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(queryableAppointments.Expression);
            _mockSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(queryableAppointments.ElementType);
            _mockSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(queryableAppointments.GetEnumerator());

            _mockContext.Setup(c => c.Appointments).Returns(_mockSet.Object);
        }

        [Fact]
        public async Task GetAppointments_ReturnsAllAppointments()
        {
            // Act
            var result = await _controller.GetAppointments();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Appointment>>>(result);
            var returnValue = Assert.IsType<List<Appointment>>(actionResult.Value);
            Assert.Equal(3, returnValue.Count);
        }

        [Fact]
        public async Task GetAppointment_ReturnsAppointment_WhenAppointmentExists()
        {
            // Arrange
            int id = 1;
            _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync(_appointments.First(a => a.Id == id));

            // Act
            var result = await _controller.GetAppointment(id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Appointment>>(result);
            var returnValue = Assert.IsType<Appointment>(actionResult.Value);
            Assert.Equal(id, returnValue.Id);
        }

        [Fact]
        public async Task GetAppointment_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            int id = 999;
            _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync((Appointment)null);

            // Act
            var result = await _controller.GetAppointment(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostAppointment_AddsAppointment()
        {
            // Arrange
            var newAppointment = new Appointment { Id = 4, Name = "Third Appointment", ContactNumber = "44444444444", AppointmentDate = DateTime.Today.AddDays(4), AppointmentTime = new TimeOnly(4, 0), EmailAddress = "four@four.com" };

            // Act
            var result = await _controller.PostAppointment(newAppointment);

            // Assert
            _mockSet.Verify(m => m.Add(newAppointment), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            var actionResult = Assert.IsType<ActionResult<Appointment>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(nameof(_controller.GetAppointment), createdAtActionResult.ActionName);
            Assert.Equal(newAppointment.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task PutAppointment_UpdatesAppointment_WhenIdsMatch()
        {
            // Arrange
            int id = 1;
            var updatedAppointment = new Appointment { Id = id, Name = "Updated Appointment One" };
            _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync(_appointments.First(a => a.Id == id));

            // Act
            var result = await _controller.PutAppointment(id, updatedAppointment);

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutAppointment_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            int id = 1;
            var updatedAppointment = new Appointment { Id = 2, Name = "Updated Appointment Fail" };

            // Act
            var result = await _controller.PutAppointment(id, updatedAppointment);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAppointment_DeletesAppointment_WhenAppointmentExists()
        {
            // Arrange
            int id = 1;
            var appointment = _appointments.First(a => a.Id == id);
            _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync(appointment);

            // Act
            var result = await _controller.DeleteAppointment(id);

            // Assert
            _mockSet.Verify(m => m.Remove(appointment), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAppointment_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            int id = 999;
            _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync((Appointment)null);

            // Act
            var result = await _controller.DeleteAppointment(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
