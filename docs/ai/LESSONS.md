# Lessons — Thai Temple VR

Short searchable notes. Search before adding. Format: **Topic**: date — problem / cause / fix.

- **mcp-unreal target**: always — confirm `/api/status` is VRGK before editor Python; wrong project risks LUCID/other.
- **LUCID process**: always — never kill UnrealEditor unless cmdline contains VRGK.uproject.
- **Import scale**: 2026-11 (reported) — FBX sometimes imports at 1/100; fix scale after import; sala and sanghatan hit this.
- **Sanghatan pivot**: 2026-11-08 (reported) — MeshBag uses **centred** pivot to match existing BP; upright snap scripted; hand grip point may need adjust after headset grab test.
- **Media UV / material**: 2026-11-08 (reported) — upside-down/stretch traced to screen material UV / failed compile (default material); fix UV + ensure material compiles; aspect matched 1280×1072 vs screen 390×327.5.
- **Idle must show monk**: 2026-11 — empty wall / still fallback rejected by Point; keep video idle always.
- **Blessing timing**: policy — queue at end of current idle loop, not instantly mid-loop.
- **Offering upright**: 2026-11 — lower COM / reasonable mass (reported ~2 kg, COM lowered) to reduce tip; snap upright on place.
- **Net shimmer**: 2026-11-08 (reported) — opacity mask mips off may shimmer in VR; alpha-to-coverage needs MSAA.
- **Float pin defaults**: AGENTS — float defaults on BP pins can crash compiler; use literals or set in Python.
- **Sound Cue in Python**: AGENTS — editor Python cannot build Sound Cue graphs.
- **Local-exec root vs Shell**: 2026-11-08/09 — Read/CopyToBox/CopyFromBox may refuse paths outside Windows user profile; Always Allow does not widen that root. Machine-targeted Shell remains the normal tool for `C:\WORK\...` under Point's local-execution policy. Do not treat Shell as a bypass of an Auto-review denial.
- **PROGRESS vs git**: PROGRESS lags; `git log` is truth for commits; dirty UE assets are invisible to git.
- **HANDOFF size**: read tail only; archive monthly if large.
