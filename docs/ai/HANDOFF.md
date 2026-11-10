# Handoff — append only

Read **only the latest entry** at session start (or search by keyword). Archive by month if this file grows large.

## 2026-11-09 11:40 (UTC+7) — PM: Docs/ai methodology bootstrap

- Problem: Project knowledge lived in chat/bot memory; need CW-style repo markdown so any agent can resume. Built-in Read/Copy* refused `C:\WORK\...` (`allowed local-exec root`); machine-targeted Shell used for file I/O (sanctioned for this PC).
- Root Causes: 1. No Docs/ai structure yet. 2. File-tool root ≠ Shell scope on Windows.
- Fixes: 1. Added `AI_AGENT_INSTRUCTIONS.md`, shortened `AGENTS.md` (preserved VRGK hard rules), `CLAUDE.md`, `GEMINI.md`. 2. Added `Docs/ai/*` RULES, ROLES, ARCHITECTURE, TASKS, PROGRESS, DECISIONS, HANDOFF, LESSONS, POINT_REVIEW, PRE_COMMIT_CHECKLIST. 3. Seeded reported historical context labelled as not freshly verified. 4. Left `README.md` and Unreal assets untouched.
- Verification: relative links checked in listing; `git diff --check` on staged docs; `git show --stat` after commit; final `git status` for leftover dirt.
- State: documentation commit only (Point-authorized). **No push.** No editor/PIE run this session. Asset backlog unchanged.
- Limitations: content facts (clip lengths, tris, transforms) copied from prior reports — re-verify before treating as measured truth.

## Template

```
## YYYY-MM-DD HH:MM (UTC+7) - <Agent/Role>: <Task title>
- Problem: ...
- Root Causes: 1. ... 2. ...
- Fixes: 1. ... (paths)
- Verification: what was actually run
- State: dirty? locks? pending Point tests?
- Limitations: ...
```

## 2026-11-09 13:55 (UTC+7) - FISH-002 Fish Release V2 import (agent, no commit)
- Imported to /Game/ThaiTemple/FishRelease/V2/ (Meshes, Textures, Materials): SM_FishBucket_V2 (SOCKET_Grip z38.6), SM_BucketWater_V2, SM_Fish_Taphian_V2, SM_Fish_SoiKhao_V2; M_FishBucket_V2 + MI Red/Blue/Green; M_Fish_V2 (opaque, WPO tail sway, built from SimpleWave template because WPO isn't exposed in Python) + MI per species; M_BucketWater_V2 (translucent ripple). Source: SourceModels/FishRelease_V2 (gitignored). Old assets kept.
- BP_FishBucket: V2 meshes, Water z0, Pour z27, HandInteraction z38.6, BucketFish_1..4 under Water, water visibility propagates to children, release count RandomInt(3,5). BP_ReleasedFish: BeginPlay RandomBool picks Taphian/SoiKhao mesh. Both compiled + saved, no errors.
- PIE check (simulated tilt): 4-5 fish spawned, both species, water + in-bucket fish hide. Headset grab/pour NOT tested.
- KNOWN ISSUE: placed FishBucket in Map_Ford still holds old instance offsets (HandInteraction z49, Pour z30) after BP recompile; Map_Ford left dirty/unsaved by agent. Fix: in Details reset those components' Location to default (or re-place the actor), then save map.
- Lesson: Python edits on SCS templates don't propagate to placed instances; BP reconstruct can restore old instance values.

## 2026-11-10 05:05 (UTC+7) - Executor: UX-001 8-step comfort plan (no commit)
- Problem: menu hard to use/spammable, warp faced wrong way, stick could walk, grabs hard, objects lost, sermon popup at sala broken, bell loud.
- Root Causes: 1. Menu actor consumed trigger input and had no busy gate. 2. Teleport points had no look-at; TeleportPoint_Dharma was far from the sala. 3. VRGK pawn movement/jump/crouch/sprint bindings active, pull grab. 4. DharmaPoint used its own (moving) location as the arrival anchor and stale camera-manager location. 5. Seated stand-up ran after the warp and tipped the pawn.
- Fixes: 1. New `/Game/ThaiTemple/Player/BP_TTPawn` (child of BP_VRPhysicsPawn; empty override axis LeftX/LeftY/RightY, actions Jump/Menu/Sprint; trigger L/R -> DoPlayerGrab*; SetRotationOptions SNAP 45), `BP_TTHand` (GrabBox 14x11x10, pull None), `BP_TTGameMode`. 2. Menu actor: bBusy gate + left haptic/click, VInterp follow 110 cm / -15 cm speed 3, inputs bConsumeInput=false, release both hands + right haptic before fade, ForceStand seat, OnWarpRearm (bAway on offering/bucket/target via RearmOffering/RearmBucket/RearmTarget instance refs, StopAndReset screen via RearmScreen). 3. BP_TeleportPoint LookAtTarget (Ubosot->MonkVideoScreen, Dharma->DharmaPoint, fish->FishBucket; viewpoint keeps yaw). 4. BP_TTLaser beam+dot (Visibility trace from right controller, shown only on visible WidgetComponent). 5. BPC_ReturnHome on offering + bucket (sim, not held, >150 cm or 50 cm below home for 3 s -> home, zero velocity). 6. BPC_TTFeedback (PlaySound2D SFX_UI_SoftClick 0.3 + HFE_LowFast 0.5) on menu + DharmaPoint Yes/No. 7. DharmaPoint ArriveAnchor + Delay 0.25 + Camera component placement. 8. Bell 1.0, offering tag pull->grab, pointer indices 0/1/2, show_debug off, bucket instance offsets.
- Verification: compiled 14 BPs, 0 errors/warnings in VRGK.log; PIE (non-VR, mcp pie_control + Python): pawn BP_TTPawn, GM BP_TTGameMode, SNAP_TURN 45; menu opens 111 cm from camera; 7x toggle spam consistent; toggle ignored while warp busy; offering moved 300 cm -> returned home (0.2 cm); warps Dharma/Ubosot/fish/viewpoint within 3-5 cm, yaw error 0; Dharma popup shows at head; sermon keeps playing at sala; warp away stops sermon and sets bAway; Ubosot(seated)->fish and ->Dharma stay upright. No runtime script errors.
- State: all assets + Map_Ford saved; **not committed** (Point). Scratch BP deleted. Temp helper files under `%TEMP%\vrgk_tt` and `C:\Users\Point\*.zip` / `tt_seat.json` (safe to delete).
- Limitations: no headset run - grab by trigger/grip, laser visuals, haptics, click loudness, real stick input, menu follow comfort, seated height are unverified on Quest 2. DharmaPoint popup sits at camera *component* height (in PIE 80 cm lower than eyes because the non-VR camera offset is applied later) - check in headset.

## 2026-11-10 14:55 (UTC+7) - Executor: BUG-WHITE white map from 2nd Play (no commit)
- Problem: Point - whole Map_Ford white ("can't see anything") from the 2nd Play / VR Preview in one editor session; editor restart clears it until the next 2nd play. Same as backlog BUG-WHITE.
- Root Causes: 1. Map_Ford has 4 unbuilt SphereReflectionCaptures (RC_River, RC_Pier, RC_Wihan, RC_Pagoda; PIE showed "REFLECTION CAPTURES NEED TO BE REBUILT (4 unbuilt)"). In the editor the PIE copies reuse cached capture data that is invalid after the first PIE, giving huge/invalid reflection values everywhere. 2. Bloom (enabled by VRGK BP_DefaultPostProcess, intensity 0.675) spreads those values over the whole frame -> pure white. Not our fade, exposure, LUT, MPC, media, or any BP-set cvar (all compared/ruled out).
- Fixes: 1. Set Visible=false on the 4 RC_* ReflectionCaptureComponents in Map_Ford (actors kept, placements preserved) and saved Map_Ford. No BP or VRGK asset changes.
- Verification: reproduced in this editor session (after Point's 2 VR Previews): every PIE white (mean luminance 227). Bisect in PIE: Bloom off -> 153, ReflectionEnvironment off -> 132, all captures hidden -> 132, r.ReflectionCaptureUpdateEveryFrame 1 (fresh recapture) -> 132; ExposureOffset/LUT/eye-adaptation/LocalExposure/fade/manual exposure had no effect. After fix: 4 consecutive PIE runs in the same session, all normal (132, screenshots), no unbuilt-capture message, no errors. Spawn-view difference with vs without captures: mean 0.1/255.
- State: Map_Ford.umap modified + saved, **not committed**. Show-flag overrides used for testing were reset to default (2).
- Limitations: VR Preview (stereo, VirtualDesktopXR) not run by agent - Point to confirm 3x VR Preview. Local reflections near river/pier/wihan/pagoda now come from the sky light only; to get them back: re-enable RC_* and Build Reflection Captures (BuiltData is gitignored -> rebuild on each machine; do as part of LIGHT-001). Thai text in on-screen debug messages renders as boxes (missing font) - separate cosmetic issue.

## 2026-11-10 15:16 (UTC+7) - Executor: grab interaction range x2 (no commit)
- Request: Point - "เพิ่มระยะของ interaction grab ให้กว้างขึ้น สองเท่า" (double the grab range).
- Changes (ThaiTemple assets only, no VRGK base asset touched): 1. BP_TTHand construction script: SetBoxExtent(GrabBox) 14,11,10 -> 28,22,20; new SetRelativeLocation(GrabBox) 18,0,0 (SCS default was 7,0,0; box is yawed 90 so hand-forward half-length = extent Y 22 -> rear edge stays at x=-4 like before, front 18 -> 40 cm; side/up symmetric so the mirrored left hand is unaffected). 2. BP_SanghatanOffering + BP_FishBucket: BPC_HandInteraction1 MaxDistance 30 -> 60 on the component template, plus "Set MaxDistance = 60" at BeginPlay. Reason: the placed Map_Ford instances keep a serialized 30 and MaxDistance is not editable on instances (Python: "cannot be edited on instances"); a fresh spawn reads 60.
- Selection safety: VRGK sorts GrabTraceProfile hits by distance (Find Sorted Hits In Grab Box) and GetNearestInteractionComponent checks MaxDistance from GrabRoot. Only actors with BPC_HandInteraction, simulating physics, or the "climb" tag are grabbable; ThaiTemple floors/buildings have none (climb tag only on VRGK demo ropes/ladders, EnableClimbTagForPD=false). Offering and bucket are ~68 m apart, so they never compete. BP_SanghatanTarget box is in range of the bigger box but has no HandInteraction/physics.
- Verification: compiled BP_TTHand, BP_SanghatanOffering, BP_FishBucket - 0 errors (the only compile errors in the log were mid-edit before the target pin was wired). PIE (non-VR): both hands GrabBox 28x22x20 @ (18,0,0); runtime MaxDistance 60 on both placed items. Box-overlap test (same box, hand facing item): old box reaches item only at 10 cm gap, new box at 10/20/30 cm (offering also 40 cm); GrabRoot->item 36-48 cm is inside 60 but was outside 30. Floor shows up in the overlap (WorldStatic) but is not grabbable.
- State: 3 BPs saved, Map_Ford re-saved (no new map edits), **not committed**.
- Limitations: real grab with Quest 2 not tested (hand actors could not be moved by script in non-VR PIE, so the trace was reproduced with a box overlap). In the editor Details panel the placed items still show MaxDistance 30; BeginPlay overrides it.
