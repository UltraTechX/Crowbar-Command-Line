Public NotInheritable Class EnumHelper

	Public Shared Function GetDescription(ByVal value As System.Enum) As String
		If value Is Nothing Then
			Throw New ArgumentNullException("value")
		End If
		Dim description As String = value.ToString()
		Dim fieldInfo As Reflection.FieldInfo = value.GetType().GetField(description)
		Dim attributes As System.ComponentModel.DescriptionAttribute() = CType(fieldInfo.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())
		If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
			description = attributes(0).Description
		End If
		Return description
	End Function

	Public Shared Function ToList(ByVal type As Type) As IList
		If type Is Nothing Then
			Throw New ArgumentNullException("type")
		End If
		'Dim list As ArrayList = New ArrayList()
		Dim list As List(Of KeyValuePair(Of System.Enum, String)) = New List(Of KeyValuePair(Of System.Enum, String))()
		Dim enumValues As Array = System.Enum.GetValues(type)
		For Each value As System.Enum In enumValues
			list.Add(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
		Next
		Return list
	End Function

	Public Shared Sub InsertIntoList(ByVal index As Integer, ByVal value As System.Enum, ByRef list As IList)
		list.Insert(index, New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Sub

	Public Shared Sub RemoveFromList(ByVal value As System.Enum, ByRef list As IList)
		list.Remove(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Sub

	Public Shared Function Contains(ByVal value As System.Enum, ByVal list As IList) As Boolean
		Return list.Contains(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Function

	Public Shared Function IndexOf(ByVal value As System.Enum, ByVal list As IList) As Integer
		Return list.IndexOf(New KeyValuePair(Of System.Enum, String)(value, GetDescription(value)))
	End Function

End Class
