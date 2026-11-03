# Agent rules (VRGK / Thai Temple VR) - MUST follow
- Work ONLY on VRGK (C:\WORK\VRGK-Community-Project-UE5\VRGK.uproject), UE 5.3, mcp-unreal on port 18090.
- FIRST call the mcp-unreal status (/api/status, http://127.0.0.1:18090/api/status) and confirm it reports VRGK. If not VRGK, STOP and report.
- NEVER close/kill/restart any UnrealEditor process unless its command line contains VRGK.uproject. Never touch the LUCID editor.
- NEVER modify anything named RAINBOWGATE.
- NEVER git commit, push, reset, or stash.
- Do only what the prompt asks; if the prompt says read-only, make no changes and do not save.
- Final reply: short plain summary (<= 15 lines): what you did, results, errors. No long logs.

# How to work fast
- First list the mcp-unreal tools once and make a short plan before acting.
- Prefer batch operations: for Blueprint graphs use the plugin's import_text (paste a whole graph as T3D/clipboard text) or ONE editor Python script (execute python via the MCP) instead of node-by-node add/connect calls. For level edits, one Python script per batch.
- Don't re-verify after every call; verify once at the end (compile + save results).
- Reuse existing BPs as templates: /Game/ThaiTemple/FishRelease/BP_FishBucket, /Game/ThaiTemple/Sanghatan/BP_SanghatanOffering, BP_SanghatanTarget, /Game/VRGK/Demo/Blueprints/Interactables/BP_Crowbar.
- Known pitfalls: float pin defaults crash the BP compiler (use literal nodes or set them in Python); editor Python cannot build Sound Cue graphs.
- If a task is big, finish and save in chunks, and ALWAYS end with a short summary even if incomplete.
- Reply in 15 lines or fewer.