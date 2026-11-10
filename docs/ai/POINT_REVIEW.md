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
