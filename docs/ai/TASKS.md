# Tasks — Thai Temple VR

Update status when work moves. Do not mark Verified in VR unless Point tested on headset.

| ID | Owner | Status | Next step | Verification |
|----|-------|--------|-----------|--------------|
| DOC-001 | PM | Done (this commit) | Keep Docs/ai current each session | `git show` docs paths; links relative |
| VID-001 | Game logic | Reported working in PIE | Point headset check of idle→blessing | PIE loop ok (hist.); **headset pending** |
| SANG-001 | VR tech + Art | MeshBag in BP (reported) | Point grab test in headset; adjust grip if needed | PIE place upright (hist.); **hand grab untested** |
| SEAT-001 | VR tech | Reported in map | Confirm comfort height in headset | PIE only so far |
| MAP-SALA | Map editor | Point reviewing | Wait for Point notes (dark roof / foliage) | Do not start unprompted |
| MAP-UBO | Map editor | Point reviewing | Wait for Point (interior dark / foliage / pad) | Do not start unprompted |
| BUG-WHITE | VR tech | Reported fixed in PIE (uncommitted) | Point: VR Preview 3x in one editor session; if water/pier reflections wanted later, re-enable RC_* + Build Reflection Captures (LIGHT-001) | 2026-11-10: root cause = unbuilt reflection captures (stale data from 2nd PIE on, bloom spreads it to full white); RC_* hidden; 4/4 consecutive PIE normal; **VR Preview pending** |
| FISH-001 | VR tech | Backlog | Grab / tilt / audio / pier — only if Point asks | Untested |
| LIGHT-001 | Map editor | Backlog | Lighting bake — only if Point asks | Pending |
| VID-FINAL | Game logic + Art | Blocked on Point | Real idle/blessing clips after dummy OK | Dummy only so far |
| UX-001 | VR tech | Reported working in PIE (uncommitted) | Point headset checklist in POINT_REVIEW | PIE 2026-11-10: compile 0 errors, menu/warp/rearm/return-home/sermon pass; **headset pending** |
| UX-002 | VR tech | Open | Headset check only: trigger+grip grab, laser dot, haptics/click loudness, left stick dead, comfort of menu follow | Needs Quest 2 |
| UX-003 | VR tech | Reported working in PIE (uncommitted) | Headset check: grab offering/bucket from ~2x previous distance; hand must not grab floor/railings or the wrong item | PIE 2026-11-10: GrabBox 28x22x20 @ x18, MaxDistance 60 at runtime on placed offering+bucket; overlap test reaches item at 30 cm gap (old 10 cm); compile 0 errors; **headset pending** |

## Status legend

`Backlog` · `Open` · `In progress` · `Reported working in PIE` · `Done` · `Blocked`

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

| UX-004 | VR tech | Reported working in PIE (uncommitted) | Headset: float/no-clip body, hands grab normally | 2026-11-10 18:50: ghost rig (kinematic + ignore-all) applied by BPC_TTBodyLock; PIE warps Ubosot/Dharma/TP_3 exact, eye 100/160/159; scripted grab of the offering works; **headset pending** |
| FISH-002 | VR tech | Reported working in PIE (uncommitted) | Headset: pour, then hold upright ~3 s (refills in hand) or let go 3 s (returns home full) | 2026-11-10 19:15: PIE scripted grab+pour+release x3: home + bEmpty false + water/fish visible; held 3 s after pour -> refilled in hand; pour again works |
| UX-005 | VR tech | Reported working in PIE (uncommitted) | Headset: menu laser visible while menu open | 2026-11-10 19:03: BP_TTLaser shows a 3 m beam from the right controller whenever the menu is open; dot only on the widget; beam 6 mm, dot 2.5 cm; PIE: hidden when closed, visible when open |

### FISH-002 v3 — glide-home reset (2026-11-11 04:47 ICT)
- BP_FishBucket Tick then_1 replaced (old in-hand 3s refill + empty-unheld teleport removed).
- After pour (bEmpty): wait 3.0 s (fish land max ~2.55 s after spawn) -> if held, pawn DoPlayerGrabLeft/RightReleased -> StaticMesh SimulatePhysics off -> GlideStart=actor transform -> bGlide.
- Glide: RefillT ms timer, alpha=min(t/1000,1), smoothstep, TLerp(GlideStart,Home) SetActorTransform(teleport) ~1 s.
- New custom event ResetBucket: bGlide=false, RefillT=0, transform=Home, physics on, zero lin/ang velocity, Refill (water+4 fish visible, bEmpty=false). Actor kept (menu RearmBucket ref valid).
- Status: compiled; SAVE PENDING (editor was in PIE, save blocked). PIE test pending.
