The worlds are filled using the following procedure:

- Assume all progression items are acquired.
- Progression items (non-dungeon) and item locations are shuffled respectively.
- Items are placed one by one. Dungeon items are placed first, followed by all
  other progression.
  - An item is placed at the next random location where the item is allowed,
    and where the player can reach it with their remaining progression
    according to the logic.
  - An item can only be placed cross world if the owning player can reach the
    same location in their world with their current progression *including* the
    item to be placed.

Some bias is applied based on game mode. For multiworld Morph Balls are placed
within the last 20% of the pool, and Moon Pearls within the last 40% (which
makes them show up earlier).

For singleworld, first sphere locations in Link to the Past are weighted down
significantly, and Green, and Pink Brinstar are weighted down slightly.
