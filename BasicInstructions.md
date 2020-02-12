/*********************************************************************************
*
*	Basic instructions for configuring and using the FIleAPI_CLI tool. 
*
*********************************************************************************/

1.INSTALLATION & USAGE
---------------------------------------------------------------------------------
	1.1 The application leverages the .Net Core Runtime library to be able to conform 
		to the multiplatform requirements. In order to run the application the current 
		.Net Core Runtime for your environment must be downloaded and installed 
		(https://dotnet.microsoft.com/download).

		The application is run through CMD by navigating to the location of the exported 
		"FIleAPI_CLI.dll" file (in most cases the bin file of the project will contain said .dll) 
		and using the command 'dotnet' followed by the .dll of the app and the accompanying 
		parameters as described further below in these instructions (Examples Section).
		(the location needs to include the configuration files "appSettings.json" and 
		"inputData.json" also by default contained in the project's bin file )

	1.2 The application can be run with two methods or by a combination of the two.
		a. By providing all the desired parameters while running the app from the 
		   CMD as arguments. This is further explained below with examples. (Sections 3,4)

		b. By filling all the desired parameters in the acompanying file named
		   "inputData.json" further explained below. (Section 2)

		c. A hybrid way to run the application is also supported. If the user enters
		   the argument "--completefromconfig" while running the app from CMD, all
		   the parameters that the user does not enter through command line arguments
		   (described in 1.2.a) will be drawn from the corresponding fields in the 
		   "inputData.json" file. (Explanation and example at Section 2.2)


2.CONFIGURATION
---------------------------------------------------------------------------------
	2.1 appSettings.json: 
			Within the "appSettings.json" file, are configured 
			the information about authorization and proxy 
			application endpoints.

			"Password" grant type is used for the authorization. 
			You will need to contact us to provide you access for the "Password" grant type authorization.
			By providing the client_id and client_secret of the APP that you created and 
			subscribed to the File API, and your own credentials (username, password) 
			that you use to sign in the NBG Technology HUB, the CLI will authorize you
			and perform the required operation.

			"client_id": "client_id_guid",
			"client_secret": "client_secret_guid"
			"username": "your_portal_username",
			"password": "your_portal_password",

			For users using Developer Portal credentials:
			arc_provider=1

			For users using iBank credentials:
			arc_provider=5

			It is suggested to use arc_provider=1 for sandbox calls and arc_provider=5 
			for production calls.

			For sandbox calls your sandbox_id also needs to be provided here.
			"sandbox_id": "MyUniqueSandbox"

	2.2 inputData.json:
			Within the "inputData.json" file, the default parameter values 
			used in each command, are configured. In order for these values 
			to be used in a command, the argument "--completefromconfig" 
			must also be used. 
			This makes every argument missing from the command to be completed
			from the value set in the inputData.json file.

			For example if the user enters the following command:
			dotnet FIleAPI_CLI.dll --u --completefromconfig --inputfile "../Files/smallfile.txt" 

			The "inputfile" parameter will be drawn from the commandline and all the other 
			required parameters (user,registry) will be draw from the "inputData.json" file

			If the "--completefromconfig" argument is not used this file will
			be ignored during the operation and only the arguments you provide
			in the command will be used.

3.COMMANDS
---------------------------------------------------------------------------------
	## General Arguments ##
		The following arguments apply to all commands.
			--user				 : The user.
			--registry			 : User registry.
			--subjectuser		 : If left blank then it will take the user value.
			--subjectregistry	 : If left blank will take the registry value.
			--completefromconfig : As stated above, with this argument in a command, 
								   every other unspecified argument will take its 
								   value from the "inputData.json" configuration.

	3.1 --upload (or --u): Uploads File from a specified path
		## Upload Arguments ##
			--inputfile		 : Required. Filename with full path.
			--filedescription: Description for the file.
			--folderid		 : Folder Guid. 
			--metadata		 : File metadata.
			--usertags		 : File tags separated by coma ','

	3.2 --download (or --d): Downloads a File by its GUID, in a specified directory
		## Download Arguments ##
			--fileid		: Required. File Guid.
			--downloadfolder: Required. Download location path. (needs to end with slash '\')
	
	3.3 --addusertags : Add User Tags To A File
		## Add User Tags Arguments ##
			--addusertagsfileid : Required. File Guid.
			--usertagstoadd		: Required. Tags separated by coma ','

	3.4 --removeusertags : Remove User Tags From A File
		## Remove User Tags Arguments ##
			--removeusertagsfileid : Required. File Guid.
			--usertagstoremove : Required. Tags separated by coma ','

	3.5 --sendtoethnofiles (or --s): Sends file to Ethnofiles
		## Send File To Ethnofiles Arguments ##
			--fileapifileid : Required. File Guid from fileAPI.
			--userid		: The user
			--idfield		: File Type Id
			--rowcount      : The count of the rows in the file
			--totalsum      : The total sum of the file
			--debtoriban    : Debtor IBAN
			--tannumber     : Tan Number
			--locale        : Locale
			--inboxid       : Inbox id
			--xactionid     : Xaction Id
			--issmsotp      : Flag that indicates if is SmsOtp
			--convid        : Ethnofiles id for file formatting (it is filled automatically from ethnofiles api)
			--xmlfilefield  : Flag that indicates if it is an xml file
			--debtorname    : Debtor name
			--acceptterms   : Accept terms flag
			--accepttrnterms: Accept Ttn Terms flag

4.EXAMPLES
---------------------------------------------------------------------------------
	Commands examples assuming being in the dll directory.

	4.1 Upload Example
		dotnet FIleAPI_CLI.dll --u --user MyUser --registry MyRegistry --inputfile "../Files/smallfile.txt" --filedescription "Example Uploaded from cli tool"

	4.2 Download Example
		dotnet FIleAPI_CLI.dll --d --user MyUser --registry MyRegistry --fileid  aa7429d0-19ad-4efd-b1ac-02e42f128cd7 --downloadfolder C:\OutputFiles\

	4.3 Add User Tags Example
		dotnet FIleAPI_CLI.dll --addusertags --user MyUser --registry MyRegistry --addusertagsfileid  aa7429d0-19ad-4efd-b1ac-02e42f128cd7 --usertagstoadd addedTag1,addedtag2,addedtag3

	4.4 Remove User Tags Example
		dotnet FIleAPI_CLI.dll --removeusertags --user MyUser --registry MyRegistry --removeusertagsfileid  aa7429d0-19ad-4efd-b1ac-02e42f128cd7 --usertagstoremove addedTag1,addedtag2

	4.5 Send File To Ethnofiles Example
		dotnet FIleAPI_CLI.dll --s --fileapifileid  b8a3f91c-0319-4748-9e77-be0a101514b4 --userid MyUser --idfield 43 --rowcount 11 --totalsum 0 --debtoriban "iban"
	
	4.6 Upload Example with filling all unspecified values from input.js
		dotnet FIleAPI_CLI.dll --u --inputfile "../Files/smallfile.txt" --filedescription "Example Uploaded from cli tool" --completefromconfig

5.OTHER NOTES
---------------------------------------------------------------------------------

** When the CLI is used with the sandboxes, the same user (e.g MyUser) must exist in both File Api Sandbox and Ethnofiles Api Sandbox.


**END**
