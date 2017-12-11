using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    [ExcludeFromCodeCoverage]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new Fixture())
        {
            Fixture.Customize(new CancellationTokenCustomization())
                   .Customize(new AutoMoqCustomization())
                   .Behaviors.Add(new OmitOnRecursionBehavior());

            Fixture.Register(() =>
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml($"<test><accommodation></accommodation></test>");
                return xmlDoc.DocumentElement;
            });
        }
    }
}