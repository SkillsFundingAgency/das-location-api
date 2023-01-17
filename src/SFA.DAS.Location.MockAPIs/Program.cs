using SFA.DAS.Location.MockAPIs;

MockApiBuilder.Create(5121)
       .StartEndPoints()
       .Build();

Console.WriteLine("Press any key to stop the servers");
Console.ReadKey();
