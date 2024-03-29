﻿using OzonEdu.MerchandiseService.Domain.AggregationModels.CreateTicketAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions.CreateTicketAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.CreateTicketAggregate
{
    public class TicketEntityTests
    {
        [Fact]
        public void CreateInstance_ValidParameters_Success()
        {
            //Arrange
            var expectedEmployee = new Employee(
                new EmployeeId(10),
                new Email("test@gmail.com"));
            var expectedTicketStatus = TicketStatus.Backlog;
            Sku expectedSku = null;

            //Act
            var actualTicket = new Ticket(expectedEmployee);

            //Assert
            Assert.Equal(expectedEmployee.Id, actualTicket.Employee.Id);
            Assert.Equal(expectedEmployee.Email, actualTicket.Employee.Email);
            Assert.Equal(expectedTicketStatus, actualTicket.TicketStatus);
            Assert.Equal(expectedSku, actualTicket.Sku);
        }
        
        [Fact]
        public void StartWork_TicketStatusBacklog_TicketStatusInWork()
        {
            //Arrange
            var expectedEmployee = new Employee(
                new EmployeeId(10),
                new Email("test@gmail.com"));
            var expectedTicketStatus = TicketStatus.InWork;
            var expectedSku = new Sku(10);

            //Act
            var actualTicket = new Ticket(expectedEmployee);
            actualTicket.StartWork(expectedSku);

            //Assert
            Assert.Equal(expectedEmployee, actualTicket.Employee);
            Assert.Equal(expectedTicketStatus, actualTicket.TicketStatus);
            Assert.Equal(expectedSku, actualTicket.Sku);
        }
        
        [Fact]
        public void CompleteWork_TicketStatusInWork_TicketStatusDone()
        {
            //Arrange
            var expectedEmployee = new Employee(
                new EmployeeId(10),
                new Email("test@gmail.com"));
            var expectedTicketStatus = TicketStatus.Done;
            var expectedSku = new Sku(10);

            //Act
            var actualTicket = new Ticket(expectedEmployee);
            actualTicket.StartWork(expectedSku);
            actualTicket.CompleteWork();

            //Assert
            Assert.Equal(expectedEmployee, actualTicket.Employee);
            Assert.Equal(expectedTicketStatus, actualTicket.TicketStatus);
            Assert.Equal(expectedSku, actualTicket.Sku);
        }
        
        [Fact]
        public void StartWork_TicketStatusDone_ExceptionThrown()
        {
            //Arrange
            var expectedEmployee = new Employee(
                new EmployeeId(10),
                new Email("test@gmail.com"));
            var sku = new Sku(10);

            //Act
            var actualTicket = new Ticket(expectedEmployee);
            actualTicket.StartWork(sku);
            actualTicket.CompleteWork();
            
            //Assert
            Assert.Throws<InvalidTicketStatusException>(() => actualTicket.StartWork(sku));
        }

        [Fact]
        public void CompleteWork_TicketStatusBacklog_ExceptionThrown()
        {
            //Arrange
            var employee = new Employee(
                new EmployeeId(10),
                new Email("test@gmail.com"));

            //Act
            var actualTicket = new Ticket(employee);
            
            //Assert
            Assert.Throws<InvalidTicketStatusException>(() => actualTicket.CompleteWork());
        }
    }
}