# Progress — Thai Temple VR

> **Stale-doc notice:** This file can lag. Prefer `git log` for **committed** history. Unsaved / dirty Unreal assets are **not** reflected here or in git until committed. Do **not** invent a completion percentage.

## Committed baseline (git)

See `git log --oneline`. Recent committed titles (as of docs bootstrap, 2026-11-09): include sangkatan video, sala/church models, dummy clips, Map_Ford updates, skt model — exact SHAs change; always re-read log.

## Reported milestones (historical — not re-verified this docs session)

- Map_Ford Thai Temple layout in progress; calm seated Quest 2 target.
- Dummy monk clips under `Content/Movies/` (`MOV_Idle.mp4` ~8s, `MOV_Blessing_Dummy.mp4` ~12s, 1280×1072) — PIE idle→offering→blessing→idle reported 2026-11-08; **headset pending**.
- Sanghatan MeshBag replaced cylinder in `BP_SanghatanOffering` (reported 2026-11-08); PIE place upright reported; **hand grab pending**.
- Sala V3 / Ubosot Lanna imported (reported); Point reviewing visual issues.
- Project-local Docs/ai methodology added (this documentation commit).

## Not started / waiting on Point

- Final real monk clips.
- Lighting bake.
- Fish system polish.
- White-start-screen headset diagnosis.
- Any backlog without explicit go-ahead.

## 2026-11-10 (UTC+7) - UX-001 comfort/controls pass (uncommitted, saved assets)

- New: `/Game/ThaiTemple/Player/BP_TTPawn`, `BP_TTHand`, `BP_TTGameMode` (Map_Ford world-settings game mode); `/Game/ThaiTemple/Teleport/BP_TTLaser` + `M_TTLaser` (placed as `TTLaser`); `/Game/ThaiTemple/Common/BPC_ReturnHome`, `BPC_TTFeedback`; `/Game/ThaiTemple/Audio/UI/SFX_UI_SoftClick` (65 ms self-made click).
- Changed: BP_TeleportMenuActor (busy gate, eased follow, non-consuming inputs, release hands + haptic before warp, OnWarpRearm, stand-up before warp), BP_TeleportPoint (LookAtTarget facing), BP_DharmaPoint (fixed arrival anchor, head placement from pawn Camera, Yes/No feedback), BP_SeatedSpot (ForceStand event), BP_SanghatanTarget (bell volume 3.0 -> 1.0), BP_SanghatanOffering ('pull' tag -> 'grab', ReturnHome), BP_FishBucket (ReturnHome, instance HandInteraction z38.6 / Pour z27), pointer indices 0/1/2 + debug off. Map_Ford: TeleportPoint_1/_2 deleted, TeleportPoint_Dharma moved to the sala.
- PIE (non-VR) passed; headset tests pending (see POINT_REVIEW checklist).
