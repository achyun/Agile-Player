# AGILE PLAYER
An Audio player with downsampler, upsampler and bit-converter written in C#.


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

## Features An Specification
- Very simple GUI, just open audio files/folders and ready to go.
- Can save/load lists files
- Full audio controls (play, pause, stop...etc)

### Agile Player Core Features

- Accurate play timing, a specific sample is played at the time where it supposed to.
- Automatic upsampling and downsampling. Can play in any desigred frequency regaldess of the source frequency, the engine take care of downsampling/upsampling if it is needed.
- Automatic bit per sample converting, can switch desired bit-per-sample regadless of the source bit-per-sample, the engine take care of converting bit-per-sampling if needed. All using correct equations.
- Ability to record wav, save wav file of current played media with target settings. (i.e. can be used to convert frequency and bit-per-sample into wav file).
- Currently supported media format (both playback and record): Loseless WAV PCM, (8 bit, 16 bit, 24 bit and 32 bit). 

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
  since Windows is running perfectly in your machine, you should not worry about ram at all when using My Nes.

NOTES:
------------------
- Agile Player can only record when downsampling/upsampling and/or bit-converting. i.e. when the input frequency does not
match output frequency (frequency settings), and/or bits-per-sample of input does not match the output settings.
- When record, the application stops and freeze, it can take minutes before it done the recording, it may take a long time,
that depends on the input size, also on target settings. After the records finishes, the application will open the folder where
the recorded file is saved.

TROUBLES SHOOTING:
------------------
- Agile Player doesn't work:

Please make sure that these packages are installed in pc:
- .Net framework 4.8
- C++ Runtime (Try latest, if My Nes doensn't work, installing older version of this package may work.)

If the problem isn't solved, please try to install SlimDX latest runtime, one can be found here: <https://code.google.com/archive/p/slimdx/downloads> (SlimDX Runtime .NET 4.0 x86 (January 2012).msi).