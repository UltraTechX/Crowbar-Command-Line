Imports System.IO
Imports System.Collections.Specialized

Public Class UnpackUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		Me.CustomMenu = New ContextMenuStrip()
		Me.CustomMenu.Items.Add(Me.DeleteSearchToolStripMenuItem)
		Me.CustomMenu.Items.Add(Me.DeleteAllSearchesToolStripMenuItem)
		Me.ContextMenuStrip = Me.CustomMenu

		Me.theSearchCount = 0

		'NOTE: Try-Catch is needed so that widget will be shown in MainForm Designer without raising exception.
		Try
			Me.Init()
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.thePackageFileNames = New BindingListEx(Of PackagePathFileNameInfo)()

		Me.PackagePathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackPackagePathFolderOrFileName", False, DataSourceUpdateMode.OnValidation)

		Me.OutputPathTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputFullPath", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSubfolderTextBox.DataBindings.Add("Text", TheApp.Settings, "UnpackOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.UpdateOutputPathComboBox()
		Me.UpdateOutputPathWidgets()

		'NOTE: Adding folder icon here means it is first in the image list, which is the icon used by default 
		Dim anIcon As Bitmap
		anIcon = Win32Api.GetShellIcon("folder", Win32Api.FILE_ATTRIBUTE_DIRECTORY)
		Me.ImageList1.Images.Add("<Folder>", anIcon)
		'NOTE: The TreeView.Sorted property does not show in Intellisense or Properties window.
		Me.PackageTreeView.Sorted = True
		Me.PackageTreeView.TreeViewNodeSorter = New NodeSorter()
		Me.PackageTreeView.Nodes.Add("<root>", "<root>")

		Me.PackageListView.Columns.Add("Name", 100)
		Me.PackageListView.Columns.Add("Size (bytes)", 100)
		Me.PackageListView.Columns.Add("Type", 100)
		Me.PackageListView.Columns.Add("Extension", 100)
		Me.PackageListView.Columns.Add("Archive", 100)
		Me.theSortColumnIndex = 0
		Me.PackageListView.ListViewItemSorter = New FolderAndFileListViewItemComparer(0, Me.PackageListView.Sorting)

		'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "UnpackGameSetupSelectedIndex", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.InitUnpackerOptions()

		Me.theOutputPathOrOutputFileName = ""
		Me.theUnpackedRelativePathFileNames = New List(Of String)
		Me.UnpackedFilesComboBox.DataSource = Me.theUnpackedRelativePathFileNames

		Me.UpdateUnpackMode()
		Me.UpdateWidgets(False)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		AddHandler Me.PackagePathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		AddHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
	End Sub

	Private Sub InitUnpackerOptions()
		Me.LogFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "UnpackLogFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub Free()
		RemoveHandler Me.PackagePathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler Me.OutputPathTextBox.DataBindings("Text").Parse, AddressOf FileManager.ParsePathFileName
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted
		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

		Me.PackagePathFileNameTextBox.DataBindings.Clear()

		Me.OutputPathTextBox.DataBindings.Clear()
		Me.OutputSubfolderTextBox.DataBindings.Clear()

		Me.UnpackComboBox.DataBindings.Clear()

		Me.UnpackedFilesComboBox.DataSource = Nothing
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub RunUnpackerToGetListOfPackageContents()
		'NOTE: This is needed to handle when Crowbar is opened by double-clicking a vpk file.
		'      Every test on my dev computer without this code raised this exception: "This BackgroundWorker is currently busy and cannot run multiple tasks concurrently."
		If TheApp.Unpacker.IsBusy Then
			TheApp.Unpacker.CancelAsync()
			While TheApp.Unpacker.IsBusy
				Application.DoEvents()
			End While
		End If

		AddHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted

		'TODO: Change to using a separate "Unpacker" object; maybe create a new class specifically for listing.
		'      Want to use a separate object so the gui isn't disabled and enabled while running, 
		'      which causes a flicker and deselects the vpk file name 
		'      if selecting the vpk file name was the cause of the listing action.
		'TODO: What happens if the listing takes a long time and what should the gui look like when it does?
		'      Maybe the DataGridView should be swapped with a textbox that shows something like "Getting a list."
		TheApp.Unpacker.Run(ArchiveAction.List, Nothing)
	End Sub

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	'Private Sub VpkPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VpkPathFileNameTextBox.Validated
	'	Me.VpkPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.VpkPathFileNameTextBox.Text)
	'End Sub

	Private Sub BrowseForPackagePathFolderOrFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForPackagePathFolderOrFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the file or folder you want to unpack"
		If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
			'ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
			'	openFileWdw.InitialDirectory = TheApp.Settings.UnpackPackagePathFolderOrFileName
		Else
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
			If openFileWdw.InitialDirectory = "" Then
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		End If
		openFileWdw.FileName = "[Folder Selection]"
		openFileWdw.Filter = "Source Engine Package Files (*.vpk;*.fpx;*.gma)|*.vpk;*.fpx;*.gma|Source Engine VPK Files (*.vpk)|*.vpk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma"
		'openFileWdw.Filter = "Source Engine Package Files (*.vpk;*.fpx;*.gma;*.hfs)|*.vpk;*.fpx;*.gma;*.hfs|Source Engine VPK Files (*.vpk)|*.vpk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma|Vindictus HFS Files (*.hfs)|*.hfs"
		openFileWdw.AddExtension = True
		openFileWdw.CheckFileExists = False
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Try
				If Path.GetFileName(openFileWdw.FileName).StartsWith("[Folder Selection]") Then
					TheApp.Settings.UnpackPackagePathFolderOrFileName = FileManager.GetPath(openFileWdw.FileName)
				Else
					TheApp.Settings.UnpackPackagePathFolderOrFileName = openFileWdw.FileName
				End If
			Catch ex As IO.PathTooLongException
				MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + vbCrLf + vbCrLf + "Error message generated by Windows: " + vbCrLf + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK)
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
	End Sub

	Private Sub GotoPackageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoPackageButton.Click
		FileManager.OpenWindowsExplorer(TheApp.Settings.UnpackPackagePathFolderOrFileName)
	End Sub

	Private Sub OutputPathTextBox_Validated(sender As Object, e As EventArgs) Handles OutputPathTextBox.Validated
		Me.UpdateOutputPathTextBox()
	End Sub

	Private Sub BrowseForOutputPathButton_Click(sender As Object, e As EventArgs) Handles BrowseForOutputPathButton.Click
		Me.BrowseForOutputPath()
	End Sub

	Private Sub GotoOutputPathButton_Click(sender As Object, e As EventArgs) Handles GotoOutputPathButton.Click
		Me.GotoFolder()
	End Sub

	Private Sub UseDefaultOutputSubfolderButton_Click(sender As Object, e As EventArgs) Handles UseDefaultOutputSubfolderButton.Click
		TheApp.Settings.SetDefaultUnpackOutputSubfolderName()
	End Sub

	'TODO: Change this to detect pressing of Enter key.
	'Private Sub FindToolStripTextBox_Validated(sender As Object, e As EventArgs) Handles FindToolStripTextBox.Validated
	'	Me.FindTextInPackageFiles(FindDirection.Next)
	'End Sub

	Private Sub PackageTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles PackageTreeView.AfterSelect
		Me.SetSelectionPathText()
		Me.ShowFilesInSelectedFolder()
	End Sub

	Private Sub PackageTreeView_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles PackageTreeView.ItemDrag
		If Me.PackageTreeView.SelectedNode IsNot Nothing Then
			Me.RunUnpackerToExtractFilesInternal(ArchiveAction.ExtractToTemp, Nothing)
		End If
	End Sub

	Private Sub PackageTreeView_MouseDown(sender As Object, e As MouseEventArgs) Handles PackageTreeView.MouseDown
		Dim treeView As TreeView
		Dim clickedNode As TreeNode

		treeView = CType(sender, Windows.Forms.TreeView)
		clickedNode = treeView.GetNodeAt(e.X, e.Y)
		'If clickedNode IsNot Nothing Then
		'End If

		''NOTE: Right-clicking on a node does not select the node. Need to select the node so context menu will work.
		'If e.Button = MouseButtons.Right Then
		'	treeView.SelectedNode = clickedNode
		'End If
		'NOTE: This selects the node before dragging starts; otherwise dragging would use whatever was selected before the mousedown.
		treeView.SelectedNode = clickedNode
	End Sub

	'NOTE: This is only needed because TreeView BackColor does not automatically change when Windows Theme is switched.
	Private Sub PackageTreeView_SystemColorsChanged(sender As Object, e As EventArgs) Handles PackageTreeView.SystemColorsChanged
		Me.PackageTreeView.BackColor = SystemColors.Control
	End Sub

	Private Sub CustomMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CustomMenu.Opening
		Me.DeleteSearchToolStripMenuItem.Enabled = Me.PackageTreeView.SelectedNode IsNot Nothing AndAlso Me.PackageTreeView.SelectedNode.Text.StartsWith("<Found>")
		Me.DeleteAllSearchesToolStripMenuItem.Enabled = Me.theSearchCount > 0
	End Sub

	Private Sub DeleteSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSearchToolStripMenuItem.Click
		Me.DeleteSearch()
	End Sub

	Private Sub CopyAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAllSearchesToolStripMenuItem.Click
		Me.DeleteAllSearches()
	End Sub

	Private Sub FindToolStripTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FindToolStripTextBox.KeyPress
		If e.KeyChar = ChrW(Keys.Return) Then
			Me.FindSubstringInFileNames()
		End If
	End Sub

	Private Sub FindToolStripButton_Click(sender As Object, e As EventArgs) Handles FindToolStripButton.Click
		Me.FindSubstringInFileNames()
	End Sub

	Private Sub PackageListView_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles PackageListView.ColumnClick
		If e.Column <> Me.theSortColumnIndex Then
			Me.theSortColumnIndex = e.Column
			Me.PackageListView.Sorting = SortOrder.Ascending
		Else
			If Me.PackageListView.Sorting = SortOrder.Ascending Then
				Me.PackageListView.Sorting = SortOrder.Descending
			Else
				Me.PackageListView.Sorting = SortOrder.Ascending
			End If
		End If

		Me.PackageListView.ListViewItemSorter = New FolderAndFileListViewItemComparer(e.Column, Me.PackageListView.Sorting)
	End Sub

	Private Sub PackageListView_DoubleClick(sender As Object, e As EventArgs) Handles PackageListView.DoubleClick
		Me.OpenSelectedFolderOrFile()
	End Sub

	Private Sub PackageListView_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles PackageListView.ItemDrag
		If Me.PackageListView.SelectedItems.Count > 0 Then
			Me.RunUnpackerToExtractFiles(ArchiveAction.ExtractToTemp, Me.PackageListView.SelectedItems)
		End If
	End Sub

	Private Sub PackageListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PackageListView.SelectedIndexChanged
		Me.UpdateSelectionCounts()
	End Sub

	Private Sub UnpackOptionsUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles UnpackOptionsUseDefaultsButton.Click
		TheApp.Settings.SetDefaultUnpackOptions()
	End Sub

	Private Sub UnpackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnpackButton.Click
		If Me.PackageListView.SelectedItems.Count > 0 Then
			Me.RunUnpackerToExtractFiles(ArchiveAction.Unpack, Me.PackageListView.SelectedItems)
		Else
			Me.RunUnpackerToExtractFilesInternal(ArchiveAction.Unpack, Nothing)
		End If
	End Sub

	Private Sub SkipCurrentPackageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipCurrentPackageButton.Click
		TheApp.Unpacker.SkipCurrentPackage()
	End Sub

	Private Sub CancelUnpackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelUnpackButton.Click
		TheApp.Unpacker.CancelAsync()
	End Sub

	Private Sub UseAllInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseAllInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = TheApp.Unpacker.GetOutputPathOrOutputFileName()
	End Sub

	Private Sub UseInPreviewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInPreviewButton.Click
		TheApp.Settings.PreviewMdlPathFileName = TheApp.Unpacker.GetOutputPathFileName(Me.theUnpackedRelativePathFileNames(Me.UnpackedFilesComboBox.SelectedIndex))
		'TheApp.Settings.PreviewGameSetupSelectedIndex = TheApp.Settings.UnpackGameSetupSelectedIndex
	End Sub

	Private Sub UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = TheApp.Unpacker.GetOutputPathFileName(Me.theUnpackedRelativePathFileNames(Me.UnpackedFilesComboBox.SelectedIndex))
	End Sub

	Private Sub GotoUnpackedFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoUnpackedFileButton.Click
		Dim pathFileName As String
		pathFileName = TheApp.Unpacker.GetOutputPathFileName(Me.theUnpackedRelativePathFileNames(Me.UnpackedFilesComboBox.SelectedIndex))
		FileManager.OpenWindowsExplorer(pathFileName)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "UnpackPackagePathFolderOrFileName" Then
			Me.UpdateUnpackMode()
			Me.RunUnpackerToGetListOfPackageContents()
		ElseIf e.PropertyName = "UnpackMode" Then
			Me.RunUnpackerToGetListOfPackageContents()
		ElseIf e.PropertyName = "UnpackOutputFolderOption" Then
			Me.UpdateOutputPathWidgets()
		ElseIf e.PropertyName = "UnpackGameSetupSelectedIndex" Then
			Me.UpdateGameModelsOutputPathTextBox()
		ElseIf e.PropertyName.StartsWith("Unpack") AndAlso e.PropertyName.EndsWith("IsChecked") Then
			Me.UpdateWidgets(TheApp.Settings.UnpackerIsRunning)
		End If
	End Sub

	Private Sub ListerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.UpdateWidgets(True)
			Me.SkipCurrentPackageButton.Enabled = False
			Me.CancelUnpackButton.Text = "Cancel Listing"
			Me.PackageTreeView.Nodes(0).Nodes.Clear()
			Me.PackageTreeView.Nodes(0).Tag = Nothing
			Me.UnpackerLogTextBox.Text = ""
			'Me.theEntryIndex = -1
		ElseIf e.ProgressPercentage = 1 Then
			Me.theEntryIndex = -1
		ElseIf e.ProgressPercentage = 2 Then
			Me.theArchivePathFileName = line
		ElseIf e.ProgressPercentage = 3 Then
			Me.theEntryIndex += 1

			'Example output:
			'addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
			'addonimage.vtf crc=0xc75861f5 metadatasz=0 fnumber=32767 ofs=0x29fd sz=8400
			'addoninfo.txt crc=0xb3d2b571 metadatasz=0 fnumber=32767 ofs=0x4acd sz=1677
			'materials/models/weapons/melee/crowbar.vmt crc=0x4aaf5f0 metadatasz=0 fnumber=32767 ofs=0x515a sz=566
			'materials/models/weapons/melee/crowbar.vtf crc=0xded2e058 metadatasz=0 fnumber=32767 ofs=0x5390 sz=174920
			'materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196

			Dim fields() As String
			fields = line.Split(" "c)

			Dim pathFileName As String = fields(0)
			'NOTE: The last 5 fields should not have any spaces, but the path+filename field might.
			For fieldIndex As Integer = 1 To fields.Length - 6
				pathFileName = pathFileName + " " + fields(fieldIndex)
			Next

			Dim foldersAndFileName() As String
			foldersAndFileName = pathFileName.Split("/"c)
			Dim parentTreeNode As TreeNode = Nothing
			Dim treeNode As TreeNode = Nothing
			Dim list As List(Of PackageResourceFileNameInfo)
			If foldersAndFileName.Length = 1 Then
				treeNode = Me.PackageTreeView.Nodes(0)
			Else
				parentTreeNode = Me.PackageTreeView.Nodes(0)
				Dim resourcePathFileName As String = ""
				For nameIndex As Integer = 0 To foldersAndFileName.Length - 2
					Dim name As String
					name = foldersAndFileName(nameIndex)

					If nameIndex = 0 Then
						resourcePathFileName = name
					Else
						resourcePathFileName += Path.DirectorySeparatorChar + name
					End If

					If parentTreeNode.Nodes.ContainsKey(name) Then
						treeNode = parentTreeNode.Nodes.Item(parentTreeNode.Nodes.IndexOfKey(name))
					Else
						treeNode = parentTreeNode.Nodes.Add(name)
						treeNode.Name = name

						Dim resourceInfo As New PackageResourceFileNameInfo()
						'resourceInfo.PathFileName = name
						resourceInfo.PathFileName = resourcePathFileName
						resourceInfo.Name = name
						resourceInfo.Size = 0
						resourceInfo.Type = "Folder"
						resourceInfo.Extension = "<Folder>"
						resourceInfo.IsFolder = True
						'resourceInfo.ArchivePathFileName = Me.theArchivePathFileName
						'NOTE: Because same folder can be in multiple archives, don't bother showing which archive the folder is in. Crowbar only shows the first one added to the list.
						resourceInfo.ArchivePathFileName = ""

						If parentTreeNode.Tag Is Nothing Then
							list = New List(Of PackageResourceFileNameInfo)()
							list.Add(resourceInfo)
							parentTreeNode.Tag = list
						Else
							list = CType(parentTreeNode.Tag, List(Of PackageResourceFileNameInfo))
							list.Add(resourceInfo)
						End If
					End If
					parentTreeNode = treeNode
				Next
			End If
			If treeNode IsNot Nothing Then
				Dim fileName As String
				Dim fileExtension As String
				Dim fileExtensionWithDot As String = ""
				If pathFileName.StartsWith("<") Then
					fileName = pathFileName
					fileExtension = ""
				Else
					fileName = Path.GetFileName(pathFileName)

					fileExtension = Path.GetExtension(pathFileName)
					If Not String.IsNullOrEmpty(fileExtension) AndAlso fileExtension(0) = "."c Then
						fileExtensionWithDot = fileExtension
						fileExtension = fileExtension.Substring(1)
					End If
				End If
				Dim fileSize As Long
				fileSize = CLng(fields(fields.Length - 1).Remove(0, 3))
				Dim fileType As String
				fileType = "<type>"

				Dim resourceInfo As New PackageResourceFileNameInfo()
				resourceInfo.PathFileName = pathFileName
				resourceInfo.Name = fileName
				resourceInfo.Size = fileSize
				If pathFileName.StartsWith("<") Then
					resourceInfo.Type = "<internal data>"
				Else
					resourceInfo.Type = Win32Api.GetFileTypeDescription(fileExtensionWithDot)
				End If
				resourceInfo.Extension = fileExtension
				resourceInfo.IsFolder = False
				resourceInfo.ArchivePathFileName = Me.theArchivePathFileName
				resourceInfo.EntryIndex = Me.theEntryIndex

				If treeNode.Tag Is Nothing Then
					list = New List(Of PackageResourceFileNameInfo)()
					list.Add(resourceInfo)
					treeNode.Tag = list
				Else
					list = CType(treeNode.Tag, List(Of PackageResourceFileNameInfo))
					list.Add(resourceInfo)
				End If

				'Me.SetNodeText(treeNode, list.Count)
			End If
		ElseIf e.ProgressPercentage = 50 Then
			Me.UnpackerLogTextBox.Text = ""
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
			'NOTE: Set the textbox to show first line of text.
			Me.UnpackerLogTextBox.Select(0, 0)
		ElseIf e.ProgressPercentage = 51 Then
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
			'NOTE: Set the textbox to show first line of text.
			Me.UnpackerLogTextBox.Select(0, 0)
		ElseIf e.ProgressPercentage = 100 Then
		End If
	End Sub

	Private Sub ListerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If Not e.Cancelled Then
			Dim unpackResultInfo As UnpackerOutputInfo
			unpackResultInfo = CType(e.Result, UnpackerOutputInfo)
		End If

		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.ListerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.ListerBackgroundWorker_RunWorkerCompleted

		If Me.PackageTreeView.Nodes.Count > 0 Then
			Me.PackageTreeView.Nodes(0).Expand()
			Me.PackageTreeView.SelectedNode = Me.PackageTreeView.Nodes(0)
			Me.ShowFilesInSelectedFolder()
		End If
		Me.SetSelectionPathText()
		Me.CancelUnpackButton.Text = "Cancel Unpack"
		Me.UpdateWidgets(False)
	End Sub

	Private Sub UnpackerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		'If e.ProgressPercentage = 75 Then
		'	Me.DoDragAndDrop(CType(e.UserState, BindingListEx(Of String)))
		'	Exit Sub
		'End If

		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.UnpackerLogTextBox.Text = ""
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
			Me.theOutputPathOrOutputFileName = ""
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.UnpackerLogTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub UnpackerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If Not e.Cancelled AndAlso e.Result IsNot Nothing Then
			Dim unpackResultInfo As UnpackerOutputInfo
			unpackResultInfo = CType(e.Result, UnpackerOutputInfo)

			Me.UpdateUnpackedRelativePathFileNames(unpackResultInfo.theUnpackedRelativePathFileNames)
			Me.theOutputPathOrOutputFileName = TheApp.Unpacker.GetOutputPathOrOutputFileName()
		End If

		RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateOutputPathComboBox()
		Dim anEnumList As IList

		anEnumList = EnumHelper.ToList(GetType(UnpackOutputPathOptions))
		Me.OutputPathComboBox.DataBindings.Clear()
		Try
			'TODO: Delete this line when game addons folder option is implemented.
			anEnumList.RemoveAt(UnpackOutputPathOptions.GameAddonsFolder)

			Me.OutputPathComboBox.DisplayMember = "Value"
			Me.OutputPathComboBox.ValueMember = "Key"
			Me.OutputPathComboBox.DataSource = anEnumList
			Me.OutputPathComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "UnpackOutputFolderOption", False, DataSourceUpdateMode.OnPropertyChanged)

			Me.OutputPathComboBox.SelectedIndex = 0
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub UpdateOutputPathWidgets()
		Me.GameModelsOutputPathTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder)
		Me.OutputPathTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.OutputSubfolderTextBox.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder)
		Me.BrowseForOutputPathButton.Enabled = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.BrowseForOutputPathButton.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Enabled = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.GotoOutputPathButton.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder) OrElse (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder)
		Me.UseDefaultOutputSubfolderButton.Enabled = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder)
		Me.UseDefaultOutputSubfolderButton.Visible = (TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder)

		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder Then
			Me.UpdateGameModelsOutputPathTextBox()
		End If
	End Sub

	Private Sub UpdateGameModelsOutputPathTextBox()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim gameModelsPath As String

			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			Me.GameModelsOutputPathTextBox.Text = gameModelsPath
		End If
	End Sub

	Private Sub UpdateOutputPathTextBox()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder Then
			If String.IsNullOrEmpty(Me.OutputPathTextBox.Text) Then
				Try
					TheApp.Settings.UnpackOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			End If
		End If
	End Sub

	Private Sub BrowseForOutputPath()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder Then
			'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
			'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
			Dim outputPathWdw As New OpenFileDialog()

			outputPathWdw.Title = "Open the folder you want as Output Folder"
			outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.UnpackOutputFullPath)
			If outputPathWdw.InitialDirectory = "" Then
				If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
					outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.UnpackPackagePathFolderOrFileName)
				ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
					outputPathWdw.InitialDirectory = TheApp.Settings.UnpackPackagePathFolderOrFileName
				Else
					outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				End If
			End If
			outputPathWdw.FileName = "[Folder Selection]"
			outputPathWdw.AddExtension = False
			outputPathWdw.CheckFileExists = False
			outputPathWdw.Multiselect = False
			outputPathWdw.ValidateNames = False

			If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
				' Allow dialog window to completely disappear.
				Application.DoEvents()

				TheApp.Settings.UnpackOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
			End If
		End If
	End Sub

	Private Sub GotoFolder()
		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.GameAddonsFolder Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim gameModelsPath As String

			gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			gameModelsPath = Path.Combine(gamePath, "models")

			If FileManager.PathExistsAfterTryToCreate(gameModelsPath) Then
				FileManager.OpenWindowsExplorer(gameModelsPath)
			End If
		ElseIf TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.WorkFolder Then
			FileManager.OpenWindowsExplorer(TheApp.Settings.UnpackOutputFullPath)
		End If
	End Sub

	Private Sub UpdateWidgets(ByVal unpackerIsRunning As Boolean)
		TheApp.Settings.UnpackerIsRunning = unpackerIsRunning

		Me.UnpackComboBox.Enabled = Not unpackerIsRunning
		Me.PackagePathFileNameTextBox.Enabled = Not unpackerIsRunning
		Me.BrowseForPackagePathFolderOrFileNameButton.Enabled = Not unpackerIsRunning

		Me.OutputPathComboBox.Enabled = Not unpackerIsRunning
		Me.OutputPathTextBox.Enabled = Not unpackerIsRunning
		Me.OutputSubfolderTextBox.Enabled = Not unpackerIsRunning
		Me.BrowseForOutputPathButton.Enabled = Not unpackerIsRunning
		Me.GotoOutputPathButton.Enabled = Not unpackerIsRunning
		Me.UseDefaultOutputSubfolderButton.Enabled = Not unpackerIsRunning

		Me.SelectionGroupBox.Enabled = Not unpackerIsRunning

		Me.OptionsGroupBox.Enabled = Not unpackerIsRunning

		Me.UnpackButton.Enabled = Not unpackerIsRunning
		Me.SkipCurrentPackageButton.Enabled = unpackerIsRunning
		Me.CancelUnpackButton.Enabled = unpackerIsRunning
		Me.UseAllInDecompileButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0

		Me.UnpackedFilesComboBox.Enabled = Not unpackerIsRunning AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.UseInPreviewButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.UseInDecompileButton.Enabled = Not unpackerIsRunning AndAlso Me.theOutputPathOrOutputFileName <> "" AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
		Me.GotoUnpackedFileButton.Enabled = Not unpackerIsRunning AndAlso Me.theUnpackedRelativePathFileNames.Count > 0
	End Sub

	Private Sub UpdateUnpackedRelativePathFileNames(ByVal iUnpackedRelativePathFileNames As List(Of String))
		If iUnpackedRelativePathFileNames IsNot Nothing Then
			Me.theUnpackedRelativePathFileNames = iUnpackedRelativePathFileNames
			Me.theUnpackedRelativePathFileNames.Sort()
			'NOTE: Need to set to nothing first to force it to update.
			Me.UnpackedFilesComboBox.DataSource = Nothing
			Me.UnpackedFilesComboBox.DataSource = Me.theUnpackedRelativePathFileNames
		End If
	End Sub

	Private Sub UpdateUnpackMode()
		Dim anEnumList As IList
		Dim previousSelectedInputOption As InputOptions

		anEnumList = EnumHelper.ToList(GetType(InputOptions))
		previousSelectedInputOption = TheApp.Settings.DecompileMode
		Me.UnpackComboBox.DataBindings.Clear()
		Try
			If File.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				' Set file mode when a file is selected.
				previousSelectedInputOption = InputOptions.File
			ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				'NOTE: Remove in reverse index order.
				If Directory.GetFiles(TheApp.Settings.UnpackPackagePathFolderOrFileName, "*.vpk").Length = 0 Then
					anEnumList.RemoveAt(InputOptions.Folder)
				End If
				anEnumList.RemoveAt(InputOptions.File)
				'Else
				'	Exit Try
			End If

			Me.UnpackComboBox.DisplayMember = "Value"
			Me.UnpackComboBox.ValueMember = "Key"
			Me.UnpackComboBox.DataSource = anEnumList
			Me.UnpackComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "UnpackMode", False, DataSourceUpdateMode.OnPropertyChanged)

			If EnumHelper.Contains(previousSelectedInputOption, anEnumList) Then
				Me.UnpackComboBox.SelectedIndex = EnumHelper.IndexOf(previousSelectedInputOption, anEnumList)
			Else
				Me.UnpackComboBox.SelectedIndex = 0
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub SetSelectionPathText()
		Dim selectionPathText As String = ""
		Dim aTreeNode As TreeNode
		aTreeNode = Me.PackageTreeView.SelectedNode
		While aTreeNode IsNot Nothing
			selectionPathText = aTreeNode.Name + "/" + selectionPathText
			aTreeNode = aTreeNode.Parent
		End While
		Me.SelectionPathTextBox.Text = selectionPathText
	End Sub

	'Private Sub SetNodeText(ByVal treeNode As TreeNode, ByVal fileCount As Integer)
	'	Dim folderCountText As String
	'	If treeNode.Nodes.Count = 1 Then
	'		folderCountText = "1 folder "
	'	Else
	'		folderCountText = treeNode.Nodes.Count.ToString() + " folders "
	'	End If
	'	Dim fileCountText As String
	'	If fileCount = 1 Then
	'		fileCountText = "1 file"
	'	Else
	'		fileCountText = fileCount.ToString() + " files"
	'	End If
	'	treeNode.Text = treeNode.Name + " <" + folderCountText + fileCountText + ">"
	'End Sub

	Private Sub ShowFilesInSelectedFolder()
		Me.PackageListView.Items.Clear()

		Dim selectedTreeNode As TreeNode
		selectedTreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim list As List(Of PackageResourceFileNameInfo)
			list = CType(selectedTreeNode.Tag, List(Of PackageResourceFileNameInfo))

			Dim item As ListViewItem
			Dim anIcon As Bitmap
			For Each info As PackageResourceFileNameInfo In list
				item = New ListViewItem(info.Name)
				item.Tag = info
				If info.IsFolder Then
					Dim treeNodeForFolder As TreeNode
					Dim listForFolder As List(Of PackageResourceFileNameInfo)
					Dim itemCountText As String
					treeNodeForFolder = selectedTreeNode.Nodes.Find(info.Name, False)(0)
					listForFolder = CType(treeNodeForFolder.Tag, List(Of PackageResourceFileNameInfo))
					itemCountText = listForFolder.Count.ToString()
					If listForFolder.Count = 1 Then
						itemCountText += " item"
					Else
						itemCountText += " items"
					End If
					item.SubItems.Add(itemCountText)
				Else
					item.SubItems.Add(info.Size.ToString("N0", TheApp.InternalCultureInfo))
				End If
				item.SubItems.Add(info.Type)
				item.SubItems.Add(info.Extension)
				item.SubItems.Add(info.ArchivePathFileName)

				If Not Me.ImageList1.Images.ContainsKey(info.Extension) Then
					If info.IsFolder Then
						anIcon = Win32Api.GetShellIcon(info.Name, Win32Api.FILE_ATTRIBUTE_DIRECTORY)
					Else
						anIcon = Win32Api.GetShellIcon(info.Name)
					End If
					Me.ImageList1.Images.Add(info.Extension, anIcon)
				End If
				item.ImageKey = info.Extension

				Me.PackageListView.Items.Add(item)
			Next

			Me.PackageListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

			Me.UpdateSelectionCounts()
		End If
	End Sub

	'NOTE: Searches the folder (and its subfolders) selected in treeview.
	Private Sub FindSubstringInFileNames()
		Dim textToFind As String

		textToFind = Me.FindToolStripTextBox.Text
		If Not String.IsNullOrWhiteSpace(textToFind) Then
			Dim selectedTreeNode As TreeNode
			selectedTreeNode = Me.PackageTreeView.SelectedNode
			If selectedTreeNode Is Nothing Then
				selectedTreeNode = Me.PackageTreeView.Nodes(0)
			End If

			Dim resultsCount As Integer = 0
			Dim resultsRootTreeNodeText As String
			resultsRootTreeNodeText = "<Found> " + textToFind + " (" + resultsCount.ToString() + ")"
			Dim resultsRootTreeNode As TreeNode
			resultsRootTreeNode = New TreeNode(resultsRootTreeNodeText)
			selectedTreeNode.Nodes.Add(resultsRootTreeNode)

			Me.CreateTreeNodesThatMatchTextToFind(selectedTreeNode, textToFind, resultsRootTreeNode, resultsRootTreeNode, resultsCount)

			Me.PackageTreeView.SelectedNode = resultsRootTreeNode

			Me.theSearchCount += 1
		End If
	End Sub

	'TODO: Probably better to have textToFind, resultsRootTreeNode, and resultsCount be private class variables because they are independent of the recursion.
	Private Sub CreateTreeNodesThatMatchTextToFind(ByVal treeNodeToSearch As TreeNode, ByVal textToFind As String, ByVal resultsRootTreeNode As TreeNode, ByVal currentResultsTreeNode As TreeNode, ByRef resultsCount As Integer)
		Dim list As List(Of PackageResourceFileNameInfo)
		list = CType(treeNodeToSearch.Tag, List(Of PackageResourceFileNameInfo))

		If list IsNot Nothing Then
			Dim infoName As String
			Dim currentResultsTreeNodeList As List(Of PackageResourceFileNameInfo)
			currentResultsTreeNodeList = CType(currentResultsTreeNode.Tag, List(Of PackageResourceFileNameInfo))

			For Each info As PackageResourceFileNameInfo In list
				If Not info.IsFolder Then
					infoName = info.Name.ToLower()
					If infoName.Contains(textToFind.ToLower()) Then
						If currentResultsTreeNodeList Is Nothing Then
							currentResultsTreeNodeList = New List(Of PackageResourceFileNameInfo)()
							currentResultsTreeNode.Tag = currentResultsTreeNodeList
						End If
						currentResultsTreeNodeList.Add(info)

						resultsCount += 1
						resultsRootTreeNode.Text = "<Found> " + textToFind + " (" + resultsCount.ToString() + ")"
					End If
				End If
			Next

			Dim count As Integer
			Dim nodeClone As TreeNode
			For Each node As TreeNode In treeNodeToSearch.Nodes
				If Not node.Text.StartsWith("<Found>") Then
					If Not currentResultsTreeNode.Nodes.ContainsKey(node.Name) Then
						'NOTE: Do not use node.Clone() because it includes the cloning of child nodes.
						'nodeClone = CType(node.Clone(), TreeNode)
						nodeClone = New TreeNode(node.Text)
						nodeClone.Name = node.Name
						currentResultsTreeNode.Nodes.Add(nodeClone)
						count = resultsCount

						Me.CreateTreeNodesThatMatchTextToFind(node, textToFind, resultsRootTreeNode, nodeClone, resultsCount)

						If count = resultsCount Then
							currentResultsTreeNode.Nodes.Remove(nodeClone)
						Else
							If currentResultsTreeNodeList Is Nothing Then
								currentResultsTreeNodeList = New List(Of PackageResourceFileNameInfo)()
								currentResultsTreeNode.Tag = currentResultsTreeNodeList
							End If
							For Each info As PackageResourceFileNameInfo In list
								If info.IsFolder Then
									infoName = info.Name.ToLower()

									If infoName = nodeClone.Name.ToLower() Then
										If currentResultsTreeNodeList Is Nothing Then
											currentResultsTreeNodeList = New List(Of PackageResourceFileNameInfo)()
											currentResultsTreeNode.Tag = currentResultsTreeNodeList
										End If
										currentResultsTreeNodeList.Add(info)
									End If
								End If
							Next
						End If
					End If
				End If
			Next
		End If
	End Sub

	Private Sub UpdateSelectionCounts()
		Dim fileCount As Integer = 0
		Dim sizeTotal As Long = 0

		Dim selectedTreeNode As TreeNode
		selectedTreeNode = Me.PackageTreeView.SelectedNode
		If selectedTreeNode IsNot Nothing AndAlso selectedTreeNode.Tag IsNot Nothing Then
			Dim list As List(Of PackageResourceFileNameInfo)
			list = CType(selectedTreeNode.Tag, List(Of PackageResourceFileNameInfo))

			fileCount = list.Count

			For Each item As ListViewItem In Me.PackageListView.SelectedItems
				sizeTotal += CType(item.Tag, PackageResourceFileNameInfo).Size
			Next
		End If

		Me.FilesSelectedCountToolStripLabel.Text = Me.PackageListView.SelectedItems.Count.ToString() + "/" + fileCount.ToString()
		Me.SizeSelectedTotalToolStripLabel.Text = sizeTotal.ToString()

		'IMPORTANT: Update the toolstrip so the items are resized properly. Needed because of the 'springing' textbox.
		Me.ToolStrip1.PerformLayout()
	End Sub

	Private Function GetEntriesFromFolderEntry(ByVal resourceInfos As List(Of PackageResourceFileNameInfo), ByVal treeNode As TreeNode, ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As SortedList(Of String, List(Of Integer))
		Dim folderNode As TreeNode
		Dim folderResourceInfos As List(Of PackageResourceFileNameInfo)
		For Each resourceInfo As PackageResourceFileNameInfo In resourceInfos
			If resourceInfo.IsFolder Then
				folderNode = GetNodeFromPath(Me.PackageTreeView.Nodes(0), treeNode.FullPath + "\" + resourceInfo.Name)
				folderResourceInfos = CType(folderNode.Tag, List(Of PackageResourceFileNameInfo))
				archivePathFileNameToEntryIndexMap = Me.GetEntriesFromFolderEntry(folderResourceInfos, folderNode, archivePathFileNameToEntryIndexMap)
			Else
				Dim archivePathFileName As String
				Dim archiveEntryIndex As Integer
				archivePathFileName = resourceInfo.ArchivePathFileName
				archiveEntryIndex = resourceInfo.EntryIndex
				Dim archiveEntryIndexes As List(Of Integer)
				If archivePathFileNameToEntryIndexMap.Keys.Contains(archivePathFileName) Then
					archiveEntryIndexes = archivePathFileNameToEntryIndexMap(archivePathFileName)
					archiveEntryIndexes.Add(archiveEntryIndex)
				Else
					archiveEntryIndexes = New List(Of Integer)()
					archiveEntryIndexes.Add(archiveEntryIndex)
					archivePathFileNameToEntryIndexMap.Add(archivePathFileName, archiveEntryIndexes)
				End If
			End If
		Next
		Return archivePathFileNameToEntryIndexMap
	End Function

	Private Function GetNodeFromPath(node As TreeNode, path As String) As TreeNode
		Dim foundNode As TreeNode = Nothing
		If node.FullPath = path Then
			Return node
		End If
		For Each tn As TreeNode In node.Nodes
			If tn.FullPath = path Then
				Return tn
			ElseIf tn.Nodes.Count > 0 Then
				foundNode = GetNodeFromPath(tn, path)
			End If
			If foundNode IsNot Nothing Then
				Return foundNode
			End If
		Next
		Return Nothing
	End Function

	Private Sub OpenSelectedFolderOrFile()
		Dim selectedItem As ListViewItem
		selectedItem = Me.PackageListView.SelectedItems(0)

		Dim resourceInfo As PackageResourceFileNameInfo
		resourceInfo = CType(selectedItem.Tag, PackageResourceFileNameInfo)

		If resourceInfo.IsFolder Then
			Dim selectedTreeNode As TreeNode
			selectedTreeNode = Me.PackageTreeView.SelectedNode
			Me.PackageTreeView.SelectedNode = selectedTreeNode.Nodes(resourceInfo.Name)
		Else
			' Extract the file to the user's temp folder and open it as if it were opened in File Explorer.
			Dim archivePathFileNameToEntryIndexMap As New SortedList(Of String, List(Of Integer))()
			Dim archiveEntryIndexes As New List(Of Integer)()
			archiveEntryIndexes.Add(resourceInfo.EntryIndex)
			archivePathFileNameToEntryIndexMap.Add(resourceInfo.ArchivePathFileName, archiveEntryIndexes)
			TheApp.Unpacker.Run(ArchiveAction.ExtractAndOpen, archivePathFileNameToEntryIndexMap)
		End If
	End Sub

	Private Sub RunUnpackerToExtractFiles(ByVal unpackerAction As ArchiveAction, ByVal selectedItems As ListView.SelectedListViewItemCollection)
		Dim selectedResourceInfo As PackageResourceFileNameInfo
		Dim selectedResourceInfos As New List(Of PackageResourceFileNameInfo)
		For Each selectedItem As ListViewItem In selectedItems
			selectedResourceInfo = CType(selectedItem.Tag, PackageResourceFileNameInfo)
			selectedResourceInfos.Add(selectedResourceInfo)
		Next

		Me.RunUnpackerToExtractFilesInternal(unpackerAction, selectedResourceInfos)
	End Sub

	Private Sub RunUnpackerToExtractFilesInternal(ByVal unpackerAction As ArchiveAction, ByVal selectedResourceInfos As List(Of PackageResourceFileNameInfo))
		Dim selectedPackageInternalPathFileNames As New List(Of String)()
		Dim archivePathFileNameToEntryIndexMap As New SortedList(Of String, List(Of Integer))()
		Dim selectedNode As TreeNode

		selectedNode = Me.PackageTreeView.SelectedNode
		If selectedNode Is Nothing Then
			selectedNode = Me.PackageTreeView.Nodes(0)
		End If

		If selectedResourceInfos Is Nothing Then
			selectedResourceInfos = CType(selectedNode.Tag, List(Of PackageResourceFileNameInfo))
		End If

		archivePathFileNameToEntryIndexMap = Me.GetEntriesFromFolderEntry(selectedResourceInfos, selectedNode, archivePathFileNameToEntryIndexMap)

		AddHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

		If unpackerAction = ArchiveAction.ExtractToTemp Then
			For Each resourceInfo As PackageResourceFileNameInfo In selectedResourceInfos
				selectedPackageInternalPathFileNames.Add(resourceInfo.PathFileName)
			Next

			TheApp.Unpacker.RunSynchronous(unpackerAction, archivePathFileNameToEntryIndexMap)

			Dim tempPathFileNames As List(Of String) = Nothing
			tempPathFileNames = TheApp.Unpacker.GetTempPathsAndPathFileNames(selectedPackageInternalPathFileNames)

			Me.DoDragAndDrop(tempPathFileNames)
		Else
			TheApp.Unpacker.Run(unpackerAction, archivePathFileNameToEntryIndexMap)
		End If
	End Sub

	Private Sub DoDragAndDrop(ByVal iUnpackedRelativePathFileNames As List(Of String))
		If iUnpackedRelativePathFileNames.Count > 0 Then
			Dim pathFileNameCollection As New StringCollection()
			For Each pathFileName As String In iUnpackedRelativePathFileNames
				pathFileNameCollection.Add(pathFileName)
			Next

			Dim dragDropDataObject As DataObject
			dragDropDataObject = New DataObject()

			dragDropDataObject.SetFileDropList(pathFileNameCollection)

			Dim result As DragDropEffects
			result = Me.PackageListView.DoDragDrop(dragDropDataObject, DragDropEffects.Move)
			TheApp.Unpacker.DeleteTempUnpackFolder()

			RemoveHandler TheApp.Unpacker.ProgressChanged, AddressOf Me.UnpackerBackgroundWorker_ProgressChanged
			RemoveHandler TheApp.Unpacker.RunWorkerCompleted, AddressOf Me.UnpackerBackgroundWorker_RunWorkerCompleted

			Me.UpdateWidgets(False)
		End If
	End Sub

	Private Sub DeleteSearch()
		Me.PackageTreeView.SelectedNode.Parent.Nodes.Remove(Me.PackageTreeView.SelectedNode)
		Me.theSearchCount -= 1
	End Sub

	Private Sub DeleteAllSearches()
		Me.RecursivelyDeleteSearchNodes(Me.PackageTreeView.Nodes)
		Me.theSearchCount = 0
	End Sub

	Private Sub RecursivelyDeleteSearchNodes(ByVal nodes As TreeNodeCollection)
		Dim aNode As TreeNode
		For i As Integer = nodes.Count - 1 To 0 Step -1
			aNode = nodes(i)
			If aNode.Text.StartsWith("<Found>") Then
				nodes.Remove(aNode)
			Else
				Me.RecursivelyDeleteSearchNodes(aNode.Nodes)
			End If
		Next
	End Sub

#End Region

#Region "Data"

	Private WithEvents CustomMenu As ContextMenuStrip

	Private WithEvents DeleteSearchToolStripMenuItem As New ToolStripMenuItem("Delete search")
	Private WithEvents DeleteAllSearchesToolStripMenuItem As New ToolStripMenuItem("Delete all searches")

	Private thePackageFileNames As BindingListEx(Of PackagePathFileNameInfo)

	Private theUnpackedRelativePathFileNames As List(Of String)
	Private theOutputPathOrOutputFileName As String

	Private theSortColumnIndex As Integer

	Private thePackEntries As List(Of Integer)
	Private theGivenHardLinkFileName As String

	Private theArchivePathFileName As String
	Private theEntryIndex As Integer

	Private theSearchCount As Integer

#End Region

End Class
