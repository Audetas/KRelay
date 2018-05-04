# KRelay
## About this fork.
This is a fork of KRelay which deviates from the version posted to MPGH.
This is slightly older than the currently posted version, and as a result it is missing some features which have been recently added.
For example, this fork **does not** have Stealth Mode or on-the-go server switching. If these are features you desire, **do not use this fork.**

This fork features dynamic resource loading. All resources are loaded from the `Resources` directory at runtime.
This feature eliminates the need for rebuilding KRelay after each game update, as the outdated resources can simply be replaced by the latest ones, and KRelay will work again.

## Using this fork.
To use the version of KRelay from this fork,
1. Download or clone the repository.
2. Open `K_Relay.sln` in Visual Studio.
3. Build the solution.
4. KRelay can now be found at `K_Relay\bin\Debug`. It is recommended to copy the entire contents of this folder to some other place on your computer which is easer to access.
5. Copy `AutoUpdater` and paste it in your `KRelay` folder.

When you are done, your `KRelay` folder should look something like
```
KRelay/
    - Plugins/
    - Resources/
    - K_Relay.exe
    - AutoUpdater/
    - Lib K Relay.dll
```

## Using the auto updater.
This fork also introduces a Nodejs based auto updater to download the latest assets. It should be run any time there is an update to the game in order to download the most up to date assets.

To use the updater, follow these steps.
1. Open the `AutoUpdater` folder and run `run.bat`.
2. Done.

The updater will download the latest `Objects.xml`, `Tiles.xml` and `client.swf`. It will extract the packets from `client.swf` and save them to `Packets.xml`. The updater saves its files to the same directory KRelay reads them from, so it is not necessary to move files around.

# Acknowledgements
KRelay was originally designed and developed by [TheKronks](https://github.com/TheKronks).

The following open source software has been used in this fork of KRelay:
 + [JPEXS Free Flash Decompiler](https://github.com/jindrapetrik/jpexs-decompiler)
