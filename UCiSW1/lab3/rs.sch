<?xml version="1.0" encoding="UTF-8"?>
<drawing version="7">
    <attr value="xc9500xl" name="DeviceFamilyName">
        <trait delete="all:0" />
        <trait editname="all:0" />
        <trait edittrait="all:0" />
    </attr>
    <netlist>
        <signal name="CLK" />
        <signal name="RS_RX" />
        <signal name="D7S_S(6:0)" />
        <signal name="X(3:0)" />
        <signal name="XLXN_20" />
        <signal name="XLXN_21" />
        <signal name="XLXN_22" />
        <signal name="X(2:0)" />
        <signal name="XLXN_27" />
        <port polarity="Input" name="CLK" />
        <port polarity="Input" name="RS_RX" />
        <port polarity="Output" name="D7S_S(6:0)" />
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
        <blockdef name="HexTo7Seg">
            <timestamp>2019-10-21T16:9:16</timestamp>
            <rect width="256" x="64" y="-64" height="64" />
            <rect width="64" x="0" y="-44" height="24" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <rect width="64" x="320" y="-44" height="24" />
            <line x2="384" y1="-32" y2="-32" x1="320" />
        </blockdef>
        <blockdef name="lab3_zad1">
            <timestamp>2019-10-21T16:43:51</timestamp>
            <rect width="256" x="64" y="-192" height="192" />
            <line x2="0" y1="-160" y2="-160" x1="64" />
            <line x2="0" y1="-96" y2="-96" x1="64" />
            <line x2="0" y1="-32" y2="-32" x1="64" />
            <rect width="64" x="320" y="-172" height="24" />
            <line x2="384" y1="-160" y2="-160" x1="320" />
        </blockdef>
        <block symbolname="RS232_RX" name="XLXI_4">
            <blockpin signalname="XLXN_22" name="Reset" />
            <blockpin signalname="CLK" name="Clk_XT" />
            <blockpin signalname="XLXN_27" name="Rdy" />
            <blockpin name="DO(7:0)" />
            <blockpin signalname="RS_RX" name="RS_RX" />
        </block>
        <block symbolname="HexTo7Seg" name="XLXI_1">
            <blockpin signalname="X(3:0)" name="Hex(3:0)" />
            <blockpin signalname="D7S_S(6:0)" name="Displ7S(6:0)" />
        </block>
        <block symbolname="lab3_zad1" name="XLXI_2">
            <blockpin signalname="CLK" name="CLK" />
            <blockpin signalname="XLXN_22" name="CLR" />
            <blockpin signalname="XLXN_27" name="CE" />
            <blockpin signalname="X(2:0)" name="Q(2:0)" />
        </block>
    </netlist>
    <sheet sheetnum="1" width="3520" height="2720">
        <instance x="1024" y="1120" name="XLXI_4" orien="R0">
        </instance>
        <branch name="RS_RX">
            <wire x2="1424" y1="960" y2="960" x1="1408" />
            <wire x2="1616" y1="960" y2="960" x1="1424" />
        </branch>
        <iomarker fontsize="28" x="1616" y="960" name="RS_RX" orien="R0" />
        <branch name="D7S_S(6:0)">
            <wire x2="1968" y1="1456" y2="1456" x1="1952" />
            <wire x2="1984" y1="1456" y2="1456" x1="1968" />
        </branch>
        <bustap x2="1456" y1="1248" y2="1248" x1="1552" />
        <branch name="XLXN_22">
            <wire x2="1008" y1="1024" y2="1024" x1="864" />
            <wire x2="1024" y1="1024" y2="1024" x1="1008" />
            <wire x2="864" y1="1024" y2="1504" x1="864" />
            <wire x2="976" y1="1504" y2="1504" x1="864" />
        </branch>
        <iomarker fontsize="28" x="1984" y="1456" name="D7S_S(6:0)" orien="R0" />
        <instance x="1568" y="1488" name="XLXI_1" orien="R0">
        </instance>
        <branch name="X(3:0)">
            <wire x2="1552" y1="1216" y2="1248" x1="1552" />
            <wire x2="1552" y1="1248" y2="1312" x1="1552" />
            <wire x2="1552" y1="1312" y2="1456" x1="1552" />
            <wire x2="1568" y1="1456" y2="1456" x1="1552" />
        </branch>
        <branch name="CLK">
            <wire x2="1008" y1="1088" y2="1088" x1="704" />
            <wire x2="1024" y1="1088" y2="1088" x1="1008" />
            <wire x2="1008" y1="1088" y2="1344" x1="1008" />
            <wire x2="1008" y1="1344" y2="1344" x1="912" />
            <wire x2="912" y1="1344" y2="1440" x1="912" />
            <wire x2="976" y1="1440" y2="1440" x1="912" />
        </branch>
        <instance x="976" y="1600" name="XLXI_2" orien="R0">
        </instance>
        <branch name="X(2:0)">
            <wire x2="1376" y1="1440" y2="1440" x1="1360" />
            <wire x2="1376" y1="1248" y2="1440" x1="1376" />
            <wire x2="1456" y1="1248" y2="1248" x1="1376" />
        </branch>
        <branch name="XLXN_27">
            <wire x2="976" y1="1568" y2="1568" x1="912" />
            <wire x2="912" y1="1568" y2="1664" x1="912" />
            <wire x2="1440" y1="1664" y2="1664" x1="912" />
            <wire x2="1440" y1="1088" y2="1088" x1="1408" />
            <wire x2="1440" y1="1088" y2="1664" x1="1440" />
        </branch>
        <iomarker fontsize="28" x="704" y="1088" name="CLK" orien="R180" />
    </sheet>
</drawing>