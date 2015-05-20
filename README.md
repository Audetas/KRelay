# K_Relay
### A modular Realm of the Mad God man-in-the-middle Proxy
##### This code holds an implicit copyright by the creators. Redistribution of this code without written consent is a violation of said copyright.
-----------------------------------------------------------

##Plugin Documentation
[Can be found here](../master/PLUGIN%20DOCUMENTATION.md)

-----------------------------------------------------------

##Core Checklist: (please add as you get ideas)
- [x] **Define all server packets**
- [x] More that I can't think of at the moment
- [x] Some intuitive way to easily get a packet casted to the desired type in callbacks
- [ ] Don't parse packets that aren't hooked
- [x] Better Run button placement
- [x] Add welcome to K Relay message
- [x] Finish the plugins tab
- [x] Fetch server list from char/list
- [x] Redo Info tab
- [ ] Keyboard hooking interface
- [x] Re-Design of the Forms

##Todo Checklist:
- [x] Remove loot bag quests to purple bags
- [x] Figure out why we can't create characters
- [ ] Fix the following exception : "An element with the same key has already been added" when two char connected to the relay use the same item (AutoAbility must be in cause)
- [x] /connect takes you to USWest only
- [x] Check for packets missing from Packets.xml
- [ ] Fix reconnect handler to handle multiple clients at once
- [x] Fix \s in directory searching for *NIX
- [x] Fix default server setting
- [x] Fix fame notifier .CurrentFame => .Fame
- [ ] Vault, PetYard, Guild Hall recon for IRH
- [ ] Make autoloot settings more user friendly
- [ ] Look into randomly being placed back into USW

##Plugin Checklist:
- [x] World Edit
- [x] Fame Notifer
- [x] ClientStat Projecter
- [x] Auto Ability
- [x] TQ
- [x] TP
- [x] Reconnect UI
- [x] Chat Suite
- [x] Partial Godmode
- [x] Loot notifications / Loot bag quest
- [x] Auto loot
- [x] Tile/Object Replacer
- [x] Skid Booter 9000 - Not going to release publicly // Kithio (kithio.lost - Skype)
For later:
- [x] Custom vault for the fun of it
- [ ] Map Cacher
- [ ] Speedhack (maybee)
- [ ] Freeze Godmode
- [ ] Plugin to auto use an HP pot at a custom threshold.
- [ ] /tl command for Teleport to the Last player you teleported to assuming they are in the same map as you.
- [ ] Plugin to auto-teleport to someone once they are at at the Boss Portal in Deadwater Docks with toggle to enable auto-join. (Not sure if this is possible)
- [ ] Plugin for a toggle to auto-enter Oryx's Castle and auto-enter Wine Cellar.
