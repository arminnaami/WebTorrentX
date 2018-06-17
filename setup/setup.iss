; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "WebTorrentX"
#define MyAppVersion "0.1.0"
#define MyAppPublisher "Minoti, Inc"
#define MyAppURL "https://github.com/WebTorrentX/WebTorrentX"
#define MyAppExeName "WebTorrentX.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{0B7EFFBC-D1A3-43E5-8C8D-1F9E0E3E46DB}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=C:\Users\Sergei\source\repos\WebTorrentX\setup
OutputBaseFilename=setup
SetupIconFile=C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\icon.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\WebTorrentX.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\boost_chrono-vc120-mt-1_55.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\boost_date_time-vc120-mt-1_55.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\boost_system-vc120-mt-1_55.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\boost_thread-vc120-mt-1_55.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\FontAwesome.WPF.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Meta.Vlc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Meta.Vlc.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Microsoft.WindowsAPICodePack.ShellExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\Ragnar.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\torrent.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\WebTorrentX\bin\x86\Release\LibVlc\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\Sergei\source\repos\WebTorrentX\setup\*.torrent"; DestDir: "{userappdata}\WebTorrentX\.torrents\"; Flags: ignoreversion
Source: "C:\Users\Sergei\source\repos\WebTorrentX\setup\.download"; DestDir: "{userappdata}\WebTorrentX\"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

