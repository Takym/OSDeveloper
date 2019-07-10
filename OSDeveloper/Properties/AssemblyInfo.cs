using System.Reflection;
using System.Runtime.InteropServices;

// アセンブリごとに変わる項目
[assembly: AssemblyTitle("OSDeveloper")]
[assembly: Guid("C81E8634-B7B8-4854-AABE-3A397B1D6900")]

// 共通項目
[assembly: AssemblyDescription("This software is an integrated development environment"
	+ "to create, build, and debug your operating systems.")]
[assembly: AssemblyCompany("Takym")]
[assembly: AssemblyCopyright("Copyright (C) 2019 Takym.")]

// 構成によって変わる項目
#if DEBUG && AnyCPU
[assembly: AssemblyConfiguration("Debug - Any CPU")]
#elif DEBUG && x64
[assembly: AssemblyConfiguration("Debug - x64")]
#elif RELEASE && AnyCPU
[assembly: AssemblyConfiguration("Release - Any CPU")]
#elif RELEASE && x64
[assembly: AssemblyConfiguration("Release - x64")]
#else
[assembly: AssemblyConfiguration("Unknown")]
#endif

// COM
[assembly: ComVisible(false)]

// バージョンによって変わる項目
[assembly: AssemblyProduct(ASMINFO.Caption)]
[assembly: AssemblyVersion(ASMINFO.Version)]
[assembly: AssemblyFileVersion(ASMINFO.Version)]
[assembly: AssemblyInformationalVersion(ASMINFO.Edition)]

/// <summary>
///  バージョン情報
/// </summary>
internal static class ASMINFO
{
	public const string Caption = "OSDeveloper IDE";
	public const string Version = "0.0.0.0";
	public const string Edition = "HinagataAlpha";
}
