# Roles — Thai Temple VR

Small team of Grok Bots + Point. Ownership reduces collisions.

## Point (owner)

- Approves plans before build.
- Authorizes commits and pushes.
- Headset VR verification (Quest 2).
- Final creative calls (composition, comfort, audio).

## PM / Producer (default Ham-style lead)

- Keeps TASKS / PROGRESS / HANDOFF current.
- Grills before build; routes work to the right role.
- Does not silently start backlog.
- Writes DECISIONS when Point decides; POINT_REVIEW when a bot decides with low confidence.

## VR technical / systems

- Seated spot, teleport/warp, grab, offering flow, media players, BP wiring.
- Owns interaction BPs under `/Game/ThaiTemple/` (e.g. Sanghatan, FishRelease) and VR pawn hooks.
- Verifies via mcp-unreal status → PIE; marks headset checks as pending until Point tests.

## Map / environment editor

- Map_Ford placement, lighting exposure, mesh import scale, materials, LODs.
- Sala / Ubosot / props placement. Does not replace systems BPs with kitchen-sink BPs.
- Lighting bake only when Point asks.

## Art / Blender pipeline

- Low-poly metre-scale assets in Blender on the bot box; FBX + PBR to `SourceModels\`.
- Opacity masks for nets/mesh bags; normal maps for carved detail.
- Delivers renders for Point approval **before** Unreal replace unless Point said import now.

## Game logic / media

- Idle → queued blessing/sermon at loop end; Media Framework sources under `Content/Movies`.
- No still fallback on monk screens.

## Method note

Prefer one owner per TASKS row. Cross-role edits need a short note in HANDOFF.
