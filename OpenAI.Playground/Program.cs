﻿using LaserCatEyes.Domain.Models;
using LaserCatEyes.HttpClientListener;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Playground;
using OpenAI.SDK;
using OpenAI.SDK.Interfaces;
using OpenAI.SDK.Models.RequestModels;

var builder = new ConfigurationBuilder()
    .AddJsonFile("ApiSettings.json")
    .AddUserSecrets<Program>();

IConfiguration configuration = builder.Build();
var serviceCollection = new ServiceCollection();
serviceCollection.AddOptions<OpenAiSettings>();
serviceCollection.AddScoped(_ => configuration);


// Laser cat eyes will help us to track request and responses between OpenAI server and our client
//It is in Beta version, if you have consider about your data privacy or if you don't want to use it just comment out from here
serviceCollection.Configure<LaserCatEyesOptions>(configuration.GetSection("LaserCatEyesOptions"));
serviceCollection.AddHttpClient<IOpenAISdk, OpenAISdk>();
serviceCollection.AddLaserCatEyesHttpClientListener();

//// to here, and uncomment from here
//serviceCollection.AddHttpClient<IOpenAISdk, OpenAISdk>();
//// to here

serviceCollection.Configure<OpenAiSettings>(configuration.GetSection(OpenAiSettings.SettingKey));
var serviceProvider = serviceCollection.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<IOpenAISdk>();
//await EngineTestHelper.FetchEnginesTest(sdk);
//await CompletionTestHelper.RunSimpleCompletionTest(sdk);

//var fileName = "AnswerQuestionsSample.json";
//var searchSampleFile = await File.ReadAllBytesAsync($"SampleData/{fileName}");
//await sdk.Files.UploadFiles(UploadFilePurpose.Answers, searchSampleFile, fileName);
//var uploadedFiles = await sdk.Files.ListFiles();
//foreach (var uploadedFile in uploadedFiles.Data)
//{
//    Console.WriteLine(uploadedFile.FileName);
//    var file = await sdk.Files.RetrieveFile(uploadedFile.Id);
//    Console.WriteLine(file.FileName);
//    //   var fileContent = sdk.Files.RetrieveFileContent(file.Id);
//    var deleteResponse = await sdk.Files.DeleteFile(file.Id);
//}


//await FileTestHelper.CleanAllFiles(sdk);
//await SearchTestHelper.UploadSampleFileAndGetSearchResponse(sdk);