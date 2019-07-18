using System;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using AutoFixture.Xunit2;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Cmx.HourTrackerToExcel.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Cmx.HourTrackerToExcel.Api.Tests
{
    public class FileControllerTests
    {
        [Theory, AutoMoqData]
        public async Task Post_ShouldReturnCorrectResult(IFixture fixture, FileController sut)
        {
            // arrange
            var files = fixture.Build<IFormFileCollection>().Create();

            // act
            var actual = await sut.Post(files);

            // assert
            actual.ShouldBeOfType<FileStreamResult>();
        }
    }
}
