-- Vhdl test bench created from schematic C:\Users\lab\lab3\rs.sch - Mon Oct 21 19:24:00 2019
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
ENTITY rs_rs_sch_tb IS
END rs_rs_sch_tb;
ARCHITECTURE behavioral OF rs_rs_sch_tb IS 

   COMPONENT rs
   PORT( CLK	:	IN	STD_LOGIC; 
          RS_RX	:	IN	STD_LOGIC; 
          D7S_S	:	OUT	STD_LOGIC_VECTOR (6 DOWNTO 0));
   END COMPONENT;

   SIGNAL CLK	:	STD_LOGIC;
   SIGNAL RS_RX	:	STD_LOGIC;
   SIGNAL D7S_S	:	STD_LOGIC_VECTOR (6 DOWNTO 0);

BEGIN

   UUT: rs PORT MAP(
		CLK => CLK, 
		RS_RX => RS_RX, 
		D7S_S => D7S_S
   );

END;
