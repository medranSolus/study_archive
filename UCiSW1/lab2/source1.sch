<?xml version="1.0" encoding="UTF-8"?>
<drawing version="7">
    <attr value="xc9500xl" name="DeviceFamilyName">
        <trait delete="all:0" />
        <trait editname="all:0" />
        <trait edittrait="all:0" />
    </attr>
    <netlist>
        <signal name="XLXN_9" />
        <signal name="XLXN_10" />
        <signal name="Y(0)">
        </signal>
        <signal name="Y(1)">
        </signal>
        <signal name="Y(2)">
        </signal>
        <signal name="XLXN_18" />
        <signal name="XLXN_19" />
        <signal name="XLXN_20" />
        <signal name="XLXN_21" />
        <signal name="XLXN_23" />
        <signal name="XLXN_24" />
        <signal name="XLXN_25" />
        <signal name="XLXN_26" />
        <signal name="XLXN_27" />
        <signal name="XLXN_32" />
        <signal name="XLXN_33" />
        <signal name="Y(2:0)" />
        <signal name="XLXN_39" />
        <signal name="XLXN_41" />
        <signal name="XLXN_42" />
        <signal name="XLXN_43" />
        <signal name="X(0)" />
        <signal name="X(1)" />
        <signal name="X(2)" />
        <signal name="X(2:0)" />
        <port polarity="Output" name="Y(2:0)" />
        <port polarity="Input" name="X(2:0)" />
        <blockdef name="and3">
            <timestamp>2000-1-1T10:10:10</timestamp>
            <line x2="64" y1="-64" y2="-64" x1="0" />
            <line x2="64" y1="-128" y2="-128" x1="0" />
            <line x2="64" y1="-192" y2="-192" x1="0" />
            <line x2="192" y1="-128" y2="-128" x1="256" />
            <line x2="144" y1="-176" y2="-176" x1="64" />
            <line x2="64" y1="-80" y2="-80" x1="144" />
            <arc ex="144" ey="-176" sx="144" sy="-80" r="48" cx="144" cy="-128" />
            <line x2="64" y1="-64" y2="-192" x1="64" />
        </blockdef>
        <blockdef name="inv">
            <timestamp>2000-1-1T10:10:10</timestamp>
            <line x2="64" y1="-32" y2="-32" x1="0" />
            <line x2="160" y1="-32" y2="-32" x1="224" />
            <line x2="128" y1="-64" y2="-32" x1="64" />
            <line x2="64" y1="-32" y2="0" x1="128" />
            <line x2="64" y1="0" y2="-64" x1="64" />
            <circle r="16" cx="144" cy="-32" />
        </blockdef>
        <blockdef name="xnor2">
            <timestamp>2000-1-1T10:10:10</timestamp>
            <line x2="64" y1="-64" y2="-64" x1="0" />
            <line x2="60" y1="-128" y2="-128" x1="0" />
            <arc ex="44" ey="-144" sx="48" sy="-48" r="56" cx="16" cy="-96" />
            <arc ex="64" ey="-144" sx="64" sy="-48" r="56" cx="32" cy="-96" />
            <line x2="64" y1="-144" y2="-144" x1="128" />
            <line x2="64" y1="-48" y2="-48" x1="128" />
            <arc ex="128" ey="-144" sx="208" sy="-96" r="88" cx="132" cy="-56" />
            <arc ex="208" ey="-96" sx="128" sy="-48" r="88" cx="132" cy="-136" />
            <circle r="8" cx="220" cy="-96" />
            <line x2="256" y1="-96" y2="-96" x1="228" />
            <line x2="60" y1="-28" y2="-28" x1="60" />
        </blockdef>
        <blockdef name="or2">
            <timestamp>2000-1-1T10:10:10</timestamp>
            <line x2="64" y1="-64" y2="-64" x1="0" />
            <line x2="64" y1="-128" y2="-128" x1="0" />
            <line x2="192" y1="-96" y2="-96" x1="256" />
            <arc ex="192" ey="-96" sx="112" sy="-48" r="88" cx="116" cy="-136" />
            <arc ex="48" ey="-144" sx="48" sy="-48" r="56" cx="16" cy="-96" />
            <line x2="48" y1="-144" y2="-144" x1="112" />
            <arc ex="112" ey="-144" sx="192" sy="-96" r="88" cx="116" cy="-56" />
            <line x2="48" y1="-48" y2="-48" x1="112" />
        </blockdef>
        <blockdef name="and2">
            <timestamp>2000-1-1T10:10:10</timestamp>
            <line x2="64" y1="-64" y2="-64" x1="0" />
            <line x2="64" y1="-128" y2="-128" x1="0" />
            <line x2="192" y1="-96" y2="-96" x1="256" />
            <arc ex="144" ey="-144" sx="144" sy="-48" r="48" cx="144" cy="-96" />
            <line x2="64" y1="-48" y2="-48" x1="144" />
            <line x2="144" y1="-144" y2="-144" x1="64" />
            <line x2="64" y1="-48" y2="-144" x1="64" />
        </blockdef>
        <block symbolname="inv" name="XLXI_2">
            <blockpin signalname="X(0)" name="I" />
            <blockpin signalname="Y(0)" name="O" />
        </block>
        <block symbolname="xnor2" name="XLXI_3">
            <blockpin signalname="X(1)" name="I0" />
            <blockpin signalname="X(0)" name="I1" />
            <blockpin signalname="Y(1)" name="O" />
        </block>
        <block symbolname="or2" name="XLXI_4">
            <blockpin signalname="XLXN_24" name="I0" />
            <blockpin signalname="XLXN_18" name="I1" />
            <blockpin signalname="Y(2)" name="O" />
        </block>
        <block symbolname="and3" name="XLXI_1">
            <blockpin signalname="X(2)" name="I0" />
            <blockpin signalname="XLXN_20" name="I1" />
            <blockpin signalname="Y(0)" name="I2" />
            <blockpin signalname="XLXN_18" name="O" />
        </block>
        <block symbolname="inv" name="XLXI_8">
            <blockpin signalname="X(1)" name="I" />
            <blockpin signalname="XLXN_20" name="O" />
        </block>
        <block symbolname="and2" name="XLXI_9">
            <blockpin signalname="XLXN_32" name="I0" />
            <blockpin signalname="XLXN_25" name="I1" />
            <blockpin signalname="XLXN_24" name="O" />
        </block>
        <block symbolname="or2" name="XLXI_10">
            <blockpin signalname="X(1)" name="I0" />
            <blockpin signalname="X(0)" name="I1" />
            <blockpin signalname="XLXN_25" name="O" />
        </block>
        <block symbolname="inv" name="XLXI_12">
            <blockpin signalname="X(2)" name="I" />
            <blockpin signalname="XLXN_32" name="O" />
        </block>
    </netlist>
    <sheet sheetnum="1" width="3520" height="2720">
        <instance x="1040" y="672" name="XLXI_2" orien="R0" />
        <instance x="1424" y="816" name="XLXI_3" orien="R0" />
        <branch name="Y(0)">
            <wire x2="1216" y1="720" y2="816" x1="1216" />
            <wire x2="1296" y1="720" y2="720" x1="1216" />
            <wire x2="1296" y1="640" y2="640" x1="1264" />
            <wire x2="1888" y1="640" y2="640" x1="1296" />
            <wire x2="1936" y1="640" y2="640" x1="1888" />
            <wire x2="1296" y1="640" y2="720" x1="1296" />
        </branch>
        <branch name="Y(1)">
            <wire x2="1872" y1="720" y2="720" x1="1680" />
            <wire x2="1936" y1="720" y2="720" x1="1872" />
        </branch>
        <instance x="1552" y="1072" name="XLXI_4" orien="R0" />
        <branch name="Y(2)">
            <wire x2="1920" y1="976" y2="976" x1="1808" />
            <wire x2="1936" y1="976" y2="976" x1="1920" />
        </branch>
        <instance x="1216" y="1008" name="XLXI_1" orien="R0" />
        <branch name="XLXN_18">
            <wire x2="1504" y1="880" y2="880" x1="1472" />
            <wire x2="1504" y1="880" y2="944" x1="1504" />
            <wire x2="1552" y1="944" y2="944" x1="1504" />
        </branch>
        <branch name="XLXN_20">
            <wire x2="1216" y1="880" y2="880" x1="1184" />
        </branch>
        <instance x="960" y="912" name="XLXI_8" orien="R0" />
        <branch name="XLXN_24">
            <wire x2="1552" y1="1008" y2="1008" x1="1536" />
            <wire x2="1536" y1="1008" y2="1088" x1="1536" />
            <wire x2="1600" y1="1088" y2="1088" x1="1536" />
            <wire x2="1600" y1="1088" y2="1168" x1="1600" />
            <wire x2="1600" y1="1168" y2="1168" x1="1536" />
        </branch>
        <instance x="1280" y="1264" name="XLXI_9" orien="R0" />
        <branch name="XLXN_25">
            <wire x2="1280" y1="1136" y2="1136" x1="1248" />
        </branch>
        <instance x="992" y="1232" name="XLXI_10" orien="R0" />
        <instance x="1008" y="1296" name="XLXI_12" orien="R0" />
        <branch name="XLXN_32">
            <wire x2="1248" y1="1264" y2="1264" x1="1232" />
            <wire x2="1248" y1="1200" y2="1264" x1="1248" />
            <wire x2="1280" y1="1200" y2="1200" x1="1248" />
        </branch>
        <iomarker fontsize="28" x="224" y="640" name="X(2:0)" orien="R180" />
        <branch name="Y(2:0)">
            <wire x2="2112" y1="576" y2="576" x1="2032" />
            <wire x2="2032" y1="576" y2="640" x1="2032" />
            <wire x2="2032" y1="640" y2="720" x1="2032" />
            <wire x2="2032" y1="720" y2="976" x1="2032" />
            <wire x2="2032" y1="976" y2="1024" x1="2032" />
        </branch>
        <iomarker fontsize="28" x="2112" y="576" name="Y(2:0)" orien="R0" />
        <branch name="X(0)">
            <wire x2="672" y1="704" y2="704" x1="432" />
            <wire x2="736" y1="704" y2="704" x1="672" />
            <wire x2="672" y1="704" y2="1104" x1="672" />
            <wire x2="992" y1="1104" y2="1104" x1="672" />
            <wire x2="1040" y1="640" y2="640" x1="736" />
            <wire x2="736" y1="640" y2="688" x1="736" />
            <wire x2="736" y1="688" y2="704" x1="736" />
            <wire x2="1424" y1="688" y2="688" x1="736" />
        </branch>
        <branch name="X(1)">
            <wire x2="480" y1="832" y2="832" x1="432" />
            <wire x2="480" y1="832" y2="880" x1="480" />
            <wire x2="960" y1="880" y2="880" x1="480" />
            <wire x2="592" y1="832" y2="832" x1="480" />
            <wire x2="928" y1="832" y2="832" x1="592" />
            <wire x2="592" y1="832" y2="1168" x1="592" />
            <wire x2="992" y1="1168" y2="1168" x1="592" />
            <wire x2="928" y1="752" y2="832" x1="928" />
            <wire x2="1424" y1="752" y2="752" x1="928" />
        </branch>
        <branch name="X(2)">
            <wire x2="480" y1="944" y2="944" x1="432" />
            <wire x2="1216" y1="944" y2="944" x1="480" />
            <wire x2="480" y1="944" y2="1264" x1="480" />
            <wire x2="1008" y1="1264" y2="1264" x1="480" />
        </branch>
        <branch name="X(2:0)">
            <wire x2="336" y1="640" y2="640" x1="224" />
            <wire x2="336" y1="640" y2="704" x1="336" />
            <wire x2="336" y1="704" y2="768" x1="336" />
            <wire x2="336" y1="768" y2="784" x1="336" />
            <wire x2="336" y1="784" y2="832" x1="336" />
            <wire x2="336" y1="832" y2="896" x1="336" />
            <wire x2="336" y1="896" y2="928" x1="336" />
            <wire x2="336" y1="928" y2="944" x1="336" />
            <wire x2="336" y1="944" y2="1024" x1="336" />
            <wire x2="336" y1="1024" y2="1424" x1="336" />
        </branch>
        <bustap x2="432" y1="704" y2="704" x1="336" />
        <bustap x2="432" y1="944" y2="944" x1="336" />
        <bustap x2="432" y1="832" y2="832" x1="336" />
        <bustap x2="1936" y1="976" y2="976" x1="2032" />
        <bustap x2="1936" y1="720" y2="720" x1="2032" />
        <bustap x2="1936" y1="640" y2="640" x1="2032" />
    </sheet>
</drawing>