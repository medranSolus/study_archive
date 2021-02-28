<?xml version="1.0" encoding="UTF-8"?>
<drawing version="7">
    <attr value="xc9500xl" name="DeviceFamilyName">
        <trait delete="all:0" />
        <trait editname="all:0" />
        <trait edittrait="all:0" />
    </attr>
    <netlist>
        <signal name="CLK" />
        <signal name="CLR" />
        <signal name="X(14:12)" />
        <signal name="RS_RX" />
        <signal name="XLXN_15" />
        <signal name="XLXN_16" />
        <signal name="XLXN_17" />
        <signal name="XLXN_18" />
        <signal name="X(15:0)" />
        <signal name="X(7:0)" />
        <signal name="XLXN_21" />
        <signal name="XLXN_22" />
        <signal name="D7S_D(3:0)" />
        <signal name="D7S_S(7:0)" />
        <port polarity="Input" name="CLK" />
        <port polarity="Input" name="CLR" />
        <port polarity="Input" name="RS_RX" />
        <port polarity="Output" name="D7S_D(3:0)" />
        <port polarity="Output" name="D7S_S(7:0)" />
        <blockdef name="lab3_zad1">
            <timestamp>2019-10-21T16:43:51</timestamp>
            <rect width="256" x="64" y="-192" height="192" />
            <line x2="0" y1="-160" y2="-160" x1="64" />
            <line x2="0" y1="-96" y2="-96" x1="64" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <rect width="64" x="320" y="-172" height="24" />
            <line x2="384" y1="-160" y2="-160" x1="320" />
        </blockdef>
        <blockdef name="RS232_RX">
            <timestamp>2019-10-21T17:8:39</timestamp>
            <rect width="256" x="64" y="-192" height="192" />
            <line x2="0" y1="-96" y2="-96" x1="64" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <line x2="384" y1="-32" y2="-32" x1="320" />
            <rect width="64" x="320" y="-108" height="24" />
            <line x2="384" y1="-96" y2="-96" x1="320" />
            <line x2="320" y1="-160" y2="-160" x1="384" />
        </blockdef>
        <blockdef name="Display4x7S">
            <timestamp>2019-10-21T17:34:15</timestamp>
            <rect width="64" x="320" y="-108" height="24" />
            <line x2="384" y1="-96" y2="-96" x1="320" />
            <rect width="64" x="320" y="-44" height="24" />
            <line x2="384" y1="-32" y2="-32" x1="320" />
            <rect width="64" x="0" y="-108" height="24" />
            <line x2="0" y1="-96" y2="-96" x1="64" />
            <rect width="256" x="64" y="-128" height="256" />
            <rect width="64" x="0" y="-44" height="24" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <line x2="0" y1="96" y2="96" x1="64" />
            <line x2="0" y1="32" y2="32" x1="64" />
            <rect width="64" x="0" y="20" height="24" />
        </blockdef>
        <block symbolname="lab3_zad1" name="XLXI_2">
            <blockpin signalname="CLK" name="CLK" />
            <blockpin signalname="CLR" name="CLR" />
            <blockpin signalname="XLXN_18" name="CE" />
            <blockpin signalname="X(14:12)" name="Q(2:0)" />
        </block>
        <block symbolname="RS232_RX" name="XLXI_4">
            <blockpin signalname="CLR" name="Reset" />
            <blockpin signalname="CLK" name="Clk_XT" />
            <blockpin signalname="XLXN_18" name="Rdy" />
            <blockpin signalname="X(7:0)" name="DO(7:0)" />
            <blockpin signalname="RS_RX" name="RS_RX" />
        </block>
        <block symbolname="Display4x7S" name="XLXI_5">
            <blockpin signalname="X(15:0)" name="DI(15:0)" />
            <blockpin signalname="D7S_D(3:0)" name="D7S_D(3:0)" />
            <blockpin signalname="D7S_S(7:0)" name="D7S_S(7:0)" />
            <blockpin name="DP(3:0)" />
            <blockpin signalname="CLK" name="Clk" />
            <blockpin name="Blank(3:0)" />
        </block>
    </netlist>
    <sheet sheetnum="1" width="3520" height="2720">
        <branch name="CLK">
            <wire x2="960" y1="576" y2="576" x1="592" />
            <wire x2="976" y1="576" y2="576" x1="960" />
            <wire x2="976" y1="576" y2="816" x1="976" />
            <wire x2="992" y1="816" y2="816" x1="976" />
            <wire x2="960" y1="496" y2="576" x1="960" />
            <wire x2="1824" y1="496" y2="496" x1="960" />
            <wire x2="1824" y1="496" y2="576" x1="1824" />
            <wire x2="1872" y1="576" y2="576" x1="1824" />
            <wire x2="1024" y1="448" y2="448" x1="976" />
            <wire x2="976" y1="448" y2="576" x1="976" />
        </branch>
        <branch name="CLR">
            <wire x2="816" y1="880" y2="880" x1="576" />
            <wire x2="976" y1="880" y2="880" x1="816" />
            <wire x2="992" y1="880" y2="880" x1="976" />
            <wire x2="1024" y1="384" y2="384" x1="816" />
            <wire x2="816" y1="384" y2="880" x1="816" />
        </branch>
        <instance x="992" y="976" name="XLXI_2" orien="R0">
        </instance>
        <branch name="X(14:12)">
            <wire x2="1392" y1="816" y2="816" x1="1376" />
            <wire x2="1392" y1="624" y2="816" x1="1392" />
            <wire x2="1472" y1="624" y2="624" x1="1392" />
            <wire x2="1664" y1="576" y2="576" x1="1472" />
            <wire x2="1472" y1="576" y2="624" x1="1472" />
        </branch>
        <branch name="RS_RX">
            <wire x2="1488" y1="320" y2="320" x1="1408" />
        </branch>
        <iomarker fontsize="28" x="1488" y="320" name="RS_RX" orien="R0" />
        <iomarker fontsize="28" x="592" y="576" name="CLK" orien="R180" />
        <instance x="1024" y="480" name="XLXI_4" orien="R0">
        </instance>
        <iomarker fontsize="28" x="576" y="880" name="CLR" orien="R180" />
        <branch name="XLXN_18">
            <wire x2="992" y1="944" y2="944" x1="928" />
            <wire x2="928" y1="944" y2="1040" x1="928" />
            <wire x2="1456" y1="1040" y2="1040" x1="928" />
            <wire x2="1456" y1="448" y2="448" x1="1408" />
            <wire x2="1456" y1="448" y2="1040" x1="1456" />
        </branch>
        <instance x="1872" y="480" name="XLXI_5" orien="R0">
        </instance>
        <branch name="X(15:0)">
            <wire x2="1872" y1="384" y2="384" x1="1760" />
            <wire x2="1760" y1="384" y2="400" x1="1760" />
            <wire x2="1760" y1="400" y2="576" x1="1760" />
            <wire x2="1760" y1="576" y2="720" x1="1760" />
        </branch>
        <branch name="X(7:0)">
            <wire x2="1568" y1="384" y2="384" x1="1408" />
            <wire x2="1568" y1="384" y2="400" x1="1568" />
            <wire x2="1664" y1="400" y2="400" x1="1568" />
        </branch>
        <bustap x2="1664" y1="400" y2="400" x1="1760" />
        <bustap x2="1664" y1="576" y2="576" x1="1760" />
        <branch name="D7S_D(3:0)">
            <wire x2="2288" y1="384" y2="384" x1="2256" />
        </branch>
        <iomarker fontsize="28" x="2288" y="384" name="D7S_D(3:0)" orien="R0" />
        <branch name="D7S_S(7:0)">
            <wire x2="2288" y1="448" y2="448" x1="2256" />
        </branch>
        <iomarker fontsize="28" x="2288" y="448" name="D7S_S(7:0)" orien="R0" />
    </sheet>
</drawing>