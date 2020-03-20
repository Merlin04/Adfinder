# Adfinder

Tool to identify Wikipedia articles that might have a promotional bias.

## Code overview

So far, there is one project in the solution:

 - Preprocessor: Get all of the articles for the dataset and store them as JSON files.
 
 Future projects:
 
  - MLNETFormatter: Create one dataset file to use with ML.NET
  - Trainer: Train the model generated by ML.NET CLI.
  - WebService: Run an API where you send the article content to it and get back whether or not it is promotional, along with a confidence score.