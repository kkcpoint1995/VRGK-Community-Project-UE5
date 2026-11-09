# AI Agent Instructions — Thai Temple VR (VRGK)

**Repo markdown is the source of truth.** Do not rely on bot-only memory to resume. When Point says "remember", write it into `Docs/ai/` (DECISIONS / RULES / LESSONS / TASKS), not only assistant memory.

Project: `C:\WORK\VRGK-Community-Project-UE5` · `VRGK.uproject` · UE 5.3 · map `Map_Ford`
Point PC machineId: `c069af01-dfcc-42cf-a787-3b19f6ba0f39` · timezone Asia/Bangkok (UTC+7)

## Startup (every session)

1. Read this file.
2. Read your role section in [Docs/ai/ROLES.md](Docs/ai/ROLES.md).
3. Read current [Docs/ai/TASKS.md](Docs/ai/TASKS.md) and [Docs/ai/PROGRESS.md](Docs/ai/PROGRESS.md).
4. Read only the **latest** entry in [Docs/ai/HANDOFF.md](Docs/ai/HANDOFF.md) (tail / last heading). Do not load the whole file.
5. Search [Docs/ai/RULES.md](Docs/ai/RULES.md), [Docs/ai/DECISIONS.md](Docs/ai/DECISIONS.md), [Docs/ai/LESSONS.md](Docs/ai/LESSONS.md) by keyword when relevant.
6. Run `git log --oneline -10`. Git tracks **committed** work only — unsaved / dirty UE assets are not in git history.

## Hard rules (summary)

Full text: [Docs/ai/RULES.md](Docs/ai/RULES.md).

1. Work only on VRGK. Never touch LUCID or anything named RAINBOWGATE.
2. Before editor work: confirm mcp-unreal `http://127.0.0.1:18090/api/status` reports **VRGK**. If not, stop.
3. Never close/kill/restart an UnrealEditor process unless its command line contains `VRGK.uproject`.
4. Commit **only** when Point explicitly authorizes that commit. Never push unless Point asks. Stage only your own files; leave others' staged/unstaged work alone.
5. Free assets only. No startling sounds. Calm single-player seated Quest 2 experience for bedridden patients.
6. No modelled monks or Buddha statues — use **moving video** only (no still-image fallback).
7. Carved detail = textures + normal maps, not geometry.
8. Keep systems as separate Blueprints.
9. Antigravity CLI is paused; prefer mcp-unreal + executors for editor work.
10. Grill Point with short questions before building; get plan approval first.
11. Speak Thai with Point unless he writes in English.
12. End replies with a short plain summary of what changed, what was verified, and what was **not** verified.

## Environment checks (when doing editor / local PC work)

```powershell
# On Point PC — confirm mcp-unreal target
Invoke-RestMethod http://127.0.0.1:18090/api/status
# Expect project/name indicating VRGK

# Git (committed truth only)
git status --porcelain
git log --oneline -10
```

On Windows, machine-targeted **Shell** is the normal way to read/write project files under `C:\WORK\...`. Built-in Read/CopyToBox/CopyFromBox may refuse paths outside the user profile (`allowed local-exec root`).

## Task → read which file

| Task | Read |
|------|------|
| Any session start | this file + TASKS + PROGRESS + latest HANDOFF |
| Ownership / who does what | ROLES |
| Hard constraints / safety | RULES |
| Past Point choices | DECISIONS |
| Pitfalls / known bugs | LESSONS (keyword search) |
| Map / BP / content layout | ARCHITECTURE |
| Low-confidence bot calls awaiting Point | POINT_REVIEW |
| About to commit | PRE_COMMIT_CHECKLIST |
| Session end | append HANDOFF; update TASKS/DECISIONS/LESSONS as needed |

## Finish checklist

1. Update TASKS (status, next step, verification actually run).
2. If Point decided something → DECISIONS (date Bangkok UTC+7).
3. If you decided with low confidence → POINT_REVIEW (include how to revert).
4. New pitfall → LESSONS (search first; keep short: problem / cause / fix).
5. Append one HANDOFF entry (Problem / Fixes / Verification / State / Limitations).
6. Commit only with Point's OK for **this** change set. Body: Changed / Why / Tested. No push unless asked.
