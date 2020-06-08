﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Crowbar.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to http://steamcommunity.com/id/zeqmacaw.
        '''</summary>
        Friend ReadOnly Property About_AuthorLink() As String
            Get
                Return ResourceManager.GetString("About_AuthorLink", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Goto Steam Group.
        '''</summary>
        Friend ReadOnly Property About_GotoSteamGroupText() As String
            Get
                Return ResourceManager.GetString("About_GotoSteamGroupText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Goto Steam Profile.
        '''</summary>
        Friend ReadOnly Property About_GotoSteamProfileText() As String
            Get
                Return ResourceManager.GetString("About_GotoSteamProfileText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to GoldSource and Source Engine Model Toolset:
        '''	* Decompiler of MDL and related files.
        '''	* Compiler interface to StudioMDL.exe tool.
        '''	* Model viewer interface to Half-Life Model Viewer tool.
        '''	* Unpacker of Tactical Intervention FPX, Garry&apos;s Mod GMA, and Source-engine VPK package files..
        '''</summary>
        Friend ReadOnly Property About_ProductDescription() As String
            Get
                Return ResourceManager.GetString("About_ProductDescription", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to http://steamcommunity.com/groups/CrowbarTool.
        '''</summary>
        Friend ReadOnly Property About_ProductLink() As String
            Get
                Return ResourceManager.GetString("About_ProductLink", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to https://steamcommunity.com/groups/CrowbarTool/discussions/1/.
        '''</summary>
        Friend ReadOnly Property BugReportLink() As String
            Get
                Return ResourceManager.GetString("BugReportLink", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        '''</summary>
        Friend ReadOnly Property crowbar_icon() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("crowbar_icon", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property crowbar_icon_large() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("crowbar_icon_large", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property CrowbarGuideBanner() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("CrowbarGuideBanner", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-ANI.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugAniFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugAniFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-MDL.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugMdlFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugMdlFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-PHY.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugPhyFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugPhyFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-SequenceGroupMDL.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugSequenceGroupMDLFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugSequenceGroupMDLFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-TextureMDL.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugTextureMDLFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugTextureMDLFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-VTX.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugVtxFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugVtxFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-VVD.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_DebugVvdFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_DebugVvdFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to decompile-log.txt.
        '''</summary>
        Friend ReadOnly Property Decompile_LogFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Decompile_LogFileNameSuffix", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property EnterArrow() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("EnterArrow", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Possible causes: The game&apos;s SDK or Authoring Tools has not been installed (usually via Steam Library Tools) or the path given to Crowbar (via Set Up Games button) is incorrect..
        '''</summary>
        Friend ReadOnly Property ErrorMessageSDKMissingCause() As String
            Get
                Return ResourceManager.GetString("ErrorMessageSDKMissingCause", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property Find() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Find", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property FindNext() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("FindNext", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property FindPrevious() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("FindPrevious", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to http://steamcommunity.com/sharedfiles/filedetails/?id=791755353.
        '''</summary>
        Friend ReadOnly Property Help_CrowbarGuideLink() As String
            Get
                Return ResourceManager.GetString("Help_CrowbarGuideLink", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property macaw() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("macaw", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property Refresh() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Refresh", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to unpack-log.txt.
        '''</summary>
        Friend ReadOnly Property Unpack_LogFileNameSuffix() As String
            Get
                Return ResourceManager.GetString("Unpack_LogFileNameSuffix", resourceCulture)
            End Get
        End Property
    End Module
End Namespace