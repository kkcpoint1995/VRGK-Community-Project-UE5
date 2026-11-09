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
