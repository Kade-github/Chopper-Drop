[![Exiled Framework](https://cdn.discordapp.com/attachments/880982483213111356/880982665178808410/developed-for-exiled-3.0.svg)](https://discord.gg/C4fMYF)[![SCP: Secret Laboratory](https://cdn.discordapp.com/attachments/880982483213111356/880984656705630238/for_-scp_-secret-laboratory.svg)](https://scpslgame.com/)

# Supply-Drop
Supply Drop is an Exiled plugin that makes the chopper and car drop supplies at the surface at the designated time.

## How to Use / Configure
**Step 1:** Download the latest release from [here](https://github.com/HeavyWolfPL/Supply-Drop/releases/latest)

**Step 2:** Place into your plugins folder.

**Step 3:** Configure it (see the section below)

**Step 4:** PROFIT!


## Config
<details>
<summary>Default Config</summary>

```yml
SD:
# Please take time to read the Github Readme.
  is_enabled: true
  # Minimum players on the server to spawn the chopper
  min_players: 2
  # List of MTF Chopper Drop items
  mtf_items:
  - item: GunCOM18
    quantity: 1
    chance: 100
  - item: GunE11SR
    quantity: 1
    chance: 100
  - item: Ammo762x39
    quantity: 2
    chance: 100
  - item: Ammo9x19
    quantity: 2
    chance: 100
  - item: Medkit
    quantity: 2
    chance: 100
  - item: Medkit
    quantity: 2
    chance: 20
  - item: Adrenaline
    quantity: 1
    chance: 100
  - item: KeycardO5
    quantity: 1
    chance: 10
  # List of Chaos Car Drop items
  chaos_items:
  - item: GunLogicer
    quantity: 2
    chance: 100
  - item: Ammo762x39
    quantity: 5
    chance: 100
  - item: Medkit
    quantity: 2
    chance: 100
  - item: ArmorCombat
    quantity: 2
    chance: 20
  - item: Adrenaline
    quantity: 1
    chance: 100
  - item: KeycardO5
    quantity: 1
    chance: 10
  # Settings for MTF Chopper Drop
  chopper_time: 600
  chopper_broadcast: <size=35><i><color=#0080FF>MTF Chopper</color> <color=#5c5c5c>with a</color> <color=#7a7a7a>Supply Drop</color> <color=#5c5c5c>has arrived!</color></i></size>
  chopper_broadcast_time: 10
  # How many drops can the helicopter do per round? Set to -1 to disable limit.
  chopper_drops_limit: -1
  # Should the plugin use coordinates set below to spawn the items? If not it will use random MTF spawn point
  chopper_manual_coordinates: true
  # Coordinates used for the items spawn
  chopper_pos_x: 173
  chopper_pos_y: 993
  chopper_pos_z: -59
  # Settings for Chaos Car Drop
  car_time: 600
  # Time difference between the chopper and car. Chopper will always spawn first. Leave at 1 if you want to disable it.
  time_difference: 300
  car_broadcast: <size=35><i><color=#5c5c5c>A</color> <color=#28AD00>Chaos Insurgency Car</color> <color=#5c5c5c>with a</color> <color=#7a7a7a>Supply Drop</color> <color=#5c5c5c>has arrived!</color></i></size>
  car_broadcast_time: 10
  # How many drops can the car do per round? Set to -1 to disable limit.
  car_drops_limit: -1
  # Should the plugin use coordinates set below to spawn the items? If not it will use random CI spawn point
  car_manual_coordinates: true
  # Coordinates used for the items spawn
  car_pos_x: 9
  car_pos_y: 998
  car_pos_z: -49
  # Don't use it unless you have issues with the plugin. When sending a log enable this please.
  debug: false
```
 
</details>

## Functionality & FAQ
**How does this work? You might ask, well heres how.**
<br>Every 10 minutes (by default) it calls a chopper, when it lands (where MTF spawns) it will spawn in all the items you configured! 5 minutes later (by default) a Chaos Insurgency car will spawn and deliver a supply drop.

<details>
<summary>FAQ - Please read.</summary>

#### How does the chance system work with quantity higher than 1?
> Chance is calculated for every item that will spawn, not item type. If you have 5 medkits with 20% chance, each one will have a 20% chance to spawn. Set the `debug` option to `true` to see how it works.

#### How to give 3 `X` with 100% chance and 2 `X` with 20% chance?
> Simple, just add two fields with the same item name, but different chance and quantity.

#### How to disable one of the drops?
> For Chaos Insurgency set the `time_difference` value to a very high one.
> For MTF use the `chopper_time` value instead.

#### How to get rid of the time difference between chopper and car?
> Set the `time_difference` value to `1`. Not `-1`, not `0`, just `1`.

#### How to get coordinates for the spawn?
> Use RemoteAdmin coordinates. How? Just request player data, its in XYZ order.

</details>

### To-Do list
- Cassie announcements

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
