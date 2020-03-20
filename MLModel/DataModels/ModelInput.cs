//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace MLModel.DataModels
{
    public class ModelInput
    {
        [ColumnName("Category"), LoadColumn(0)]
        public bool Category { get; set; }


        [ColumnName("Extracts"), LoadColumn(1)]
        public string Extracts { get; set; }


    }
}
