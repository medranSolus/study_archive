<?xml version="1.0" encoding="UTF-8"?>
<drawing version="7">
    <attr value="xc9500xl" name="DeviceFamilyName">
        <trait delete="all:0" />
        <trait editname="all:0" />
        <trait edittrait="all:0" />
    </attr>
    <netlist>
        <signal name="Y" />
        <signal name="X" />
        <signal name="CE" />
        <signal name="CLR" />
        <signal name="CLK" />
        <signal name="Q(2:0)" />
        <port polarity="Output" name="Y" />
        <port polarity="Input" name="X" />
        <port polarity="Input" name="CE" />
        <port polarity="Input" name="CLR" />
        <port polarity="Input" name="CLK" />
        <port polarity="Output" name="Q(2:0)" />
        <blockdef name="detector">
            <timestamp>2019-11-18T17:41:9</timestamp>
            <rect width="64" x="320" y="84" height="24" />
            <line x2="384" y1="96" y2="96" x1="320" />
            <line x2="0" y1="-224" y2="-224" x1="64" />
            <line x2="0" y1="-160" y2="-160" x1="64" />
            <line x2="0" y1="-96" y2="-96" x1="64" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <line x2="384" y1="-224" y2="-224" x1="320" />
            <rect width="256" x="64" y="-256" height="384" />
        </blockdef>
        <block symbolname="detector" name="XLXI_1">
            <blockpin signalname="X" name="X" />
            <blockpin signalname="CE" name="CE" />
            <blockpin signalname="CLR" name="CLR" />
            <blockpin signalname="CLK" name="CLK" />
            <blockpin signalname="Y" name="Y" />
            <blockpin signalname="Q(2:0)" name="Qo(2:0)" />
        </block>
    </netlist>
    <sheet sheetnum="1" width="3520" height="2720">
        <instance x="1312" y="1376" name="XLXI_1" orien="R0">
        </instance>
        <branch name="Y">
            <wire x2="1728" y1="1152" y2="1152" x1="1696" />
        </branch>
        <iomarker fontsize="28" x="1728" y="1152" name="Y" orien="R0" />
        <branch name="X">
            <wire x2="1312" y1="1152" y2="1152" x1="1280" />
        </branch>
        <iomarker fontsize="28" x="1280" y="1152" name="X" orien="R180" />
        <branch name="CE">
            <wire x2="1312" y1="1216" y2="1216" x1="1280" />
        </branch>
        <iomarker fontsize="28" x="1280" y="1216" name="CE" orien="R180" />
        <branch name="CLR">
            <wire x2="1312" y1="1280" y2="1280" x1="1280" />
        </branch>
        <iomarker fontsize="28" x="1280" y="1280" name="CLR" orien="R180" />
        <branch name="CLK">
            <wire x2="1312" y1="1344" y2="1344" x1="1280" />
        </branch>
        <iomarker fontsize="28" x="1280" y="1344" name="CLK" orien="R180" />
        <branch name="Q(2:0)">
            <wire x2="1728" y1="1472" y2="1472" x1="1696" />
        </branch>
        <iomarker fontsize="28" x="1728" y="1472" name="Q(2:0)" orien="R0" />
    </sheet>
</drawing>