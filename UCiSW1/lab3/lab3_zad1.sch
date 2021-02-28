<?xml version="1.0" encoding="UTF-8"?>
<drawing version="7">
    <attr value="xc9500xl" name="DeviceFamilyName">
        <trait delete="all:0" />
        <trait editname="all:0" />
        <trait edittrait="all:0" />
    </attr>
    <netlist>
        <signal name="Q(2:0)" />
        <signal name="Q(1)" />
        <signal name="Q(0)" />
        <signal name="XLXN_10" />
        <signal name="XLXN_11" />
        <signal name="CLK" />
        <signal name="XLXN_13" />
        <signal name="XLXN_14" />
        <signal name="XLXN_17" />
        <signal name="CLR" />
        <signal name="XLXN_20" />
        <signal name="CE" />
        <signal name="XLXN_32" />
        <signal name="Q(2)" />
        <signal name="XLXN_41" />
        <signal name="XLXN_43" />
        <signal name="XLXN_45" />
        <signal name="XLXN_46" />
        <signal name="XLXN_47" />
        <signal name="XLXN_48" />
        <signal name="XLXN_49" />
        <signal name="XLXN_50" />
        <signal name="XLXN_51" />
        <signal name="XLXN_53" />
        <signal name="XLXN_54" />
        <port polarity="Output" name="Q(2:0)" />
        <port polarity="Input" name="CLK" />
        <port polarity="Input" name="CLR" />
        <port polarity="Input" name="CE" />
        <blockdef name="fjkce">
            <timestamp>2000-1-1T10:10:10</timestamp>
            <line x2="64" y1="-192" y2="-192" x1="0" />
            <line x2="64" y1="-32" y2="-32" x1="192" />
            <line x2="192" y1="-64" y2="-32" x1="192" />
            <line x2="64" y1="-128" y2="-144" x1="80" />
            <line x2="80" y1="-112" y2="-128" x1="64" />
            <rect width="256" x="64" y="-384" height="320" />
            <line x2="320" y1="-256" y2="-256" x1="384" />
            <line x2="64" y1="-320" y2="-320" x1="0" />
            <line x2="64" y1="-32" y2="-32" x1="0" />
            <line x2="64" y1="-128" y2="-128" x1="0" />
            <line x2="64" y1="-256" y2="-256" x1="0" />
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
        <block symbolname="fjkce" name="XLXI_12">
            <blockpin signalname="CLK" name="C" />
            <blockpin signalname="CE" name="CE" />
            <blockpin signalname="CLR" name="CLR" />
            <blockpin signalname="XLXN_50" name="J" />
            <blockpin signalname="XLXN_50" name="K" />
            <blockpin signalname="Q(0)" name="Q" />
        </block>
        <block symbolname="fjkce" name="XLXI_13">
            <blockpin signalname="CLK" name="C" />
            <blockpin signalname="CE" name="CE" />
            <blockpin signalname="CLR" name="CLR" />
            <blockpin signalname="XLXN_47" name="J" />
            <blockpin signalname="Q(0)" name="K" />
            <blockpin signalname="Q(1)" name="Q" />
        </block>
        <block symbolname="fjkce" name="XLXI_14">
            <blockpin signalname="CLK" name="C" />
            <blockpin signalname="CE" name="CE" />
            <blockpin signalname="CLR" name="CLR" />
            <blockpin signalname="Q(0)" name="J" />
            <blockpin signalname="XLXN_45" name="K" />
            <blockpin signalname="Q(2)" name="Q" />
        </block>
        <block symbolname="xnor2" name="XLXI_15">
            <blockpin signalname="Q(0)" name="I0" />
            <blockpin signalname="Q(1)" name="I1" />
            <blockpin signalname="XLXN_45" name="O" />
        </block>
        <block symbolname="xnor2" name="XLXI_16">
            <blockpin signalname="Q(2)" name="I0" />
            <blockpin signalname="Q(0)" name="I1" />
            <blockpin signalname="XLXN_47" name="O" />
        </block>
        <block symbolname="or2" name="XLXI_17">
            <blockpin signalname="Q(2)" name="I0" />
            <blockpin signalname="Q(1)" name="I1" />
            <blockpin signalname="XLXN_50" name="O" />
        </block>
    </netlist>
    <sheet sheetnum="1" width="3520" height="2720">
        <branch name="Q(0)">
            <wire x2="624" y1="160" y2="352" x1="624" />
            <wire x2="896" y1="352" y2="352" x1="624" />
            <wire x2="1040" y1="352" y2="352" x1="896" />
            <wire x2="1040" y1="352" y2="880" x1="1040" />
            <wire x2="1680" y1="880" y2="880" x1="1040" />
            <wire x2="1040" y1="880" y2="1264" x1="1040" />
            <wire x2="1040" y1="1264" y2="1312" x1="1040" />
            <wire x2="1680" y1="1312" y2="1312" x1="1040" />
            <wire x2="896" y1="352" y2="688" x1="896" />
            <wire x2="2224" y1="160" y2="160" x1="624" />
            <wire x2="2224" y1="160" y2="400" x1="2224" />
            <wire x2="2304" y1="400" y2="400" x1="2224" />
            <wire x2="816" y1="688" y2="768" x1="816" />
            <wire x2="864" y1="768" y2="768" x1="816" />
            <wire x2="896" y1="688" y2="688" x1="816" />
            <wire x2="1040" y1="1264" y2="1264" x1="848" />
            <wire x2="848" y1="1264" y2="1408" x1="848" />
            <wire x2="896" y1="1408" y2="1408" x1="848" />
            <wire x2="2224" y1="400" y2="400" x1="2064" />
        </branch>
        <bustap x2="2304" y1="400" y2="400" x1="2400" />
        <bustap x2="2304" y1="880" y2="880" x1="2400" />
        <bustap x2="2304" y1="1376" y2="1376" x1="2400" />
        <branch name="CLK">
            <wire x2="1312" y1="1104" y2="1104" x1="512" />
            <wire x2="1488" y1="1104" y2="1104" x1="1312" />
            <wire x2="1312" y1="1104" y2="1504" x1="1312" />
            <wire x2="1680" y1="1504" y2="1504" x1="1312" />
            <wire x2="1312" y1="1088" y2="1104" x1="1312" />
            <wire x2="1328" y1="1088" y2="1088" x1="1312" />
            <wire x2="1680" y1="528" y2="528" x1="1328" />
            <wire x2="1328" y1="528" y2="1088" x1="1328" />
            <wire x2="1488" y1="1008" y2="1104" x1="1488" />
            <wire x2="1680" y1="1008" y2="1008" x1="1488" />
        </branch>
        <iomarker fontsize="28" x="512" y="1104" name="CLK" orien="R180" />
        <instance x="1680" y="656" name="XLXI_12" orien="R0" />
        <instance x="1680" y="1136" name="XLXI_13" orien="R0" />
        <instance x="1680" y="1632" name="XLXI_14" orien="R0" />
        <branch name="CLR">
            <wire x2="1472" y1="1888" y2="1888" x1="544" />
            <wire x2="1472" y1="624" y2="1120" x1="1472" />
            <wire x2="1680" y1="1120" y2="1120" x1="1472" />
            <wire x2="1472" y1="1120" y2="1600" x1="1472" />
            <wire x2="1472" y1="1600" y2="1888" x1="1472" />
            <wire x2="1680" y1="1600" y2="1600" x1="1472" />
            <wire x2="1680" y1="624" y2="624" x1="1472" />
            <wire x2="1680" y1="1104" y2="1120" x1="1680" />
        </branch>
        <iomarker fontsize="28" x="544" y="1888" name="CLR" orien="R180" />
        <branch name="CE">
            <wire x2="1200" y1="464" y2="464" x1="352" />
            <wire x2="1680" y1="464" y2="464" x1="1200" />
            <wire x2="1200" y1="464" y2="944" x1="1200" />
            <wire x2="1200" y1="944" y2="1440" x1="1200" />
            <wire x2="1680" y1="1440" y2="1440" x1="1200" />
            <wire x2="1680" y1="944" y2="944" x1="1200" />
        </branch>
        <iomarker fontsize="28" x="352" y="464" name="CE" orien="R180" />
        <branch name="Q(2)">
            <wire x2="864" y1="832" y2="832" x1="640" />
            <wire x2="640" y1="832" y2="1168" x1="640" />
            <wire x2="1136" y1="1168" y2="1168" x1="640" />
            <wire x2="2224" y1="1168" y2="1168" x1="1136" />
            <wire x2="2224" y1="1168" y2="1376" x1="2224" />
            <wire x2="2320" y1="1376" y2="1376" x1="2224" />
            <wire x2="1136" y1="256" y2="256" x1="1072" />
            <wire x2="1072" y1="256" y2="336" x1="1072" />
            <wire x2="1136" y1="336" y2="336" x1="1072" />
            <wire x2="1136" y1="336" y2="1168" x1="1136" />
            <wire x2="2224" y1="1376" y2="1376" x1="2064" />
            <wire x2="2320" y1="1376" y2="1376" x1="2304" />
        </branch>
        <instance x="896" y="1472" name="XLXI_15" orien="R0" />
        <branch name="XLXN_45">
            <wire x2="1680" y1="1376" y2="1376" x1="1152" />
        </branch>
        <instance x="864" y="896" name="XLXI_16" orien="R0" />
        <branch name="XLXN_47">
            <wire x2="1392" y1="800" y2="800" x1="1120" />
            <wire x2="1392" y1="800" y2="816" x1="1392" />
            <wire x2="1680" y1="816" y2="816" x1="1392" />
        </branch>
        <instance x="1136" y="320" name="XLXI_17" orien="R0" />
        <branch name="XLXN_50">
            <wire x2="1536" y1="224" y2="224" x1="1392" />
            <wire x2="1536" y1="224" y2="336" x1="1536" />
            <wire x2="1680" y1="336" y2="336" x1="1536" />
            <wire x2="1536" y1="336" y2="400" x1="1536" />
            <wire x2="1680" y1="400" y2="400" x1="1536" />
        </branch>
        <branch name="Q(1)">
            <wire x2="640" y1="640" y2="752" x1="640" />
            <wire x2="800" y1="752" y2="752" x1="640" />
            <wire x2="800" y1="752" y2="1344" x1="800" />
            <wire x2="896" y1="1344" y2="1344" x1="800" />
            <wire x2="976" y1="640" y2="640" x1="640" />
            <wire x2="2224" y1="640" y2="640" x1="976" />
            <wire x2="2224" y1="640" y2="880" x1="2224" />
            <wire x2="2304" y1="880" y2="880" x1="2224" />
            <wire x2="1136" y1="192" y2="192" x1="976" />
            <wire x2="976" y1="192" y2="640" x1="976" />
            <wire x2="2224" y1="880" y2="880" x1="2064" />
        </branch>
        <iomarker fontsize="28" x="2688" y="288" name="Q(2:0)" orien="R0" />
        <branch name="Q(2:0)">
            <wire x2="2400" y1="288" y2="400" x1="2400" />
            <wire x2="2400" y1="400" y2="880" x1="2400" />
            <wire x2="2400" y1="880" y2="1376" x1="2400" />
            <wire x2="2400" y1="1376" y2="1680" x1="2400" />
            <wire x2="2608" y1="288" y2="288" x1="2400" />
            <wire x2="2688" y1="288" y2="288" x1="2608" />
        </branch>
    </sheet>
</drawing>