//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*               It has been modified manually, so it isn't entirely computer generated. *
//*****************************************************************************************

using System;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.ML;
using MLModel.DataModels;

namespace ModelAccess
{
    public class Access
    {
        //Machine Learning model to load and use for predictions
        private const string ModelFilepath = @"../../../../MLModel/MLModel.zip";

        private readonly PredictionEngine<ModelInput, ModelOutput> _predEngine; 

        public Access()
        {
            var mlContext = new MLContext();
            var mlModel = mlContext.Model.Load(GetAbsolutePath(ModelFilepath), out _);
            _predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
        
        public ModelOutput Predict(string extracts)
        {
            // Create sample data to do a single prediction with it 
            //ModelInput data = CreateSingleDataSample(_mlContext, DATA_FILEPATH);
            ModelInput data = new ModelInput
            {
                Extracts = extracts
            };
            // Try a single prediction
            ModelOutput predictionResult = _predEngine.Predict(data);

            //Console.WriteLine($"Single Prediction --> Actual value: {data.Category} | Predicted value: {predictionResult.Prediction}");
            return predictionResult;
        }

        private static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Access).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
