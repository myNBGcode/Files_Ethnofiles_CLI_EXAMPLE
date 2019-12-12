using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using static FIleAPI_CLI.ConfigurationSingleton;

namespace FIleAPI_CLI
{
    class Options
    {
        [Option( Default = false, HelpText = "Complete missing parameters from 'inputData.json' configuration file")]
        public bool CompleteFromConfig { get; set; }

        //General Parameters:
        [Option]
        public string User { get; set; }
        [Option]
        public string Registry { get; set; }
        [Option(HelpText ="Same as User if left blank")]
        public string SubjectUser { get; set; }
        [Option(HelpText = "Same as Registry if left blank")]
        public string SubjectRegistry { get; set; }

        //Upload Parameters Required:
        [Option('u',"upload", SetName = "upload", Required = true, HelpText ="Uploads File")]
        public bool Upload { get; set; }
        [Option(SetName = "upload", HelpText = "Required. Filename with full path")]
        public string InputFile { get; set; }

        //Upload Parameters Optional:
        [Option(SetName = "upload", HelpText = "(Only for Upload operation)")]
        public string FileDescription { get; set; }
        [Option(SetName = "upload", HelpText = "(Only for Upload operation)")]
        public string FolderId { get; set; }
        [Option(SetName = "upload", HelpText = "Folder Guid. (Only for Upload operation)")]
        public string Metadata { get; set; }
        [Option(Separator = ',', SetName = "upload", HelpText = "Tags separated by coma ',' (Only for Upload operation)")]
        public IEnumerable<string> UserTags { get; set; }


        //Download Parameters:
        [Option('d', "download", SetName = "download", Required = true, HelpText ="Downloads File")]
        public bool Download { get; set; }
        [Option(SetName = "download", HelpText = "Required. File Guid. (Only for Download operation)")]
        public string FileId { get; set; }
        [Option(SetName = "download", HelpText = "Required. Download location path. (Only for Download operation)")]
        public string DownloadFolder { get; set; }

        //Add User Tag Parameters:
        [Option("addusertags", SetName = "addusertags", Required = true, HelpText = "Add User Tags To A File")]
        public bool AddUserTags { get; set; }
        [Option(SetName = "addusertags", HelpText = "Required. File Guid. (Only for AddUserTags operation)")]
        public string AddUserTagsFileId { get; set; }
        [Option(Separator = ',', SetName = "addusertags", HelpText = "Required. Tags separated by coma ',' (Only for AddUserTags operation)")]
        public IEnumerable<string> UserTagsToAdd { get; set; }

        //Remove User Tag Parameters:
        [Option("removeusertags", SetName = "removeusertags", Required = true, HelpText = "Remove User Tags From A File")]
        public bool RemoveUserTags { get; set; }
        [Option(SetName = "removeusertags", HelpText = "Required. File Guid. (Only for RemoveUserTags operation)")]
        public string RemoveUserTagsFileId { get; set; }
        [Option(Separator = ',', SetName = "removeusertags", HelpText = "Required. Tags separated by coma ',' (Only for RemoveUserTags operation)")]
        public IEnumerable<string> UserTagsToRemove { get; set; }

        //Send to EthnoFiles Parameters:
        [Option('s',"sendtoethnofiles",SetName ="ethnofiles", Required =true, HelpText ="Sends file to Ethnofiles")]
        public bool SendToEthnofiles { get; set; }
        [Option(SetName = "ethnofiles", Required = true, HelpText = "Required. File Guid from fileAPI. (Only for Send file to ethnofiles operation)")]
        public string FileApiFileId { get; set; }
        [Option(SetName ="ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string UserID { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string IdField { get;  set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public int? RowCount { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public float? TotalSum { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string DebtorIBAN { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string TanNumber { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string Locale { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string InboxId { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string XactionId { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public bool? IsSmsOtp { get; set; }
        //[Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        //public string ConvId { get; set; }
        //[Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        //public bool? XmlFileField { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public string DebtorName { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public bool? AcceptTerms { get; set; }
        [Option(SetName = "ethnofiles", HelpText = "(Only for Send file to ethnofiles operation)")]
        public bool? AcceptTrnTerms { get; set; }

        public void FillFromConfig()
        {            
            User = ParseParameter("User", User);
            Registry = ParseParameter("Registry", Registry);
            SubjectUser = ParseParameter("SubjectUser", SubjectUser);
            SubjectRegistry = ParseParameter("SubjectRegistry", SubjectRegistry);

            InputFile = ParseParameter("InputFile", InputFile);
            FileDescription = ParseParameter("FileDescription", FileDescription);
            FolderId = ParseParameter("FolderId", FolderId);
            Metadata = ParseParameter("Metadata", Metadata);

            UserTags = ParseParameter("UserTags", UserTags);

            FileId = ParseParameter("FileId", FileId);

            //add user tag parameters
            AddUserTagsFileId = ParseParameter("AddUserTagsFileId", AddUserTagsFileId);
            UserTagsToAdd = ParseParameter("UserTagsToAdd", UserTagsToAdd);

            //remove user tag parameters
            RemoveUserTagsFileId = ParseParameter("RemoveUserTagsFileId", RemoveUserTagsFileId);
            UserTagsToRemove = ParseParameter("UserTagsToRemove", UserTagsToRemove);

            //send to ethnofiles parameters
            FileApiFileId = ParseParameter("FileApiFileId", FileApiFileId);
            UserID = ParseParameter("UserID", UserID);
            IdField = ParseParameter("IdField", IdField);
            RowCount = ParseParameter("RowCount", RowCount);
            TotalSum = ParseParameter("TotalSum", TotalSum);
            DebtorIBAN = ParseParameter("DebtorIBAN", DebtorIBAN);
            TanNumber = ParseParameter("TanNumber", TanNumber);
            Locale = ParseParameter("Locale", Locale);
            InboxId = ParseParameter("InboxId", InboxId);
            XactionId = ParseParameter("XactionId", XactionId);
            IsSmsOtp = ParseParameter("IsSmsOtp", IsSmsOtp);
            //ConvId = ParseParameter("ConvId", ConvId);
            //XmlFileField = ParseParameter("XmlFileField", XmlFileField);
            DebtorName = ParseParameter("DebtorName", DebtorName);
            AcceptTerms = ParseParameter("AcceptTerms", AcceptTerms);
            AcceptTrnTerms = ParseParameter("AcceptTrnTerms", AcceptTrnTerms);

            //var readableProperties = this.GetType().GetProperties().Where(p => p.GetGetMethod() != null);

            //foreach (var property in readableProperties)
            //{
            //    property.SetValue(this, ParseParameter(property.Name, property.GetValue(this, null).ToString())); 


            //    //var value = (int)property.GetValue(filter);
            //    //Console.WriteLine($"{property.Name} =  {value + 3}");
            //}
        }

        private string ParseParameter(string paramName, string paramValue)
        {
            return string.IsNullOrEmpty(paramValue) ? Settings.inputParameters[paramName] : paramValue;
        }

        private int? ParseParameter(string paramName, int? paramValue)
        {
            if (paramValue == null)
            {
                if (int.TryParse(Settings.inputParameters[paramName], out int result))
                    return result;
            }

            return paramValue;
        }

        private float? ParseParameter(string paramName, float? paramValue)
        {
            if (paramValue == null)
            {
                if (float.TryParse(Settings.inputParameters[paramName], out float result))
                    return result;
            }

            return paramValue;
        }
        private bool? ParseParameter(string paramName, bool? paramValue)
        {
            if (paramValue == null)
            {
                if (bool.TryParse(Settings.inputParameters[paramName], out bool result))
                    return result;
            }

            return paramValue;
        }

        private IEnumerable<string> ParseParameter(string paramName, IEnumerable<string> paramValue)
        {
            return !paramValue.Any() ? Settings.inputParameters.GetSection(paramName).GetChildren().Select(x => x.Value).ToList() : paramValue;
        }

        //[Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        //public IEnumerable<string> InputFiles { get; set; }

        //// Omitting long name, defaults to name of property, ie "--verbose"
        //[Option(Default = false, HelpText = "Prints all messages to standard output.")]
        //public bool Verbose { get; set; }

        //[Option("stdin", Default = false, HelpText = "Read from stdin")]
        //public bool stdin { get; set; }

        //[Value(0, MetaName = "offset", HelpText = "File offset.")]
        //public long? Offset { get; set; }
    }
}
