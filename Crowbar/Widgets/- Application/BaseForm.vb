Public Class BaseForm

	Public Sub New()

		' This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
		'TEST: See if this prevents the overlapping or larger text on Chinese Windows.
		Me.Font = New Font(SystemFonts.MessageBoxFont.Name, 8.25)
		'Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)

		' This call is required by the designer.
		InitializeComponent()
	End Sub

	'Protected Sub InitWidgets(ByVal container As Control)
	'	For Each c As Control In container.Controls
	'		c.Font = SystemFonts.MessageBoxFont

	'		If c.Controls.Count > 0 Then
	'			Me.InitWidgets(c)
	'		End If
	'	Next
	'End Sub

	'NOTE: Without this, when a user changes text in a textbox and then drags a file onto Crowbar, the text reverts back to the text before the change.
	'      With this, the changed text is kept.
	Protected Overrides Sub OnDeactivate(e As EventArgs)
		MyBase.OnDeactivate(e)
		Me.Validate()
	End Sub

End Class