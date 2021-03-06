# AGILE PLAYER
An Audio player with downsampler, upsampler and bit-converter written in C#.

![snap1](/snap.png?raw=true "Snapshot1")

**Songs are played using Agile-Player in this snap are licensed under the Common Creative Free License. To listen/download albums, [please visit here](https://soundcloud.com/alaa-hadid-810442910/albums).**

**Please note that this is the official repository of the program, that means official updates on source and releases will be commited/published here.**

### [DOWNLOAD LATEST RELEASE (VERSION)](https://github.com/alaahadid/Agile-Player/releases)

## Donation

*Donation now can be done using PayPal - The safer, easier way to pay online!*

*To Donate, please click on this button below*

[![Donation](https://www.paypalobjects.com/en_US/DE/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate?hosted_button_id=KV25VFRMVKLM2)

*Also QR code can be scaned to do so !!*

![Donate QR Code](https://github.com/alaahadid/AHD-Subtitles-Maker/blob/main/QR%20Code.png)

*Thanks in advance :)*

## Introduction
Agile Player is An Audio player with downsampler, upsampler and bit-converter written in C#.
An open source freeware, licensed under the GNU GENERAL PUBLIC LICENSE; Version 3, 29 June 2007.

The main goal of Agile Player is to be as accurate as possible and brings the best listening experience possible.

What the point of having another audio player while there are planty out there .... well, the answer is that 
Agile Player render audio in new unique way, making sure that:
1. Accurate play timing, a specific sample is played at the time where it supposed to
2. When it is neccessary , do downsampling/upsampling and/or bits-converting accuretly during play time.
3. Very simple GUI, just open audio files/folders and ready to go.

Agile Player is based on My Nes emulater (see <https://github.com/alaahadid/My-Nes>), the idea is to build
an audio player based on emulation way. i.e. it does like an emulator which emulates a machine, 
Agile Player load audio file, then clock like real machine: process samples on clocks based way, intervals in time, 
not just simply copy-paste buffers into a renderer.

This will deliver accuracy with timing, a specific sample plays at the time when it supposed to.

**Agile Player does not use any kind of media codec, it uses built in audio processing.**

**Agile Player plays files with READ-ONLY, it does not effect nor make and change in the source file. In order to save playback settings, Record feature needs to be used.**

**For best playback performance, please make sure that the CPS=44 and BufferSize=9 Kb, see [Settings File (i.e. Switching Renderer, Modify Performance Settings)](https://github.com/alaahadid/Agile-Player/wiki/Settings-File-(i.e.-Switching-Renderer,-Modify-Performance-Settings))**

## Features An Specification
- Very simple GUI, just open audio files/folders and ready to go.
- Can save/load lists files
- Can edit list by dragging-dropping files, also able to delete and sort.
- Can display time in different formats in media bar and display.
- Full audio controls (play, pause, stop...etc)

### Agile Player Core Features

- Multithread player, the play-engine clocks on thread other than the gui (main thread).
- Support multi-renderer can be switched in settings (settings file in Documents). Currently supported renderers: SDL2 Audio and SlimDX DirectSound.
- Accurate play timing, a specific sample is played at the time where it supposed to.
- Automatic upsampling and downsampling. Can play in any desigred frequency regaldess of the source frequency, the engine take care of downsampling/upsampling if it is needed.
- Automatic bit per sample converting, can switch desired bit-per-sample regadless of the source bit-per-sample, the engine take care of converting bit-per-sampling if needed. All using correct equations.
- DB Fix feature, enables a simple fix for songs/tracks 0 samples by replacing each sample with input 0 with input of 1 bit which is 1. It will be 0 db instead of infinity db. This is correct.
- Wave Shift feature, shift audio wave to make sure all samples above 0. Enabling this with DB Fix will make sure that the song/track is 100% correct in physics.
- Ability to record wav, save wav file of current played media with target settings (Channels, Frequency, Bits Per Sample, DB Fix and Wave Shift).
- Currently supported media formats (playback): 

1. Loseless WAV PCM, (8 bit, 16 bit, 24 bit and 32 bit). 
2. MP3 (16 bits only, still issues with it but works).

Note: Both DB Fix and Wave Shift features are developed based on information can be found here: <https://github.com/alaahadid/Docs/blob/main/Audio%20And%20DB.txt>

They are turned off by default.

## System Requirements
Usually Agile Player comes in portable package, which can be installed simply by extracting the content of that package 
anywhere in your machine.
Note that Agile Player save settings and user files at the documents.
In order to run Agile Player correctly in your machine, please make sure that your machine meets up these requirements:

- Agile Player can run at any version of windows that can run .net framework 4.8, such as Windows 7, 8, 8.1, 10 and 11.
- .Net Framework version 4.8 is required.
- Latest DirectX package from Microsoft.
- CPU: 2400 MHz or faster, multicore cpu is recommended for better performance. Agile Player is built for x86 cpus, 
  but it should run without problems with x64 cpus (tested and runs perfectly)
- RAM: Agile Player usually uses about 30 to 60 MB ram. When huge list is loaded, it may use up to 200 MB. In other words, 
  since Windows is running perfectly in your machine, you should not worry about ram at all when using Agile Player.

NOTES:
------------------
- Agile Player records to wave file with these settings: Channels Number, Frequency, Bits Per Sample, DB Fix and Wave Shift.
- When record, the application stops and freeze, it can take minutes before it done the recording, it may take a long time,
that depends on the input size, also on target settings. After the records finishes, the application will open the folder where
the recorded file is saved.
- To change time-format in the display, simply left-mouse-click. In the media bar, right-mouse-click.
- When dragging-droping files into the list, the list will be cleard first. 
  To add files into the list without clearing it, simply hold Left-Shift while dragging-droping files.
- To delete selected files from the list, simply press Delete from keyboard.
- How to switch renderer ?

1. Please go to documents folder `C:\Users\<user>\OneDrive\Documents\AgilePlayer`, `<user>` is the user name.
2. Open file `coresettings.ini` (or make new file with that name if it does not exist)
3. Find line `Audio_RendererID=` (or add it) then 

Set to `Audio_RendererID=slimdx.directsound` for **SlimDX DirectSound** renderer.

Set to `Audio_RendererID=sdl2.audio` for **SDL2 Audio** renderer.

**SlimDX DirectSound** renderer is stable.

See also [Settings File (i.e. Switching Renderer, Modify Performance Settings)](https://github.com/alaahadid/Agile-Player/wiki/Settings-File-(i.e.-Switching-Renderer,-Modify-Performance-Settings))

TROUBLES SHOOTING:
------------------
### Agile Player doesn't work:

Please make sure that these packages are installed in pc:
- .Net framework 4.8
- C++ Runtime (Try latest, if doensn't work, installing older version of this package may work.)

If the problem isn't solved, please try to install SlimDX latest runtime, one can be found here: <https://code.google.com/archive/p/slimdx/downloads> (SlimDX Runtime .NET 4.0 x86 (January 2012).msi).


### Performance problems, errors in sound and/or synchronization
1. Please go to documents folder `C:\Users\<user>\OneDrive\Documents\AgilePlayer`, `<user>` is the user name.
2. Open file `coresettings.ini` (or make new file with that name if it does not exist)
3. Find line `Audio_RenderBufferInKB=` (or add it) then set it to `Audio_RenderBufferInKB=9`. Find line `CPS_TargetCPS=` and set it to `CPS_TargetCPS=44`. If problem remains after saving the file and trying the application, set `Audio_RenderBufferInKB=16` and `CPS_TargetCPS=50` then try again. Increasing RenderBufferSize and trying different values might help if the problem remain.


Credits
-----------------------
Agile Player uses MP3Sharp to decode mp3, here is a copy-pase text from readme file at <https://github.com/ZaneDubya/MP3Sharp>

**MP3Sharp**

Decode MP3 files to PCM bitstreams entirely in .NET managed code

**LICENSE**

MP3Sharp is licensed under the [LGPL Version 3](https://github.com/ZaneDubya/MP3Sharp/blob/master/license.txt).

**CREDITS**

MP3Sharp is a port of JavaLayer, a MP3 decoder written by [JavaZoom](http://www.javazoom.net) and released under the LGPL. JavaLayer was initially ported to C# by [Robert Burke](http://www.robburke.net/), in what he modestly describes as a 'half day project'. [tekHedd](http://www.byteheaven.com/) added some significant speed optimizations. This repository includes bug fixes (fixes for correctly outputting VBR, mono, and ability to loop without crashing; nice!). The sample MP3 file used in this project is by [BenSound](http://www.bensound.com), and is included under the terms of the Creative Commons - Attribution - No Derivative Works license.

**Please read the file "Copyright Notice.txt" in the latest release package for more details about copyright.**