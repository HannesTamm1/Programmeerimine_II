using KooliProjekt.Data;
using KooliProjekt.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitofWork> _uowMock;
        private readonly Mock<IOrderService> _repositoryMock;
        private readonly OrderService _orderService;
            

        public OrderServiceTests() 
        {
            _uowMock = new Mock<IOrderService>();
            _repositoryMock = new Mock<IOrderService>();
            _orderService = new OrderService(_repositoryMock.Object); 
        }
        [Fact]
        public async Task List_Should_return_list_of_cars()
        {
            // Arrange
            var results = new PagedResult<Order>();
        }
    }
}
