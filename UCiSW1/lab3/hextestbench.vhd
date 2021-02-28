-- Vhdl test bench created from schematic C:\Users\lab\lab3\hesLab3.sch - Mon Oct 21 18:59:57 2019
--
-- Notes: 
-- 1) This testbench template has been automatically generated using types
-- std_logic and std_logic_vector for the ports of the unit under test.
-- Xilinx recommends that these types always be used for the top-level
-- I/O of a design in order to guarantee that the testbench will bind
-- correctly to the timing (post-route) simulation model.
-- 2) To use this template as your testbench, change the filename to any
-- name of your choice with the extension .vhd, and use the "Source->Add"
-- menu in Project Navigator to import the testbench. Then
-- edit the user defined section below, adding code to generate the 
-- stimulus for your design.
--
LIBRARY ieee;
USE ieee.std_logic_1164.ALL;
USE ieee.numeric_std.ALL;
LIBRARY UNISIM;
USE UNISIM.Vcomponents.ALL;
ENTITY hesLab3_hesLab3_sch_tb IS
END hesLab3_hesLab3_sch_tb;
ARCHITECTURE behavioral OF hesLab3_hesLab3_sch_tb IS 

   COMPONENT hesLab3
   PORT( CLK	:	IN	STD_LOGIC; 
          CLR	:	IN	STD_LOGIC; 
          CE	:	IN	STD_LOGIC; 
          D7S_S	:	OUT	STD_LOGIC_VECTOR (6 DOWNTO 0));
   END COMPONENT;

   SIGNAL CLK	:	STD_LOGIC := '0';
   SIGNAL CLR	:	STD_LOGIC := '0';
   SIGNAL CE	:	STD_LOGIC := '1';
   SIGNAL D7S_S	:	STD_LOGIC_VECTOR (6 DOWNTO 0);

BEGIN

   UUT: hesLab3 PORT MAP(
		CLK => CLK, 
		CLR => CLR, 
		CE => CE, 
		D7S_S => D7S_S
   );
	CLK <= not CLK after 50 ns;
END;
