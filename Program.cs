using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FIleAPI_CLI
{
    class Program
    {

        static void Main(string[] args)
        {            
            Parser.Default.ParseArguments<Options>(args)
             .WithParsed(opts => OperationResolver(opts));
             //.WithNotParsed<Options>((errs) => HandleParseError(errs));

        }

        private static void OperationResolver(Options opts)
        {
            if (opts.CompleteFromConfig) opts.FillFromConfig();
            
            IFileApiRestRequest fileApiRestRequest = new FileApiRestRequest();

            if (opts.Upload)
            {
                string inputFile = opts.InputFile;

                FileInfo fi = new FileInfo(inputFile);
                var uploadInitiationRequest = new UploadInitiationRequest()
                {
                    FileName = fi.Name,
                    FileSize = (long)Math.Ceiling((decimal)fi.Length / 1024 / 1024),
                    Requester = new RequestEntity { Id = opts.User, Registry = opts.Registry },
                    Subject = new RequestEntity
                    {
                        Id = string.IsNullOrEmpty(opts.SubjectUser) ? opts.User : opts.SubjectUser,
                        Registry = string.IsNullOrEmpty(opts.SubjectRegistry) ? opts.Registry : opts.SubjectRegistry
                    },
                    FileDescription = opts.FileDescription,
                    FolderId = Guid.TryParse(opts.FolderId, out Guid folderId) ? (Guid?)folderId : null,
                    Metadata = opts.Metadata,
                    UserTags = opts.UserTags.ToList()
                };

                CliService.UploadFile(fileApiRestRequest, uploadInitiationRequest, inputFile);
            }
            else if (opts.Download)
            {
                var fileId = opts.FileId;
                string downloadFolder = opts.DownloadFolder;
                opts.SubjectUser = string.IsNullOrEmpty(opts.SubjectUser) ? opts.User : opts.SubjectUser;
                opts.SubjectRegistry = string.IsNullOrEmpty(opts.SubjectRegistry) ? opts.Registry : opts.SubjectRegistry;

                CliService.DownloadFile(fileApiRestRequest, Guid.Parse(fileId), downloadFolder, $"{opts.Registry}:{opts.User}", $"{opts.SubjectRegistry}:{opts.SubjectUser}");
            }
            else if (opts.AddUserTags)
            {
                var addUserTagsFileId = opts.AddUserTagsFileId;
                var userTags = opts.UserTagsToAdd.ToList();
                var updateFileRequest = new UpdateFileRequest()
                {
                    Requester = new RequestEntity { Id = opts.User, Registry = opts.Registry },
                    Subject = new RequestEntity
                    {
                        Id = string.IsNullOrEmpty(opts.SubjectUser) ? opts.User : opts.SubjectUser,
                        Registry = string.IsNullOrEmpty(opts.SubjectRegistry) ? opts.Registry : opts.SubjectRegistry
                    },
                };
                updateFileRequest.UserTags = userTags;
                CliService.AddUserTags(fileApiRestRequest, Guid.Parse(addUserTagsFileId), updateFileRequest);
            }
            else if (opts.RemoveUserTags)
            {
                var removeUserTagsFileId = opts.RemoveUserTagsFileId;
                var userTags = opts.UserTagsToRemove.ToList();
                var updateFileRequest = new UpdateFileRequest()
                {
                    Requester = new RequestEntity { Id = opts.User, Registry = opts.Registry },
                    Subject = new RequestEntity
                    {
                        Id = string.IsNullOrEmpty(opts.SubjectUser) ? opts.User : opts.SubjectUser,
                        Registry = string.IsNullOrEmpty(opts.SubjectRegistry) ? opts.Registry : opts.SubjectRegistry
                    },
                };
                CliService.RemoveUserTags(fileApiRestRequest, Guid.Parse(removeUserTagsFileId), updateFileRequest, userTags);
            }
            else if (opts.SendToEthnofiles)
            {
                // call populate file types and get the selected file type
                var populateFileTyesRequest = new PopulateFileTypesRequest()
                {
                    Payload = new PopulateFileTypesRequestPayload { UserID = opts.UserID }
                };
                var selectedFileType = CliService.PopulateFileTypes(fileApiRestRequest, populateFileTyesRequest, opts.IdField);
                if (selectedFileType == null) return;

                // create request and call send file to ethnofiles
                var sendFileToEthnofilesRequest = new SendFileToEthnofilesRequest()
                {
                    Payload = new SendFileToEthnofilesRequestPayload
                    {
                        FileApiFileId = Guid.Parse(opts.FileApiFileId),
                        UserID = opts.UserID,
                        FileTypeId = opts.IdField,
                        TanNumber = opts.TanNumber,
                        Locale = opts.Locale,
                        InboxId = opts.InboxId,
                        XactionId = opts.XactionId,
                        IsSmsOtp = opts.IsSmsOtp,
                        ConvId = selectedFileType.ConvId,
                        XmlFileField = selectedFileType.XmlFileField,
                        DebtorName = opts.DebtorName,
                        AcceptTerms = opts.AcceptTerms,
                        AcceptTrnTerms = opts.AcceptTrnTerms
                    }
                };

                if (selectedFileType.SendRowNum())
                    sendFileToEthnofilesRequest.Payload.RowCount = opts.RowCount;

                if (selectedFileType.SendTotalSum())
                    sendFileToEthnofilesRequest.Payload.TotalSum = opts.TotalSum;

                if (selectedFileType.HasAccountInput())
                    sendFileToEthnofilesRequest.Payload.DebtorIBAN = opts.DebtorIBAN;

                if (!sendFileToEthnofilesRequest.Payload.IsSmsOtp == null || sendFileToEthnofilesRequest.Payload.IsSmsOtp == false)
                    sendFileToEthnofilesRequest.Payload.TanNumber = "bypass";

                CliService.SendToEthnofiles(fileApiRestRequest, sendFileToEthnofilesRequest);
            }
            //else
            //{
            //    Console.Write("Please enter a valid operation method. ([U]pload / [D]ownload): ");
            //    OperationResolver(new List<string> { Console.ReadLine() }.ToArray());
            //};

        }


    }
}