# Mobile UI and input specification

## Orientation and layout

- Portrait-only launch layout; safe area, notches, variable aspect ratios and touch target sizes tested. Fixed-angle top-down 3D camera, pan only; no pinch zoom/rotation. No automatic follow after manual pan; double-tap empty map returns view to hero.
- Gameplay HUD: world/hero view, objective/time, HP (combined only), CB/equipment indication, currencies, current hand near thumb reach, queue along side displaying first 3 plus `+N` for hidden count (omit if <=3). Queue FIFO ordering visually obvious.
- Visual hierarchy prioritizes where hero is going, valid tile drops, enemies' facing, trap timing and next queue reward. Avoid dense popups during live run.

## Gesture arbitration

1. Pointer on HUD/control begins that UI interaction and never pans camera.
2. Drag from a tile in hand enters placement preview; each frame highlight all compatible empty cells >=2 Manhattan away using live hero position.
3. On release revalidate authoritative current cell, occupancy and distance; invalid returns tile intact to hand.
4. Touch down on noninteractable empty map + movement over configurable threshold pans camera; short tap does nothing; double tap on empty map recenters hero.
5. Tap unactivated placed action tile selects for drag/retrieve; retrieval queues tile, never injects directly into hand.
6. Multi-touch behavior is explicitly constrained (ignore second finger for gameplay in MVP); always cancel drags safely on focus loss/gesture cancellation.

## Camera and views

- Fixed orthographic or near-orthographic angle for readability (final choice OPEN), fixed initial yaw, configured bounds and smooth pan; future yaw architecture can exist without UI control.
- Show facing arrows, height (future), current route preview and action tile ownership clearly. During combat <=0.5s animation without stopping timer.
- Screens: boot, title, mode selection, level selection/objectives, gameplay, pause/settings (UI pause policy OPEN because world time never stops during gameplay), success, death/summary, unlocks, store/consent if enabled.

## Accessibility and desktop portability

- Color + shape/icon redundancies, legible text, scalable UI, reduced flashes/camera motion options, audio independent cues, language fallback. Test touch with one hand and left/right-handed hand placement as options.
- Unity's New Input System is the shared input layer for touch, desktop pointer and gamepad focus/selection; do not hardwire game logic to touchscreen gestures or device-specific APIs.
- No misleading interaction (e.g. highlighting a cell that becomes invalid after release); failure must be explained visually and without losing tile.
