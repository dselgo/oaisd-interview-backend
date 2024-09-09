using Microsoft.AspNetCore.Mvc;

using OaisdInterviewBackend.Controllers;
using OaisdInterviewBackend.Models;

namespace OaisdInterviewBackendTests
{
    public class NumToTextControllerTest
    {
        private readonly NumToTextController _controller;

        public NumToTextControllerTest()
        {
            _controller = new NumToTextController();
        }

        [Fact]
        public async void ConvertToText_NormalInput_ReturnsOkResult()
        {
            var result = await _controller.ConvertToText("-1,0,10");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okValue = Assert.IsType<List<ViewModel>>(okResult.Value);
            Assert.Equal(3, okValue.Count);
        }

        [Fact]
        public async void ConvertToText_InputContainsSpaces_ReturnsOkResult()
        {
            var result = await _controller.ConvertToText("1, 2");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_InputContainsLongMinValuePlusOne_ReturnsOkResult()
        {
            var result = await _controller.ConvertToText("-9223372036854775807");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_InputContainsLongMaxValue_ReturnsOkResult()
        {
            var result = await _controller.ConvertToText("9223372036854775807");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_EmptyInput_ReturnsBadRequestObject()
        {
            var result = await _controller.ConvertToText("");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_InputContainsInvalidCharacter_ReturnsBadRequestObject()
        {
            var result1 = await _controller.ConvertToText("1,two");
            var result2 = await _controller.ConvertToText("1,!");

            Assert.IsType<BadRequestObjectResult>(result1);
            Assert.IsType<BadRequestObjectResult>(result2);
        }

        [Fact]
        public async void ConvertToText_InputContainsNegativeInMiddleOfNumber_ReturnsBadRequestObject()
        {
            var result = await _controller.ConvertToText("100-1");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_InputContainsLongMinValue_ReturnsBadRequestObject()
        {
            var result = await _controller.ConvertToText("-9223372036854775808");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_InputContainsTooLargeNegativeNumber_ReturnsBadRequestObject()
        {
            var result = await _controller.ConvertToText("-9223372036854775809");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ConvertToText_InputContainsTooLargeNumber_ReturnsBadRequestObject()
        {
            var result = await _controller.ConvertToText("9223372036854775808");

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}