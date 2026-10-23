// Copyright (c) mcp-unreal project contributors. Apache-2.0 license.

using UnrealBuildTool;

public class MCPUnreal : ModuleRules
{
	public MCPUnreal(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.NoPCHs; // UE 5.3: avoid engine shared PCH (C4668 with MSVC 14.4x)
		// UE 5.3 + MSVC 14.4x: engine headers trip C4668 (__has_feature)
		bEnableUndefinedIdentifierWarnings = false;

		PublicDependencyModuleNames.AddRange(new string[]
		{
			"Core",
			"CoreUObject",
			"Engine",
		});

		PrivateDependencyModuleNames.AddRange(new string[]
		{
			"HTTPServer",
			"Json",
			"JsonUtilities",
			"Slate",
			"SlateCore",
			"UnrealEd",
			"AssetRegistry",
			"BlueprintGraph",
			"KismetCompiler",
			"Kismet",
			"AnimGraph",
			"ImageCore",
			"AssetTools",
			"InputCore",
			"EnhancedInput",
			"LevelEditor",
			"EditorSubsystem",
			"UMG","UMGEditor",
			"HTTP",
			"ProceduralMeshComponent",
			"PCG",
			"GameplayAbilities",
			"GameplayTags",
			"GameplayTasks",
			"Niagara",
			"NiagaraCore",
		});

		// Optional Fab plugin dependency ÃƒÂ¢Ã¢â€šÂ¬Ã¢â‚¬Â only if installed.
		if (Target.bBuildWithEditorOnlyData && (Target.Version.MajorVersion > 5 || Target.Version.MinorVersion >= 4)) // UE 5.3 Fab plugin lacks FabAssetsCache
		{
			// Fab is an editor-only plugin bundled with UE 5.3+.
			PrivateDependencyModuleNames.Add("Fab");
			PublicDefinitions.Add("WITH_FAB=1");
		}
		else
		{
			PublicDefinitions.Add("WITH_FAB=0");
		}

		// LiveCoding is Windows-only.
		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			PrivateDependencyModuleNames.Add("LiveCoding");
			PublicDefinitions.Add("WITH_LIVE_CODING=1");
		}
		else
		{
			PublicDefinitions.Add("WITH_LIVE_CODING=0");
		}

		// UE 5.3 compat: FHttpRequestHandler is a TFunction (delegate in 5.4+); MSVC 14.4x lacks __has_feature
		if (Target.Version.MajorVersion == 5 && Target.Version.MinorVersion <= 3)
		{
			PublicDefinitions.Add("MCP_MAKE_HTTP_HANDLER(f)=FHttpRequestHandler(f)");
			PublicDefinitions.Add("MCP_MAKE_HTTP_HANDLER_RAW(o,f)=FHttpRequestHandler([o](const FHttpServerRequest& Req, const FHttpResultCallback& Cb){ return (o->*f)(Req, Cb); })");
			PublicDefinitions.Add("__has_feature(x)=0");
		}
		else
		{
			PublicDefinitions.Add("MCP_MAKE_HTTP_HANDLER(f)=FHttpRequestHandler::CreateStatic(f)");
			PublicDefinitions.Add("MCP_MAKE_HTTP_HANDLER_RAW(o,f)=FHttpRequestHandler::CreateRaw(o,f)");
		}

		// Preprocessor guards ÃƒÂ¢Ã¢â€šÂ¬Ã¢â‚¬Â set to 0 to compile without the dependency.
		PublicDefinitions.Add("WITH_PCG=1");
		PublicDefinitions.Add("WITH_GAMEPLAY_ABILITIES=1");
		PublicDefinitions.Add("WITH_NIAGARA=1");
	}
}
