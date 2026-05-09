using System.Diagnostics;
using System.IO;
using System.Windows.Automation;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using GodHandStyler;
using ControlType = FlaUI.Core.Definitions.ControlType;


Status status = Status.Unconnected;
ControlLayout.Init();
string statusText = string.Empty;
void setConnected()
{
    status = Status.Connected;
    statusText = "Connected";
}
Console.CursorVisible = false;
IntPtr m_ipc = PineIPC.NewPcsx2();
PineIPC.EmuStatus emuStatus;

void reloadCheats()
{
    var handle = Process.GetProcessesByName("pcsx2-qt").FirstOrDefault();
    statusText = "Reloading cheats ...";
    using (var automation = new UIA3Automation())
    {
        var window = automation.FromHandle(handle.MainWindowHandle).AsWindow();
        var windows = window.Parent.FindAllDescendants(x=>x.ByControlType(ControlType.Window)).Select(x=>x.AsWindow()).ToList();
        Window mainWindow = null;
        Window gameWindow = null;
        
        if (windows.Count > 1)
        {
            mainWindow = windows.FirstOrDefault(x => x.AsWindow().Title.StartsWith("PCSX2"));
            gameWindow = windows.FirstOrDefault(x => x.AsWindow().Title.StartsWith("God"));
        }
        else
        {
            mainWindow = window;
            gameWindow = window;
        }
        
        mainWindow?.SetForeground();
        mainWindow?.FocusNative();

        var toolsMenu = mainWindow.FindAllChildren()[2].AsMenuItem().Items[3].AsMenuItem();
        
        toolsMenu.Click(); 
        Thread.Sleep(300);
        
        var reloadItem = toolsMenu.FindAllDescendants(x => x.ByControlType(ControlType.MenuItem))[5].AsMenuItem();
        reloadItem?.Click();
        gameWindow?.SetForeground();
        gameWindow?.Focus();
        
        statusText += reloadItem is null ? "failed" : "Done";
    }
    
}

void updateStatus(IntPtr m_ipc)
{
    emuStatus = PineIPC.Status(m_ipc);
    switch (status)
    {
        case Status.Unconnected:
            {
                if (PineIPC.GetError(m_ipc) == PineIPC.IPCStatus.Success)
                    setConnected();
            }
            break;
        case Status.Connected:
            if (emuStatus != PineIPC.EmuStatus.Shutdown)
            {
                try
                {
                    string gameVersion = PineIPC.GetGameTitle(m_ipc);
                    string titleId = PineIPC.GetGameID(m_ipc);
                    string title = "=\t\t\t" + gameVersion + " - " + titleId;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(title);
                    Console.Write("========================================================================");
                    Console.ResetColor();
                    
                    var val = PineIPC.Read(m_ipc, 0x20568880, PineIPC.IPCCommand.MsgRead16);
                    var layout = ControlLayout.GetLayout(m_ipc);

                    for (int i = 2; i < 10; ++i)
                    {
                        Console.SetCursorPosition(0, i);
                        //Console.Write(new string(new char[Console.WindowWidth-1]));
                        Console.Write("                                                                                 ");
                    }
                    
                    int counter = 0;
                    foreach (var pair in layout)
                    {
                        counter++;
                        Console.SetCursorPosition(counter < 7 ? 0 : 32, 1 + (counter < 7 ? counter : counter - 6));

                        Console.WriteLine($"{pair.Key}: {ControlLayout.MoveTable[pair.Value.ToString("X2")]}");
                    }
                    Console.WriteLine("========================================================================");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                                
            }
            break;
        default:
            break;
    }
}

uint menuItemSelected = 1;

void drawStatusbar()
{
    Console.SetCursorPosition(0, Console.WindowHeight-1);
    Console.Write(statusText);
}
void drawMenu()
{
    
    Console.SetCursorPosition(0, 10);
    Console.Write("Save style as:");
   
    var selection = new string[3]  {"<", " ", ">"};
    Console.SetCursorPosition(5, 11);
    Console.Write($"{selection[1==menuItemSelected?0:1]}Style Up{selection[menuItemSelected==1?2:1]}    " +
                      $"{selection[menuItemSelected==2?0:1]}Style Down{selection[menuItemSelected==2?2:1]}    " +
                      $"{selection[menuItemSelected==3?0:1]}Style Left{selection[menuItemSelected==3?2:1]}    " +
                      $"{selection[menuItemSelected==4?0:1]}Style Right{selection[menuItemSelected==4?2:1]}");
    Console.ResetColor();
}

void UpdateBinding(ref string[] strings, Dictionary<string, ulong> dictionary, int baseLine)
{
    strings[baseLine + 5] = $"patch=1,EE,20568880,extended,{dictionary["Square1"]:X2}";
    strings[baseLine + 8] = $"patch=1,EE,20568884,extended,{dictionary["Square2"]:X2}";
    strings[baseLine + 11] = $"patch=1,EE,20568888,extended,{dictionary["Square3"]:X2}";
    strings[baseLine + 14] = $"patch=1,EE,2056888C,extended,{dictionary["Square4"]:X2}";
    strings[baseLine + 17] = $"patch=1,EE,20568890,extended,{dictionary["Square5"]:X2}";
    strings[baseLine + 20] = $"patch=1,EE,20568894,extended,{dictionary["Square6"]:X2}";
            
    strings[baseLine + 25] = $"patch=1,EE,205688A4,extended,{dictionary["Triangle     "]:X2}";
    strings[baseLine + 30] = $"patch=1,EE,205688C8,extended,{dictionary["Cross        "]:X2}";
    strings[baseLine + 35] = $"patch=1,EE,205688EC,extended,{dictionary["Down_Square  "]:X2}";
    strings[baseLine + 40] = $"patch=1,EE,20568910,extended,{dictionary["Down_Triangle"]:X2}";
    strings[baseLine + 45] = $"patch=1,EE,20568934,extended,{dictionary["Down_Cross   "]:X2}";
    strings[baseLine + 49] = $"patch=1,EE,2056839C,extended,{dictionary["Circle       "]:X}";
}

void saveStyle(uint style)
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), "6FB692AB.pnach");
    var reader = File.ReadAllLines(path);
    var layout = ControlLayout.GetLayout(m_ipc);
    statusText = "Saving style " + style;
    switch (style)
    {
        case 1:
            UpdateBinding(ref reader, layout, 57);
            break;
        case 2:
            UpdateBinding(ref reader, layout, 110);
            break;
        case 3:
            UpdateBinding(ref reader, layout, 164);
            break;
        case 4:
            UpdateBinding(ref reader, layout, 218);
            break;
    }
    File.Delete(path);
    File.WriteAllLines(path, reader);
    reloadCheats();
}

int counter = 0;

updateStatus(m_ipc);
drawMenu();
drawStatusbar();

while (true)
{
    counter++;
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey().Key;
        switch (key)
        {
            case ConsoleKey.RightArrow:
                menuItemSelected++;
                menuItemSelected = Math.Min(4, menuItemSelected);
                break;
            case ConsoleKey.LeftArrow:
                menuItemSelected--;
                menuItemSelected = Math.Max(1, menuItemSelected);
                break;
            case ConsoleKey.Enter:
                saveStyle(menuItemSelected);
                break;
            default: 
                break;
        }
        
        
        updateStatus(m_ipc);
        drawMenu();
        drawStatusbar();
    }
    
    if (counter == 10)
    {
        counter = 0;
        //Console.Clear();
        updateStatus(m_ipc);        
    }
    
    Thread.Sleep(10);
}


public enum Status
{
    Unconnected,
    Connected,
}

