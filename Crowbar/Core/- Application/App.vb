Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text

Public Class App
    Implements IDisposable

#Region "Create and Destroy"

    Public Sub New()
        Me.IsDisposed = False

        'NOTE: To use a particular culture's NumberFormat that doesn't change with user settings, 
        '      must use this constructor with False as second param.
        Me.theInternalCultureInfo = New CultureInfo("en-US", False)
        Me.theInternalNumberFormat = Me.theInternalCultureInfo.NumberFormat

        Me.theSmdFilesWritten = New List(Of String)()
    End Sub

#Region "IDisposable Support"

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.IsDisposed Then
            If disposing Then
                Me.Free()
            End If
            'NOTE: free shared unmanaged resources
        End If
        Me.IsDisposed = True
    End Sub

    'Protected Overrides Sub Finalize()
    '	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '	Dispose(False)
    '	MyBase.Finalize()
    'End Sub

#End Region

#End Region

#Region "Init and Free"

    Public Sub Init()
        Me.theAppPath = Application.StartupPath
        Me.LoadAppSettings()

        If Me.Settings.SteamLibraryPaths.Count = 0 Then
            Dim libraryPath As New SteamLibraryPath()
            Me.Settings.SteamLibraryPaths.Add(libraryPath)
        End If

        Me.theUnpacker = New Unpacker()
        'Me.theModelViewer = New Viewer()

        Dim documentsPath As String
        documentsPath = Path.Combine(Me.theAppPath, "Documents")
        AppConstants.HelpTutorialLink = Path.Combine(documentsPath, AppConstants.HelpTutorialLink)
        AppConstants.HelpContentsLink = Path.Combine(documentsPath, AppConstants.HelpContentsLink)
        AppConstants.HelpIndexLink = Path.Combine(documentsPath, AppConstants.HelpIndexLink)
        AppConstants.HelpTipsLink = Path.Combine(documentsPath, AppConstants.HelpTipsLink)

        Dim clArgs() As String = Environment.GetCommandLineArgs()
        ' Hold the command line values
        Dim mdlPa As String = String.Empty
        Dim mdlOPa As String = String.Empty

        Dim OSName As String = Environment.OSVersion.VersionString
        If OSName.Contains("Windows") Then
            AttachConsole(-1)
        End If

        ' Test to see if two switchs and two values were passed in
        ' if yes parse the array
        If clArgs.Count() >= 3 Then
            For i As Integer = 1 To 3 Step 2
                If i < clArgs.Count() Then
                    If clArgs(i) = "-p" Then
                        mdlPa = clArgs(i + 1)
                    ElseIf clArgs(i) = "-o" Then
                        mdlOPa = clArgs(i + 1)
                    End If
                End If
            Next
        End If

        Console.WriteLine(mdlPa)

        If mdlOPa Is String.Empty And mdlPa IsNot String.Empty Then
            If Not mdlPa.LastIndexOf("\") = -1 Then
                mdlOPa = mdlPa.Substring(0, mdlPa.LastIndexOf("\"))
            Else
                If Not mdlPa.LastIndexOf("/") = -1 Then
                    mdlOPa = mdlPa.Substring(0, mdlPa.LastIndexOf("/"))
                End If
            End If
        End If

        Console.WriteLine(mdlOPa)

            If mdlPa IsNot String.Empty And mdlOPa IsNot String.Empty Then
            Me.theDecompiler = New Decompiler(mdlPa, mdlOPa)
        Else
            Console.WriteLine("Usage: -p ""Path\To\File.mdl"" -o ""Path\To\Output\Folder""")
            Console.WriteLine("Alternate Usage (if output folder is same): -p ""Path\To\File.mdl""")
        End If
    End Sub

    Private Sub Free()
        If Me.theSettings IsNot Nothing Then
            Me.SaveAppSettings()
        End If
        'If Me.theCompiler IsNot Nothing Then
        'End If
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property Settings() As AppSettings
        Get
            Return Me.theSettings
        End Get
    End Property

    Public ReadOnly Property ErrorPathFileName() As String
        Get
            Return Path.Combine(Me.GetAppDataPath(), Me.ErrorFileName)
        End Get
    End Property

    Public ReadOnly Property Unpacker() As Unpacker
        Get
            Return Me.theUnpacker
        End Get
    End Property

    Public ReadOnly Property Decompiler() As Decompiler
        Get
            Return Me.theDecompiler
        End Get
    End Property

    Public ReadOnly Property Compiler() As Compiler
        Get
            Return Me.theCompiler
        End Get
    End Property

    'Public ReadOnly Property Viewer() As Viewer
    '	Get
    '		Return Me.theModelViewer
    '	End Get
    'End Property

    'Public Property ModelRelativePathFileName() As String
    '	Get
    '		Return Me.theModelRelativePathFileName
    '	End Get
    '	Set(ByVal value As String)
    '		Me.theModelRelativePathFileName = value
    '	End Set
    'End Property

    Public ReadOnly Property InternalCultureInfo() As CultureInfo
        Get
            Return Me.theInternalCultureInfo
        End Get
    End Property

    Public ReadOnly Property InternalNumberFormat() As NumberFormatInfo
        Get
            Return Me.theInternalNumberFormat
        End Get
    End Property

    Public Property SmdFileNames() As List(Of String)
        Get
            Return Me.theSmdFilesWritten
        End Get
        Set(ByVal value As List(Of String))
            Me.theSmdFilesWritten = value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Function GetDebugPath(ByVal outputPath As String, ByVal modelName As String) As String
        'Dim logsPath As String

        'logsPath = Path.Combine(outputPath, modelName + "_" + App.LogsSubFolderName)

        'Return logsPath
        Return outputPath
    End Function

    Public Sub SaveAppSettings()
        Dim appSettingsPath As String
        Dim appSettingsPathFileName As String

        appSettingsPathFileName = Me.GetAppSettingsPathFileName()
        appSettingsPath = FileManager.GetPath(appSettingsPathFileName)

        If FileManager.PathExistsAfterTryToCreate(appSettingsPath) Then
            FileManager.WriteXml(Me.theSettings, appSettingsPathFileName)
        End If
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadAppSettings()
        Dim appSettingsPathFileName As String
        appSettingsPathFileName = Me.GetAppSettingsPathFileName()

        If File.Exists(appSettingsPathFileName) Then
            Try
                Me.theSettings = CType(FileManager.ReadXml(GetType(AppSettings), appSettingsPathFileName), AppSettings)
            Catch
                Me.CreateAppSettings()
            End Try
        Else
            ' File not found, so init default values.
            Me.CreateAppSettings()
        End If
    End Sub

    Private Function GetAppSettingsPathFileName() As String
        Return Path.Combine(Me.GetCustomDataPath(), App.theAppSettingsFileName)
    End Function

    Private Sub CreateAppSettings()
        Me.theSettings = New AppSettings()

        Dim gameSetup As New GameSetup()
        Me.theSettings.GameSetups.Add(gameSetup)

        Dim aPath As New SteamLibraryPath()
        Me.theSettings.SteamLibraryPaths.Add(aPath)

        Me.SaveAppSettings()
    End Sub

    Private Function GetCustomDataPath() As String
        Dim customDataPath As String
        Dim appDataPath As String

        ' If the settings file exists in the app's Data folder, then load it.
        appDataPath = Me.GetAppDataPath()
        If appDataPath <> "" Then
            customDataPath = appDataPath
        Else
            'NOTE: Use "standard Windows location for app data".
            'NOTE: Using Path.Combine in case theStartupFolder is a root folder, like "C:\".
            customDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZeqMacaw")
            customDataPath += Path.DirectorySeparatorChar
            'customDataPath += "Crowbar"
            customDataPath += My.Application.Info.ProductName
            customDataPath += " "
            customDataPath += My.Application.Info.Version.ToString(2)
        End If

        Return customDataPath
    End Function

    Declare Function AttachConsole Lib "kernel32.dll" (ByVal dwProcessId As Int32) As Boolean
    Declare Function FreeConsole Lib "kernel32.dll" () As Boolean

    Private Function GetAppDataPath() As String
        Dim appDataPath As String
        Dim appDataPathFileName As String

        appDataPath = Path.Combine(Me.theAppPath, App.theDataFolderName)
        appDataPathFileName = Path.Combine(appDataPath, App.theAppSettingsFileName)

        If File.Exists(appDataPathFileName) Then
            Return appDataPath
        Else
            Return ""
        End If
    End Function

    Public Function GetHeaderComment() As String
        Dim line As String

        line = "Created by "
        line += Me.GetProductNameAndVersion()

        Return line
    End Function

    Public Function GetProductNameAndVersion() As String
        Dim result As String

        result = My.Application.Info.ProductName
        result += " "
        result += My.Application.Info.Version.ToString(2)

        Return result
    End Function

    Public Function GetProcessedPathFileName(ByVal pathFileName As String) As String
        Dim result As String
        Dim aMacro As String

        result = pathFileName

        For Each aSteamLibraryPath As SteamLibraryPath In Me.Settings.SteamLibraryPaths
            aMacro = aSteamLibraryPath.Macro
            If pathFileName.StartsWith(aMacro) Then
                pathFileName = pathFileName.Remove(0, aMacro.Length)
                If pathFileName.StartsWith("\") Then
                    pathFileName = pathFileName.Remove(0, 1)
                End If
                result = Path.Combine(aSteamLibraryPath.LibraryPath, pathFileName)
            End If
        Next

        Return result
    End Function

#End Region

#Region "Data"

    Private IsDisposed As Boolean

    Private theInternalCultureInfo As CultureInfo
    Private theInternalNumberFormat As NumberFormatInfo

    Private theSettings As AppSettings

    ' Location of the exe.
    Private theAppPath As String

    Private Const theDataFolderName As String = "Data"
    Private Const theAppSettingsFileName As String = "Crowbar Settings.xml"

    Public Const AnimsSubFolderName As String = "anims"
    Public Const LogsSubFolderName As String = "logs"

    Private ErrorFileName As String = "decompile error.txt"

    Private theUnpacker As Unpacker
    Private theDecompiler As Decompiler
    Private theCompiler As Compiler
    'Private theModelViewer As Viewer
    Private theModelRelativePathFileName As String

    Private theSmdFilesWritten As List(Of String)

#End Region

End Class
