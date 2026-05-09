# GodHandStyler
Godhand style-switcher mod TUI Tool (PCSX2)

[![IMAGE ALT TEXT](http://img.youtube.com/vi/wI4GRarIHz4/0.jpg)](http://www.youtube.com/watch?v=wI4GRarIHz4)

### Installation meow:
1. Download [the archive](GodHandStyler.7z)
2. Extract into your PCSX2 cheats folder (in my case it is "C:\Users\user\Documents\PCSX2\cheats", for portable PCSX2 setup it would be inside your PCSX2)
3. Open PCSX2 and toggle Tools -> Show Advanced Settings
4. Open System -> Advanced -> Pine settings and enable "Pine". Slot should stay the same 28011
5. Enable Interface -> Render to separate Window. This thing is optional but you want the menu part to be present, so its either main window in windowed mode with menu visible, or have separate window rendering so the menu is still available in the other window

### How to use:
1. Boot up the game
2. Start Godhand Styler
3. Configure your current style however you like
4. Switch to Godhand Styler, select the style you wanna write your style to and press enter. (There is currently a bug im a little lazy to fix, if you just press enter first time you boot up the program it doesnt do anything, but if you hit arrow keys left/right it starts working)
5. If you wanna edit the cheat file, just have so the line numbers for styles part remain unchanged, the program writes at those exact lines. So if you want to add things, add to the bottom of the file

### How it works:
It reads Godhand's memory directly inside of the PCSX2 through Pine-IPC. 
As the cheat file has all the virtual addresses of the values it sets for the styles. 
Once it has the values I just update the cheat file at the certain lines to reflect the changes. 
There is no API for PCSX2 to make it refresh cheats (and no hotkey for it) I made a quick UIAutomation script to click through the menus and reload those cheats.  
