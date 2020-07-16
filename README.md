# Chopper-Drop
Chopper Drop is a Exiled plugin which emulates a Chopper Dropping supplys for MTF (or really anyone who is up there). This could be really useful for RP Sites/Gameplay sites. Really anything.

## How to Use / Configure
**Step 1:** Download the latest release from [here](https://github.com/KadeDev/Chopper-Drop/releases/latest)

**Step 2:** Place into your plugins folder.

**Step 3:** Configure it (see the section below)

**Step 4:** PROFIT!

## Config
Default Config
```yml
CD: !ChopperDrop.Config
  is_enabled: true
  chopper_items:
    GunE11SR: 1 # This works like this:
    Medkit: 3   # Item Name (Found in #resources): Amount
    Adrenaline: 2
    Ammo762: 2
  chopper_time: 600
  chopper_text: <color=lime>A supply drop is at the surface!</color>
```

## Functionality
how does this work?

You might ask, well heres how.

Every 10 (by default) minutes it calls a chopper, when it lands (it lands where mtf spawn) it will spawn in all the items you configured!

That's how :D

## Item Names
Found in the exiled discord. https://discord.gg/C4fMYF