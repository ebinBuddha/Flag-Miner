# ***Flag Miner***
Are you a /flag/tist drowning in flag dumps? :^)  
Do you want to parse the archives for yourself?  
You came to the right place!

With Flag Miner you can:  
1. **Mine** the archives of /int/, /pol/ and /sp/ to look for Extra Flags regionals  
2. **Parse** flag dump posts  
3. **Mark** all the flags you've already saved and **Purge** all wrong setup flags  

<kbd><img src="https://github.com/ebinBuddha/Flag-Miner/raw/master/MinerUI.png" /></kbd>


# Table of Contents
- [Notes](#notes)
- [How To](#how-to)
	- [1. Get the program](#1-get-the-program)
	- [2. Set it up](#2-set-it-up)
	- [3. Mine the archives](#3-mine-the-archives)
	- [4. Parse](#4-parse)
	- [5. Copy to Clipboard](#5-copy-to-clipboard)
	- [6. Check existing](#6-check-existing)
	- [7. Purge wrong flags](#7-purge-wrong-flags)
	- [8. Save and Load](#8-save-and-load)
	- [9. Other commands](#9-other-commands)
	- [10. Have fun](#10-have-fun)

# Notes
* Uses ObjectListView: http://objectlistview.sourceforge.net/cs/index.html
* Uses .Net Framework 4.5
* Made for Windows
* May™ work properly on Linux using Wine: there are some problems recognizing the folders

# How To
## 1. Get the program
### 1a. For Codefags
**1.** Clone or download the repo  
**2.** Compile the code using the .sln file (e.g. with Visual Studio)

### 1b. For the others
**1.** Follow this link to download the .zip file https://github.com/ebinBuddha/Flag-Miner/raw/master/Binaries/FlagMiner.zip  
**2.** Copy the files in a folder of your choice

## 2. Set it up
**1.** Open the options screen with the "Wrench" icon  
**2.** Choose the settings that fit you best.  
Remember that saved flags can be looked up if your local folders match the hierarchy of the **Extra Flags** repository, e.g.:

|Flag Url       | https://raw.githubusercontent.com/flaghunters/Extra-Flags-for-int-/master/flags/United%20States/Minnesota.png |
|:---|---|
|**Valid local path**   | C:\\my flag folder\\**United States\\Minnesota.png**
|**Invalid local path**   | C:\\my flag folder\\USA\\Minnesota.png
|**Invalid local path**   | C:\\my flag folder\\Minnesota.png 

## 3. Mine the archives
#### If you want to dig for yourself
**1.** Tick the boards you want to parse, and the way you want to exclude threads based on archive date and time (set them in local timezone - it'll be converted to Unix timestamp automatically) or whether the thread has already been parsed before or not.

Note: Parsed thread numbers will be stored on disk named *int.db*, *pol.db* and *sp.db* as editable Xml files.  

**2.** Hit **Mine it!** and let it do the job! It may take several hours to parse the archives thoroughly.

Note: It may look like it hangs when its starts parsing /pol/: it's normal as there's a fuckton of threads being created everyday and the program is trying to figure out which one has already parsed

**3.** The trayIcon will blink when it's finished  
**4.** You can always hit **Abort** to stop the process

## 4. Parse
#### If you just want to parse flag dumps
**1.** Navigate with your browser to the thread containing the flag dump and copy the url in the upper textbox  
**2.** Fill the lower textbox with the post numbers containing the dump
You can easily get the by simulating a "quick reply" by clicking the numbers on your browser. Valid input can be:

    XXXXXXX
    XXXXYYY
    XXXYYZX

or, if you're lazy, just keep the pointy brackets:

    >>XXXXXXX
    >>XXXXYYY
    >>XXXYYZX

**3.** Hit **Parse!** and let it run

## 5. Copy to Clipboard
#### For the good guys
Do you want to share your dump on the chans? Just hit **Copy to clipboard**.  
It will copy to clipboard all the tree in a suitable format:

	...
	\>>>/sp/79117130 Brisbane, South East Queensland, Queensland, Australia
	\>>>/pol/146091443 Adelaide, Metropolitan, South Australia, Australia
	\>>>/int/80822824 Victoria, Australia
	\>>>/int/80826985 Western Australia, Australia
	...

The copypasta is already alphabetically sorted and non redundant that is if a flag has a higher level regional, only lines for higher levels will be generated as the lower ones are implicit.
just remember to cut it every 2000 characters... :^)

## 6. Check existing
When the program has finished parsing or mining you may want to see which flags you already have. To do this hit **Check**.  
All the flags already saved on disk will be highlighted in green.  
This button will be enabled if the Options dialog is set up for this ( look for *Mark saved flags using the local folder* )

## 7. Purge wrong flags
You may want to delete all the flags that have been detected but have been wrongly set up by the user. To do this hit **Purge**.  
This button will be enabled if the Options dialog is set up for this ( look for *Enable validation and purging* )

## 8. Save and Load
You can save and load your flag dumps by clicking on the **Save tree** and **Load tree** in the lower right corner. A save/open dialog will appear to let you choose a valid file.  
Files are saved in editable Xml format.

## 9. Other commands
Right clicking the tree will bring up a small menu  
<kbd><img src="https://github.com/ebinBuddha/Flag-Miner/raw/master/Menu.png" /></kbd>
The commands are self explanatory :^)


## 10. Have fun
Have fun!
