﻿; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Sample Windows Service"
#define MyAppExeName "SampleWindowsService.exe"
#define MyAppVersion GetFileVersion('..\Bin\' + MyAppExeName)
#define MyAppPublisher "Acme Technology"
#define MyAppURL "http://www.acme.com/"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{571A0A04-2E63-4930-85A1-8A19986CDAA8}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
AppContact=contact@acme.com
AppSupportPhone=+902122222222
AppCopyright={#MyAppPublisher}
WizardImageFile=Setup_Banner.bmp
WizardSmallImageFile=compiler:WizModernSmallImage-IS.bmp
WizardImageBackColor=$272727
WizardImageStretch=false
LicenseFile=End User Licence Agreement.rtf
UsePreviousAppDir=False
DefaultDirName={pf32}\{#MyAppPublisher}\{#MyAppName}\
DirExistsWarning=False
EnableDirDoesntExistWarning=False
AppendDefaultDirName=False
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename={#MyAppName} {#MyAppVersion}
Compression=lzma
SolidCompression=yes
UninstallDisplayName={#MyAppName} {#MyAppVersion}
UninstallDisplayIcon={app}\SetupIcon.ico
;Uncomment following lines if this is a 64 bit application
;ArchitecturesInstallIn64BitMode=x64
;ArchitecturesAllowed=x64
MinVersion=0,6.1

;[Languages]
;Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
;Directories that setup will create automatically
Name: "{app}\Logs"
Name: "{app}\AppData\Projects"

[Files]
Source: "..\Bin\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Bin\{#MyAppExeName}.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "SetupIcon.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"; IconFilename: "{app}\SetupIcon.ico";

[Code]
////// INSTALLED CONTROL ///////////////////
function GetUninstallString(): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
begin
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#emit SetupSetting("AppId")}_is1');
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade(): Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function InitializeSetup(): Boolean;
begin
    Result:=true;
    if (IsUpgrade()) then
    begin
      MsgBox('Product is already installed. Please uninstall and try again.', mbInformation, MB_OK);
      Result := false;
    end;
end;
////// INSTALLED CONTROL ///////////////////