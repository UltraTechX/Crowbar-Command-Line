<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PatchUserControl
	Inherits BaseUserControl

	''UserControl overrides dispose to clean up the component list.
	'<System.Diagnostics.DebuggerNonUserCode()> _
	'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	'	Try
	'		If disposing AndAlso components IsNot Nothing Then
	'			components.Dispose()
	'		End If
	'	Finally
	'		MyBase.Dispose(disposing)
	'	End Try
	'End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.ViewButton = New System.Windows.Forms.Button()
		Me.MdlPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForMdlFileButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.GotoMdlFileButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.MessageTextBox = New Crowbar.TextBoxEx()
		Me.DecompileComboBox = New System.Windows.Forms.ComboBox()
		Me.CancelDecompileButton = New System.Windows.Forms.Button()
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button()
		Me.TextBoxEx1 = New Crowbar.TextBoxEx()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.TextBoxEx2 = New Crowbar.TextBoxEx()
		Me.CheckBox1 = New System.Windows.Forms.CheckBox()
		Me.CheckBox2 = New System.Windows.Forms.CheckBox()
		Me.CheckBox3 = New System.Windows.Forms.CheckBox()
		Me.DataGridView1 = New System.Windows.Forms.DataGridView()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.Button4 = New System.Windows.Forms.Button()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.DataGridView2 = New System.Windows.Forms.DataGridView()
		Me.Button5 = New System.Windows.Forms.Button()
		Me.Button6 = New System.Windows.Forms.Button()
		Me.Button7 = New System.Windows.Forms.Button()
		Me.Button8 = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.TextBoxEx3 = New Crowbar.TextBoxEx()
		Me.GroupBox4 = New System.Windows.Forms.GroupBox()
		Me.DataGridView3 = New System.Windows.Forms.DataGridView()
		Me.Button9 = New System.Windows.Forms.Button()
		Me.Button11 = New System.Windows.Forms.Button()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.TextBoxEx4 = New Crowbar.TextBoxEx()
		Me.TextBoxEx5 = New Crowbar.TextBoxEx()
		Me.TextBoxEx6 = New Crowbar.TextBoxEx()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.GroupBox5 = New System.Windows.Forms.GroupBox()
		Me.DataGridView4 = New System.Windows.Forms.DataGridView()
		Me.Panel2.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox4.SuspendLayout()
		CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox5.SuspendLayout()
		CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'ViewButton
		'
		Me.ViewButton.Enabled = False
		Me.ViewButton.Location = New System.Drawing.Point(0, 2)
		Me.ViewButton.Name = "ViewButton"
		Me.ViewButton.Size = New System.Drawing.Size(50, 23)
		Me.ViewButton.TabIndex = 8
		Me.ViewButton.Text = "Patch"
		Me.ViewButton.UseVisualStyleBackColor = True
		'
		'MdlPathFileNameTextBox
		'
		Me.MdlPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileNameTextBox.Location = New System.Drawing.Point(209, 5)
		Me.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox"
		Me.MdlPathFileNameTextBox.Size = New System.Drawing.Size(441, 21)
		Me.MdlPathFileNameTextBox.TabIndex = 1
		'
		'BrowseForMdlFileButton
		'
		Me.BrowseForMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlFileButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForMdlFileButton.Name = "BrowseForMdlFileButton"
		Me.BrowseForMdlFileButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForMdlFileButton.TabIndex = 2
		Me.BrowseForMdlFileButton.Text = "Browse..."
		Me.BrowseForMdlFileButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(58, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL input:"
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.DecompileComboBox)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.MdlPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.BrowseForMdlFileButton)
		Me.Panel2.Controls.Add(Me.GotoMdlFileButton)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.TabIndex = 8
		'
		'GotoMdlFileButton
		'
		Me.GotoMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoMdlFileButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoMdlFileButton.Name = "GotoMdlFileButton"
		Me.GotoMdlFileButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoMdlFileButton.TabIndex = 3
		Me.GotoMdlFileButton.Text = "Goto"
		Me.GotoMdlFileButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 32)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
		Me.SplitContainer1.Panel1MinSize = 90
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.CancelDecompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.SkipCurrentModelButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.ViewButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.MessageTextBox)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(770, 501)
		Me.SplitContainer1.SplitterDistance = 384
		Me.SplitContainer1.TabIndex = 13
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.GroupBox5)
		Me.GroupBox1.Controls.Add(Me.Label9)
		Me.GroupBox1.Controls.Add(Me.TextBoxEx6)
		Me.GroupBox1.Controls.Add(Me.TextBoxEx5)
		Me.GroupBox1.Controls.Add(Me.TextBoxEx4)
		Me.GroupBox1.Controls.Add(Me.Label8)
		Me.GroupBox1.Controls.Add(Me.GroupBox4)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.TextBoxEx3)
		Me.GroupBox1.Controls.Add(Me.GroupBox3)
		Me.GroupBox1.Controls.Add(Me.GroupBox2)
		Me.GroupBox1.Controls.Add(Me.CheckBox3)
		Me.GroupBox1.Controls.Add(Me.CheckBox2)
		Me.GroupBox1.Controls.Add(Me.CheckBox1)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.TextBoxEx2)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.TextBoxEx1)
		Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(770, 384)
		Me.GroupBox1.TabIndex = 4
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Patchable Values"
		'
		'MessageTextBox
		'
		Me.MessageTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MessageTextBox.Location = New System.Drawing.Point(0, 31)
		Me.MessageTextBox.Multiline = True
		Me.MessageTextBox.Name = "MessageTextBox"
		Me.MessageTextBox.ReadOnly = True
		Me.MessageTextBox.Size = New System.Drawing.Size(770, 81)
		Me.MessageTextBox.TabIndex = 12
		'
		'DecompileComboBox
		'
		Me.DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.DecompileComboBox.FormattingEnabled = True
		Me.DecompileComboBox.Location = New System.Drawing.Point(63, 4)
		Me.DecompileComboBox.Name = "DecompileComboBox"
		Me.DecompileComboBox.Size = New System.Drawing.Size(140, 21)
		Me.DecompileComboBox.TabIndex = 14
		'
		'CancelDecompileButton
		'
		Me.CancelDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelDecompileButton.Enabled = False
		Me.CancelDecompileButton.Location = New System.Drawing.Point(182, 2)
		Me.CancelDecompileButton.Name = "CancelDecompileButton"
		Me.CancelDecompileButton.Size = New System.Drawing.Size(80, 23)
		Me.CancelDecompileButton.TabIndex = 14
		Me.CancelDecompileButton.Text = "Cancel Patch"
		Me.CancelDecompileButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(56, 2)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentModelButton.TabIndex = 13
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'TextBoxEx1
		'
		Me.TextBoxEx1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBoxEx1.Location = New System.Drawing.Point(122, 14)
		Me.TextBoxEx1.Name = "TextBoxEx1"
		Me.TextBoxEx1.Size = New System.Drawing.Size(441, 21)
		Me.TextBoxEx1.TabIndex = 2
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(6, 17)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(101, 13)
		Me.Label2.TabIndex = 3
		Me.Label2.Text = "Internal MDL name:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(569, 17)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(56, 13)
		Me.Label3.TabIndex = 4
		Me.Label3.Text = "(64 chars)"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(698, 44)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(66, 13)
		Me.Label4.TabIndex = 7
		Me.Label4.Text = "(any length)"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(6, 44)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(110, 13)
		Me.Label5.TabIndex = 6
		Me.Label5.Text = "Internal MDL name 2:"
		'
		'TextBoxEx2
		'
		Me.TextBoxEx2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBoxEx2.Location = New System.Drawing.Point(122, 41)
		Me.TextBoxEx2.Name = "TextBoxEx2"
		Me.TextBoxEx2.Size = New System.Drawing.Size(570, 21)
		Me.TextBoxEx2.TabIndex = 5
		'
		'CheckBox1
		'
		Me.CheckBox1.AutoSize = True
		Me.CheckBox1.Location = New System.Drawing.Point(320, 97)
		Me.CheckBox1.Name = "CheckBox1"
		Me.CheckBox1.Size = New System.Drawing.Size(95, 17)
		Me.CheckBox1.TabIndex = 8
		Me.CheckBox1.Text = "Ambient Boost"
		Me.CheckBox1.UseVisualStyleBackColor = True
		'
		'CheckBox2
		'
		Me.CheckBox2.AutoSize = True
		Me.CheckBox2.Location = New System.Drawing.Point(434, 97)
		Me.CheckBox2.Name = "CheckBox2"
		Me.CheckBox2.Size = New System.Drawing.Size(64, 17)
		Me.CheckBox2.TabIndex = 9
		Me.CheckBox2.Text = "Opaque"
		Me.CheckBox2.UseVisualStyleBackColor = True
		'
		'CheckBox3
		'
		Me.CheckBox3.AutoSize = True
		Me.CheckBox3.Location = New System.Drawing.Point(515, 97)
		Me.CheckBox3.Name = "CheckBox3"
		Me.CheckBox3.Size = New System.Drawing.Size(98, 17)
		Me.CheckBox3.TabIndex = 10
		Me.CheckBox3.Text = "Mostly Opaque"
		Me.CheckBox3.UseVisualStyleBackColor = True
		'
		'DataGridView1
		'
		Me.DataGridView1.AllowUserToAddRows = False
		Me.DataGridView1.AllowUserToDeleteRows = False
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Location = New System.Drawing.Point(6, 13)
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.Size = New System.Drawing.Size(240, 81)
		Me.DataGridView1.TabIndex = 11
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(252, 13)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(56, 23)
		Me.Button1.TabIndex = 13
		Me.Button1.Text = "Add"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(252, 42)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(56, 23)
		Me.Button2.TabIndex = 14
		Me.Button2.Text = "Delete"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'Button3
		'
		Me.Button3.Location = New System.Drawing.Point(252, 71)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(25, 23)
		Me.Button3.TabIndex = 15
		Me.Button3.Text = "Up"
		Me.Button3.UseVisualStyleBackColor = True
		'
		'Button4
		'
		Me.Button4.Location = New System.Drawing.Point(283, 71)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(25, 23)
		Me.Button4.TabIndex = 16
		Me.Button4.Text = "Dn"
		Me.Button4.UseVisualStyleBackColor = True
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.DataGridView1)
		Me.GroupBox2.Controls.Add(Me.Button4)
		Me.GroupBox2.Controls.Add(Me.Button1)
		Me.GroupBox2.Controls.Add(Me.Button3)
		Me.GroupBox2.Controls.Add(Me.Button2)
		Me.GroupBox2.Location = New System.Drawing.Point(6, 137)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(311, 100)
		Me.GroupBox2.TabIndex = 17
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "CD Materials"
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.DataGridView2)
		Me.GroupBox3.Controls.Add(Me.Button5)
		Me.GroupBox3.Controls.Add(Me.Button6)
		Me.GroupBox3.Controls.Add(Me.Button7)
		Me.GroupBox3.Controls.Add(Me.Button8)
		Me.GroupBox3.Location = New System.Drawing.Point(6, 243)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(311, 100)
		Me.GroupBox3.TabIndex = 18
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Include Models"
		'
		'DataGridView2
		'
		Me.DataGridView2.AllowUserToAddRows = False
		Me.DataGridView2.AllowUserToDeleteRows = False
		Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView2.Location = New System.Drawing.Point(6, 13)
		Me.DataGridView2.Name = "DataGridView2"
		Me.DataGridView2.Size = New System.Drawing.Size(240, 81)
		Me.DataGridView2.TabIndex = 11
		'
		'Button5
		'
		Me.Button5.Location = New System.Drawing.Point(283, 71)
		Me.Button5.Name = "Button5"
		Me.Button5.Size = New System.Drawing.Size(25, 23)
		Me.Button5.TabIndex = 16
		Me.Button5.Text = "Dn"
		Me.Button5.UseVisualStyleBackColor = True
		'
		'Button6
		'
		Me.Button6.Location = New System.Drawing.Point(252, 13)
		Me.Button6.Name = "Button6"
		Me.Button6.Size = New System.Drawing.Size(56, 23)
		Me.Button6.TabIndex = 13
		Me.Button6.Text = "Add"
		Me.Button6.UseVisualStyleBackColor = True
		'
		'Button7
		'
		Me.Button7.Location = New System.Drawing.Point(252, 71)
		Me.Button7.Name = "Button7"
		Me.Button7.Size = New System.Drawing.Size(25, 23)
		Me.Button7.TabIndex = 15
		Me.Button7.Text = "Up"
		Me.Button7.UseVisualStyleBackColor = True
		'
		'Button8
		'
		Me.Button8.Location = New System.Drawing.Point(252, 42)
		Me.Button8.Name = "Button8"
		Me.Button8.Size = New System.Drawing.Size(56, 23)
		Me.Button8.TabIndex = 14
		Me.Button8.Text = "Delete"
		Me.Button8.UseVisualStyleBackColor = True
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(698, 71)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(66, 13)
		Me.Label6.TabIndex = 21
		Me.Label6.Text = "(any length)"
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(6, 71)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(75, 13)
		Me.Label7.TabIndex = 20
		Me.Label7.Text = "ANI file name:"
		'
		'TextBoxEx3
		'
		Me.TextBoxEx3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBoxEx3.Location = New System.Drawing.Point(122, 68)
		Me.TextBoxEx3.Name = "TextBoxEx3"
		Me.TextBoxEx3.Size = New System.Drawing.Size(570, 21)
		Me.TextBoxEx3.TabIndex = 19
		'
		'GroupBox4
		'
		Me.GroupBox4.Controls.Add(Me.DataGridView3)
		Me.GroupBox4.Controls.Add(Me.Button9)
		Me.GroupBox4.Controls.Add(Me.Button11)
		Me.GroupBox4.Location = New System.Drawing.Point(323, 243)
		Me.GroupBox4.Name = "GroupBox4"
		Me.GroupBox4.Size = New System.Drawing.Size(280, 100)
		Me.GroupBox4.TabIndex = 18
		Me.GroupBox4.TabStop = False
		Me.GroupBox4.Text = "Body Group Names"
		'
		'DataGridView3
		'
		Me.DataGridView3.AllowUserToAddRows = False
		Me.DataGridView3.AllowUserToDeleteRows = False
		Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView3.Location = New System.Drawing.Point(6, 13)
		Me.DataGridView3.Name = "DataGridView3"
		Me.DataGridView3.Size = New System.Drawing.Size(240, 81)
		Me.DataGridView3.TabIndex = 11
		'
		'Button9
		'
		Me.Button9.Location = New System.Drawing.Point(252, 42)
		Me.Button9.Name = "Button9"
		Me.Button9.Size = New System.Drawing.Size(25, 23)
		Me.Button9.TabIndex = 16
		Me.Button9.Text = "Dn"
		Me.Button9.UseVisualStyleBackColor = True
		'
		'Button11
		'
		Me.Button11.Location = New System.Drawing.Point(252, 13)
		Me.Button11.Name = "Button11"
		Me.Button11.Size = New System.Drawing.Size(25, 23)
		Me.Button11.TabIndex = 15
		Me.Button11.Text = "Up"
		Me.Button11.UseVisualStyleBackColor = True
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(6, 98)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(73, 13)
		Me.Label8.TabIndex = 22
		Me.Label8.Text = "Illum Position:"
		'
		'TextBoxEx4
		'
		Me.TextBoxEx4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBoxEx4.Location = New System.Drawing.Point(85, 95)
		Me.TextBoxEx4.Name = "TextBoxEx4"
		Me.TextBoxEx4.Size = New System.Drawing.Size(40, 21)
		Me.TextBoxEx4.TabIndex = 23
		'
		'TextBoxEx5
		'
		Me.TextBoxEx5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBoxEx5.Location = New System.Drawing.Point(131, 95)
		Me.TextBoxEx5.Name = "TextBoxEx5"
		Me.TextBoxEx5.Size = New System.Drawing.Size(40, 21)
		Me.TextBoxEx5.TabIndex = 24
		'
		'TextBoxEx6
		'
		Me.TextBoxEx6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBoxEx6.Location = New System.Drawing.Point(177, 95)
		Me.TextBoxEx6.Name = "TextBoxEx6"
		Me.TextBoxEx6.Size = New System.Drawing.Size(40, 21)
		Me.TextBoxEx6.TabIndex = 25
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Location = New System.Drawing.Point(223, 98)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(39, 13)
		Me.Label9.TabIndex = 26
		Me.Label9.Text = "(X Y Z)"
		'
		'GroupBox5
		'
		Me.GroupBox5.Controls.Add(Me.DataGridView4)
		Me.GroupBox5.Location = New System.Drawing.Point(323, 137)
		Me.GroupBox5.Name = "GroupBox5"
		Me.GroupBox5.Size = New System.Drawing.Size(280, 100)
		Me.GroupBox5.TabIndex = 19
		Me.GroupBox5.TabStop = False
		Me.GroupBox5.Text = "Hitboxes"
		'
		'DataGridView4
		'
		Me.DataGridView4.AllowUserToAddRows = False
		Me.DataGridView4.AllowUserToDeleteRows = False
		Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView4.Location = New System.Drawing.Point(6, 13)
		Me.DataGridView4.Name = "DataGridView4"
		Me.DataGridView4.Size = New System.Drawing.Size(271, 81)
		Me.DataGridView4.TabIndex = 11
		'
		'PatchUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "PatchUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.Panel2.PerformLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox3.ResumeLayout(False)
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox4.ResumeLayout(False)
		CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox5.ResumeLayout(False)
		CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ViewButton As System.Windows.Forms.Button
	Friend WithEvents MdlPathFileNameTextBox As TextBoxEx
	Friend WithEvents BrowseForMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents GotoMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents MessageTextBox As Crowbar.TextBoxEx
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents DecompileComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents CancelDecompileButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentModelButton As System.Windows.Forms.Button
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents TextBoxEx2 As Crowbar.TextBoxEx
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents TextBoxEx1 As Crowbar.TextBoxEx
	Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
	Friend WithEvents Button4 As System.Windows.Forms.Button
	Friend WithEvents Button3 As System.Windows.Forms.Button
	Friend WithEvents Button2 As System.Windows.Forms.Button
	Friend WithEvents Button1 As System.Windows.Forms.Button
	Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
	Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
	Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
	Friend WithEvents Button5 As System.Windows.Forms.Button
	Friend WithEvents Button6 As System.Windows.Forms.Button
	Friend WithEvents Button7 As System.Windows.Forms.Button
	Friend WithEvents Button8 As System.Windows.Forms.Button
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents TextBoxEx3 As Crowbar.TextBoxEx
	Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
	Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
	Friend WithEvents Button9 As System.Windows.Forms.Button
	Friend WithEvents Button11 As System.Windows.Forms.Button
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents TextBoxEx6 As Crowbar.TextBoxEx
	Friend WithEvents TextBoxEx5 As Crowbar.TextBoxEx
	Friend WithEvents TextBoxEx4 As Crowbar.TextBoxEx
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
	Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView

End Class
