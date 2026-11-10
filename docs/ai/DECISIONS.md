# Decisions — Point

Dated Asia/Bangkok (UTC+7). Append only. Prefer facts Point stated; label inferred items.

## 2026-11-08 and earlier (reported / historical)

- **No commit / no push by default** — Point: "อย่าพึ่ง commit". Exception: individual authorizations (e.g. docs methodology commit).
- **Do not close LUCID** — only VRGK UnrealEditor processes.
- **Do not modify RAINBOWGATE.**
- **No modelled monks / Buddha statues** — video only; **no still-image fallback** ("ไม่เอาภาพนิ่งนะ").
- **Carving** via normal maps / textures, not geometry.
- **Systems stay separate BPs.**
- **Antigravity CLI paused** — mcp-unreal + executors.
- **Grill before build**; Point approves plans first.
- **Seated spot:** single ubosot centre warp; ~1 m seated eye; lock position; free look / hands.
- **Sanghatan:** reachable while seated; prefer side placement; when placed **stand upright** ("ตั้งตรงๆ").
- **Monk media policy:** idle always shows monk; offering queues blessing at end of current idle loop, then idle; sermon same.
- **Dummy clips first** for Point self-test before final generation.
- **Depth framing** for monk shot (not flat on wall); hide screen edges.
- **Free assets only; no startling sounds.**
- **Repo md as memory** (2026-11-08): apply CW-style project markdown; remember → write Docs/ai, not bot-only memory. Commit docs locally, do not push (authorized for that docs commit only).

## 2026-11-10 (UTC+7) - Point (via PM) - UX-001 8-step plan approved
- Decision: implement the 8-step comfort plan; menu follows head **softly/eased**; **delete TeleportPoint_1**; disable left-Y height re-calibration (PlayerMenu) in the new pawn; new pawn = no move/jump/crouch, snap turn forced, pull None, BP_TTHand; trigger OR grip grabs; return-home 3 s; soft UI click ~0.3 volume; bell <= 1.0; rearm events on every warp.
- Context: bedridden-patient seated Quest 2 experience; production polish.
- Commit (if any): none (Point: do not commit).


## Template

```
## YYYY-MM-DD HH:MM (UTC+7) — Point
- Decision: ...
- Context: ...
- Commit (if any): <sha or none>
```

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
