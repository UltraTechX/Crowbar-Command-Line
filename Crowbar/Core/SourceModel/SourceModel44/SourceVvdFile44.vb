Imports System.IO
Imports System.Text

Public Class SourceVvdFile44

#Region "Creation and Destruction"

	Public Sub New(ByVal vvdFileReader As BinaryReader, ByVal vvdFileData As SourceVvdFileData44)
		Me.theInputFileReader = vvdFileReader
		Me.theVvdFileData = vvdFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadSourceVvdHeader()
		Me.theVvdFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theVvdFileData.version = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.checksum = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.lodCount = Me.theInputFileReader.ReadInt32()
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theVvdFileData.lodVertexCount(i) = Me.theInputFileReader.ReadInt32()
		Next
		Me.theVvdFileData.fixupCount = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.fixupTableOffset = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.vertexDataOffset = Me.theInputFileReader.ReadInt32()
		Me.theVvdFileData.tangentDataOffset = Me.theInputFileReader.ReadInt32()
	End Sub

	Public Sub ReadVertexes()
		If Me.theVvdFileData.lodCount <= 0 Then
			Exit Sub
		End If

		Me.theInputFileReader.BaseStream.Seek(Me.theVvdFileData.vertexDataOffset, SeekOrigin.Begin)

		'Dim boneWeightingIsIncorrect As Boolean
		Dim weight As Single
		Dim boneIndex As Byte

		Dim vertexCount As Integer
		vertexCount = Me.theVvdFileData.lodVertexCount(0)
		Me.theVvdFileData.theVertexes = New List(Of SourceVertex)(vertexCount)
		For j As Integer = 0 To vertexCount - 1
			Dim aStudioVertex As New SourceVertex()

			Dim boneWeight As New SourceBoneWeight()
			'boneWeightingIsIncorrect = False
			For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
				weight = Me.theInputFileReader.ReadSingle()
				boneWeight.weight(x) = weight
				'If weight > 1 Then
				'	boneWeightingIsIncorrect = True
				'End If
			Next
			For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
				boneIndex = Me.theInputFileReader.ReadByte()
				boneWeight.bone(x) = boneIndex
				'If boneIndex > 127 Then
				'	boneWeightingIsIncorrect = True
				'End If
			Next
			boneWeight.boneCount = Me.theInputFileReader.ReadByte()
			''TODO: ReadVertexes() -- boneWeight.boneCount > MAX_NUM_BONES_PER_VERT, which seems like incorrect vvd format 
			'If boneWeight.boneCount > MAX_NUM_BONES_PER_VERT Then
			'	boneWeight.boneCount = CByte(MAX_NUM_BONES_PER_VERT)
			'End If
			'If boneWeightingIsIncorrect Then
			'	boneWeight.boneCount = 0
			'End If
			aStudioVertex.boneWeight = boneWeight

			aStudioVertex.positionX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.positionY = Me.theInputFileReader.ReadSingle()
			aStudioVertex.positionZ = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalY = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalZ = Me.theInputFileReader.ReadSingle()
			aStudioVertex.texCoordX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.texCoordY = Me.theInputFileReader.ReadSingle()
			Me.theVvdFileData.theVertexes.Add(aStudioVertex)
		Next
	End Sub

	Public Sub ReadFixups()
		If Me.theVvdFileData.fixupCount > 0 Then
			Me.theInputFileReader.BaseStream.Seek(Me.theVvdFileData.fixupTableOffset, SeekOrigin.Begin)

			Me.theVvdFileData.theFixups = New List(Of SourceVvdFixup)(Me.theVvdFileData.fixupCount)
			For fixupIndex As Integer = 0 To Me.theVvdFileData.fixupCount - 1
				Dim aFixup As New SourceVvdFixup()

				aFixup.lodIndex = Me.theInputFileReader.ReadInt32()
				aFixup.vertexIndex = Me.theInputFileReader.ReadInt32()
				aFixup.vertexCount = Me.theInputFileReader.ReadInt32()
				Me.theVvdFileData.theFixups.Add(aFixup)
			Next
			If Me.theVvdFileData.lodCount > 0 Then
				Me.theInputFileReader.BaseStream.Seek(Me.theVvdFileData.vertexDataOffset, SeekOrigin.Begin)

				For lodIndex As Integer = 0 To Me.theVvdFileData.lodCount - 1
					Me.SetupFixedVertexes(lodIndex)
				Next
				Dim i As Integer = 0
			End If
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub SetupFixedVertexes(ByVal lodIndex As Integer)
		Dim aFixup As SourceVvdFixup
		Dim aStudioVertex As SourceVertex

		Me.theVvdFileData.theFixedVertexesByLod(lodIndex) = New List(Of SourceVertex)
		For fixupIndex As Integer = 0 To Me.theVvdFileData.theFixups.Count - 1
			aFixup = Me.theVvdFileData.theFixups(fixupIndex)

			If aFixup.lodIndex >= lodIndex Then
				For j As Integer = 0 To aFixup.vertexCount - 1
					aStudioVertex = Me.theVvdFileData.theVertexes(aFixup.vertexIndex + j)
					Me.theVvdFileData.theFixedVertexesByLod(lodIndex).Add(aStudioVertex)
				Next
			End If
		Next
	End Sub

#End Region

#Region "Data"

	Private theInputFileReader As BinaryReader
	Private theVvdFileData As SourceVvdFileData44

#End Region

End Class
