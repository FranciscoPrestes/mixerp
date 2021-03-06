// ReSharper disable All
using System;
using System.Diagnostics;
using System.Linq;
using MixERP.Net.Api.Office.Fakes;
using MixERP.Net.ApplicationState.Cache;
using Xunit;

namespace MixERP.Net.Api.Office.Tests
{
    public class GetOfficeNameByIdTests
    {
        public static GetOfficeNameByIdController Fixture()
        {
            GetOfficeNameByIdController controller = new GetOfficeNameByIdController(new GetOfficeNameByIdRepository(), "", new LoginView());
            return controller;
        }

        [Fact]
        [Conditional("Debug")]
        public void Execute()
        {
            var actual = Fixture().Execute(new GetOfficeNameByIdController.Annotation());
            Assert.Equal("FizzBuzz", actual);
        }
    }
}