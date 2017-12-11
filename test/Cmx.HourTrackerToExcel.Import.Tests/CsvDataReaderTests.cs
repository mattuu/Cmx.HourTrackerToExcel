using System;
using System.IO;
using System.Linq;
using Cmx.HourTrackerToExcel.TestUtils;
using Ploeh.AutoFixture.Idioms;
using Shouldly;
using Xunit;

namespace Cmx.HourTrackerToExcel.Import.Tests
{
    public class CsvDataReaderTests
    {
        [Theory, AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            //assert
            assertion.Verify(typeof(CsvDataReader).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void Read_ShouldProcessHeaderCorrectly(CsvDataReader sut)
        {
            // arrange..
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(HeaderString);
            streamWriter.Flush();
            stream.Position = 0;

            // act..
            var actual = sut.Read(stream);

            // assert..
            actual.ShouldNotBeNull();
            actual.ShouldBeEmpty();

            // teardown..
            streamWriter.Dispose();
            stream.Dispose();
        }

        [Theory, AutoMoqData]
        public void Read_ShouldProcessDataCorrectly(CsvDataReader sut)
        {
            // arrange..
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(HeaderString);
            streamWriter.Write(DataString);
            streamWriter.Flush();
            stream.Position = 0;

            // act..
            var actual = sut.Read(stream);

            // assert..
            actual.ShouldNotBeNull();
            actual.ShouldHaveSingleItem();
            actual.First().Job.ShouldBe("Jet2.com");
            actual.First().ClockedIn.ShouldBe(new DateTime(2017, 11, 01, 8, 33, 0));
            actual.First().ClockedOut.ShouldBe(new DateTime(2017, 11, 01, 17, 31, 0));
            actual.First().Duration.ShouldBe(new TimeSpan(8, 20, 0));
            actual.First().HourlyRate.ShouldBe(1234.5M);
            actual.First().Earnings.ShouldBe(5432.1M);
            actual.First().Comment.ShouldBe("Sample comment");
            actual.First().Tags.ShouldBe("Sample tags");
            actual.First().Breaks.ShouldBe("0:41 (13:10 to 13:51)");
            actual.First().TotalTimeAdjustment.ShouldBe(new TimeSpan(0, -41, 0));

            // teardown..
            streamWriter.Dispose();
            stream.Dispose();
        }

        private const string HeaderString = @"Job,Clocked In,Clocked Out,Duration,Hourly Rate,Earnings,Comment,Tags,Breaks,Adjustments,TotalTimeAdjustment,TotalEarningsAdjustment
";    
        private const string DataString = @"Jet2.com,01/11/2017 08:33,01/11/2017 17:31,08:20,1234.5,5432.1,Sample comment,Sample tags,0:41 (13:10 to 13:51),,-0:41,0
";    
    }
}
