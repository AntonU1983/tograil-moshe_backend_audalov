using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageResizer;
using Microsoft.Azure.WebJobs;

namespace GenericBackend.ImagePreviewWebJob
{
    public class Functions
    {
        private const string InputBlobPath =  "goodnightmedical/{name}.{ext}";
        private const string OutputBlobPath = "goodnightmedical-preview/{name}-60x60.{ext}";

        public static void CreatePreviewImage([BlobTrigger(InputBlobPath)] Stream input,
        [Blob(OutputBlobPath, FileAccess.Write)] Stream output)
        {
            ResizeImage(input, output, 60);
        }
        private static void ResizeImage(Stream input, Stream output, int width)
        {
            var instructions = new Instructions
            { 
                Width = width,
                Mode = FitMode.Carve,
                Scale = ScaleMode.Both,
            };
            ImageBuilder.Current.Build(new ImageJob(input, output, instructions));
        }
    }
}
