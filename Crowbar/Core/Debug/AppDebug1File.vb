Imports System.IO
Imports System.Text

Public Class AppDebug1File

#Region "Write Log File Methods"

	Public Sub WriteFile(ByVal pathFileName As String, ByVal aMdlFileData As SourceMdlFileData)
		If aMdlFileData Is Nothing Then
			Return
		End If

		Try
			Me.theOutputFileStream = File.CreateText(pathFileName)

			'Me.theSourceEngineModel = aMdlFileData

			Me.WriteHeaderComment()

			Me.WriteMdlFileInfo(aMdlFileData)
			'Me.WritePhyFileInfo()
			'Me.WriteVtxFileInfo()
			'Me.WriteVtxFileInfoCalculatedOffsets2()
			'Me.WriteVtxFileInfoCalculatedOffsets()
			'Me.WriteVvdFileInfo()

			'Me.WriteMdlModelGroupMdlFileInfos(Me.theSourceEngineModel.theMdlFileData)
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

#Region "Write MDL-File Related"

	Private Sub WriteMdlModelGroupMdlFileInfos(ByVal aMdlFileData As SourceMdlFileData)
		If aMdlFileData.theModelGroups IsNot Nothing Then
			For i As Integer = 0 To aMdlFileData.theModelGroups.Count - 1
				Me.WriteMdlFileInfo(aMdlFileData.theModelGroups(i).theMdlFileData)
				Me.WriteMdlModelGroupMdlFileInfos(aMdlFileData.theModelGroups(i).theMdlFileData)
			Next
		End If
	End Sub

	Private Sub WriteMdlFileInfo(ByVal aMdlFileHeader As SourceMdlFileData)
		Me.WriteFileSeparatorLines()

		Me.WriteMdlHeader(aMdlFileHeader)
		Me.WriteMdlBones(aMdlFileHeader)
		Me.WriteMdlAnimationDescs(aMdlFileHeader)
		Me.WriteMdlSequenceDescs(aMdlFileHeader)
		Me.WriteMdlTextures(aMdlFileHeader)
		Me.WriteMdlTexturePaths(aMdlFileHeader)

		Me.WriteMdlModelGroups(aMdlFileHeader)
		Me.WriteMdlBodyParts(aMdlFileHeader)

		Me.WriteMdlAttachments(aMdlFileHeader)
		Me.WriteMdlFlexDescs(aMdlFileHeader)
		Me.WriteMdlFlexControllers(aMdlFileHeader)
		Me.WriteMdlFlexRules(aMdlFileHeader)
		Me.WriteMdlIkChains(aMdlFileHeader)

		Me.WriteMdlMouths(aMdlFileHeader)
		Me.WriteMdlPoseParamDescs(aMdlFileHeader)

		Me.WriteMdlHitboxSets(aMdlFileHeader)
		Me.WriteMdlFlexControllerUis(aMdlFileHeader)
	End Sub

	Private Sub WriteMdlHeader(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		line = "====== MDL Header ======"
		Me.WriteLogLine(0, line)

		Me.WriteLogLine(0, DebugFormatModule.FormatStringLine("00 id", aMdlFileHeader.id))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("04 version", aMdlFileHeader.version))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerAsHexLine("08 checksum", aMdlFileHeader.checksum))
		Me.WriteLogLine(0, DebugFormatModule.FormatStringLine("0C name ($modelname)", aMdlFileHeader.name))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("4C fileSize (size of mdl file in bytes)", aMdlFileHeader.fileSize))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("50 eyePositionX ($eyeposition)", aMdlFileHeader.eyePositionX))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("54 eyePositionY", aMdlFileHeader.eyePositionY))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("58 eyePositionZ", aMdlFileHeader.eyePositionZ))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("5C illuminationPositionX ($illumposition)", aMdlFileHeader.illuminationPositionX))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("60 illuminationPositionY", aMdlFileHeader.illuminationPositionY))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("64 illuminationPositionZ", aMdlFileHeader.illuminationPositionZ))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("68 hullMinPositionX", aMdlFileHeader.hullMinPositionX))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("6C hullMinPositionY", aMdlFileHeader.hullMinPositionY))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("70 hullMinPositionZ", aMdlFileHeader.hullMinPositionZ))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("74 hullMaxPositionX", aMdlFileHeader.hullMaxPositionX))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("78 hullMaxPositionY", aMdlFileHeader.hullMaxPositionY))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("7C hullMaxPositionZ", aMdlFileHeader.hullMaxPositionZ))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("80 viewBoundingBoxMinPositionX", aMdlFileHeader.viewBoundingBoxMinPositionX))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("84 viewBoundingBoxMinPositionY", aMdlFileHeader.viewBoundingBoxMinPositionY))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("88 viewBoundingBoxMinPositionZ", aMdlFileHeader.viewBoundingBoxMinPositionZ))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("8C viewBoundingBoxMaxPositionX", aMdlFileHeader.viewBoundingBoxMaxPositionX))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("90 viewBoundingBoxMaxPositionY", aMdlFileHeader.viewBoundingBoxMaxPositionY))
		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("94 viewBoundingBoxMaxPositionZ", aMdlFileHeader.viewBoundingBoxMaxPositionZ))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerAsHexLine("98 flags", aMdlFileHeader.flags))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("9C boneCount", aMdlFileHeader.boneCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("A0 boneOffset", aMdlFileHeader.boneOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("A4 boneControllerCount", aMdlFileHeader.boneControllerCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("A8 boneControllerOffset", aMdlFileHeader.boneControllerOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("AC hitboxSetCount", aMdlFileHeader.hitboxSetCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("B0 hitboxSetOffset", aMdlFileHeader.hitboxSetOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("B4 localAnimationCount", aMdlFileHeader.localAnimationCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("B8 localAnimationOffset", aMdlFileHeader.localAnimationOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("BC localSequenceCount ($sequence)", aMdlFileHeader.localSequenceCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("C0 localSequenceOffset", aMdlFileHeader.localSequenceOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("C4 activityListVersion", aMdlFileHeader.activityListVersion))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("C8 eventsIndexed", aMdlFileHeader.eventsIndexed))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("CC textureCount", aMdlFileHeader.textureCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("D0 textureOffset", aMdlFileHeader.textureOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("D4 texturePathCount ($cdmaterials)", aMdlFileHeader.texturePathCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("D8 texturePathOffset", aMdlFileHeader.texturePathOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("DC skinReferenceCount", aMdlFileHeader.skinReferenceCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("E0 skinFamilyCount ($texturegroup skinfamilies)", aMdlFileHeader.skinFamilyCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("E4 skinFamilyOffset", aMdlFileHeader.skinFamilyOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("E8 bodyPartCount ($model)", aMdlFileHeader.bodyPartCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("EC bodyPartOffset", aMdlFileHeader.bodyPartOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("localAttachmentCount ($attachment)", aMdlFileHeader.localAttachmentCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("localAttachmentOffset", aMdlFileHeader.localAttachmentOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("localNodeCount", aMdlFileHeader.localNodeCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("localNodeOffset", aMdlFileHeader.localNodeOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("localNodeNameOffset", aMdlFileHeader.localNodeNameOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("flexDescCount", aMdlFileHeader.flexDescCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("flexDescOffset", aMdlFileHeader.flexDescOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("flexControllerCount", aMdlFileHeader.flexControllerCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("flexControllerOffset", aMdlFileHeader.flexControllerOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("flexRuleCount", aMdlFileHeader.flexRuleCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("flexRuleOffset", aMdlFileHeader.flexRuleOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("ikChainCount ($ikchain)", aMdlFileHeader.ikChainCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("ikChainOffset", aMdlFileHeader.ikChainOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("mouthCount ($model {mouth})", aMdlFileHeader.mouthCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("mouthOffset", aMdlFileHeader.mouthOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("localPoseParamaterCount ($poseparameter)", aMdlFileHeader.localPoseParamaterCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("localPoseParameterOffset", aMdlFileHeader.localPoseParameterOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("surfacePropOffset ($surfaceprop)", aMdlFileHeader.surfacePropOffset))
		Me.WriteLogLine(0, "    surfaceprop name: " + aMdlFileHeader.theSurfacePropName)

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("keyValueOffset", aMdlFileHeader.keyValueOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("keyValueSize", aMdlFileHeader.keyValueSize))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("localIkAutoPlayLockCount", aMdlFileHeader.localIkAutoPlayLockCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("localIkAutoPlayLockOffset", aMdlFileHeader.localIkAutoPlayLockOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatSingleFloatLine("mass ($collisionjoints {$mass})", aMdlFileHeader.mass))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("contents", aMdlFileHeader.contents))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("includeModelCount ($includemodel)", aMdlFileHeader.includeModelCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("includeModelOffset", aMdlFileHeader.includeModelOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("virtualModelP", aMdlFileHeader.virtualModelP))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("animBlockNameOffset", aMdlFileHeader.animBlockNameOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("animBlockCount", aMdlFileHeader.animBlockCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("animBlockOffset", aMdlFileHeader.animBlockOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("animBlockModelP", aMdlFileHeader.animBlockModelP))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("boneTableByNameOffset", aMdlFileHeader.boneTableByNameOffset))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("vertexBaseP", aMdlFileHeader.vertexBaseP))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("indexBaseP", aMdlFileHeader.indexBaseP))

		Me.WriteLogLine(0, DebugFormatModule.FormatByteWithHexLine("directionalLightDot", aMdlFileHeader.directionalLightDot))

		Me.WriteLogLine(0, DebugFormatModule.FormatByteWithHexLine("rootLod", aMdlFileHeader.rootLod))

		Me.WriteLogLine(0, DebugFormatModule.FormatByteWithHexLine("allowedRootLodCount", aMdlFileHeader.allowedRootLodCount_VERSION48))

		Me.WriteLogLine(0, DebugFormatModule.FormatByteWithHexLine("unused", aMdlFileHeader.unused))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("unused4", aMdlFileHeader.unused4))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("flexControllerUiCount", aMdlFileHeader.flexControllerUiCount_VERSION48))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("flexControllerUiOffset", aMdlFileHeader.flexControllerUiOffset_VERSION48))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("unused3(0)", aMdlFileHeader.unused3(0)))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("unused3(1)", aMdlFileHeader.unused3(1)))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("studioHeader2Offset", aMdlFileHeader.studioHeader2Offset_VERSION48))

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("unused2", aMdlFileHeader.unused2))

		line = "========================"
		Me.WriteLogLine(0, line)

		line = "[studiohdr2]"
		Me.WriteLogLine(0, line)
		If aMdlFileHeader.version > 44 AndAlso aMdlFileHeader.studioHeader2Offset_VERSION48 > 0 Then
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("sourceBoneTransformCount", aMdlFileHeader.sourceBoneTransformCount))
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("sourceBoneTransformOffset", aMdlFileHeader.sourceBoneTransformOffset))
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("illumPositionAttachmentIndex", aMdlFileHeader.illumPositionAttachmentIndex))
			Me.WriteLogLine(0, DebugFormatModule.FormatDoubleFloatLine("maxEyeDeflection", aMdlFileHeader.maxEyeDeflection))
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("linearBoneOffset", aMdlFileHeader.linearBoneOffset))
			For x As Integer = 0 To aMdlFileHeader.reserved.Length - 1
				Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("reserved(" + x.ToString() + ")", aMdlFileHeader.reserved(x)))
			Next
		Else
			line = "[This MDL does not have studiohdr2.]"
			Me.WriteLogLine(0, line)
		End If

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

	Private Sub WriteMdlBones(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theBones IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Bones (nodes) ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theBones.Count - 1
				Dim aBone As SourceMdlBone
				aBone = aMdlFileHeader.theBones(i)

				Dim parentBoneName As String
				If aBone.parentBoneIndex >= 0 AndAlso aBone.parentBoneIndex < aMdlFileHeader.theBones.Count Then
					parentBoneName = aMdlFileHeader.theBones(aBone.parentBoneIndex).theName
				Else
					parentBoneName = "[no parent bone]"
				End If

				'Me.WriteLogLine(1, "[index: " + i.ToString() + "]")
				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Bone (node)", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("name", aBone.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("parentBoneIndex", aBone.parentBoneIndex) + " (" + parentBoneName + ")")

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("flags", aBone.flags))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("proceduralRuleType", aBone.proceduralRuleType))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("proceduralRuleOffset", aBone.proceduralRuleOffset))

				Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("position", aBone.position.x, aBone.position.y, aBone.position.z))
				Me.WriteLogLine(1, DebugFormatModule.FormatQuaternionLine("quat", aBone.quat))

				If aMdlFileHeader.version <> 2531 Then
					Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("rotation", aBone.rotation.x, aBone.rotation.y, aBone.rotation.z))
					Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("positionScale", aBone.positionScale.x, aBone.positionScale.y, aBone.positionScale.z))
					Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("rotationScale", aBone.rotationScale.x, aBone.rotationScale.y, aBone.rotationScale.z))
				End If

				Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("poseToBoneColumn0", aBone.poseToBoneColumn0))
				Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("poseToBoneColumn1", aBone.poseToBoneColumn1))
				Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("poseToBoneColumn2", aBone.poseToBoneColumn2))
				Me.WriteLogLine(1, DebugFormatModule.FormatVectorLine("poseToBoneColumn3", aBone.poseToBoneColumn3))

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlAnimationDescs(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theAnimationDescs IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Animation Descs ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theAnimationDescs.Count - 1
				Dim anAnimationDesc As SourceMdlAnimationDesc
				anAnimationDesc = aMdlFileHeader.theAnimationDescs(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Animation Desc", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("baseHeaderOffset", anAnimationDesc.baseHeaderOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("nameOffset", anAnimationDesc.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", anAnimationDesc.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("fps", anAnimationDesc.fps))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("flags", anAnimationDesc.flags))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("frameCount", anAnimationDesc.frameCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("movementCount", anAnimationDesc.movementCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("movementOffset", anAnimationDesc.movementOffset))

				For x As Integer = 0 To 5
					Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("unused1(" + x.ToString() + ")", anAnimationDesc.unused1(x)))
				Next

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("animBlock", anAnimationDesc.animBlock))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("animOffset", anAnimationDesc.animOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("ikRuleCount", anAnimationDesc.ikRuleCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("ikRuleOffset", anAnimationDesc.ikRuleOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("animblockIkRuleOffset", anAnimationDesc.animblockIkRuleOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("localHierarchyCount", anAnimationDesc.localHierarchyCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("localHierarchyOffset", anAnimationDesc.localHierarchyOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("sectionOffset", anAnimationDesc.sectionOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("sectionFrameCount", anAnimationDesc.sectionFrameCount))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("spanFrameCount", anAnimationDesc.spanFrameCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("spanCount", anAnimationDesc.spanCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("spanOffset", anAnimationDesc.spanOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("spanStallTime", anAnimationDesc.spanStallTime))

				'For x As Integer = 0 To 5
				'	Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("unknown(" + x.ToString() + ")", anAnimationDesc.unknown(x)))
				'Next

				If anAnimationDesc.animBlock = 0 AndAlso ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
					Me.WriteMdlAnimation(aMdlFileHeader, anAnimationDesc)
					Me.WriteMdlIkRules(aMdlFileHeader, anAnimationDesc)
					'Me.WriteMdlAnimationSections(aMdlFileHeader, anAnimationDesc)
				End If

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlAnimation(ByVal aMdlFileHeader As SourceMdlFileData, ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim line As String
		Dim anAnimation As SourceMdlAnimation

		'If anAnimationDesc.theAnimations IsNot Nothing Then
		'line = "====== Animation ======"
		If anAnimationDesc.theSectionsOfAnimations(0) IsNot Nothing Then
			line = "====== Animation (for Section 1) ======"
			Me.WriteLogLine(1, line)

			'For i As Integer = 0 To anAnimationDesc.theAnimations.Count - 1
			For i As Integer = 0 To anAnimationDesc.theSectionsOfAnimations(0).Count - 1
				Try
					'anAnimation = anAnimationDesc.theAnimations(i)
					anAnimation = anAnimationDesc.theSectionsOfAnimations(0)(i)

					Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("Animation", i))

					Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("boneIndex", anAnimation.boneIndex))
					If anAnimation.boneIndex = 255 Then
						line = "--------------------"
						Me.WriteLogLine(2, line)
						Continue For
					End If
					Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("flags", anAnimation.flags))
					Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("nextSourceMdlAnimationOffset", anAnimation.nextSourceMdlAnimationOffset))

					line = ""
					If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
						line += "    STUDIO_ANIM_RAWPOS: "
						line += "("
						line += anAnimation.thePos.x.ToString("0.000000", TheApp.InternalNumberFormat)
						line += ", "
						line += anAnimation.thePos.y.ToString("0.000000", TheApp.InternalNumberFormat)
						line += ", "
						line += anAnimation.thePos.z.ToString("0.000000", TheApp.InternalNumberFormat)
						line += ")"
					End If
					If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
						line += "    STUDIO_ANIM_ANIMPOS: "
						line += "("
						line += ", "
						line += ", "
						line += ")"
					End If
					If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
						line += "    STUDIO_ANIM_RAWROT2: "
						line += "("
						line += ", "
						line += ", "
						line += ")"
					End If
					If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
						line += "    STUDIO_ANIM_RAWROT: "
						line += "("
						line += ", "
						line += ", "
						line += ")"
					End If
					If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
						line += "    STUDIO_ANIM_ANIMROT: "
						line += "("
						line += ", "
						line += ", "
						line += ")"
					End If
					Me.WriteLogLine(2, line)

					'If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
					'	line += " "
					'	line += anAnimation.thePos.x.ToString("0.000000", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.thePos.y.ToString("0.000000", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.thePos.z.ToString("0.000000", TheApp.InternalNumberFormat)
					'ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
					'	line += " "
					'	line += anAnimation.thePosV.theAnimValue(0).value.ToString("0", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.thePosV.theAnimValue(1).value.ToString("0", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.thePosV.theAnimValue(2).value.ToString("0", TheApp.InternalNumberFormat)
					'Else
					'	line += " 0 0 0"
					'End If

					'If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
					'	line += " "
					'	line += anAnimation.theRot64.xRadians.ToString("0.000000", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.theRot64.yRadians.ToString("0.000000", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.theRot64.zRadians.ToString("0.000000", TheApp.InternalNumberFormat)
					'ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
					'	line += " "
					'	line += anAnimation.theRot48.x.ToString("0.000000", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.theRot48.y.ToString("0.000000", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.theRot48.z.ToString("0.000000", TheApp.InternalNumberFormat)
					'ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
					'	line += " "
					'	line += anAnimation.theRotV.theAnimValue(0).value.ToString("0", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.theRotV.theAnimValue(1).value.ToString("0", TheApp.InternalNumberFormat)
					'	line += " "
					'	line += anAnimation.theRotV.theAnimValue(2).value.ToString("0", TheApp.InternalNumberFormat)
					'Else
					'	line += " 0 0 0"
					'End If

					line = "--------------------"
					Me.WriteLogLine(2, line)
				Catch
				End Try
			Next
		End If
	End Sub

	Private Sub WriteMdlIkRules(ByVal aMdlFileHeader As SourceMdlFileData, ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim line As String

		If anAnimationDesc.theIkRules IsNot Nothing Then
			line = "====== Animation IK Rules ======"
			Me.WriteLogLine(1, line)

			For i As Integer = 0 To anAnimationDesc.theIkRules.Count - 1
				Dim anIkRule As SourceMdlIkRule
				anIkRule = anAnimationDesc.theIkRules(i)

				Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("IK Rule", i))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("index", anIkRule.index))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("type", anIkRule.type))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("chain", anIkRule.chain))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("bone", anIkRule.bone))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("slot", anIkRule.slot))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("height", anIkRule.height))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("radius", anIkRule.radius))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("slot", anIkRule.floor))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("pos.x", anIkRule.pos.x))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("pos.y", anIkRule.pos.y))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("pos.z", anIkRule.pos.z))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("q.x", anIkRule.q.x))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("q.y", anIkRule.q.y))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("q.z", anIkRule.q.z))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("q.w", anIkRule.q.w))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("compressedIkErrorOffset", anIkRule.compressedIkErrorOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused2", anIkRule.unused2))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("ikErrorIndexStart", anIkRule.ikErrorIndexStart))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("ikErrorOffset", anIkRule.ikErrorOffset))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("influenceStart", anIkRule.influenceStart))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("influencePeak", anIkRule.influencePeak))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("influenceTail", anIkRule.influenceTail))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("influenceEnd", anIkRule.influenceEnd))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("unused3", anIkRule.unused3))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("contact", anIkRule.contact))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("drop", anIkRule.drop))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("top", anIkRule.top))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused6", anIkRule.unused6))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused7", anIkRule.unused7))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused8", anIkRule.unused8))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("attachmentNameOffset", anIkRule.attachmentNameOffset))
				Me.WriteLogLine(3, DebugFormatModule.FormatStringLine("theAttachmentName", anIkRule.theAttachmentName))

				For x As Integer = 0 To anIkRule.unused.Length - 1
					Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused(" + x.ToString() + ")", anIkRule.unused(x)))
				Next

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	'Private Sub WriteMdlAnimationSections(ByVal aMdlFileHeader As SourceMdlFileData, ByVal anAnimationDesc As SourceMdlAnimationDesc)
	'	Dim line As String

	'	If anAnimationDesc.theSections IsNot Nothing Then
	'		line = "====== Animation Sections ======"
	'		Me.WriteLogLine(1, line)

	'		For i As Integer = 0 To anAnimationDesc.theSections.Count - 1
	'			Dim anAnimationSection As SourceMdlAnimationSection
	'			anAnimationSection = anAnimationDesc.theSections(i)

	'			Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("Animation Section", i))

	'			Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("animBlock", anAnimationSection.animBlock))
	'			Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("animOffset", anAnimationSection.animOffset))

	'			'Me.WriteMdlMeshes(aMdlFileHeader, aModel)

	'			line = "--------------------"
	'			Me.WriteLogLine(2, line)
	'		Next
	'	End If
	'End Sub

	Private Sub WriteMdlSequenceDescs(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theSequenceDescs IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Sequence Descs ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theSequenceDescs.Count - 1
				Dim aSequenceDesc As SourceMdlSequenceDesc
				aSequenceDesc = aMdlFileHeader.theSequenceDescs(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Sequence Desc", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("baseHeaderOffset", aSequenceDesc.baseHeaderOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("labelOffset", aSequenceDesc.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("label", aSequenceDesc.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("activityNameOffset", aSequenceDesc.activityNameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("activityName", aSequenceDesc.theActivityName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("flags", aSequenceDesc.flags))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("activity", aSequenceDesc.activity))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("activityWeight", aSequenceDesc.activityWeight))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("eventCount", aSequenceDesc.eventCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("eventOffset", aSequenceDesc.eventOffset))

				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("bbMin.x", aSequenceDesc.bbMin.x))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("bbMin.y", aSequenceDesc.bbMin.y))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("bbMin.z", aSequenceDesc.bbMin.z))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("bbMax.x", aSequenceDesc.bbMax.x))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("bbMax.y", aSequenceDesc.bbMax.y))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("bbMax.z", aSequenceDesc.bbMax.z))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("blendCount", aSequenceDesc.blendCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("animIndexOffset", aSequenceDesc.animIndexOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("movementIndex", aSequenceDesc.movementIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("groupSize(0)", aSequenceDesc.groupSize(0)))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("groupSize(1)", aSequenceDesc.groupSize(1)))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("paramIndex(0)", aSequenceDesc.paramIndex(0)))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("paramIndex(1)", aSequenceDesc.paramIndex(1)))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("paramStart(0)", aSequenceDesc.paramStart(0)))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("paramStart(1)", aSequenceDesc.paramStart(1)))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("paramEnd(0)", aSequenceDesc.paramEnd(0)))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("paramEnd(1)", aSequenceDesc.paramEnd(1)))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("paramParent", aSequenceDesc.paramParent))

				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("fadeInTime", aSequenceDesc.fadeInTime))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("fadeOutTime", aSequenceDesc.fadeOutTime))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("localEntryNode", aSequenceDesc.localEntryNodeIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("localExitNode", aSequenceDesc.localExitNodeIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("nodeFlags", aSequenceDesc.nodeFlags))

				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("entryPhase", aSequenceDesc.entryPhase))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("exitPhase", aSequenceDesc.exitPhase))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("lastFrame", aSequenceDesc.lastFrame))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("nextSeq", aSequenceDesc.nextSeq))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("pose", aSequenceDesc.pose))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("ikRuleCount", aSequenceDesc.ikRuleCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("autoLayerCount", aSequenceDesc.autoLayerCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("autoLayerOffset", aSequenceDesc.autoLayerOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("weightListOffset", aSequenceDesc.weightOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("poseKeyOffset", aSequenceDesc.poseKeyOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("ikLockCount", aSequenceDesc.ikLockCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("ikLockOffset", aSequenceDesc.ikLockOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("keyValueOffset", aSequenceDesc.keyValueOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("keyValueSize", aSequenceDesc.keyValueSize))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("cyclePoseIndex", aSequenceDesc.cyclePoseIndex))

				For x As Integer = 0 To 6
					Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("unused(" + x.ToString() + ")", aSequenceDesc.unused(x)))
				Next

				Me.WriteMdlAnimIndexes(aMdlFileHeader, aSequenceDesc)
				Me.WriteMdlAnimBoneWeights(aMdlFileHeader, aSequenceDesc)

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlAnimIndexes(ByVal aMdlFileHeader As SourceMdlFileData, ByVal aSequenceDesc As SourceMdlSequenceDesc)
		Dim line As String

		If aSequenceDesc.theAnimDescIndexes IsNot Nothing Then
			line = "====== Animation Indexes ======"
			Me.WriteLogLine(1, line)

			For i As Integer = 0 To aSequenceDesc.theAnimDescIndexes.Count - 1
				Dim anAnimDescIndex As Short
				anAnimDescIndex = aSequenceDesc.theAnimDescIndexes(i)

				Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("Animation Index", i))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("animIndex", anAnimDescIndex))

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlAnimBoneWeights(ByVal aMdlFileHeader As SourceMdlFileData, ByVal aSequenceDesc As SourceMdlSequenceDesc)
		Dim line As String

		If aSequenceDesc.theBoneWeights IsNot Nothing Then
			line = "====== Animation Bone Weights ======"
			Me.WriteLogLine(1, line)

			For i As Integer = 0 To aSequenceDesc.theBoneWeights.Count - 1
				Dim anAnimBoneWeight As Double
				anAnimBoneWeight = aSequenceDesc.theBoneWeights(i)

				Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("Animation Bone Weight", i))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boneWeight", anAnimBoneWeight))

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlTextures(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theTextures IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Textures ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theTextures.Count - 1
				Dim aTexture As SourceMdlTexture
				aTexture = aMdlFileHeader.theTextures(i)

				'Me.WriteLogLine(1, "[index: " + i.ToString() + "]")
				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Texture", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 nameOffset", aTexture.nameOffset) + " (" + aTexture.thePathFileName + ")")
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlTexturePaths(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theTexturePaths IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Texture Paths ($cdmaterials) ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theTexturePaths.Count - 1
				Dim aTexturePath As String
				aTexturePath = aMdlFileHeader.theTexturePaths(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Texture Path", i))

				Me.WriteLogLine(1, aTexturePath)
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	'Private Sub WriteSourceSkinFamilies(ByVal aMdlFileHeader As SourceMdlFileData)
	'	Dim line As String
	'End Sub

	Private Sub WriteMdlModelGroups(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theModelGroups IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Model Groups ($includemodel) ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theModelGroups.Count - 1
				Dim aModelGroup As SourceMdlModelGroup
				aModelGroup = aMdlFileHeader.theModelGroups(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Model Group", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("00 labelOffset", aModelGroup.labelOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("label", aModelGroup.theLabel))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("04 fileNameOffset", aModelGroup.fileNameOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("fileName", aModelGroup.theFileName))
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlBodyParts(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theBodyParts IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Body Parts ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theBodyParts.Count - 1
				Dim aBodyPart As SourceMdlBodyPart
				aBodyPart = aMdlFileHeader.theBodyParts(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Body Part", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("00 nameOffset", aBodyPart.nameOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("name", aBodyPart.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("04 modelCount", aBodyPart.modelCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("08 base", aBodyPart.base))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("0C modelOffset", aBodyPart.modelOffset))

				Me.WriteMdlModels(aMdlFileHeader, aBodyPart)

				line = "------------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlModels(ByVal aMdlFileHeader As SourceMdlFileData, ByVal aBodyPart As SourceMdlBodyPart)
		Dim line As String

		If aBodyPart.theModels IsNot Nothing Then
			line = "====== Models ======"
			Me.WriteLogLine(1, line)

			For i As Integer = 0 To aBodyPart.theModels.Count - 1
				Dim aModel As SourceMdlModel
				aModel = aBodyPart.theModels(i)

				Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("Model", i))

				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", aModel.name))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("type", aModel.type))
				Me.WriteLogLine(2, DebugFormatModule.FormatSingleFloatLine("boundingRadius", aModel.boundingRadius))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("meshCount", aModel.meshCount))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("meshOffset", aModel.meshOffset))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("vertexCount", aModel.vertexCount))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("vertexOffset", aModel.vertexOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("tangentOffset", aModel.tangentOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("attachmentCount", aModel.attachmentCount))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("attachmentOffset", aModel.attachmentOffset))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("eyeballCount", aModel.eyeballCount))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("eyeballOffset", aModel.eyeballOffset))

				For x As Integer = 0 To 7
					Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused(" + x.ToString() + ")", aModel.unused(x)))
				Next

				Me.WriteMdlMeshes(aMdlFileHeader, aModel)

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlMeshes(ByVal aMdlFileHeader As SourceMdlFileData, ByVal aModel As SourceMdlModel)
		Dim line As String

		If aModel.theMeshes IsNot Nothing Then
			line = "====== Meshes ======"
			Me.WriteLogLine(2, line)

			' Total the vertexCounts.
			Dim aMeshTotalVertexCount As Integer = 0
			For k As Integer = 0 To aModel.theMeshes.Count - 1
				Dim aMesh As SourceMdlMesh
				aMesh = aModel.theMeshes(k)
				aMeshTotalVertexCount += aMesh.vertexCount
			Next
			Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("Total vertexCount", aMeshTotalVertexCount))
			line = "--------------------"
			Me.WriteLogLine(3, line)

			For i As Integer = 0 To aModel.theMeshes.Count - 1
				Dim aMesh As SourceMdlMesh
				aMesh = aModel.theMeshes(i)

				Me.WriteLogLine(3, DebugFormatModule.FormatIndexLine("Mesh", i))

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("materialIndex", aMesh.materialIndex))

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("modelOffset", aMesh.modelOffset))

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("vertexCount", aMesh.vertexCount))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("vertexIndexStart", aMesh.vertexIndexStart))

				'Me.WriteStudioVertexIds(aMesh)

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("flexCount", aMesh.flexCount))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("flexOffset", aMesh.flexOffset))

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("materialType", aMesh.materialType))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("materialParam", aMesh.materialParam))

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("id", aMesh.id))

				Me.WriteLogLine(3, DebugFormatModule.FormatSingleFloatLine("centerX", aMesh.centerX))
				Me.WriteLogLine(3, DebugFormatModule.FormatSingleFloatLine("centerY", aMesh.centerY))
				Me.WriteLogLine(3, DebugFormatModule.FormatSingleFloatLine("centerZ", aMesh.centerZ))

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("vertexData.modelVertexDataP", aMesh.vertexData.modelVertexDataP))
				For x As Integer = 0 To MAX_NUM_LODS - 1
					Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("vertexData.lodVertexCount(" + x.ToString() + ")", aMesh.vertexData.lodVertexCount(x)))
				Next

				For x As Integer = 0 To 7
					Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("unused(" + x.ToString() + ")", aMesh.unused(x)))
				Next

				Me.WriteMdlFlexes(aMdlFileHeader, aMesh)

				line = "--------------------"
				Me.WriteLogLine(3, line)
			Next
		End If
	End Sub

	'TEST: Probably invalid data
	'Private Sub WriteStudioVertexIds(ByVal aMesh As StudioMesh)
	'	Dim line As String

	'	If aMesh.theVertexIds IsNot Nothing Then
	'		line = "====== VertexIds ======"
	'		Me.WriteLogLine(3, line)

	'		For k As Integer = 0 To aMesh.theVertexIds.Count - 1
	'			Dim vertexId As Byte
	'			vertexId = aMesh.theVertexIds(k)

	'			Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("vertexId", vertexId))

	'			'line = "--------------------"
	'			'Me.WriteLogLine(4, line)
	'		Next
	'	End If
	'End Sub

	Private Sub WriteMdlFlexes(ByVal aMdlFileHeader As SourceMdlFileData, ByVal aMesh As SourceMdlMesh)
		Dim line As String

		If aMesh.theFlexes IsNot Nothing Then
			line = "====== Flexes ======"
			Me.WriteLogLine(3, line)

			For k As Integer = 0 To aMesh.theFlexes.Count - 1
				Dim aFlex As SourceMdlFlex
				aFlex = aMesh.theFlexes(k)

				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("aFlex.flexDescIndex", aFlex.flexDescIndex))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("aFlex.flexDescPartnerIndex", aFlex.flexDescPartnerIndex))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("aFlex.theVertAnims.Count", aFlex.theVertAnims.Count))

				line = "--------------------"
				Me.WriteLogLine(4, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlAttachments(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theAttachments IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Attachments ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theAttachments.Count - 1
				Dim anAttachment As SourceMdlAttachment
				anAttachment = aMdlFileHeader.theAttachments(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Attachment", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("00 nameOffset", anAttachment.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", anAttachment.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("04 flags", anAttachment.flags))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("08 localBone", anAttachment.localBoneIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("0C localM11", anAttachment.localM11))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("10 localM12", anAttachment.localM12))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("14 localM13", anAttachment.localM13))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("18 localM14", anAttachment.localM14))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("1C localM21", anAttachment.localM21))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("20 localM22", anAttachment.localM22))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("24 localM23", anAttachment.localM23))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("28 localM24", anAttachment.localM24))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("2C localM31", anAttachment.localM31))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("30 localM32", anAttachment.localM32))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("34 localM33", anAttachment.localM33))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("38 localM34", anAttachment.localM34))
				For x As Integer = 0 To 7
					Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("unused(" + x.ToString() + ")", anAttachment.unused(x)))
				Next

				line = "------------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlFlexDescs(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theFlexDescs IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Flex Descs ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theFlexDescs.Count - 1
				Dim aFlexDesc As SourceMdlFlexDesc
				aFlexDesc = aMdlFileHeader.theFlexDescs(i)

				'Me.WriteLogLine(1, "[index: " + i.ToString() + "]")
				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Flex Desc", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 nameOffset", aFlexDesc.nameOffset) + " (" + aFlexDesc.theName + ")")
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlFlexControllers(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theFlexControllers IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Flex Controllers ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theFlexControllers.Count - 1
				Dim aFlexController As SourceMdlFlexController
				aFlexController = aMdlFileHeader.theFlexControllers(i)

				'Me.WriteLogLine(1, "[index: " + i.ToString() + "]")
				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Flex Controller", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 typeOffset", aFlexController.typeOffset) + " (" + aFlexController.theType + ")")
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("04 nameOffset", aFlexController.nameOffset) + " (" + aFlexController.theName + ")")
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("08 localToGlobal", aFlexController.localToGlobal))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("0C min", aFlexController.min))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("10 max", aFlexController.max))
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlFlexRules(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theFlexRules IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Flex Rules ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theFlexRules.Count - 1
				Dim aFlexRule As SourceMdlFlexRule
				aFlexRule = aMdlFileHeader.theFlexRules(i)

				'Me.WriteLogLine(1, "[index: " + i.ToString() + "]")
				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Flex Rule", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 flexIndex", aFlexRule.flexIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("04 opCount", aFlexRule.opCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("08 opOffset", aFlexRule.opOffset))
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlIkChains(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theIkChains IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== IK Chains ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theIkChains.Count - 1
				Dim anIkChain As SourceMdlIkChain
				anIkChain = aMdlFileHeader.theIkChains(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("IK Chain", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("00 nameOffset", anIkChain.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", anIkChain.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("04 linkType", anIkChain.linkType))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("08 linkCount", anIkChain.linkCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("0C linkOffset", anIkChain.linkOffset))

				Me.WriteMdlIkLinks(aMdlFileHeader, anIkChain)

				line = "------------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlIkLinks(ByVal aMdlFileHeader As SourceMdlFileData, ByVal anIkChain As SourceMdlIkChain)
		Dim line As String

		If anIkChain.theLinks IsNot Nothing Then
			line = "====== IK Links ======"
			Me.WriteLogLine(1, line)

			For i As Integer = 0 To anIkChain.theLinks.Count - 1
				Dim anIkLink As SourceMdlIkLink
				anIkLink = anIkChain.theLinks(i)

				Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("IK Link", i))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("boneIndex", anIkLink.boneIndex))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("idealBendingDirection.x", anIkLink.idealBendingDirection.x))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("idealBendingDirection.y", anIkLink.idealBendingDirection.y))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("idealBendingDirection.z", anIkLink.idealBendingDirection.z))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("unused0.x", anIkLink.unused0.x))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("unused0.y", anIkLink.unused0.y))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("unused0.z", anIkLink.unused0.z))

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlMouths(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theMouths IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Mouths ($model { mouth }) ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theMouths.Count - 1
				Dim aMouth As SourceMdlMouth
				aMouth = aMdlFileHeader.theMouths(i)

				'Me.WriteLogLine(1, "[index: " + i.ToString() + "]")
				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Mouth", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 boneIndex", aMouth.boneIndex) + " (" + aMdlFileHeader.theBones(aMouth.boneIndex).theName + ")")
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("04 forwardX", aMouth.forward.x))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("08 forwardY", aMouth.forward.y))
				Me.WriteLogLine(1, DebugFormatModule.FormatDoubleFloatLine("0C forwardZ", aMouth.forward.z))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("10 flexDescIndex", aMouth.flexDescIndex) + " (" + aMdlFileHeader.theFlexDescs(aMouth.flexDescIndex).theName + ")")
				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlPoseParamDescs(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.thePoseParamDescs IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Pose Parameters ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.thePoseParamDescs.Count - 1
				Dim aPoseParamDesc As SourceMdlPoseParamDesc
				aPoseParamDesc = aMdlFileHeader.thePoseParamDescs(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Pose Parameter", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("00 nameOffset", aPoseParamDesc.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", aPoseParamDesc.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("04 flags", aPoseParamDesc.flags))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("04 startingValue", aPoseParamDesc.startingValue))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("08 endingValue", aPoseParamDesc.endingValue))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("0C loopingRange", aPoseParamDesc.loopingRange))

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlHitboxSets(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theHitboxSets IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Hitbox Sets ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theHitboxSets.Count - 1
				Dim aHitboxSet As SourceMdlHitboxSet
				aHitboxSet = aMdlFileHeader.theHitboxSets(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Hitbox Set", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("nameOffset", aHitboxSet.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", aHitboxSet.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("hitboxCount", aHitboxSet.hitboxCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("hitboxOffset", aHitboxSet.hitboxOffset))

				Me.WriteMdlHitboxes(aMdlFileHeader, aHitboxSet)

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlHitboxes(ByVal aMdlFileHeader As SourceMdlFileData, ByVal aHitboxSet As SourceMdlHitboxSet)
		Dim line As String

		If aHitboxSet.theHitboxes IsNot Nothing Then
			line = "====== Hitboxes ======"
			Me.WriteLogLine(1, line)

			For i As Integer = 0 To aHitboxSet.theHitboxes.Count - 1
				Dim aHitbox As SourceMdlHitbox
				aHitbox = aHitboxSet.theHitboxes(i)

				Me.WriteLogLine(2, DebugFormatModule.FormatIndexLine("Hitbox", i))

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("boneIndex", aHitbox.boneIndex))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("groupIndex", aHitbox.groupIndex))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boundingBoxMinX", aHitbox.boundingBoxMin.x))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boundingBoxMinY", aHitbox.boundingBoxMin.y))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boundingBoxMinZ", aHitbox.boundingBoxMin.z))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boundingBoxMaxX", aHitbox.boundingBoxMax.x))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boundingBoxMaxY", aHitbox.boundingBoxMax.y))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("boundingBoxMaxZ", aHitbox.boundingBoxMax.z))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("nameOffset", aHitbox.nameOffset))
				Me.WriteLogLine(3, DebugFormatModule.FormatStringLine("name", aHitbox.theName))

				For x As Integer = 0 To aHitbox.unused.Length - 1
					Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("unused(" + x.ToString() + ")", aHitbox.unused(x)))
				Next

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteMdlFlexControllerUis(ByVal aMdlFileHeader As SourceMdlFileData)
		Dim line As String

		If aMdlFileHeader.theFlexControllerUis IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Flex Controller Uis ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aMdlFileHeader.theFlexControllerUis.Count - 1
				Dim aFlexControllerUi As SourceMdlFlexControllerUi
				aFlexControllerUi = aMdlFileHeader.theFlexControllerUis(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIndexLine("Flex Controller", i))

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("nameOffset", aFlexControllerUi.nameOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatStringLine("name", aFlexControllerUi.theName))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("config0", aFlexControllerUi.config0))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("config1", aFlexControllerUi.config1))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("config2", aFlexControllerUi.config2))
				Me.WriteLogLine(1, DebugFormatModule.FormatByteWithHexLine("remapType", aFlexControllerUi.remapType))
				Me.WriteLogLine(1, DebugFormatModule.FormatByteWithHexLine("controlIsStereo", aFlexControllerUi.controlIsStereo))
				For x As Integer = 0 To aFlexControllerUi.unused.Length - 1
					Me.WriteLogLine(1, DebugFormatModule.FormatByteWithHexLine("unused(" + x.ToString() + ")", aFlexControllerUi.unused(x)))
				Next

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

#End Region

#Region "Write PHY-File Related"

	Private Sub WritePhyFileInfo(ByVal aPhyFileData As SourcePhyFileData)
		If aPhyFileData Is Nothing Then
			Exit Sub
		End If

		Me.WriteFileSeparatorLines()

		Me.WriteSourcePhyHeader(aPhyFileData)
		Me.WritePhyKeyValueDataStartOffset(aPhyFileData)
		Me.WriteSourcePhysCollsionModels(aPhyFileData)
		Me.WriteSourcePhyRagdollConstraintDescs(aPhyFileData)
		Me.WriteSourcePhyCollisionPairs(aPhyFileData)
	End Sub

	Private Sub WriteSourcePhyHeader(ByVal aPhyFileData As SourcePhyFileData)
		Dim line As String

		line = "====== PHY Header ======"
		Me.WriteLogLine(0, line)

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("00 size", aPhyFileData.size))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("04 id", aPhyFileData.id))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("08 solidCount", aPhyFileData.solidCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerAsHexLine("0C checksum", aPhyFileData.checksum))

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

	Private Sub WritePhyKeyValueDataStartOffset(ByVal aPhyFileData As SourcePhyFileData)
		Dim line As String

		line = "====== PHY Key Value Data Offset ======"
		Me.WriteLogLine(0, line)

		Me.WriteLogLine(0, DebugFormatModule.FormatLongWithHexLine("offset", aPhyFileData.theSourcePhyKeyValueDataOffset))

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

	Private Sub WriteSourcePhysCollsionModels(ByVal aPhyFileData As SourcePhyFileData)
		If aPhyFileData.theSourcePhyPhysCollisionModels IsNot Nothing Then
			Dim line As String
			Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel

			line = "====== PHY Collision Models ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aPhyFileData.theSourcePhyPhysCollisionModels.Count - 1
				aSourcePhysCollisionModel = aPhyFileData.theSourcePhyPhysCollisionModels(i)
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theIndex", aSourcePhysCollisionModel.theIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("theName", aSourcePhysCollisionModel.theName))
				If aSourcePhysCollisionModel.theParentIsValid Then
					Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("theParentName", aSourcePhysCollisionModel.theParentName))
				End If
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theMass", aSourcePhysCollisionModel.theMass))
				Me.WriteLogLine(1, DebugFormatModule.FormatStringLine("theSurfaceProp", aSourcePhysCollisionModel.theSurfaceProp))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theDamping", aSourcePhysCollisionModel.theDamping))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theRotDamping", aSourcePhysCollisionModel.theRotDamping))
				If aSourcePhysCollisionModel.theDragCoefficientIsValid Then
					Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theDragCoefficient", aSourcePhysCollisionModel.theDragCoefficient))
				End If
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theInertia", aSourcePhysCollisionModel.theInertia))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theVolume", aSourcePhysCollisionModel.theVolume))
				If aSourcePhysCollisionModel.theMassBiasIsValid Then
					Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theMassBias", aSourcePhysCollisionModel.theMassBias))
				End If

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next

			line = "========================"
			Me.WriteLogLine(0, line)
		End If
	End Sub

	Private Sub WriteSourcePhyRagdollConstraintDescs(ByVal aPhyFileData As SourcePhyFileData)
		If aPhyFileData.theSourcePhyRagdollConstraintDescs IsNot Nothing Then
			Dim line As String
			Dim aSourceRagdollConstraintDesc As SourcePhyRagdollConstraint

			line = "====== PHY Ragdoll Constraints ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aPhyFileData.theSourcePhyRagdollConstraintDescs.Count - 1
				aSourceRagdollConstraintDesc = aPhyFileData.theSourcePhyRagdollConstraintDescs.Values(i)
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("theParentIndex", aSourceRagdollConstraintDesc.theParentIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("theChildIndex", aSourceRagdollConstraintDesc.theChildIndex))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theXMin", aSourceRagdollConstraintDesc.theXMin))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theXMax", aSourceRagdollConstraintDesc.theXMax))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theXFriction", aSourceRagdollConstraintDesc.theXFriction))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theYMin", aSourceRagdollConstraintDesc.theYMin))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theYMax", aSourceRagdollConstraintDesc.theYMax))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theYFriction", aSourceRagdollConstraintDesc.theYFriction))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theZMin", aSourceRagdollConstraintDesc.theZMin))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theZMax", aSourceRagdollConstraintDesc.theZMax))
				Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("theZFriction", aSourceRagdollConstraintDesc.theZFriction))

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next

			line = "========================"
			Me.WriteLogLine(0, line)
		End If
	End Sub

	Private Sub WriteSourcePhyCollisionPairs(ByVal aPhyFileData As SourcePhyFileData)
		If aPhyFileData.theSourcePhyCollisionPairs IsNot Nothing Then
			Dim line As String
			Dim aSourcePhyCollisionPair As SourcePhyCollisionPair

			line = "====== PHY Collision Pairs ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aPhyFileData.theSourcePhyCollisionPairs.Count - 1
				aSourcePhyCollisionPair = aPhyFileData.theSourcePhyCollisionPairs(i)
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("obj0", aSourcePhyCollisionPair.obj0))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("obj1", aSourcePhyCollisionPair.obj1))

				line = "--------------------"
				Me.WriteLogLine(1, line)
			Next

			line = "========================"
			Me.WriteLogLine(0, line)
		End If
	End Sub

	Private Sub WriteVertexes(ByVal aPhyFileData As SourcePhyFileData)
		'line = "====== Vertexes ======"
		'Me.WriteLogLine(0, line)

		'For i As Integer = 0 To Me.theVertexes.Count - 1
		'	Dim aVertex As StudioVertex
		'	aVertex = Me.theVertexes(i)

		'	For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
		'		Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("boneWeights.weight(" + x.ToString() + ")", aVertex.boneWeightData.weight(x)))
		'	Next
		'	For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
		'		Me.WriteLogLine(1, DebugFormatModule.FormatByteWithHexLine("boneWeights.bone(" + x.ToString() + ")", aVertex.boneWeightData.bone(x)))
		'	Next
		'	Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("boneWeights.boneCount", aVertex.boneWeightData.boneCount))

		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("positionX", aVertex.positionX))
		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("positionY", aVertex.positionY))
		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("positionZ", aVertex.positionZ))

		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("normalX", aVertex.normalX))
		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("normalY", aVertex.normalY))
		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("normalZ", aVertex.normalZ))

		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("texCoordX", aVertex.texCoordX))
		'	Me.WriteLogLine(1, DebugFormatModule.FormatSingleFloatLine("texCoordY", aVertex.texCoordY))

		'	line = "--------------------"
		'	Me.WriteLogLine(1, line)
		'Next
	End Sub

#End Region

#Region "Write VTX-File Related"

	Private Sub WriteVtxFileInfo(ByVal aVtxFileData As SourceVtxFileData)
		Me.WriteFileSeparatorLines()

		Me.WriteVtxHeader(aVtxFileData)
		Me.WriteVtxBodyParts(aVtxFileData)
	End Sub

	Private Sub WriteVtxHeader(ByVal aVtxFileData As SourceVtxFileData)
		Dim line As String

		line = "====== VTX Header ======"
		Me.WriteLogLine(0, line)

		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("00 version", aVtxFileData.version))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("04 vertexCacheSize", aVtxFileData.vertexCacheSize))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("08 maxBonesPerStrip", aVtxFileData.maxBonesPerStrip))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("0A maxBonesPerTri", aVtxFileData.maxBonesPerTri))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("0C maxBonesPerVertex", aVtxFileData.maxBonesPerVertex))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("10 checksum", aVtxFileData.checksum))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("14 lodCount", aVtxFileData.lodCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("18 materialReplacementListOffset", aVtxFileData.materialReplacementListOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("1C bodyPartCount", aVtxFileData.bodyPartCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("20 bodyPartOffset", aVtxFileData.bodyPartOffset))

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

	Private Sub WriteVtxBodyParts(ByVal aVtxFileData As SourceVtxFileData)
		Dim line As String

		If aVtxFileData.theVtxBodyParts IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Vtx Body Parts ======"
			Me.WriteLogLine(0, line)

			For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
				Dim aBodyPart As SourceVtxBodyPart
				aBodyPart = aVtxFileData.theVtxBodyParts(i)

				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 modelCount", aBodyPart.modelCount))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("04 modelOffset", aBodyPart.modelOffset))

				Me.WriteVtxModels(aBodyPart)

				line = "------------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxModels(ByVal aBodyPart As SourceVtxBodyPart)
		Dim line As String

		If aBodyPart.theVtxModels IsNot Nothing Then
			line = "====== Vtx Models ======"
			Me.WriteLogLine(1, line)

			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("lodCount", aModel.lodCount))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("lodOffset", aModel.lodOffset))

				Me.WriteVtxModelLods(aModel)

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxModelLods(ByVal aModel As SourceVtxModel)
		Dim line As String

		If aModel.theVtxModelLods IsNot Nothing Then
			line = "====== Vtx Model LODs ======"
			Me.WriteLogLine(2, line)

			'' Total the vertexCounts.
			'Dim aMeshTotalVertexCount As Integer = 0
			'For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
			'	Dim aMesh As SourceMdlMesh
			'	aMesh = aModel.theVtxModelLods(k)
			'	aMeshTotalVertexCount += aMesh.vertexCount
			'Next
			'Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("Total vertexCount", aMeshTotalVertexCount))
			'line = "--------------------"
			'Me.WriteLogLine(3, line)

			For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
				Dim aModelLod As SourceVtxModelLod
				aModelLod = aModel.theVtxModelLods(k)

				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("meshCount", aModelLod.meshCount))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("meshOffset", aModelLod.meshOffset))
				Me.WriteLogLine(3, DebugFormatModule.FormatSingleFloatLine("switchPoint", aModelLod.switchPoint))

				Me.WriteVtxMeshes(aModelLod)

				line = "--------------------"
				Me.WriteLogLine(3, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxMeshes(ByVal aModelLod As SourceVtxModelLod)
		Dim line As String

		If aModelLod.theVtxMeshes IsNot Nothing Then
			line = "====== Vtx Meshes ======"
			Me.WriteLogLine(3, line)

			For j As Integer = 0 To aModelLod.theVtxMeshes.Count - 1
				Dim aVtxMesh As SourceVtxMesh
				aVtxMesh = aModelLod.theVtxMeshes(j)

				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("stripGroupCount", aVtxMesh.stripGroupCount))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("stripGroupOffset", aVtxMesh.stripGroupOffset))
				Me.WriteLogLine(4, DebugFormatModule.FormatByteWithHexLine("flags", aVtxMesh.flags))

				Me.WriteVtxStripGroups(aVtxMesh)

				line = "--------------------"
				Me.WriteLogLine(4, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxStripGroups(ByVal aVtxMesh As SourceVtxMesh)
		Dim line As String

		If aVtxMesh.theVtxStripGroups IsNot Nothing Then
			line = "====== Vtx Strip Groups ======"
			Me.WriteLogLine(4, line)

			For j As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
				Dim aStripGroup As SourceVtxStripGroup
				aStripGroup = aVtxMesh.theVtxStripGroups(j)

				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("vertexCount", aStripGroup.vertexCount))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("vertexOffset", aStripGroup.vertexOffset))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("indexCount", aStripGroup.indexCount))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("indexOffset", aStripGroup.indexOffset))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("stripCount", aStripGroup.stripCount))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("stripOffset", aStripGroup.stripOffset))
				Me.WriteLogLine(5, DebugFormatModule.FormatByteWithHexLine("flags", aStripGroup.flags))

				Me.WriteVtxStrips(aStripGroup)

				line = "--------------------"
				Me.WriteLogLine(5, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxStrips(ByVal aStripGroup As SourceVtxStripGroup)
		Dim line As String

		If aStripGroup.theVtxStrips IsNot Nothing Then
			line = "====== Vtx Strips ======"
			Me.WriteLogLine(5, line)

			For j As Integer = 0 To aStripGroup.theVtxStrips.Count - 1
				Dim aVtxStrip As SourceVtxStrip
				aVtxStrip = aStripGroup.theVtxStrips(j)

				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("indexCount", aVtxStrip.indexCount))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("indexMeshIndex", aVtxStrip.indexMeshIndex))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("vertexCount", aVtxStrip.vertexCount))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("vertexMeshIndex", aVtxStrip.vertexMeshIndex))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("boneCount", aVtxStrip.boneCount))
				Me.WriteLogLine(6, DebugFormatModule.FormatByteWithHexLine("flags", aVtxStrip.flags))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("boneStateChangeCount", aVtxStrip.boneStateChangeCount))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("boneStateChangeOffset", aVtxStrip.boneStateChangeOffset))

				'Me.WriteVtxBoneStateChanges(aVtxStrip)

				line = "--------------------"
				Me.WriteLogLine(6, line)
			Next
		End If
	End Sub

	'===========================================================================

	Private Sub WriteVtxFileInfoCalculatedOffsets(ByVal aVtxFileData As SourceVtxFileData)
		Me.WriteFileSeparatorLines()

		Me.WriteVtxHeader(aVtxFileData)
		Me.WriteVtxBodyPartsCalculatedOffsets(aVtxFileData)
	End Sub

	Private Sub WriteVtxBodyPartsCalculatedOffsets(ByVal aVtxFileData As SourceVtxFileData)
		Dim line As String
		Dim addressStart As Integer
		Dim addressStop As Integer

		If aVtxFileData.theVtxBodyParts IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Vtx Body Parts ======"
			Me.WriteLogLine(0, line)
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("BodyPart count", aVtxFileData.theVtxBodyParts.Count))

			addressStart = aVtxFileData.bodyPartOffset
			For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
				Dim aBodyPart As SourceVtxBodyPart
				aBodyPart = aVtxFileData.theVtxBodyParts(i)

				addressStop = addressStart + 7
				'Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 modelCount", aBodyPart.modelCount))
				'Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("04 modelOffset", aBodyPart.modelOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("BodyPart index", i))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				Me.WriteVtxModelsCalculatedOffsets(aBodyPart, addressStart + aBodyPart.modelOffset)
				addressStart += 8

				line = "------------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxModelsCalculatedOffsets(ByVal aBodyPart As SourceVtxBodyPart, ByVal addressStart As Integer)
		Dim line As String
		Dim addressStop As Integer

		If aBodyPart.theVtxModels IsNot Nothing Then
			line = "====== Vtx Models ======"
			Me.WriteLogLine(1, line)
			Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("Model count", aBodyPart.theVtxModels.Count))

			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)

				addressStop = addressStart + 7
				'Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("lodCount", aModel.lodCount))
				'Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("lodOffset", aModel.lodOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("Model index", j))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				Me.WriteVtxModelLodsCalculatedOffsets(aModel, addressStart + aModel.lodOffset)
				addressStart += 8

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxModelLodsCalculatedOffsets(ByVal aModel As SourceVtxModel, ByVal addressStart As Integer)
		Dim line As String
		Dim addressStop As Integer

		If aModel.theVtxModelLods IsNot Nothing Then
			line = "====== Vtx Model LODs ======"
			Me.WriteLogLine(2, line)
			Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("Model LOD count", aModel.theVtxModelLods.Count))

			For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
				Dim aModelLod As SourceVtxModelLod
				aModelLod = aModel.theVtxModelLods(k)

				addressStop = addressStart + 11
				'Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("meshCount", aModelLod.meshCount))
				'Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("meshOffset", aModelLod.meshOffset))
				'Me.WriteLogLine(3, DebugFormatModule.FormatSingleFloatLine("switchPoint", aModelLod.switchPoint))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("Model LOD index", k))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				Me.WriteVtxMeshesCalculatedOffsets(aModelLod, addressStart + aModelLod.meshOffset)
				addressStart += 12

				line = "--------------------"
				Me.WriteLogLine(3, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxMeshesCalculatedOffsets(ByVal aModelLod As SourceVtxModelLod, ByVal addressStart As Integer)
		Dim line As String
		Dim addressStop As Integer

		If aModelLod.theVtxMeshes IsNot Nothing Then
			line = "====== Vtx Meshes ======"
			Me.WriteLogLine(3, line)
			Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("Mesh count", aModelLod.theVtxMeshes.Count))

			For j As Integer = 0 To aModelLod.theVtxMeshes.Count - 1
				Dim aVtxMesh As SourceVtxMesh
				aVtxMesh = aModelLod.theVtxMeshes(j)

				addressStop = addressStart + 8
				'Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("stripGroupCount", aVtxMesh.stripGroupCount))
				'Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("stripGroupOffset", aVtxMesh.stripGroupOffset))
				'Me.WriteLogLine(4, DebugFormatModule.FormatByteWithHexLine("flags", aVtxMesh.flags))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("Mesh index", j))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				Me.WriteVtxStripGroupsCalculatedOffsets(aVtxMesh, addressStart + aVtxMesh.stripGroupOffset)
				addressStart += 9

				line = "--------------------"
				Me.WriteLogLine(4, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxStripGroupsCalculatedOffsets(ByVal aVtxMesh As SourceVtxMesh, ByVal addressStart As Integer)
		Dim line As String
		Dim addressStop As Integer

		If aVtxMesh.theVtxStripGroups IsNot Nothing Then
			line = "====== Vtx Strip Groups ======"
			Me.WriteLogLine(4, line)
			Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("Strip Group count", aVtxMesh.theVtxStripGroups.Count))

			For j As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
				Dim aStripGroup As SourceVtxStripGroup
				aStripGroup = aVtxMesh.theVtxStripGroups(j)

				addressStop = addressStart + 24
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("vertexCount", aStripGroup.vertexCount))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("vertexOffset", aStripGroup.vertexOffset))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("indexCount", aStripGroup.indexCount))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("indexOffset", aStripGroup.indexOffset))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("stripCount", aStripGroup.stripCount))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("stripOffset", aStripGroup.stripOffset))
				'Me.WriteLogLine(5, DebugFormatModule.FormatByteWithHexLine("flags", aStripGroup.flags))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("Strip Group index", j))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				Me.WriteVtxStripsCalculatedOffsets(aStripGroup, addressStart + aStripGroup.stripOffset)
				addressStart += 25

				line = "--------------------"
				Me.WriteLogLine(5, line)
			Next
		End If
	End Sub

	Private Sub WriteVtxStripsCalculatedOffsets(ByVal aStripGroup As SourceVtxStripGroup, ByVal addressStart As Integer)
		Dim line As String
		Dim addressStop As Integer

		If aStripGroup.theVtxStrips IsNot Nothing Then
			line = "====== Vtx Strips ======"
			Me.WriteLogLine(5, line)
			Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("Strip count", aStripGroup.theVtxStrips.Count))

			For j As Integer = 0 To aStripGroup.theVtxStrips.Count - 1
				Dim aVtxStrip As SourceVtxStrip
				aVtxStrip = aStripGroup.theVtxStrips(j)

				addressStop = addressStart + 26
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("indexCount", aVtxStrip.indexCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("indexMeshIndex", aVtxStrip.indexMeshIndex))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("vertexCount", aVtxStrip.vertexCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("vertexMeshIndex", aVtxStrip.vertexMeshIndex))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("boneCount", aVtxStrip.boneCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatByteWithHexLine("flags", aVtxStrip.flags))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("boneStateChangeCount", aVtxStrip.boneStateChangeCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("boneStateChangeOffset", aVtxStrip.boneStateChangeOffset))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("Strip index", j))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				'Me.WriteVtxBoneStateChanges(aVtxStrip)
				addressStart += 27

				line = "--------------------"
				Me.WriteLogLine(6, line)
			Next
		End If
	End Sub

	'===========================================================================

	Private Sub WriteVtxFileInfoCalculatedOffsets2(ByVal aVtxFileData As SourceVtxFileData)
		Me.WriteFileSeparatorLines()

		Me.WriteVtxHeader(aVtxFileData)
		Me.WriteVtxBodyPartsCalculatedOffsets2(aVtxFileData)
		Dim addressStart As Integer
		Dim bodyPartCount As Integer = 0
		'Dim modelCount As Integer = 0
		'Dim modelLodCount As Integer = 0
		'Dim meshCount As Integer = 0
		'Dim stripGroupCount As Integer = 0

		addressStart = aVtxFileData.bodyPartOffset
		bodyPartCount = aVtxFileData.theVtxBodyParts.Count
		addressStart += bodyPartCount * 8
		For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
			Dim aBodyPart As SourceVtxBodyPart
			aBodyPart = aVtxFileData.theVtxBodyParts(i)
			addressStart = Me.WriteVtxModelsCalculatedOffsets2(aBodyPart, addressStart)
		Next

		For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
			Dim aBodyPart As SourceVtxBodyPart
			aBodyPart = aVtxFileData.theVtxBodyParts(i)
			'modelCount += aBodyPart.theVtxModels.Count
			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)
				addressStart = Me.WriteVtxModelLodsCalculatedOffsets2(aModel, addressStart)
			Next
		Next

		For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
			Dim aBodyPart As SourceVtxBodyPart
			aBodyPart = aVtxFileData.theVtxBodyParts(i)
			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)
				'modelLodCount += aModel.theVtxModelLods.Count
				For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
					Dim aModelLod As SourceVtxModelLod
					aModelLod = aModel.theVtxModelLods(k)
					addressStart = Me.WriteVtxMeshesCalculatedOffsets2(aModelLod, addressStart)
				Next
			Next
		Next

		For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
			Dim aBodyPart As SourceVtxBodyPart
			aBodyPart = aVtxFileData.theVtxBodyParts(i)
			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)
				For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
					Dim aModelLod As SourceVtxModelLod
					aModelLod = aModel.theVtxModelLods(k)
					If aModelLod.theVtxMeshes IsNot Nothing Then
						'meshCount += aModelLod.theVtxMeshes.Count
						For m As Integer = 0 To aModelLod.theVtxMeshes.Count - 1
							Dim aVtxMesh As SourceVtxMesh
							aVtxMesh = aModelLod.theVtxMeshes(m)
							addressStart = Me.WriteVtxStripGroupsCalculatedOffsets2(aVtxMesh, addressStart)
						Next
					End If
				Next
			Next
		Next

		For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
			Dim aBodyPart As SourceVtxBodyPart
			aBodyPart = aVtxFileData.theVtxBodyParts(i)
			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)
				For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
					Dim aModelLod As SourceVtxModelLod
					aModelLod = aModel.theVtxModelLods(k)
					If aModelLod.theVtxMeshes IsNot Nothing Then
						For m As Integer = 0 To aModelLod.theVtxMeshes.Count - 1
							Dim aVtxMesh As SourceVtxMesh
							aVtxMesh = aModelLod.theVtxMeshes(m)
							If aVtxMesh.theVtxStripGroups IsNot Nothing Then
								'stripGroupCount += aVtxMesh.theVtxStripGroups.Count
								For n As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
									Dim aStripGroup As SourceVtxStripGroup
									aStripGroup = aVtxMesh.theVtxStripGroups(n)
									addressStart = Me.WriteVtxStripsCalculatedOffsets2(aStripGroup, addressStart)
								Next
							End If
						Next
					End If
				Next
			Next
		Next

	End Sub

	Private Sub WriteVtxBodyPartsCalculatedOffsets2(ByVal aVtxFileData As SourceVtxFileData)
		Dim line As String
		Dim addressStart As Integer
		Dim addressStop As Integer

		If aVtxFileData.theVtxBodyParts IsNot Nothing Then
			Me.WriteLogLine(0, "")
			Me.WriteLogLine(0, "")
			line = "====== Vtx Body Parts ======"
			Me.WriteLogLine(0, line)
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("BodyPart count", aVtxFileData.theVtxBodyParts.Count))

			addressStart = aVtxFileData.bodyPartOffset
			For i As Integer = 0 To aVtxFileData.theVtxBodyParts.Count - 1
				Dim aBodyPart As SourceVtxBodyPart
				aBodyPart = aVtxFileData.theVtxBodyParts(i)

				addressStop = addressStart + 7
				'Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("00 modelCount", aBodyPart.modelCount))
				'Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("04 modelOffset", aBodyPart.modelOffset))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("BodyPart index", i))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(1, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				addressStart += 8

				line = "------------------------"
				Me.WriteLogLine(1, line)
			Next
		End If
	End Sub

	Private Function WriteVtxModelsCalculatedOffsets2(ByVal aBodyPart As SourceVtxBodyPart, ByVal addressStart As Integer) As Integer
		Dim line As String
		Dim addressStop As Integer

		If aBodyPart.theVtxModels IsNot Nothing Then
			line = "====== Vtx Models ======"
			Me.WriteLogLine(1, line)
			Me.WriteLogLine(1, DebugFormatModule.FormatIntegerLine("Model count", aBodyPart.theVtxModels.Count))

			For j As Integer = 0 To aBodyPart.theVtxModels.Count - 1
				Dim aModel As SourceVtxModel
				aModel = aBodyPart.theVtxModels(j)

				addressStop = addressStart + 7
				'Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("lodCount", aModel.lodCount))
				'Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("lodOffset", aModel.lodOffset))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("Model index", j))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				addressStart += 8

				line = "--------------------"
				Me.WriteLogLine(2, line)
			Next
		End If

		Return addressStart
	End Function

	Private Function WriteVtxModelLodsCalculatedOffsets2(ByVal aModel As SourceVtxModel, ByVal addressStart As Integer) As Integer
		Dim line As String
		Dim addressStop As Integer

		If aModel.theVtxModelLods IsNot Nothing Then
			line = "====== Vtx Model LODs ======"
			Me.WriteLogLine(2, line)
			Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("Model LOD count", aModel.theVtxModelLods.Count))

			For k As Integer = 0 To aModel.theVtxModelLods.Count - 1
				Dim aModelLod As SourceVtxModelLod
				aModelLod = aModel.theVtxModelLods(k)

				addressStop = addressStart + 11
				'Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("meshCount", aModelLod.meshCount))
				'Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("meshOffset", aModelLod.meshOffset))
				'Me.WriteLogLine(3, DebugFormatModule.FormatSingleFloatLine("switchPoint", aModelLod.switchPoint))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("Model LOD index", k))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(3, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				addressStart += 12

				line = "--------------------"
				Me.WriteLogLine(3, line)
			Next
		End If

		Return addressStart
	End Function

	Private Function WriteVtxMeshesCalculatedOffsets2(ByVal aModelLod As SourceVtxModelLod, ByVal addressStart As Integer) As Integer
		Dim line As String
		Dim addressStop As Integer

		If aModelLod.theVtxMeshes IsNot Nothing Then
			line = "====== Vtx Meshes ======"
			Me.WriteLogLine(3, line)
			Me.WriteLogLine(3, DebugFormatModule.FormatIntegerLine("Mesh count", aModelLod.theVtxMeshes.Count))

			For j As Integer = 0 To aModelLod.theVtxMeshes.Count - 1
				Dim aVtxMesh As SourceVtxMesh
				aVtxMesh = aModelLod.theVtxMeshes(j)

				addressStop = addressStart + 8
				'Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("stripGroupCount", aVtxMesh.stripGroupCount))
				'Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("stripGroupOffset", aVtxMesh.stripGroupOffset))
				'Me.WriteLogLine(4, DebugFormatModule.FormatByteWithHexLine("flags", aVtxMesh.flags))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("Mesh index", j))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(4, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))
				Me.WriteLogLine(4, DebugFormatModule.FormatByteWithHexLine("flags", aVtxMesh.flags))

				addressStart += 9

				line = "--------------------"
				Me.WriteLogLine(4, line)
			Next
		End If

		Return addressStart
	End Function

	Private Function WriteVtxStripGroupsCalculatedOffsets2(ByVal aVtxMesh As SourceVtxMesh, ByVal addressStart As Integer) As Integer
		Dim line As String
		Dim addressStop As Integer

		If aVtxMesh.theVtxStripGroups IsNot Nothing Then
			line = "====== Vtx Strip Groups ======"
			Me.WriteLogLine(4, line)
			Me.WriteLogLine(4, DebugFormatModule.FormatIntegerLine("Strip Group count", aVtxMesh.theVtxStripGroups.Count))

			For j As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
				Dim aStripGroup As SourceVtxStripGroup
				aStripGroup = aVtxMesh.theVtxStripGroups(j)

				addressStop = addressStart + 24
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("vertexCount", aStripGroup.vertexCount))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("vertexOffset", aStripGroup.vertexOffset))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("indexCount", aStripGroup.indexCount))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("indexOffset", aStripGroup.indexOffset))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("stripCount", aStripGroup.stripCount))
				'Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("stripOffset", aStripGroup.stripOffset))
				'Me.WriteLogLine(5, DebugFormatModule.FormatByteWithHexLine("flags", aStripGroup.flags))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("Strip Group index", j))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(5, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))
				Me.WriteLogLine(5, DebugFormatModule.FormatByteWithHexLine("flags", aStripGroup.flags))

				addressStart += 25

				line = "--------------------"
				Me.WriteLogLine(5, line)
			Next
		End If

		Return addressStart
	End Function

	Private Function WriteVtxStripsCalculatedOffsets2(ByVal aStripGroup As SourceVtxStripGroup, ByVal addressStart As Integer) As Integer
		Dim line As String
		Dim addressStop As Integer

		If aStripGroup.theVtxStrips IsNot Nothing Then
			line = "====== Vtx Strips ======"
			Me.WriteLogLine(5, line)
			Me.WriteLogLine(5, DebugFormatModule.FormatIntegerLine("Strip count", aStripGroup.theVtxStrips.Count))

			For j As Integer = 0 To aStripGroup.theVtxStrips.Count - 1
				Dim aVtxStrip As SourceVtxStrip
				aVtxStrip = aStripGroup.theVtxStrips(j)

				addressStop = addressStart + 26
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("indexCount", aVtxStrip.indexCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("indexMeshIndex", aVtxStrip.indexMeshIndex))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("vertexCount", aVtxStrip.vertexCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("vertexMeshIndex", aVtxStrip.vertexMeshIndex))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("boneCount", aVtxStrip.boneCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatByteWithHexLine("flags", aVtxStrip.flags))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("boneStateChangeCount", aVtxStrip.boneStateChangeCount))
				'Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("boneStateChangeOffset", aVtxStrip.boneStateChangeOffset))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerLine("Strip index", j))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("Expected address start", addressStart))
				Me.WriteLogLine(6, DebugFormatModule.FormatIntegerWithHexLine("Expected address stop", addressStop))

				'Me.WriteVtxBoneStateChanges(aVtxStrip)
				addressStart += 27

				line = "--------------------"
				Me.WriteLogLine(6, line)
			Next
		End If

		Return addressStart
	End Function

#End Region

#Region "Write VVD-File Related"

	Private Sub WriteVvdFileInfo(ByVal aVvdFileData As SourceVvdFileData)
		Me.WriteFileSeparatorLines()

		Me.WriteVvdData(aVvdFileData)
	End Sub

	Private Sub WriteVvdData(ByVal aVvdFileData As SourceVvdFileData)
		Dim line As String

		line = "====== VVD Data ======"
		Me.WriteLogLine(0, line)

		Me.WriteLogLine(0, DebugFormatModule.FormatStringLine("00 id", aVvdFileData.id))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("04 version", aVvdFileData.version))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("08 checksum", aVvdFileData.checksum))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("0C lodCount", aVvdFileData.lodCount))
		For i As Integer = 0 To aVvdFileData.lodVertexCount.Length - 1
			Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("lodVertexCount(" + i.ToString() + ")", aVvdFileData.lodVertexCount(i)))
		Next
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerLine("fixupCount", aVvdFileData.fixupCount))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("fixupTableOffset", aVvdFileData.fixupTableOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("vertexDataOffset", aVvdFileData.vertexDataOffset))
		Me.WriteLogLine(0, DebugFormatModule.FormatIntegerWithHexLine("tangentDataOffset", aVvdFileData.tangentDataOffset))

		Me.WriteVvdFixups(aVvdFileData)
		Me.WriteVvdVertexes(aVvdFileData)

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

	Private Sub WriteVvdFixups(ByVal aVvdFileData As SourceVvdFileData)
		Dim line As String

		If aVvdFileData.theFixups IsNot Nothing Then
			Me.WriteLogLine(1, "")
			Me.WriteLogLine(1, "")
			line = "====== Vvd Fixups ======"
			Me.WriteLogLine(1, line)

			For fixupIndex As Integer = 0 To aVvdFileData.theFixups.Count - 1
				Dim aFixup As New SourceVvdFixup()
				aFixup = aVvdFileData.theFixups(fixupIndex)

				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("lodIndex", aFixup.lodIndex))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("vertexIndex", aFixup.vertexIndex))
				Me.WriteLogLine(2, DebugFormatModule.FormatIntegerLine("vertexCount", aFixup.vertexCount))

				line = "------------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

	Private Sub WriteVvdVertexes(ByVal aVvdFileData As SourceVvdFileData)
		Dim line As String

		If aVvdFileData.theVertexes IsNot Nothing Then
			Me.WriteLogLine(1, "")
			Me.WriteLogLine(1, "")
			line = "====== Vvd Vertexes ======"
			Me.WriteLogLine(1, line)

			For k As Integer = 0 To aVvdFileData.theVertexes.Count - 1
				Dim aVertex As SourceVertex
				aVertex = aVvdFileData.theVertexes(k)

				line = "VVD Vertex Index: " + k.ToString()
				Me.WriteLogLine(2, line)
				For i As Integer = 0 To aVertex.boneWeight.weight.Length - 1
					Me.WriteLogLine(2, DebugFormatModule.FormatSingleFloatLine("boneWeight.weight(" + i.ToString() + ")", aVertex.boneWeight.weight(i)))
				Next
				For j As Integer = 0 To aVertex.boneWeight.bone.Length - 1
					Me.WriteLogLine(2, DebugFormatModule.FormatByteWithHexLine("boneWeight.bone(" + j.ToString() + ")", aVertex.boneWeight.bone(j)))
				Next
				Me.WriteLogLine(2, DebugFormatModule.FormatByteWithHexLine("boneCount", aVertex.boneWeight.boneCount))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("positionX", aVertex.positionX))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("positionY", aVertex.positionY))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("positionZ", aVertex.positionZ))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("normalX", aVertex.normalX))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("normalY", aVertex.normalY))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("normalZ", aVertex.normalZ))

				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("texCoordX", aVertex.texCoordX))
				Me.WriteLogLine(2, DebugFormatModule.FormatDoubleFloatLine("texCoordY", aVertex.texCoordY))

				line = "------------------------"
				Me.WriteLogLine(2, line)
			Next
		End If
	End Sub

#End Region

#End Region

#Region "Data"

	'Private theSourceEngineModel As SourceModel_Old
	Private theOutputFileStream As StreamWriter

#End Region

End Class
