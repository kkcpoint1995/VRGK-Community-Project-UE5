# Pre-commit checklist

Point must authorize the commit. Default remains: do not commit.

1. [ ] Point asked for **this** commit (not a standing blanket).
2. [ ] `git status` reviewed — know every dirty path.
3. [ ] Stage **only your** files (`git add -- path1 path2`). Do not `git add -A` if foreign dirt exists.
4. [ ] No secrets, credentials, or huge binaries you did not mean to add.
5. [ ] Docs updated if behaviour/decisions changed (TASKS / DECISIONS / LESSONS / HANDOFF).
6. [ ] Commit message body: **Changed** / **Why** / **Tested** (what was actually tested; say headset pending if so).
7. [ ] `git diff --check` clean on what you stage.
8. [ ] After commit: `git show --stat` / `--name-only` matches intent.
9. [ ] **Do not push** unless Point explicitly asked.
10. [ ] Leave other people's staged files undisturbed.
