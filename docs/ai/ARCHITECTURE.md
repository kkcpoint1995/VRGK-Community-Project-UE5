# Architecture — Thai Temple VR (reported layout)

Paths and numbers below are **reported / historical** from project work through 2026-11-08 unless a later HANDOFF re-verifies them. Dirty UE assets may differ from last commit.

## Stack

- Unreal Engine **5.3**, project `VRGK.uproject`, map **Map_Ford**.
- VR: Meta Quest 2, VRGK locomotion/interaction base.
- Editor automation: **mcp-unreal** expected on `127.0.0.1:18090` (confirm `/api/status` → VRGK).
- Authored meshes often built in Blender 4.x on the bot box, exported FBX metre-scale → `SourceModels\`.

## Content roots (reported)

| Area | Path (Content) | Notes |
|------|----------------|-------|
| Thai Temple | `/Game/ThaiTemple/` | Primary map content |
| Sala V3 | `/Game/ThaiTemple/SalaDharmaV3/` | SM_*_V3, M_SalaDharma_PBR_V3, BP_SalaDharma_V3 |
| Ubosot Lanna | `/Game/ThaiTemple/UbosotLanna/` | SM + materials |
| Sanghatan | `/Game/ThaiTemple/Sanghatan/` | BP_SanghatanOffering, MeshBag |
| Fish | `/Game/ThaiTemple/FishRelease/` | BP_FishBucket |
| Movies | `Content/Movies/` | Idle / blessing mp4 + FileMediaSource |

## Key systems (separate BPs)

- **Seated spot** — single ubosot warp; ~1 m eye height; positional lock; free look / hand warp & grab.
- **Sanghatan offering** — grab + place upright; triggers blessing queue on monk media.
- **Monk media** — MediaPlayer + MediaTexture + screen material; idle loops; blessing/sermon after current idle ends.
- **Fish release** — separate from sanghatan (backlog / untested items may remain).

## Source on disk (reported)

- `SourceModels\Sanghatan_MeshBag\` — centred FBX + textures for mesh bag.
- `SourceModels\Ubosot_Lanna\` — ubosot authored set.
- Sala import scripts historically under bot workspace `sala_dharma/ue_import/` (not necessarily in this repo).

## Local file access note

On Point's Windows PC, project lives under `C:\WORK\...`. Machine-targeted Shell is the usual tool for those paths; some file APIs only allow the user profile root.
