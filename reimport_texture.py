import unreal

print("=== Starting Texture Import ===")
task = unreal.AssetImportTask()
task.filename = r"c:/WORK/VRGK-Community-Project-UE5/SourceModels/WatKhokKhian/source/MAHS-THA-NWT-MNT-KHK-S-001-DA-002_u0_v0_diffuse.jpg"
task.destination_path = "/Game/Point/ThaiTemple/Vihan"
task.destination_name = "TEX_MAHS-THA-NWT-MNT-KHK-S-001-DA-002_u0_v0_diffuse"
task.replace_existing = True
task.automated = True
task.save = True

unreal.AssetToolsHelpers.get_asset_tools().import_asset_tasks([task])
print("=== Texture Import Completed Successfully ===")
