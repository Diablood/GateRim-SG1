#!/usr/bin/env bash
set -euo pipefail

MAIN_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
WIKI_ROOT="${1:-"$MAIN_ROOT/../GateRim-SG1.wiki"}"

if [[ ! -d "$WIKI_ROOT/.git" ]]; then
    echo "Wiki repository not found: $WIKI_ROOT" >&2
    echo "Expected a sibling clone named GateRim-SG1.wiki." >&2
    exit 1
fi

cp -R "$MAIN_ROOT/docs/wiki/." "$WIKI_ROOT/"

echo "Wiki drafts copied directly to: $WIKI_ROOT"
echo "Review changes with:"
echo "  cd "$WIKI_ROOT" && git status"
