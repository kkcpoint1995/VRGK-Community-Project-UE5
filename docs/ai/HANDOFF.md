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
