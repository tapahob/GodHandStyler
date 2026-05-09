namespace GodHandStyler;

public class ControlLayout
{


    public static Dictionary<string, string> MoveTable = new Dictionary<string, string>();
    
    public static Dictionary<string, ulong> GetLayout(IntPtr ipc)
    {
        var dict = new Dictionary<string, ulong>();
        // autocombo
        dict["Square1"] = PineIPC.Read(ipc, 0x20568880, PineIPC.IPCCommand.MsgRead32);
        dict["Square2"] = PineIPC.Read(ipc, 0x20568884, PineIPC.IPCCommand.MsgRead32);
        dict["Square3"] = PineIPC.Read(ipc, 0x20568888, PineIPC.IPCCommand.MsgRead32);
        dict["Square4"] = PineIPC.Read(ipc, 0x2056888C, PineIPC.IPCCommand.MsgRead32);
        dict["Square5"] = PineIPC.Read(ipc, 0x20568890, PineIPC.IPCCommand.MsgRead32);
        dict["Square6"] = PineIPC.Read(ipc, 0x20568894, PineIPC.IPCCommand.MsgRead32);
        
        dict["Triangle     "] = PineIPC.Read(ipc, 0x205688A4, PineIPC.IPCCommand.MsgRead32);
        dict["Cross        "] = PineIPC.Read(ipc, 0x205688C8, PineIPC.IPCCommand.MsgRead32);
        dict["Down_Square  "] = PineIPC.Read(ipc, 0x205688EC, PineIPC.IPCCommand.MsgRead32);
        dict["Down_Triangle"] = PineIPC.Read(ipc, 0x20568910, PineIPC.IPCCommand.MsgRead32);
        dict["Down_Cross   "] = PineIPC.Read(ipc, 0x20568934, PineIPC.IPCCommand.MsgRead32);
        
        // Grapple?
        dict["Circle       "] = PineIPC.Read(ipc, 0x2056839C, PineIPC.IPCCommand.MsgRead32);
        return dict;
    }
    
     public static void Init()
    {
        IEnumerable<string> str = @"
1.Left Jab 1           - 00 	

2.Left Jab 2           - 01 	

3.Left Jab 3           - 02	

4.Left Hook 1          - 0A	

5.Left Hook 2          - 0B	

6.Left Hook 3          - 0C	

7.Uppercut 1           - 0D	

8.Uppercut 2           - 0E	

9.Uppercut 3           - 0F	

10.Pimp Hand            - 17		

11.Pimp Smack           - 18	

12.Straight 1           - 04	

13.Straight 2           - 05	

14.Straight 3           - 06	

15.Long Straight 1      - 07	

16.Long Straight 2      - 08	

17.Long Straight 3      - 09	

18.Right Hook 1         - 59	

19.Right Hook 2         - 5A	

20.Right Hook 3         - 5B	

21.Short Uppercut 1     - 56

22.Short Uppercut 2     - 57

23.Short Uppercut 3     - 58

24.Pay Up               - 14

25.Really Pay Up        - 15

26.Chop                 - 10

27.Elbow Spin 1         - 11

28.Elbow Spin 2         - 12

29.Elbow Spin 3         - 13

30.Axe Kick    - 21

31.Hand Aura Effect 1    - 22

32.Hand Aura Effect 2    - 6C

33.Chin Music           - 43

34.Punch Rush           - 42

35.Sugar Gene Combo     - 3F

36.Floating Butterfly   - 40

37.Stinging Bee         - 41

38.Fist of Justice      - 2E

39.Mach Speed Jab 1     - 03

40.Mach Speed Jab 2     - 66

41.Haymaker 1           - 27

42.Haymaker 2           - 68

43.Rider Kick 1      - 3B

44.Rider Kick 2      - 67

45.One-Two Kick        - 33

46.Elbow Vortex         - 3A

47.Fatal Fury           - 3C

48.Low Kick 1           - 1A

49.Low Kick 2           - 1B

50.Right Roundhouse 1   - 1D

51.Right Roundhouse 2   - 1E

52.Right Roundhouse 3   - 1F

53.Left Roundhouse      - 20

54.High Side Kick 1     - 25

55.High Side Kick 2     - 64

56.High Side Kick 3     - 65

57.Legs Effect 2        - 28

58.Face Runner          - 29

59.Crescent Kick      - 45

60.Flying Knee          - 39

61.Reverse Sweep        - 2F

62.Typhoon Kick      - 30

63.Reverse Hell Kick    - 3D

64.Expert Sobat         - 35

65.Dashing Sobat        - 36

66.Head Slicer Kick      - 38

67.Spinning Sobat       - 31

68.Lazy Punch        - 26

69.Drunken Fist 1       - 47

70.Drunken Fist 2       - 6D

71.La bomba          - 4D

72.Side Swipe           - 4C

73.Legs Effect       - 46

74.Kung Fu Tango        - 4F

75.Stomping Fist        - 50

76.Rocket Uppercut      - 4E

77.Right Twister        - 51

78.Trigger Pummel        - 52

79.Discombobulator       - 48

80.Ball Breaker         - 4A

81.Ball Kick      - 37

82.Double Snap Kick     - 3E

83.El Sikut       - 49

84.Heel Drop            - 4B

85.Half Moon Kick       - 53

86.Somersault           - 32

87.High Snap Kick       - 2A

88.Triple Side Kick     - 2B

89.Flying Triple        - 2C

90.Body Effect       - 2D

91.Mule Kick            - 24

92.Double Spin Kick     - 23

93.Surprise Left Punch     - 34

94.Guard Breaker 1      - 54

95.Guard Breaker 2      - 6A

96.Charged Punch 1      - 55

97.Charged Punch 2      - 6E

98.Charged Punch 3      - 6F

99.Charged Punch 4      - 70

100.Granny Smacker       - 5C

101.Sucker Header      - 5D

102.Jab of God           - 5E

103.Godly Straight       - 5F

104.God Hook             - 60

105.God Uppercut         - 61

106.Godly Smack          - 19

107.Pay Up NOW!          - 16

108.Godly Chop           - 62

109.Godly Low Kick       - 1C

110.Haymaker of God      - 69

111.God Breaker          - 6B

112.God Charged Punch    - 71

113.Fist of God          - 63

114.Invincible Fist      - 44
Pummel               - 2DD270

Spanking             - 2DD6C0

Stinger              - 2DD480

Suplex               - 2DD300

Cobra Twist          - 2DD600

Demon Counter        - 2DC2F0

Poke of God          - 2DC898

Stake Driver         - 2DD3C0

Midget Counter       - 2DC67060
".Split("\r\n").Where(x=>x != "").ToList();
        
        foreach (var line in str)
        {
            var pair = line.Split(" - ");
            MoveTable.Add(pair[1].Trim(), pair[0].Trim());
        }
    }   
}