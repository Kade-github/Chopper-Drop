# Chopper-Drop
Chopper Drop is a Exiled plugin which emulates a Chopper Dropping supplys for MTF (or really anyone who is up there). This could be really useful for RP Sites/Gameplay sites. Really anything.

## How to Use / Configure
**Step 1:** Download the latest release from [here](https://github.com/KadeDev/Chopper-Drop/releases/latest)

**Step 2:** Place into your plugins folder.

**Step 3:** Configure it (see the section below)

**Step 4:** PROFIT!

## Config
`chopper_drops`
The 'chopper_drops' config is a **dictionary.**
Heres the default:

`chopper_drops: GrenadeFrag:4,Flashlight:1,GunMP7:4,GunUSP:2,Painkillers:4`

Lets break it down.

The first value is, **'GrenadeFrag'** then it's value is **4.** It can be broke down into this:

First Value = Item. Second Value = Amount.

So, just put an item in there. And then its amount, then add a ',' and then do the same thing.

**Do not add spaces in this though.**

We also have `chopper_time` which is how long it takes for the chopper to come in, by default it is 600 seconds.

Default:

`chopper_time: 600`

## Functionality
how does this work?

You might ask, well heres how.

Every 10 (by default) minutes it calls a chopper, when it lands (it lands where mtf spawn) it will spawn in all the items you configured!

That's how :D
