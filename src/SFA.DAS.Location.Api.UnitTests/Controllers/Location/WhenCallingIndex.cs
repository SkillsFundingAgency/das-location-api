using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.Location.Api.Controllers;

namespace SFA.DAS.Location.Api.UnitTests.Controllers.Location
{
    public class WhenCallingIndex
    {
        [Test]
        public void Then_An_Ok_Result_Is_Returned()
        {
            var controller = new LocationController();

            var actual = controller.Index() as ObjectResult;;
            
            actual.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}