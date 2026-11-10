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

## 2026-11-10 — Sanghatan placement fix + body lock (uncommitted)
- Offering/bucket: StaticMesh profile + BPC_HandGrabUpdater DefaultCollisionProfile = GrabbedPhysicsActor (ignores VRPhysicsPawn rig channel) on SCS template and re-applied in BeginPlay. Root cause of the knock: on release the updater switched the bag back to PhysicsActor, which blocks the rig, so the bag depenetrated out of the 10 kg rig capsule and shoved the player.
- BP_SanghatanTarget: Tick check — offering not held, not Done, within NearRadius (50 cm) of target+(0,0,20) → physics off, grab tag cleared, 0.4 s ease-in-out glide (Lerp transform) to the spot, then the existing chime → 1.5 s → blessing. The old overlap-snap nodes were removed. Rearm clears bGliding + Done; the offering's own ReturnHome re-enables physics and the grab tag.
- New BPC_TTBodyLock component on BP_TTPawn: Tick holds virtual crouch at 0 and zeroes the rig's XY linear/angular velocity (gravity stays). Body only: head and hands are not clamped.
- EyeHeight (int cm, instance editable) on BP_TeleportPoint, default 160; TeleportPoint_Ubosot = 100. BodyLock.SpawnEyeHeight = 160. At spawn (screen held black) and after each menu warp: ApplyEyeHeight → trace the floor under the pawn → VROriginRoot world Z offset so that camera − floor = EyeHeight (two passes, at +0.25 s and +1.05 s). Head tracking is free after that. Menu fade-in delayed to 1.2 s.
- BP_SeatedSpot ArriveRadius default = 0 (auto-seat disabled; the Ubosot point's EyeHeight 100 replaces it).
- PIE (non-VR) evidence: camera − floor = spawn 160.0, TP_3 158.9, TP_4 160.1, Dharma 160.0, Ubosot 100.7–101.7. Release near target → bag at target+20, physics off, tags cleared within 0.3 s; pawn moved 0.4 cm. Bag dropped inside the rig capsule → rig moved 0.6 cm. Rearm (warp away and back) → bag home, physics on, Done false. All 8 BPs compile with no errors.
- Known limits: VRGK OnPlayerCalibrationUpdate is not overridden (if a recalibration fires, the next warp re-applies EyeHeight). Grabbables still Block the plain Pawn channel (the rig uses VRPhysicsPawn, which is ignored). In one PIE run the Ubosot warp landed ~1.3 m from the point and drifted a few cm — check in headset.

### POINT_REVIEW headset checks
1. Spawn and every warp: does your eye height feel like 160 cm (Ubosot seated ~100 cm), whatever your real height? Is head lean/crouch still free?
2. Place the bag near the tray and let go: it should glide into place with no push on your body. Then the chime and blessing play.
3. Drop the bag/bucket at your feet or inside your body: no push.
4. Stick, jump and crouch do nothing. Hands are free.
5. Warp away and back: the bag returns home and can be grabbed again.
6. Ubosot warp lands on the mat (not ~1 m off).

## 2026-11-10 18:50 (UTC+7) - Executor: no-clip floating body (no commit)
- Request: Point - remove player collision; float and pass through everything; hands still grab. Also: on reset, fish should be back in the bucket.
- Fix: BPC_TTBodyLock MeasureApply now starts with Cast BP_VRPhysicsPawn -> PhysicsRig -> SetSimulatePhysics(false) + SetCollisionResponseToAllChannels(Ignore) on FenderSphere/LocomotionSphere/BodyCapsule/HeadSphere, then AttachToComponent(FenderSphere, KeepWorld, no weld) for the other 3. This runs at spawn and after every warp. The rig is now a kinematic ghost, so the hand constraints (HandBase/PDBase -> FenderSphere) still drive the hands. Pawn CollisionCylinder was already NoCollision, CharMove MOVE_None. Only collision-ignore on a still-simulating rig was tried first: the rig fell or exploded, so it was rejected.
- Verification (non-VR PIE): 0 compile errors; at spawn and after each warp sim=false, resp=Ignore, children attached. Warps: Ubosot (110,-3370) exact, eye 100.0; Dharma eye 160.0; TP_3 159.1; TP_4 160 (earlier run); stable +2 s, no drift. Scripted DoPlayerGrabRightPressed grabbed the offering (held, still blocks WorldStatic), rig unmoved. Ubosot 1.3 m landing drift was not seen (rig no longer collides).
- Not done: FISH-002 bucket refill (stopped for time). Plan: the empty bucket returns home through BPC_ReturnHome without a refill; add a Refill (bEmpty=false + Water visible, fish are children of Water) on any reset, plus empty+not-held 3 s -> home+refill. Released fish already self-destroy after ~6 s swimming.
- State: BPC_TTBodyLock + BP_TTPawn saved, **not committed**. Editor pref bThrottleCPUWhenNotForeground set back to True.

## 2026-11-10 18:56 (UTC+7) - Executor: FISH-002 bucket refill (no commit)
- BP_FishBucket: new custom event **Refill** (bEmpty=false, RefillT=0, Water SetVisibility true + propagate, so BucketFish_1-4 (children of Water) show again). Tick now goes through a Sequence: then_0 = old logic (pour / bAway reset, unchanged); then_1 = if bEmpty AND NOT pawn.IsObjectGrabbedByHand(self), RefillT += Delta*1000 (int ms), at >=3000 do SetActorTransform(Home, teleport), zero lin/ang velocity, Refill; otherwise RefillT=0. This covers BPC_ReturnHome (it uses the same 3 s), a drop near home, and a drop anywhere. The warp-away bAway path already refilled the bucket the same way.
- RefillT is an **int** (mcp add_variable 'float' made an int), so it holds milliseconds; truncation makes it ~0-5% slow.
- Released fish: unchanged; they swim ~6 s and destroy themselves.
- Verification: compile 0 errors, saved. **PIE not run** (Point was using the editor at the time).

## 2026-11-10 19:03 (UTC+7) - Executor: menu laser always visible while menu open (no commit)
- Cause: the beam showed only when the Visibility trace from MotionControllerRight hit the visible MenuWidget, and it was 3 mm thick, so in the headset it looked missing. The logic itself worked (PIE: a hit placed the beam and dot).
- Fix (BP_TTLaser): new instance var MenuRef (set to BP_TeleportMenuActor_C_0 in Map_Ford). The no-hit paths now Cast MenuRef -> if bOpen: beam from controller+8 cm, 300 cm long, visible, dot hidden; else hide as before. Hit-path beam thickness 0.003 -> 0.006; Dot template scale 0.015 -> 0.025. (GetActorOfClass with a class pin default could not be set via mcp - string default rejected.)
- Verification: compile 0 errors, BP + Map_Ford saved; PIE: menu closed -> beam hidden; menu open -> beam visible.

## 2026-11-10 19:15 (UTC+7) - Executor: FISH-002 fix - bucket did not reset in headset (no commit)
- Finding: in PIE the 18:56 logic worked (tilt or real scripted grab, pour, release -> home + refill after 3 s, repeatable). It needed the bucket to be *released*. In the headset Point probably kept holding it, or a grip+trigger double grab kept it held, so it never reset.
- Fix (BP_FishBucket): the refill timer now also runs while held if the bucket is upright-ish (up.z > 0.6; pour triggers at < 0.35). At 3 s: if held -> Refill in the hand (no teleport); if not held -> teleport home + zero velocity + Refill.
- PIE (non-VR, scripted DoPlayerGrabRight + tilt): held pour -> empty; 3.6 s later still held -> refilled (water + 4 fish visible); pour #2 works; release -> home (740,3360) full after 4.5 s. Also 2 release-only cycles earlier. Compile 0 errors, saved.

## 2026-11-11 04:40 (UTC+7) - Executor: PC test hotkeys 1-4 (no commit)
- BP_TeleportMenuActor: InputKey events One/Two/Three/Four (bConsumeInput=False) -> SelectPoint(HotkeyPoint1..4), so they use the same path as the menu buttons (busy gate, release hands, fade, ForceStand, EyeHeight, LookAt, rearm). New instance vars HotkeyPoint1-4 set on the placed menu in Map_Ford in menu order (SortOrder 0,2,3,4): 1=TeleportPoint_Ubosot, 2=TeleportPoint_Dharma, 3=TeleportPoint_3 (viewpoint), 4=TeleportPoint_4 (fish release). No UI change. DefaultInput.ini has no 1-4 bindings. Keyboard number keys only (not numpad).
- Verification: compile 0 errors, BP + Map_Ford saved. PIE: SelectPoint(HotkeyPointN) for N=1..4 landed on (110,-3370), (-2330,-1890), (-3500,2500), (690,3310). Real key presses not sent (would steal window focus).

### FISH-002 v3 — glide-home reset (2026-11-11 04:47 ICT)
- BP_FishBucket Tick then_1 replaced (old in-hand 3s refill + empty-unheld teleport removed).
- After pour (bEmpty): wait 3.0 s (fish land max ~2.55 s after spawn) -> if held, pawn DoPlayerGrabLeft/RightReleased -> StaticMesh SimulatePhysics off -> GlideStart=actor transform -> bGlide.
- Glide: RefillT ms timer, alpha=min(t/1000,1), smoothstep, TLerp(GlideStart,Home) SetActorTransform(teleport) ~1 s.
- New custom event ResetBucket: bGlide=false, RefillT=0, transform=Home, physics on, zero lin/ang velocity, Refill (water+4 fish visible, bEmpty=false). Actor kept (menu RearmBucket ref valid).
- Status: compiled; SAVE PENDING (editor was in PIE, save blocked). PIE test pending.
