using System;
namespace SFA.DAS.Location.Domain.Extensions;
public static class OsExtension
{
    public static string ToCountry(this string countryCode)
    {
        return countryCode switch
        {
            "E" => "England",
            "W" => "Wales",
            "S" => "Scotland",
            "N" => "Northern Ireland",
            "L" => "Channel Islands",
            "M" => "Isle of Man",
            "J" => "Unknown", // Jersey, Guernsey, Sark, Alderney
            _ => throw new ArgumentOutOfRangeException(nameof(countryCode), $"Unknown country code: {countryCode}")
        };
    }
}
