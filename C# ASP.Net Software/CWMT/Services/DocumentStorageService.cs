using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace CWMasterTeacher3
{
    public class DocumentStorageService
    {
        private const string _containerName = "msudenver";

        //public void DownloadDocument(string storageFileName, string serverPath, Stream outputStream)
        //{
        //    if (IsRunningInAzure())
        //    {
        //        var container = GetBlobContainer(_containerName);
        //        container.GetBlobReference(storageFileName).DownloadToStream(outputStream);
        //    }
        //    else
        //    {
        //        var path = Path.Combine(serverPath, storageFileName);
        //        Stream file = File.OpenRead(path);
        //        file.CopyTo(outputStream);
        //    }
        //}

        public Stream GetDownloadStream(string storageFileName, string serverPath)
        {
            if (IsRunningInAzure())
            {
                var container = GetBlobContainer(_containerName);
                var stream = new MemoryStream();
                container.GetBlobReference(storageFileName).DownloadToStream(stream);
                stream.Position = 0;
                return stream;
            }
            else
            {
                var path = Path.Combine(serverPath, storageFileName);
                return  File.OpenRead(path);
            }
        }

        /// <summary>
        /// Probably dead because Spire doesn't work with the equation editor.  I'm keeping it for reference in case we get a real pdf converter some day.
        /// </summary>
        /// <returns></returns>

        //public Stream DocOrDocxToPDF(Stream docStream, string extension)
        //{
        //    if(extension == "doc" || extension == "docx" || extension == ".doc" || extension == ".docx")
        //    {
        //        Spire.Doc.FileFormat format = new Spire.Doc.FileFormat();
        //        format = extension == "docx" || extension == ".docx" ? Spire.Doc.FileFormat.Docx : Spire.Doc.FileFormat.Doc;
        //        Spire.Doc.Document spireDoc = new Spire.Doc.Document();
        //        spireDoc.LoadFromStream(docStream, format);
        //        Stream pdfStream = new MemoryStream();
        //        Spire.Doc.ToPdfParameterList parameterList = new Spire.Doc.ToPdfParameterList();
        //        parameterList.PdfConformanceLevel = Spire.Pdf.PdfConformanceLevel.Pdf_X1A2001;
        //        parameterList.IsEmbeddedAllFonts = true;
        //        spireDoc.SaveToStream(pdfStream, parameterList);
        //        pdfStream.Position = 0;
        //        return pdfStream;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public bool IsRunningInAzure()
        {
            return WebAppUtilities.IsRunningInAzure;
            //return true;
        }

        private CloudBlobContainer GetBlobContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            return container;
        }

        public void UploadFile(string fileName, string serverPath, HttpPostedFileBase file)
        {
            if (IsRunningInAzure())
            {
                var container = GetBlobContainer(_containerName);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                //This is new to incorporate timeout
                //BlobRequestOptions blobOptions = new BlobRequestOptions();
                //blobOptions.ServerTimeout = new TimeSpan(0, 0, 30);
                //blockBlob.UploadFromStream(file.InputStream, null, blobOptions, null);
                //end new

                blockBlob.UploadFromStream(file.InputStream);
            }
            else
            {
                var path = Path.Combine(serverPath, fileName);
                file.SaveAs(path);
            }
        }




    }//End Class
}