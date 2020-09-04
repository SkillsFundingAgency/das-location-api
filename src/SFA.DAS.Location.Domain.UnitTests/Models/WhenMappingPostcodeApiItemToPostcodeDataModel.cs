using AutoFixture.NUnit3;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingPostcodeApiItemToPostcodeDataModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(PostcodesLocationApiItem source)
        {
            var actual = (PostcodeData)source;
            actual.Equals(source);
        }
    }
}
