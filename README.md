[![forthebadge](https://forthebadge.com/images/badges/gluten-free.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/ages-12.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/built-by-codebabes.svg)](https://forthebadge.com)
# Chopper-Drop
Chopper Drop is an Exiled plugin that makes a chopper drop supplies at the surface at the designated time.

## How to Use / Configure
**Step 1:** Download the latest release from [here](https://github.com/KadeDev/Chopper-Drop/releases/latest)

**Step 2:** Place into your plugins folder.

**Step 3:** Configure it (see the section below)

**Step 4:** PROFIT!


## Config
Default Config
```yml
CD:
  is_enabled: true
  chopper_items:
    GunCOM18: 1
    GunE11SR: 1
    Ammo762x39: 1
    Ammo9x19: 1
    Medkit: 2
    Adrenaline: 1
    Coin: 1
  chopper_time: 600
  chopper_text: <color=lime>A supply drop is at the surface!</color>
  # Minimum players on the server to spawn the chopper
  min_players: 2
```

## Functionality
**How does this work? You might ask, well heres how.**
Every 10 minutes (by default) it calls a chopper, when it lands (where MTF spawns) it will spawn in all the items you configured!

## Item Names

<details>
<summary>Ammunition</summary>

| Name | Notes |
| --- | --- |
| Ammo12gauge | |
| Ammo44cal | |
| Ammo556x45 | Quantity is in packs, not bullets. |
| Ammo762x39 | |
| Ammo9x19 | |
 
</details>

<details>
<summary>Weapons</summary>

| Name | Notes |
| --- | --- |
| GunCOM18 | |
| GunE11SR | |
| GunCrossvec | |
| GunFSP9 | |
| GunLogicer | |
| GunRevolver | Weapons spawn without attachments |
| GunShotgun | |
| GunAK | |
| --- | |  
| MicroHID | |
| GrenadeFlash | |
| GrenadeHE | |

</details>

<details>
<summary>Keycards</summary>

| Name | Notes |
| --- | --- |
| KeycardO5 | |
| KeycardFacilityManager | |
| KeycardZoneManager | |
| KeycardResearchCoordinator | |
| KeycardContainmentEngineer | |
| KeycardScientist | |
| KeycardJanitor | |
| KeycardNTFCommander | |
| KeycardNTFLieutenant | |
| KeycardNTFOfficer | |
| KeycardGuard | |
| KeycardChaosInsurgency | |
 
</details>

<details>
<summary>Armors</summary>

| Name | Notes |
| --- | --- |
| ArmorCombat | |
| ArmorHeavy | |
| ArmorLight | |
 
</details>

<details>
<summary>Items</summary>

| Name | Notes |
| --- | --- |
| Radio | |
| --- | | 
| Medkit | |
| Adrenaline | |
| Painkillers | |
| --- | | 
| SCP018 | |
| SCP207 | |
| SCP268 | |
| SCP500 | |

</details>

Also available in Exiled [discord](https://discord.gg/C4fMYF 'Click me!').
