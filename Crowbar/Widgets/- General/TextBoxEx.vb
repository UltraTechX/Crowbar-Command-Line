Public Class TextBoxEx
	Inherits TextBox

#Region "Creation and Destruction"

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	'Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
	'	If e.Control AndAlso e.KeyCode = Keys.A Then
	'		Me.SelectAll()
	'	End If
	'	MyBase.OnKeyDown(e)
	'End Sub

	'Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
	'	If e.KeyChar = ChrW(Keys.Return) Then
	'		Try
	'			' Cause validation, which means Validating and Validated events are raised.
	'			Me.FindForm().Validate()
	'			If TypeOf Me.Parent Is ContainerControl Then
	'				CType(Me.Parent, ContainerControl).Validate()
	'			End If
	'			'NOTE: Prevent annoying beep when textbox is single line.
	'			e.Handled = True
	'		Catch ex As Exception
	'			Dim debug As Integer = 4242
	'		End Try
	'	End If
	'	MyBase.OnKeyPress(e)
	'End Sub

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = ChrW(Keys.Return) Then
			Try
				' Cause validation, which means Validating and Validated events are raised.
				Me.FindForm().Validate()
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If
		MyBase.OnKeyPress(e)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
