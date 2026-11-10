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

## Status legend

`Backlog` · `Open` · `In progress` · `Reported working in PIE` · `Done` · `Blocked`
