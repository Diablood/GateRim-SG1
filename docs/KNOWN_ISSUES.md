# Known issues

## Deferred fixes

### Tok'ra relay sabotage site: mountain-roof collapse

- **Affected content:** Goa'uld relay sabotage mission site.
- **Observed behavior:** the generated relay structure can be placed beneath an
  overhead mountain. When the map is entered, the mountain roof above the
  structure can collapse immediately.
- **Expected behavior:** completed structural walls and other valid roof-supporting
  cells should prevent the roof from collapsing, consistent with vanilla
  building behavior.
- **Likely investigation areas:**
  - site-placement rules allowing the structure beneath overhead mountain;
  - generation order between roof assignment, structure spawning and roof-collapse
    validation;
  - whether generated walls/support cells are fully spawned and registered before
    the first roof-collapse check;
  - whether the structure should instead reject unsuitable mountain-covered
    locations.
- **Required regression test:**
  1. generate the relay sabotage site repeatedly, including mountain tiles;
  2. enter the generated map;
  3. confirm that no supported roof collapses on arrival;
  4. confirm that unsupported overhead mountain still follows normal vanilla
     collapse rules;
  5. confirm that mission generation and sabotage interaction remain unchanged.
- **Status:** deferred; not part of the validated relay-control-node visual change.
