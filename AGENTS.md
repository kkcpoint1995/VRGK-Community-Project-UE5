# Agent entry — Thai Temple VR (VRGK)

**Canonical instructions:** [AI_AGENT_INSTRUCTIONS.md](AI_AGENT_INSTRUCTIONS.md)
**Rules / tasks / handoff:** [Docs/ai/](Docs/ai/)

Repo `.md` is the source of truth (not bot memory). Read the canonical file at session start; update Docs/ai when finishing work.

## Hard rules (must follow)

- Work ONLY on VRGK (`C:\WORK\VRGK-Community-Project-UE5\VRGK.uproject`), UE 5.3, mcp-unreal port **18090**.
- FIRST call mcp-unreal `/api/status` (`http://127.0.0.1:18090/api/status`) and confirm it reports **VRGK**. If not VRGK, STOP and report.
- NEVER close/kill/restart any UnrealEditor process unless its command line contains `VRGK.uproject`. Never touch the LUCID editor.
- NEVER modify anything named RAINBOWGATE.
- Commit **only** when Point explicitly authorizes that commit. Never push / reset / stash / amend unless he asks. Stage only your own files.
- Do only what the prompt asks; if read-only, make no changes and do not save.
- Final reply: short plain summary (what you did, results, errors / unverified items). No long logs.

## How to work fast

- List mcp-unreal tools once and make a short plan before acting.
- Prefer batch operations: Blueprint `import_text` / one editor Python script per batch for level edits.
- Don't re-verify after every call; verify once at the end (compile + save).
- Reuse existing BPs as templates: `/Game/ThaiTemple/FishRelease/BP_FishBucket`, `/Game/ThaiTemple/Sanghatan/BP_SanghatanOffering`, `BP_SanghatanTarget`, `/Game/VRGK/Demo/Blueprints/Interactables/BP_Crowbar`.
- Known pitfalls: float pin defaults can crash the BP compiler (use literal nodes or set in Python); editor Python cannot build Sound Cue graphs.
- If a task is big, finish and save in chunks; ALWAYS end with a short summary even if incomplete.
- Reply in a short summary unless Point asks for detail.
