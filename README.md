# Adfinder

Tool to identify Wikipedia articles that might have a promotional bias.

![.NET Core](https://github.com/Merlin04/Adfinder/workflows/.NET%20Core/badge.svg)

## Code overview

There are two main parts to this repository. The first is the backend.

There are 5 projects in the solution:

 - Preprocessor: Get all of the articles for the dataset and store them as JSON files.
 - MLNETFormatter: Merge the JSON files from Preprocessor into one dataset file (TSV format) to use with ML.NET
 - WebService: Run an API where you send the article content to it and get back whether or not it is promotional, along with a confidence score.
 - ModelAccess: Contains all of the logic for getting predictions from the model.
 - MLModel: Contains the ModelInput and ModelOutput classes, as well as the model zip file.

The second part of this repository is a Firefox extension, which has three main parts:

 - A background script that sends a message to the content script when the page finishes loading (it also handles loading the settings)
 - A popup that opens when you click the icon in the browser toolbar and allows you to change the settings
 - A content script that sends a POST to the API to get the score of the article and if necessary add the popup (the notification that the page is promotional, not the above item) to the page

There is also a Chrome extension that is mostly the same as the Firefox extension, there's just a few small tweaks I made to get it working in Chrome.

## Usage

The GitHub repository includes a pre-trained model, but if you would like to re-train it, follow these steps:

1. Make sure you have .NET Core 3.1 and 2.1 installed, as well as the ML.NET CLI.
2. `cd` into the `Preprocessor` directory, and run `dotnet run`. This will download all of the articles for the dataset, and it might take a few hours. This will consume about 800 megabytes of storage.
3. Note the path to the dataset directory that was created. `cd` into the `MLNETFormatter` directory, and run `dotnet publish`. Once that's done, `cd` into the `bin/Debug/netcoreapp3.1` directory, and run `./MLNETFormatter [path to dataset directory]`. This will merge all of the JSON files created by Preprocessor into one TSV file, and will consume about 700 megabytes of storage. 
4. Run `mlnet auto-train --task binary-classification --dataset "your-dataset-tsv-file.tsv" --label-column-name "Category" --max-exploration-time 3600`, replacing 3600 with however much time you have (in seconds) for it to explore different algorithms. This will give you a new directory called "SampleBinaryClassification."
5. If `mlnet` chose the `LightGBMBinary` algorithm, you can just replace the `MLModel.zip` file in the `MLModel` project with the new generated model file. However, if it did not choose this algorithm, you will need to update the `ModelBuilder.cs` file in the `ModelAccess` project with the new methods from the new `ModelBuilder.cs` file.
6. `cd` to the `Adfinder` directory and run `dotnet publish --configuration Release`. To start the API, `cd` to `WebService/bin/Release/netcoreapp3.1` and run `./WebService`.

To help with developing the Firefox extension, I use Mozilla's `web-ext` tool. This will automatically reload the extension whenever you modify a file.

## API

By running `WebService`, you get an API at `https://localhost:5001/MLLookup` that you can post an `articleTitle` to. This will send back a score, with a lower value meaning it is more confident that the article is promotional.
