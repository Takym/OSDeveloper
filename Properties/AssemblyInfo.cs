using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;

// プロジェクトごとに設定が変わる項目
#if App
[assembly: AssemblyTitle("OSDeveloper.App")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-03013FEAAA20")]
#elif Core
[assembly: AssemblyTitle("OSDeveloper.Core")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-03013FEAAA21")]
#elif Native
[assembly: AssemblyTitle("OSDeveloper.Native")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-03013FEAAA22")]
#elif OLR
[assembly: AssemblyTitle("OSDeveloper.OLR")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-03013FEAAA23")]
#elif Documents
[assembly: AssemblyTitle("OSDeveloper.Documents")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-03013FEAAA24")]
#elif YenconTool
[assembly: AssemblyTitle("OSDeveloper.Core.Settings.YenconTool")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-03013FEAAA25")]
#else
[assembly: AssemblyTitle("OSDeveloper.##UNKNOWN##")]
[assembly: Guid("FA89E0F1-44C3-4897-8FFC-F0F0F0F0F0F0")]
#endif

// 構成ごとに設定が変わる項目
#if DEBUG
[assembly: AssemblyConfiguration("Debug x86-32")]
#else
[assembly: AssemblyConfiguration("Release x86-64")]
#endif

// 共通の項目
[assembly: AssemblyDescription("This software is a development environment to create, build, and debug your operating systems.")]
[assembly: AssemblyCompany("Takym Systems")]
[assembly: AssemblyCopyright("Copyright (C) 2018 Takym.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// バージョンごとに設定が変わる項目
[assembly: AssemblyProduct(ASMINFO.Caption)]
[assembly: AssemblyVersion(ASMINFO.Version)]
[assembly: AssemblyFileVersion(ASMINFO.Version)]
[assembly: AssemblyInformationalVersion(ASMINFO.Edition)]

// 言語
[assembly: NeutralResourcesLanguage("")]

internal static class ASMINFO
{
	public const string Caption = "OSDeveloper IDE";
	public const string Version = "0.0.0.0";
	public const string Edition = "HinagataAlpha";
}
