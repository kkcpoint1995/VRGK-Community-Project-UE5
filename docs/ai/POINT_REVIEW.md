# Point review — low-confidence bot calls

Rows the bot decided without a clear Point instruction. Point should confirm or revert.

| Date (UTC+7) | Call | Confidence | By | Revert |
|--------------|------|------------|-----|--------|
| 2026-11 (hist.) | Sanghatan placed on player **right** side | Medium | PM | Move target/placement to left if Point prefers |
| 2026-11-08 (hist.) | MeshBag scale ~40 cm tall, orange net look | Medium | Art/PM | Re-export size/colors after Point feedback |
| 2026-11-09 | Seeded Docs/ai historical facts from chat summary without re-measuring assets | Medium | PM | Edit DECISIONS/PROGRESS/ARCHITECTURE after local re-verify |
| 2026-11-10 | Feedback component named `BPC_TTFeedback` (ActorComponent under /Game/ThaiTemple/Common) instead of an actor `BP_TTFeedback` | Medium | Executor | Rename asset; references are the `Feedback` components on menu + DharmaPoint |
| 2026-11-10 | Menu follow: 110 cm ahead, 15 cm below eye, VInterp speed 3; yaw-only | Medium | Executor | Edit Multiply 110 / Add (0,0,-15) / InterpSpeed 3 in BP_TeleportMenuActor Tick |
| 2026-11-10 | Return-home rule: 150 cm from home or 50 cm below it, not held, 3 s | Medium | Executor | Edit literals in BPC_ReturnHome Tick |
| 2026-11-10 | Seat stands up during the warp fade (ForceStand) to stop the pawn tipping after leaving the Ubosot seat | Medium | Executor | Remove ForceStand branch between Delay and Cast in SelectPoint |
| 2026-11-10 | DharmaPoint popup appears 0.25 s after arrival; arrival anchor = its editor position | Medium | Executor | Remove Delay / ArriveAnchor nodes in BP_DharmaPoint |
| 2026-11-10 | Left-hand trigger + right-hand trigger also grab (in addition to grip); releasing either button drops | Medium | Executor | Delete PlayerTrigger nodes in BP_TTPawn |
| 2026-11-10 | BUG-WHITE fix: hid (Visible=false) the 4 unbuilt reflection captures RC_River/RC_Pier/RC_Wihan/RC_Pagoda instead of baking them (bake = LIGHT-001, only on Point's go) | Medium | Executor | Select RC_* in Map_Ford, tick Visible, then Build > Build Reflection Captures (must build, or the white bug returns) |
| 2026-11-10 | Grab range x2: BP_TTHand GrabBox 14x11x10 -> 28x22x20 cm and moved forward x7 -> x18 (rear edge stays ~4 cm behind hand origin, front 18 -> 40 cm); BPC_HandInteraction1 MaxDistance 30 -> 60 on BP_SanghatanOffering + BP_FishBucket (template + BeginPlay "Set MaxDistance 60", because placed Map_Ford instances keep a stale 30 that the editor will not let us edit per-instance) | Medium | Executor | Too far/grabs neighbours: set BP_TTHand UCS SetBoxExtent back to 14,11,10 and SetRelativeLocation to 7,0,0; set 30 in the Set MaxDistance nodes (BeginPlay) of both item BPs |

## Template

`| YYYY-MM-DD | <call + why> | Low/Medium | <role> | <how to undo> |`

## Headset test checklist - UX-001 (2026-11-10)

Put on the Quest 2, seated. Tick each:

1. Start: you spawn at the start point, the white screen issue is unchanged/OK, no walking when pushing the **left stick** in any direction; right stick left/right = 45 degree snap turn; right stick up/down = nothing (no crouch).
2. **Left Y** does nothing (no height re-calibration). **Left X** (menu button) opens the warp menu.
3. Menu appears ~1 m in front, a little below eye level, and **eases** after your head when you look around (no snapping, no nausea). Pressing the menu button many times fast does not break it; it does nothing during a warp.
4. Point the right hand at the menu: a thin warm **laser + dot** shows only while pointing at a menu/popup. Hovering + right trigger selects. You feel a soft buzz and hear a quiet click (not startling) on open and on select.
5. Each warp: short fade, you land **facing** the target: Ubosot -> monk screen, Sala -> sermon popup, Fish pier -> bucket, Viewpoint -> view. Anything held is dropped before the fade.
6. Ubosot: you are lowered to seated eye height (~1 m). Warp away -> you are at normal height, **not floating or tilted**.
7. Grab the sanghatan with **grip** and with **trigger** (each hand). Easy to grab at ~10-14 cm. Drop it on the floor -> it returns to its stand after ~3 s. Offer it -> bell is calm (not loud), blessing plays.
8. Fish bucket: grab by the handle (grip and trigger), pour works; drop it/leave it on the floor -> returns after ~3 s.
9. Sala: sermon popup appears in front of you shortly after arriving; Yes = buzz + click + sermon plays and keeps playing while you stay; No closes it. Warp away -> sermon stops, idle monk returns at the Ubosot.
10. Come back to each place -> offering/bucket/target are reset and work again.
Report anything that felt wrong (with place + hand) to the PM.

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

### 2026-11-10 no-clip body (headset checks)
1. Walk your real body into walls/props/floor: you pass through and are never pushed; you never fall or float off after a warp.
2. Eye height at spawn/each warp still right (Ubosot ~100 cm seated), landing on the Ubosot mat.
3. Grab bag and bucket with Grip and with Trigger; held items still hit tables/floor; hands not stuck after warps.
4. Revert: delete the 16 nodes at y>=2900 in BPC_TTBodyLock and reconnect MeasureApply.then -> Cast To BP_VRBasePawn.

### 2026-11-10 fish refill (FISH-002)
1. Pour the bucket, then let go of it (drop it or put it on the stand). About 3 s later it should go back to its spot full (water + fish) and pour again.
2. While you hold the empty bucket it must not jump home.
3. Revert: delete the Sequence then_1 branch and the Refill nodes (y>=2050) in BP_FishBucket and reconnect Event Tick -> the first Branch.

### 2026-11-10 menu laser
1. Open the menu: a thin laser comes out of the right controller at once. On the menu, the beam ends at the button with a dot. Close the menu: the laser is gone.
2. Does the laser follow where the pointer actually clicks? If it points off-angle, the controller pose (grip vs aim) needs adjusting.

### 2026-11-10 fish refill fix
1. Pour, then hold the bucket upright: about 3 s later water + fish come back in your hand, and you can pour again.
2. Pour and let go: about 3 s later the bucket is back on its stand, full.
3. If it refills too early or too late while held, adjust the up.z threshold (0.6) or the 3 s.

### 2026-11-11 PC hotkeys
1. In PIE/VR Preview, with the game window focused, press 1/2/3/4 on the number row: Ubosot / Dharma sala / viewpoint / fish release. Each warp should fade the same as from the menu.

### FISH-002 v3 — glide-home reset (2026-11-11 04:47 ICT)
- BP_FishBucket Tick then_1 replaced (old in-hand 3s refill + empty-unheld teleport removed).
- After pour (bEmpty): wait 3.0 s (fish land max ~2.55 s after spawn) -> if held, pawn DoPlayerGrabLeft/RightReleased -> StaticMesh SimulatePhysics off -> GlideStart=actor transform -> bGlide.
- Glide: RefillT ms timer, alpha=min(t/1000,1), smoothstep, TLerp(GlideStart,Home) SetActorTransform(teleport) ~1 s.
- New custom event ResetBucket: bGlide=false, RefillT=0, transform=Home, physics on, zero lin/ang velocity, Refill (water+4 fish visible, bEmpty=false). Actor kept (menu RearmBucket ref valid).
- Status: compiled; SAVE PENDING (editor was in PIE, save blocked). PIE test pending.

### FISH-002 v3 — saved + PIE verified (2026-11-11 05:16 ICT)
- BP_FishBucket saved. PIE x2: tilt -> pour, bEmpty; +3 s glide (physics off) ~1 s eased -> home; ResetBucket -> water + 4 fish visible, bEmpty=false, physics on. Both rounds OK.

### SANGHATAN-TRAY — teak tray + reed mat replace blue placeholder (2026-11-11 05:16 ICT)
- Imported from SourceModels/SanghatanTray to /Game/ThaiTemple/Sanghatan/Tray (SM_SanghatanTray, SM_ReedMat, SM_SanghatanGlow + Textures/). FBX came in 1/100 -> reimported with import_uniform_scale=100 (now 45x35x9.3, 90x60x0.7 cm). UV2 lightmap index 1 OK.
- UCX boxes did NOT import -> added simple box collision (full bounds) to tray and mat; glow NoCollision.
- Textures: ORM sRGB off TC_Masks, glow mask sRGB off Grayscale, normals TC_Normalmap (DirectX).
- Materials /Game/ThaiTemple/Sanghatan: M_TeakTray, M_ReedMat (BC/N/ORM); M_SanghatanGlow unlit additive two-sided, emissive = GlowColor(1,0.62,0.18) x mask x Intensity(4) x pulse lerp(0.55..1, sin(t*PulseSpeed 0.35)).
- BP_SanghatanTarget: new ReedMat (root, 0) -> Tray (z 0.55) -> Glow (re-used component, now SM_SanghatanGlow at 0,0,0). Tray/Mat collision BlockAll but Pawn=Ignore, no step-up. Existing glow show/hide logic kept (hide on lock, show on Rearm).
- Glide/snap target offset 0,0,20 -> 0,0,26.95 (bag pivot centre, half height 20 + mat 0.55 + floor 6.4). NearRadius 50 unchanged.
- Map_Ford: target instance scale 0.6 -> 1.0 (tray real size; bag fits), instance Glow overrides cleared. Map saved.
- PIE: glow on at start; bag dropped within radius -> Done, glides, bottom exactly on tray floor (186.95), glow off; Rearm -> glow on.
- Known: per-frame PIE warning 'PhysicsRig ... Simulate Physics ... AddForce' from no-clip rig (VRGK base pawn AddForce) — not silenced (would need VRGK base edit).

### UBOSOT-LOOK — seamless monk-video illusion (2026-11-11 06:09 ICT)
- New /Game/ThaiTemple/UbosotLook: M_UbLook_Wood (BC/N/R/AO + Tint, Desat, Brightness, UVScale, UVSwap, NormalStrength, RoughnessMul, Specular); MI_UbLook_TeakWall (brown_planks_03, UVSwap=1 -> HORIZONTAL planks like the clip, Brightness 0.30), MI_UbLook_TeakFloor (brown_planks_03, planks run toward the screen, 0.34), MI_UbLook_TeakPost, MI_UbLook_TeakDark. Textures = existing Poly Haven CC0 set in UbosotLanna/Textures.
- Map_Ford BP_UbosotLanna_C_0 instance overrides only (BP/base MIs untouched): Floor slot2 TeakFloor; Structure slot0 TeakPost, slot3 (RedTeak walls) TeakWall, slot4 TeakDark; Props slot0 TeakWall, slot2 TeakDark; Railing slot1 TeakWall; VideoScreenSlot hidden (old placeholder, was z-fighting). Carved red/gold WallPanel panels kept original.
- Lights (folder UbosotLook/Lights): UbLook_Light_Hall_Front/Back (8 cd, 2700K, r900, shadows), UbLook_Light_Candle_L/R (3 cd, 2700K, r380).
- Video: M_VideoScreen_Blend (unlit TRANSLUCENT; same params VideoTex/Fade/Brightness/Desaturation + Tint, EdgeSharpness (sides/top fade, 10 = ~10% soft edge), BottomSharpness 30 (crisper floor line), OpacityMax). UV = (v, 1-u) to match the Plane's pitch-90 orientation. MI_VideoScreen_Ubosot (Brightness 1.1) on the level instance Screen.
- Screen scaled x0.65 (Screen rel scale 2.129 x 2.535 = 254 x 213 cm, same aspect) so the monk reads life-size from the seated player (EyeHeight 100) at ~2.7 m; actor Z set so screen bottom = 197 (dais top ~199) -> clip horizon ~ eye height. For a new clip with same aspect: just swap the media; if aspect changes, set Screen scale X:Y to the new aspect, keep bottom at 197.
- No niche/frame (removed all UbLook_Niche_*/Trim_* per Point: illusion, no border).
- Screenshots: Temp\vrgk_tt\shots\ub_before.png ... ub_v7.png.
- Monk clip flow check: PlayBlessing sets CurSrc and TryStart does nothing while idle is playing; TickPlay starts the pending clip only when the player stops (idle end, MP_MonkVideo Loop=false) -> already waits for the idle loop end; after blessing ends StopAndReset reopens Idle. Seam risk: Screen visibility = IsPlaying, so the screen hides for the few frames while the next file prepares (possible blink). Not changed.

### UBOSOT-LOOK v2 (2026-11-11 06:16 ICT)
- Current clip MOV_Idle.mp4 is 1280x1072 (1.194:1) and is framed at the monk's lap: feet/mat are NOT in this clip (cut in source, not UE). New v2 clips (960x720, 4:3) show full body.
- Perspective: measured current clip vanishing point at (0.59 W, 0.35 H from bottom) -> screen H=224 cm, W=267.5, bottom z=200 (just above dais 199, no occlusion), centre x=86 (VP lands on player x=110). Rotation unchanged (yaw -90, plane faces player squarely; no tilt needed). BottomSharpness 60.
- NOTE: BP_MonkVideoScreen changed concurrently (not by me): DefaultSceneRoot rel scale 1.5, new 'Pointer' comp, actor y -3633.5. Screen rel scale now compensates (/1.5): 1.493 x 1.783.
- For new 4:3 clips (VP ~0.52 W, 0.42 H from bottom): H=185, W=247 cm -> Screen rel scale X=1.85/k, Y=2.47/k (k = root scale, now 1.5 -> 1.233 x 1.647), bottom z=200, centre x~105. Monk then reads ~as if 4-5 m away (farther, as requested).
- Windows untouched (per Point).

## UBOSOT-LOOK v3 - 4:3 screen sizing (2026-11-11)
- BP_MonkVideoScreen_C_0 (Map_Ford) root scale reset 1.5 -> 1.0. Screen rel scale X=2.05 (height), Y=2.7333 (width) -> world 273.33 x 205.0 cm (exact 4:3, matches 960x720 v2 clips).
- Actor loc (113.7, -3633.5, 281.1), yaw -90. Screen bounds x -22.97..250.37, z 178.6..383.6.
- Math: clip horizon/VP at 0.4875 of H from bottom, x 0.4865 (frame v2_f0). Eye at Ubosot point z 278.4, real floor at screen z 178.4 (trace) -> H=(278.4-178.4)/0.4875=205; centre x = 110 + 0.0135*W.
- Fit: back wall y -3655, interior x -84.6..304.6, ceiling 506 -> ~62/54 cm side margin, 122 cm above. No post/window overlap. Plane 100x100, UV 0..1, no tiling.
- Fade: monk+mat x 22..72%, mat bottom 12.9% from bottom -> inside non-faded zone.
- Preview: PIE-only MID VideoTex=/Game/ThaiTemple/UbosotLook/Preview/T_MonkV2_Preview (frame 0) -> ubosot_v2clip_preview.png; reverted on PIE stop. Clip NOT swapped; current 1.19:1 clip shows ~12% horizontal stretch until swap.

## MONK-CLIP v3 in game (2026-11-11, Point approved)
- Content/Movies (new, old kept as backup): MOV_Idle_v3.mp4 (4 s), MOV_Blessing_v2.mp4 (12 s loop, raw), MOV_BlessToIdle_v3.mp4 (0.5 s), MOV_Blessing_v2_bridge.mp4 (= blessing + bridge re-encoded, 12.5 s, used in game). All 960x720 h264 24fps.
- New FMS in /Game/ThaiTemple/MonkVideo: FMS_Idle_v3, FMS_Blessing_v2 (-> _bridge file), FMS_BlessToIdle_v3 (spare). BP_MonkVideoScreen class defaults SrcIdle=FMS_Idle_v3, SrcBlessing=FMS_Blessing_v2 (instance inherits). Old FMS_Idle/FMS_Blessing untouched = rollback (set the defaults back).
- BP_MonkVideoScreen TickPlay: (1) Screen visibility = IsPlaying OR GameTime>2 s -> holds last frame between files (MT_MonkVideo AutoClear already false) instead of hiding; (2) end-detect guard 5.0 -> 1.0 s (the 5 s guard was longer than the 4 s idle -> 1 s blank/freeze per loop). Undo: reconnect IsPlaying->bNewVisibility, delete the 2 nodes at (-300/-120,13420), B=5.0.
- Floor-line: MI_UbLook_TeakFloor Tint (0.55,0.42,0.34)->(0.62,0.42,0.31), Brightness 0.34->0.38, RoughnessMul 0.55->0.45; MI_VideoScreen_Ubosot Brightness 1.1->1.0.
- PIE per-frame monitor: idle loops every ~4.07 s, reopen 20-40 ms with vis=1 the whole time; PlayBlessing waited for idle end -> blessing+bridge 12.6 s -> idle. 0 hidden frames. Shots: ubosot_v3_ingame.png, ubosot_v3_blessing.png.
- T_MonkV2_Preview asset removed (no referencers).

## UBOSOT-LOOK v4 - screen fills back wall, VP locked to eye (2026-11-11)
- Screen plane = whole back wall: 389.0 x 327.2 cm (x -84.5..304.5, z 178.6..505.8), actor (110, -3650, 342.2), root scale 1, Screen rel scale X 3.272 / Y 3.89. y -3650 = 1 cm in front of the baseboard (wall y -3655, baseboard to y -3651, top z 192, which was hiding the mat at y -3654).
- M_VideoScreen_Blend rebuilt (ub15.py): new params ClipScaleH/ClipOffsetH/ClipScaleV/ClipOffsetV (screen UV -> clip UV, defaults identity) + ClipEdgeSharpness (soft fade at clip edges inside the screen). Screen-edge fades unchanged.
- MI_VideoScreen_Ubosot: clip 4:3 at 349.33 x 262 cm, VP (0.4865, 0.4875) pinned to (x 110, z 278.4 = eye). ClipScaleH 1.11355, ClipOffsetH -0.07027, ClipScaleV 1.24885, ClipOffsetV 0.10658. Clip covers x -59.9..289.4, z 150.7..412.7; below-floor part cropped by UV; the side gaps (25/15 cm) and the 93 cm above fade into wall/ceiling.
- Tradeoff: covering the full wall with VP at the eye needs a 444 cm tall clip, which pushes the mat/feet under the floor. Chose the largest size that keeps the mat above the floor (mat bottom ~z 184.5).
- MI_UbLook_TeakFloor UVSwap 0 -> 1: floor planks now run toward the screen (along Y, vertical in view).
- Shot: ubosot_v4.png (+ ubosot_v4_vpcheck.png overlay on the box).

## UBOSOT-LOOK v5 - background plate, no real back wall visible (2026-11-11)
- Plate: /Game/ThaiTemple/UbosotLook/T_UbLook_BackPlate (1069x899 = wall 389x327.2 cm at clip scale 2.748 px/cm, clamp, never stream). Made from an AI outpaint of anchor_v3_bless_first.png, aligned to the frame with SIFT + similarity (155 inliers, ~1 px residual), colour-gain matched in the edge band (BGR gain .763/.93/.981, residual <1/255). The real frame is pasted at its mapped rect (plate px 67,256) with a 48 px feather. Source on box: /workspace/ubosot_look/T_UbLook_BackPlate.png.
- M_VideoScreen_Blend (ub16.py): PlateTex sampled at screen UV (v, 1-u) = whole wall; video at the clip UV; Lerp(plate, video, smoothstep(clip edge fade, ClipEdgeSharpness 10)) -> grading -> emissive. Opacity = screen-edge fade only.
- MI_VideoScreen_Ubosot: EdgeSharpness 80, BottomSharpness 80 (~4-5 cm blend into the real side walls/ceiling/floor). Clip mapping unchanged: VP at eye (110, 278.4), clip 349.33 x 262.
- Screen unchanged: 389 x 327.2 cm, actor (110, -3650, 342.2), scale 1. PIE v5: no real back-wall wood visible; clip edges melt into the plate.

## DHARMA SALA - carved teak screen + single "ฟังธรรม" button (2026-11-11, not committed)
- Clips (Point approved 7:35): Content/Movies/MOV_Sala_Idle.mp4 (6.5 MB) + MOV_Sala_Sermon.mp4 (50 MB, ~39 s). FMS_SalaIdle / FMS_SalaSermon (/Game/ThaiTemple/Sala/Media) repointed to them. Light version not needed.
- Carved screen: SM_SalaCarvedScreen (Blender on box: /workspace/sala_look/blender/build_screen.py), Lanna teak frame as real geometry, carving via sala PBR textures (MI_SalaScreen_TeakDark/Gold/CarvedTeak/CarvedBand, parent M_SalaDharma_PBR_V3). Actor Sala_CarvedScreen at (-2990,-1890,270) yaw -90 standing on the chukchi dais (top z270). Overall 304.4 x 304 cm; opening 224 x 168 cm, bottom = dais level.
- Video: Sala_MonkScreen (BP_SalaMonkScreen = child of BP_MonkVideoScreen; Player MP_SalaVideo, MT_SalaVideo, M/MI_SalaVideoScreen unlit). Actor (-2998,-1890,354) yaw 180; plane 228 x 171 cm (4:3), x -2998, y -2004..-1776, z 268.5..439.5. ~5 cm of each clip edge hidden behind the frame; visible ~217 x 164.5; clip floor line sits on the dais (lower part hidden by the altar from the Dharma point). VP not matched to the eye (life-size priority).
- Button: DharmaPoint MenuWidget on lectern Sala_ListenStand (SM_SalaButtonStand) at (-2560,-1890,171); widget world (-2558.6,-1890,246.5) pitch 20, draw 440x170, transparent bg, scale 0.1. WBP_DharmaPopup BuildPopup: title blank, single entry "ฟังธรรม", gold bg (.85,.58,.16), dark-brown text, RenderScale 1.8, No entry removed. BP_DharmaPoint ShowPopup no longer calls PlaceAtHead (widget stays on the lectern). Laser + trigger/LMB same as before.
- Flow (PIE verified): idle loop -> press -> button hides -> sermon starts at end of current idle loop (~1.6 s) -> 39 s -> back to idle; button stays hidden until the player leaves and re-arrives.
- Light: Sala_Light_Rect (RectLight 25 cd 2700K at (-2760,-1890,560)), Sala_Light_Candle (PointLight 4 cd 2200K at (-2880,-1890,320)). Sala model/roof untouched.
- Shots: /workspace/sala_look/sala_v1.png, sala_v1_pressed.png, sala_v1_after.png (box).
- Leftover: /Game/ThaiTemple/Sala/Meshes/SM_SalaListenStand (100x scale, unused; delete failed) - safe to delete.
- Undo: delete actors in outliner folder ThaiTemple/Sala; DharmaPoint VideoScreen back to the ubosot screen; reconnect ShowPopup Delay -> PlaceAtHead and BuildPopup Set YesEntry.then -> No entry.
## DHARMA SALA v2 - bigger monk, wheel emblem removed, raised above altar (2026-11-11, not committed)
- Measured clip (960x720): head top y192, seat/cushion top y597, cushion bottom y627, mat front y640. In v1 the 171 cm plane meant 0.2375 cm/px, so the monk was 96 cm sitting height (103 cm head-to-cushion-bottom). That is already physically life-size but read small from 6.5 m behind the altar.
- Carved screen v2 (Blender build_screen.py; old v1 script kept as build_screen_v1.py on the box):
  - New 35 cm teak plinth (carved band, gold foot, gold bead) under the opening, plus a gold bottom sill (MoldB 3.5 cm).
  - Opening is still 224 x 168 cm but now sits at z305..473. Frame is 304.5 x 339 cm at (-2990,-1890,270) yaw -90, top z609.
  - Dharma wheel removed. Replaced by a small Thai flame-lotus emblem (phum khao bin style): carved-band flame with a gold rim, gold inner flame and 3 gold lotus petals.
  - Mesh reimported in place (scale 1, same 4 MI slots, box collision).
- Video: Sala_MonkScreen z 354 -> 389.
  - Plane 228 x 171 cm at x -2998, y -2004..-1776, z 303.5..474.5. Clip edges overlap the frame by about 5 cm on every side; visible area is about 217 x 161 cm.
- M_SalaVideoScreen: new params ClipScale / ClipOffsetU / ClipOffsetV (clipUV = (v,1-u)*ClipScale + offset).
  - MI_SalaVideoScreen: ClipScale 0.8333 (1.2x zoom), ClipOffsetU 0.0573, ClipOffsetV 0.1444, so it shows clip px x55..855, y104..704 at 0.285 cm/px.
  - Monk now: sitting height 115 cm, head-to-cushion-bottom 124 cm (1.2x life-size, per Point "too small"). Head z449, seat z334, cushion bottom z325.
- Occlusion: altar items (Props) top z320. From the eye (-2345, z331.3) their shadow line on the screen plane is at most z~317, so the cushion bottom (z325) clears with ~8 cm. Mat front ~z322 is just visible. No altar change.
- PIE: idle -> press -> sermon at idle end (1.5 s) -> 38 s -> idle; button stays hidden. Shots on the box: /workspace/sala_look/sala_v2.png, sala_v2_pressed.png.
- Undo:
  - Set Sala_CarvedScreen mesh to /Game/ThaiTemple/Sala/Meshes/SM_SalaCarvedScreen_v1 (backup of v1).
  - Set Sala_MonkScreen z back to 354.
  - Set MI_SalaVideoScreen ClipScale 1, offsets 0.
  - For strict life-size only: keep v2 and set ClipScale 1 + offsets 0. That gives 96 cm sitting, cushion bottom z325.6, still clear of the altar.
## DHARMA SALA v3 - "F" shortcut for ฟังธรรม + glowing button (2026-11-11, not committed, NOT PIE-tested - Point tests)
- Existing keys: BP_TeleportMenuActor InputKey 1-4 = teleports (Ubosot / Dharma / viewpoint / fish), M = menu, all with bConsumeInput=False and working from anywhere. VRGK non-VR handler uses W A S D Q Space LShift LCtrl and the mouse. Legacy input maps are Oculus controllers only. No VR-controller shortcut convention exists (the menu uses PlayerMenu / ButtonLeft / TriggerRight actions).
- New key: F ("Fang"), set up like 1-4. BP_DharmaPoint (AutoReceiveInput=Player0) has InputKey F (bConsumeInput=False) -> Branch(bPopup) -> the same Hide Popup -> Cast VideoScreen -> PlaySermon chain as the laser/trigger press (no haptic).
  - Scope: works only while the ฟังธรรม button is showing (player near the sala and not yet pressed), exactly like the button. Dev flow from anywhere: press 2, then F.
- Glow:
  - Sala_ButtonGlow is a StaticMeshActor (/Engine/BasicShapes/Plane, 40 x 70 cm) at (-2557.8,-1890,246.8), pitch -70. It sits 0.8 cm in front of the widget, parallel to it. Hidden in game by default.
  - Material MI_SalaButtonGlow / M_SalaButtonGlow: unlit, additive, two-sided, emissive only (lights nothing; additive, so it cannot cover the monk).
    - Emissive = GlowColor (1, .62, .18) x T_SalaButtonGlow halo mask x Intensity 2.5 x calm pulse.
    - The mask is 0 inside the button and fades out over ~3 cm around a 18 x 45 cm rect.
    - Pulse goes from PulseMin 0.45 to 1 at PulseSpeed 0.22 (~4.5 s per cycle), sine, no flashing.
    - Hover: lerp to HoverBoost 1.9 by param Hover.
  - BP_DharmaPoint: new instance var GlowActor = Sala_ButtonGlow. ShowPopup -> SetActorHiddenInGame(false); HidePopup -> SetActorHiddenInGame(true). TickPopup -> GlowActor.StaticMeshComponent SetScalarParameterValueOnMaterials("Hover", YesEntry.IsHovered).
- Leftover (harmless, unused): MPC_SalaButton + var GlowMPC + 4 "Set Scalar Parameter Value (Collection)" nodes. The MPC route did not drive the material, so the material no longer reads the MPC. Safe to delete later.
- Tuning: MI_SalaButtonGlow Intensity / PulseMin / PulseSpeed / HoverBoost / GlowColor. If the halo is off-size, regenerate T_SalaButtonGlow (box: /workspace/sala_look/T_SalaButtonGlow.png) or scale the actor.
- Undo:
  - Delete actor Sala_ButtonGlow.
  - In BP_DharmaPoint, delete the InputKey F + Branch, the 2 SetActorHiddenInGame nodes, the SetScalarParameterValueOnMaterials node and the 4 MPC nodes. Reconnect Set World Transform.then -> Branch(44A903), remove the vars GlowActor / GlowMPC, and compile.
  - Optionally delete M/MI_SalaButtonGlow, T_SalaButtonGlow and MPC_SalaButton.