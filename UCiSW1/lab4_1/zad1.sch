<?xml version="1.0" encoding="UTF-8"?>
<drawing version="7">
    <attr value="xc9500xl" name="DeviceFamilyName">
        <trait delete="all:0" />
        <trait editname="all:0" />
        <trait edittrait="all:0" />
    </attr>
    <netlist>
        <signal name="X(3:0)" />
        <signal name="XLXN_2(3:0)" />
        <signal name="D7S_S(6:0)" />
        <port polarity="Input" name="X(3:0)" />
        <port polarity="Output" name="D7S_S(6:0)" />
        <blockdef name="AdderModuloArch">
            <timestamp>2019-11-4T16:17:5</timestamp>
            <rect width="256" x="64" y="-64" height="64" />
            <rect width="64" x="0" y="-44" height="24" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <rect width="64" x="320" y="-44" height="24" />
            <line x2="384" y1="-32" y2="-32" x1="320" />
        </blockdef>
        <blockdef name="HexTo7Seg">
            <timestamp>2019-11-4T16:27:57</timestamp>
            <rect width="256" x="64" y="-64" height="64" />
            <rect width="64" x="0" y="-44" height="24" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <rect width="64" x="320" y="-44" height="24" />
            <line x2="384" y1="-32" y2="-32" x1="320" />
        </blockdef>
        <block symbolname="AdderModuloArch" name="XLXI_1">
            <blockpin signalname="X(3:0)" name="X(3:0)" />
            <blockpin signalname="XLXN_2(3:0)" name="Y(3:0)" />
        </block>
        <block symbolname="HexTo7Seg" name="XLXI_2">
            <blockpin signalname="XLXN_2(3:0)" name="Hex(3:0)" />
            <blockpin signalname="D7S_S(6:0)" name="Displ7S(6:0)" />
        </block>
    </netlist>
    <sheet sheetnum="1" width="3520" height="2720">
        <instance x="912" y="1344" name="XLXI_1" orien="R0">
        </instance>
        <branch name="X(3:0)">
            <wire x2="912" y1="1312" y2="1312" x1="880" />
        </branch>
        <iomarker fontsize="28" x="880" y="1312" name="X(3:0)" orien="R180" />
        <instance x="1376" y="1344" name="XLXI_2" orien="R0">
        </instance>
        <branch name="XLXN_2(3:0)">
            <wire x2="1376" y1="1312" y2="1312" x1="1296" />
        </branch>
        <branch name="D7S_S(6:0)">
            <wire x2="1792" y1="1312" y2="1312" x1="1760" />
        </branch>
        <iomarker fontsize="28" x="1792" y="1312" name="D7S_S(6:0)" orien="R0" />
    </sheet>
</drawing>