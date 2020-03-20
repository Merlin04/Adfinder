# Adfinder

Tool to identify Wikipedia articles that might have a promotional bias.
![.NET Core](https://github.com/Merlin04/Adfinder/workflows/.NET%20Core/badge.svg)

## Code overview

There are 5 projects in the solution:

 - Preprocessor: Get all of the articles for the dataset and store them as JSON files.
 - MLNETFormatter: Merge the JSON files from Preprocessor into one dataset file (TSV format) to use with ML.NET
 - WebService: Run an API where you send the article content to it and get back whether or not it is promotional, along with a confidence score.
 - ModelAccess: Contains all of the logic for getting predictions from the model.
 - MLModel: Contains the ModelInput and ModelOutput classes, as well as the model zip file.

## Usage

The GitHub repository includes a pre-trained model, but if you would like to re-train it, follow these steps:

1. Make sure you have .NET Core 3.1 and 2.1 installed, as well as the ML.NET CLI.
2. `cd` into the `Preprocessor` directory, and run `dotnet run`. This will download all of the articles for the dataset, and it might take a few hours. This will consume about 800 megabytes of storage.
3. Note the path to the dataset directory that was created. `cd` into the `MLNETFormatter` directory, and run `dotnet publish`. Once that's done, `cd` into the `bin/Debug/netcoreapp3.1` directory, and run `./MLNETFormatter [path to dataset directory]`. This will merge all of the JSON files created by Preprocessor into one TSV file, and will consume about 700 megabytes of storage. 
4. Run `mlnet auto-train --task binary-classification --dataset "your-dataset-tsv-file.tsv" --label-column-name "Category" --max-exploration-time 3600`, replacing 3600 with however much time you have (in seconds) for it to explore different algorithms. This will give you a new directory called "SampleBinaryClassification."
5. If `mlnet` chose the `LightGBMBinary` algorithm, you can just replace the `MLModel.zip` file in the `MLModel` project with the new generated model file. However, if it did not choose this algorithm, you will need to update the `ModelBuilder.cs` file in the `ModelAccess` project with the new methods from the new `ModelBuilder.cs` file.
6. `cd` to the `Adfinder` directory and run `dotnet publish --configuration Release`. To start the API, `cd` to `WebService/bin/Release/netcoreapp3.1` and run `./WebService`.
