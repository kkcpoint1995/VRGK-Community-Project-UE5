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
