using System.Threading.Tasks;
using AutoFixture;
using Cmx.HourTrackerToExcel.Api.Controllers;
using Cmx.HourTrackerToExcel.TestUtils.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Api.Tests.Controllers
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
