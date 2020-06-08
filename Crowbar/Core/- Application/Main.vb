Module Main

	' Entry point of application.
	Public Function Main() As Integer
		'' Create a job with JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE flag, so that all processes 
		''	(e.g. HLMV called by Crowbar) associated with the job 
		''	terminate when the last handle to the job is closed.
		'' From MSDN: By default, processes created using CreateProcess by a process associated with a job 
		''	are associated with the job.
		'TheJob = New WindowsJob()
		'TheJob.AddProcess(Process.GetCurrentProcess().Handle())

		Dim anExceptionHandler As New AppExceptionHandler()
		AddHandler Application.ThreadException, AddressOf anExceptionHandler.Application_ThreadException
		' Set the unhandled exception mode to call Application.ThreadException event for all Windows Forms exceptions.
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

        'Dim appUniqueIdentifier As String
        'Dim appMutex As System.Threading.Mutex
        'appUniqueIdentifier = Application.ExecutablePath.Replace("\", "_")
        'appMutex = New System.Threading.Mutex(False, appUniqueIdentifier)
        'If appMutex.WaitOne(0, False) = False Then
        '	appMutex.Close()
        '	appMutex = Nothing
        '	'MessageBox.Show("Another instance is already running!")
        '	Win32Api.PostMessage(CType(Win32Api.WindowsMessages.HWND_BROADCAST, IntPtr), appUniqueWindowsMessageIdentifier, IntPtr.Zero, IntPtr.Zero)
        'Else
        'NOTE: Use the Windows Vista and later visual styles (such as rounded buttons).

        TheApp = New App()
		'Try
		TheApp.Init()
		If TheApp.Settings.AppIsSingleInstance Then
            'SingleInstanceApplication.Run(New MainForm(), AddressOf StartupNextInstanceEventHandler)
        Else
            'Windows.Forms.Application.Run(MainForm)
        End If
		'Catch e As Exception
		'	MsgBox(e.Message)
		'Finally
		'End Try
		TheApp.Dispose()
		'End If

		Return 0
	End Function

    Private Sub StartupNextInstanceEventHandler(ByVal sender As Object, ByVal e As SingleInstanceEventArgs)
    End Sub

    'Public TheJob As WindowsJob
    Public TheApp As App

End Module
