Imports System.IO
Imports System.Text

Public Class VpkFile
	Inherits BasePackageFile

#Region "Creation and Destruction"

	Public Sub New(ByVal archiveDirectoryFileReader As BinaryReader, ByVal vpkFileReader As BinaryReader, ByVal vpkFileData As VpkFileData)
		Me.theArchiveDirectoryInputFileReader = archiveDirectoryFileReader
		Me.theInputFileReader = vpkFileReader
		Me.theVpkFileData = vpkFileData
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property FileData() As VpkFileData
		Get
			Return Me.theVpkFileData
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Sub ReadHeader()
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theVpkFileData.id = Me.theInputFileReader.ReadUInt32()
		Me.theVpkFileData.version = Me.theInputFileReader.ReadUInt32()
		Me.theVpkFileData.directoryLength = Me.theInputFileReader.ReadUInt32()

		If Me.theVpkFileData.version = 2 Then
			Me.theVpkFileData.unused01 = Me.theInputFileReader.ReadUInt32()
			Me.theVpkFileData.archiveHashLength = Me.theInputFileReader.ReadUInt32()
			Me.theVpkFileData.extraLength = Me.theInputFileReader.ReadUInt32()
			Me.theVpkFileData.unused01 = Me.theInputFileReader.ReadUInt32()
		ElseIf Me.theVpkFileData.version = 196610 Then
			Me.theVpkFileData.unused01 = Me.theInputFileReader.ReadUInt32()
		End If

		Me.theVpkFileData.theDirectoryOffset = Me.theInputFileReader.BaseStream.Position

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theVpkFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VPK File Header")
	End Sub

	'Example output:
	'addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
	'addonimage.vtf crc=0xc75861f5 metadatasz=0 fnumber=32767 ofs=0x29fd sz=8400
	'addoninfo.txt crc=0xb3d2b571 metadatasz=0 fnumber=32767 ofs=0x4acd sz=1677
	'materials/models/weapons/melee/crowbar.vmt crc=0x4aaf5f0 metadatasz=0 fnumber=32767 ofs=0x515a sz=566
	'materials/models/weapons/melee/crowbar.vtf crc=0xded2e058 metadatasz=0 fnumber=32767 ofs=0x5390 sz=174920
	'materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196
	Public Overrides Sub ReadEntries()
		'Dim inputFileStreamPosition As Long
		'Dim fileOffsetStart As Long
		'Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		'If Me.theVpkFileData.id <> VpkFileData.VPK_ID OrElse Me.theVpkFileData.id <> VpkFileData.FPX_ID Then
		'	Exit Sub
		'End If
		If Not Me.theVpkFileData.IsSourcePackage Then
			Exit Sub
		End If

		Dim vpkFileHasMoreToRead As Boolean = True
		Dim entryExtension As String = ""
		Dim entryPath As String = ""
		Dim entryFileName As String = ""
		Dim entry As VpkDirectoryEntry
		'Dim entryDataOutputText As String
		Dim entryDataOutputText As New StringBuilder
		While vpkFileHasMoreToRead
			Try
				entryExtension = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
				If String.IsNullOrEmpty(entryExtension) Then
					Exit While
				End If
			Catch ex As Exception
				'vpkFileHasMoreToRead = False
				Exit While
			End Try

			While vpkFileHasMoreToRead
				Try
					entryPath = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
					If String.IsNullOrEmpty(entryPath) Then
						Exit While
					End If
				Catch ex As Exception
					vpkFileHasMoreToRead = False
					Exit While
				End Try

				While vpkFileHasMoreToRead
					Try
						entryFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)
						If String.IsNullOrEmpty(entryFileName) Then
							Exit While
						End If
					Catch ex As Exception
						vpkFileHasMoreToRead = False
						Exit While
					End Try

					entry = New VpkDirectoryEntry()
					entry.crc = Me.theInputFileReader.ReadUInt32()
					entry.preloadByteCount = Me.theInputFileReader.ReadUInt16()
					entry.archiveIndex = Me.theInputFileReader.ReadUInt16()
					If Me.theVpkFileData.version = 196610 Then
						'TODO: Exit for now so Crowbar does not freeze.
						Exit Sub
						'' 01 01
						'entry.unknown01 = Me.theInputFileReader.ReadUInt16()
						'' 00 00 00 80 
						'entry.unknown02 = Me.theInputFileReader.ReadUInt32()
						'entry.dataOffset = Me.theInputFileReader.ReadUInt32()
						'entry.unknown03 = Me.theInputFileReader.ReadUInt32()
						'entry.dataLength = Me.theInputFileReader.ReadUInt32()
						'entry.unknown04 = Me.theInputFileReader.ReadUInt32()
						'entry.fileSize = Me.theInputFileReader.ReadUInt32()
						'entry.unknown05 = Me.theInputFileReader.ReadUInt32()
						'' FF FF
						'entry.endOfEntryBytes = Me.theInputFileReader.ReadUInt16()
					Else
						entry.dataOffset = Me.theInputFileReader.ReadUInt32()
						entry.dataLength = Me.theInputFileReader.ReadUInt32()
						entry.endBytes = Me.theInputFileReader.ReadUInt16()

						If entry.preloadByteCount > 0 Then
							entry.preloadBytesOffset = Me.theInputFileReader.BaseStream.Position
							Me.theInputFileReader.ReadBytes(entry.preloadByteCount)
						End If
					End If

					If entryPath = " " Then
						entry.thePathFileName = entryFileName + "." + entryExtension
					Else
						entry.thePathFileName = entryPath + "/" + entryFileName + "." + entryExtension
					End If
					Me.theVpkFileData.theEntries.Add(entry)

					'entryDataOutputText = entry.thePathFileName
					'entryDataOutputText += " crc=0x" + entry.crc.ToString("X8")
					'entryDataOutputText += " metadatasz=" + entry.preloadBytes.ToString("G0")
					'entryDataOutputText += " fnumber=" + entry.archiveIndex.ToString("G0")
					'entryDataOutputText += " ofs=0x" + entry.dataOffset.ToString("X8")
					'entryDataOutputText += " sz=" + entry.dataLength.ToString("G0")
					'Me.theVpkFileData.theEntryDataOutputTexts.Add(entryDataOutputText)
					entryDataOutputText.Append(entry.thePathFileName)
					entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"))
					entryDataOutputText.Append(" metadatasz=" + entry.preloadByteCount.ToString("G0"))
					entryDataOutputText.Append(" fnumber=" + entry.archiveIndex.ToString("G0"))
					entryDataOutputText.Append(" ofs=0x" + entry.dataOffset.ToString("X8"))
					entryDataOutputText.Append(" sz=" + (entry.preloadByteCount + entry.dataLength).ToString("G0"))
					Me.theVpkFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString())
					entryDataOutputText.Clear()
				End While
			End While
		End While

		'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'Me.theVpkFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VPK File Header")
	End Sub

	Public Overrides Sub UnpackEntryDataToFile(ByVal iEntry As BasePackageDirectoryEntry, ByVal outputPathFileName As String)
		Dim entry As VpkDirectoryEntry
		entry = CType(iEntry, VpkDirectoryEntry)

		Dim outputFileStream As FileStream = Nothing
		Try
			outputFileStream = New FileStream(outputPathFileName, FileMode.Create)
			If outputFileStream IsNot Nothing Then
				Try
					Me.theOutputFileWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)

					If entry.preloadByteCount > 0 Then
						Me.theArchiveDirectoryInputFileReader.BaseStream.Seek(entry.preloadBytesOffset, SeekOrigin.Begin)
						Dim preloadBytes() As Byte
						preloadBytes = Me.theArchiveDirectoryInputFileReader.ReadBytes(CInt(entry.preloadByteCount))
						Me.theOutputFileWriter.Write(preloadBytes)
					End If
					If entry.archiveIndex = &H7FFF Then
						Me.theInputFileReader.BaseStream.Seek(Me.theVpkFileData.theDirectoryOffset + Me.theVpkFileData.directoryLength + entry.dataOffset, SeekOrigin.Begin)
					Else
						Me.theInputFileReader.BaseStream.Seek(entry.dataOffset, SeekOrigin.Begin)
					End If
					Dim bytes() As Byte
					bytes = Me.theInputFileReader.ReadBytes(CInt(entry.dataLength))
					Me.theOutputFileWriter.Write(bytes)
				Catch ex As Exception
					Dim debug As Integer = 4242
				Finally
					If Me.theOutputFileWriter IsNot Nothing Then
						Me.theOutputFileWriter.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If outputFileStream IsNot Nothing Then
				outputFileStream.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private theArchiveDirectoryInputFileReader As BinaryReader
	Private theInputFileReader As BinaryReader
	Private theOutputFileWriter As BinaryWriter
	Private theVpkFileData As VpkFileData

#End Region

End Class
