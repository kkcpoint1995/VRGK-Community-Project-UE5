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

- **Machine Shell double-run**: 2026-11-10 - the PC Shell tool sometimes runs a command twice at once; guard every mutating step with a lock file (`[IO.File]::Open(lock,'CreateNew')`) and re-read result files.
- **T3D import_text via mcp**: 2026-11-10 - works for building BP graphs. Exec links and CallFunction-to-CallFunction data links survive with partial pins. **Variable get/set data links, struct/byte defaults and object defaults do not** (the pin gets orphaned) - make those with `connect_pins` / `set_pin_value` after import; object defaults -> use a variable set on the CDO. `bIsPureCast` is ignored -> use impure casts in the exec chain. Self-call to a custom event imported in the same paste has no pins -> import the call in a second job.
- **PowerShell alias clash**: 2026-11-10 - a script function named `R` is shadowed by the built-in alias `r` (Invoke-History). Use unique names.
- **set_pin_value does not dirty the package**: 2026-11-10 - save with `EditorAssetLibrary.save_asset(p, only_if_is_dirty=False)` or the change is lost.
- **Two BP_TeleportPoint classes**: 2026-11-10 - VRGK has its own BP_TeleportPoint_C actors in Map_Ford; filter by class path `/Game/ThaiTemple/` before editing.
- **Camera manager lag after warp**: 2026-11-10 - PlayerCameraManager location was stale after TeleportVRPawnWithRotation (popup placed at old spot). Use the pawn `Camera` component instead.
- **Seated stand-up after warp tips pawn**: 2026-11-10 - BP_SeatedSpot raising VROriginRoot on the first tick after a long warp made the physics pawn climb/tilt (fish pier z 860). Fix: menu calls `ForceStand` during the fade, before the teleport.
- **Log check**: 2026-11-10 - mcp output_log keeps only 100 entries; read new bytes of `Saved/Logs/VRGK.log` for `[Compiler]` lines instead.
- **White screen from 2nd PIE (BUG-WHITE)**: 2026-11-10 - whole view white from the 2nd Play in an editor session, fixed by editor restart. Cause: 4 *unbuilt* SphereReflectionCaptures (RC_River/Pier/Wihan/Pagoda) in Map_Ford; in the editor (forward shading) PIE re-uploads their cached capture data, which is invalid after the first PIE -> bad reflection values everywhere -> **bloom** smears them over the whole screen. Bisect that found it: `ShowFlag.Bloom 0` / `ShowFlag.ReflectionEnvironment 0` / `r.ReflectionCaptureUpdateEveryFrame 1` each removed the white; exposure, LUT, fade, MPC, cvars were all normal. Fix: hide captures (Visible=false) or build them (Build > Build Reflection Captures; BuiltData is gitignored so every machine must rebuild). Never leave reflection captures unbuilt in Map_Ford.
- **ShowFlag console overrides persist across PIE**: 2026-11-10 - `ShowFlag.X 0/1` stays for later PIE sessions in the same editor; reset with `ShowFlag.X 2` (default).
- **PIE window capture without stealing focus**: 2026-11-10 - Win32 `PrintWindow(hwnd, dc, 3)` on the "VRGK Community Project Preview" window grabs the PIE frame even when it is behind other windows; mean luminance ~227 = white-out, ~132 = normal at spawn.

- **PIE frozen when editor minimized**: 2026-11-10 - with the main VRGK editor window minimized, PIE world time stays 0 (nothing ticks); 'Use Less CPU in Background' throttles to 3 fps and physics explodes. Restore with ShowWindow(SW_SHOWNOACTIVATE) + set bThrottleCPUWhenNotForeground False while testing; restore after.
- **No-clip VRGK rig**: 2026-11-10 - ignore-all responses on a simulating rig make it fall/explode; make rig bodies kinematic (SetSimulatePhysics false), attach to FenderSphere, then ignore-all.
