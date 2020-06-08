Imports System.IO
Imports System.Text

Public Class AppDebug2File

#Region "Write Log File Methods"

	Public Sub WriteFile(ByVal pathFileName As String, ByVal name As String, ByVal aFileSeekLog As FileSeekLog)
		If aFileSeekLog Is Nothing Then
			Exit Sub
		End If

		Try
			Me.theOutputFileStream = File.CreateText(pathFileName)

			Me.WriteHeaderComment()
			Me.WriteFileSeekLog(name, aFileSeekLog)
		Catch
		Finally
			Me.theOutputFileStream.Flush()
			Me.theOutputFileStream.Close()
		End Try
	End Sub

#End Region

#Region "Private Methods"

	Private Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteFileSeparatorLines()
		Dim line As String

		Me.WriteLogLine(0, "")
		Me.WriteLogLine(0, "")
		line = "################################################################################"
		Me.WriteLogLine(0, line)
		Me.WriteLogLine(0, "")
		Me.WriteLogLine(0, "")
	End Sub

	Private Sub WriteLogLine(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String = ""
		For i As Integer = 1 To indentLevel
			indentedLine += vbTab
		Next
		indentedLine += line
		Me.theOutputFileStream.WriteLine(indentedLine)
		Me.theOutputFileStream.Flush()
	End Sub

	'Private Sub WriteFileSeekLog(ByVal name As String, ByVal aFileSeekList As SortedList(Of Long, Long))
	'	Dim line As String

	'	line = "====== " + name + " File Seek ======"
	'	Me.WriteLogLine(0, line)

	'	line = "--- Summary ---"
	'	Me.WriteLogLine(0, line)

	'	Dim offsetStart As Long
	'	Dim offsetEnd As Long
	'	offsetStart = -1
	'	For i As Integer = 0 To aFileSeekList.Count - 1
	'		If offsetStart = -1 Then
	'			offsetStart = aFileSeekList.Keys(i)
	'		End If
	'		offsetEnd = aFileSeekList.Values(i)
	'		If i = aFileSeekList.Count - 1 Then
	'			Me.WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0"))
	'		ElseIf offsetEnd + 1 <> aFileSeekList.Keys(i + 1) Then
	'			Me.WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0"))
	'			offsetStart = -1
	'		End If
	'	Next

	'	line = "------------------------"
	'	Me.WriteLogLine(0, line)
	'	line = "--- Each Section or Loop ---"
	'	Me.WriteLogLine(0, line)

	'	For i As Integer = 0 To aFileSeekList.Count - 1
	'		Me.WriteLogLine(1, aFileSeekList.Keys(i).ToString("N0") + " - " + aFileSeekList.Values(i).ToString("N0"))
	'	Next

	'	line = "========================"
	'	Me.WriteLogLine(0, line)
	'End Sub

	Private Sub WriteFileSeekLog(ByVal name As String, ByVal aFileSeekLog As FileSeekLog)
		Dim line As String

		line = "====== " + name + " File Seek Log ======"
		Me.WriteLogLine(0, line)

		line = "--- Summary ---"
		Me.WriteLogLine(0, line)

		Dim offsetStart As Long
		Dim offsetEnd As Long
		offsetStart = -1
		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
			If offsetStart = -1 Then
				offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
			End If
			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)
			'If i = aFileSeekLog.theFileSeekList.Count - 1 Then
			'	Me.WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0"))
			'ElseIf offsetEnd + 1 <> aFileSeekLog.theFileSeekList.Keys(i + 1) Then
			'	Me.WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0"))
			'	offsetStart = -1
			'End If
			If (i = aFileSeekLog.theFileSeekList.Count - 1) OrElse (offsetEnd + 1 <> aFileSeekLog.theFileSeekList.Keys(i + 1)) Then
				Me.WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0"))
				offsetStart = -1
			End If
		Next

		line = "------------------------"
		Me.WriteLogLine(0, line)
		line = "--- Each Section or Loop ---"
		Me.WriteLogLine(0, line)

		offsetEnd = -1
		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
			offsetStart = aFileSeekLog.theFileSeekList.Keys(i)

			If offsetEnd <> offsetStart - 1 Then
				Me.theOutputFileStream.WriteLine()
				Me.theOutputFileStream.Flush()
			End If

			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)

			Me.WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0") + " " + aFileSeekLog.theFileSeekDescriptionList.Values(i))
		Next

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStream As StreamWriter

#End Region

End Class
