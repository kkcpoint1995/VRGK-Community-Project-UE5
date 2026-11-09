# Rules — Thai Temple VR

Hard constraints. Prefer this file over scattered chat memory.

## Project boundary

- Only `C:\WORK\VRGK-Community-Project-UE5` / `VRGK.uproject` (UE 5.3).
- Never modify **RAINBOWGATE**.
- Never close, kill, or restart UnrealEditor unless the process command line contains `VRGK.uproject`. Never touch **LUCID**.
- Before editor work: `GET http://127.0.0.1:18090/api/status` must report **VRGK**. Otherwise stop.

## Git

- Commit only when Point explicitly authorizes **that** change set.
- Never push unless Point asks.
- Never force, reset, stash, or amend unless Point asks.
- Stage **only your own** files. If other staged files exist, commit path-limited to yours without unstaging theirs.
- Prefer commit body: **Changed** / **Why** / **Tested**.
- Unsaved or dirty Unreal assets are not "done" in git until committed; do not invent completion from PIE alone.

## Product / content

- Calm single-player seated Meta Quest 2 experience for bedridden patients.
- Free assets only. No startling sounds.
- No modelled monks or Buddha statues. Use **moving video** (idle / blessing / sermon). **No still-image fallback.**
- Carving and ornate surface detail: textures + normal maps, not high-poly geometry.
- Keep gameplay systems as **separate Blueprints**.
- Antigravity CLI is paused; use mcp-unreal and executors for editor automation.

## Player / interaction (standing decisions)

- Single warp / seated spot in ubosot centre. Seated eye height ~1 m. Position locked; free look; hands may warp and grab.
- Monk screen: idle video must always show the monk. After offering, queue blessing at end of current idle loop, then return to idle. Sermon uses the same end-of-loop policy.
- When placing offerings: stand upright (no tilt).

## Working style

- Grill Point with a few short questions before building; approve the plan first.
- Speak Thai with Point unless he writes English.
- Label reported/historical facts that were not re-verified this session.
- Do not start backlog items without Point's go-ahead.
