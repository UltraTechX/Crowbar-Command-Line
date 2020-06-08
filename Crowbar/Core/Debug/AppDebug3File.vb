Imports System.IO
Imports System.Text

Public Class AppDebug3File

#Region "Write Log File Methods"

	Public Sub WriteFile(ByVal pathFileName As String, ByVal mdlFileData As List(Of UnknownValue))
		If mdlFileData Is Nothing Then
			Return
		End If

		Try
			Me.theOutputFileStream = File.CreateText(pathFileName)

			Me.WriteHeaderComment()

			Me.WriteUnknownValues(mdlFileData)
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

	Private Sub WriteUnknownValues(ByVal mdlFileData As List(Of UnknownValue))
		Dim line As String

		line = "====== MDL Unknown Bytes ======"
		Me.WriteLogLine(0, line)

		If mdlFileData Is Nothing Then
			Return
		End If

		For i As Integer = 0 To mdlFileData.Count - 1
			Dim anUnknownValue As UnknownValue
			anUnknownValue = mdlFileData(i)
			line = DebugFormatModule.FormatLongWithHexLine("offset", anUnknownValue.offset)
			line += " ("
			line += anUnknownValue.type
			line += "): "
			If anUnknownValue.type = "Byte" Then
				line += anUnknownValue.value.ToString("N0")
				line += " (0x"
				line += anUnknownValue.value.ToString("X2")
				line += ")"
			ElseIf anUnknownValue.type = "Int32" Then
				line += anUnknownValue.value.ToString("N0")
				line += " (0x"
				line += anUnknownValue.value.ToString("X8")
				line += ")"
			End If
			Me.WriteLogLine(0, line)
		Next
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStream As StreamWriter

#End Region

End Class
