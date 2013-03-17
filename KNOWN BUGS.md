-- Please update this file on new commits.

Known Bugs (Features?) in this Build
========================


Josh:

- Sprites don't work! Who's in charge of this shit?
- In very specific instances, MAT button /doesn't/ work properly. With materials in hand (9 at time of testing), meter decreases to 0 but no materials drop. No idea what the specifics of it happening were, but it lasted until I stopped moving.
- Clearly visible (to the correct player's camera object) base zones, so a player isn't confused when they can't drop a base
- If you get really close to the other player and drop materials, there's a good chance of trapping them underneath one; they can shoot, but can't hit the material and can't move. (Feature? The problem is, a player who's a dick can just go kill their base first before coming back for the kill, and the trapped player has to hope a zombie comes for them while they sit there just getting annoyed.)
- Not a bug per se, but testing suggests controls can be awkward; subject would prefer a pair of close keys for shoot/material e.g. N,M; the use of SPACE for Player 1 makes Player 2 cramped on the left side. (Right Shift to shoot and Right Ctrl for MAT?)
- If a zombie sees a player object approaching a base that it's currently attacking, it should always switch and stay attracted to the player. Right now zombies don't know what-the-fuck.
- Zombies seem to occasionally have blind spots -- they stop chasing and seem to immediately move in a different direction from the player at a slower speed, until the player re-aggros them; other nearby zombies stay on the player
- Multiple audio listeners?



Suggested Features
========================

- ability to heal base by 10 points for a large number of materials (MAT button next to base); helps mitigate random zombie attacks; placing materials around the base helps but is tedious
- ability to change drop type; tester wanted to place materials in his zone with his base destroyed to defend against an immediate attack by me
- ability to drop materials while shooting