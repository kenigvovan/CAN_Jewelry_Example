# C&N Jewelry — compatibility example

A small [Vintage Story](https://www.vintagestory.at/) mod that shows how a third-party mod can
integrate with **[C&N Jewelry](https://mods.vintagestory.at/canjewelry)**: give its own (or vanilla) items gem sockets, choose which gems fit
into them, and control where the gems are drawn on the item model.

It serves as a reference implementation for mod authors and covers both integration paths that
C&N Jewelry offers — **JSON asset patches** and the **C# registry API**.

## What it demonstrates

| Item | Technique | Where |
|---|---|---|
| Hoe | Sockets and allowed gems via a JSON patch, optional when C&N Jewelry is absent (`dependsOn`) | `assets/canjewelryexample/patches/hoe.json` |
| Cleaver | Sockets from code, item added to the `melee` gem group | `canjewelryexampleModSystem.cs` |
| Shears | Socket layout that depends on the item's metal variant | `canjewelryexampleModSystem.cs` |
| Saw | The same attributes as the hoe patch, written from code in `AssetsFinalize` | `canjewelryexampleModSystem.cs` |
| Gem visuals | Wildcard rules, custom groups, per-cut/per-size poses, hidden gems | `assets/canjewelryexample/config/gemvisuals/` |

Key points shown in the code:

- registering in `StartPre` with a lower `ExecuteOrder()` so entries reach the core's config defaults on first start;
- the difference between accepting a gem group (`canGemGroups`) and joining a config group (`RegisterItemGroupMembers`);
- keeping server and client in agreement by writing item attributes that both sides read.

## Documentation

The full integration guide — socket tiers, gem groups, gem codes, the `gemvisuals` rule format and
rule priority, and the in-game debug tool for tuning gem poses — is in
[`WIKI.md`](canjewelryexample/canjewelryexample/WIKI.md).

## Project structure

```
canjewelryexample/
├── canjewelryexample/            # the mod
│   ├── canjewelryexampleModSystem.cs
│   ├── modinfo.json
│   ├── WIKI.md
│   └── assets/canjewelryexample/
│       ├── patches/              # JSON patches for vanilla items
│       ├── config/gemvisuals/    # gem placement rules
│       └── lang/
└── ZZCakeBuild/
```

## Author

KenigVovan
