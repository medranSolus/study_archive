-- Vhdl test bench created from schematic C:\XilinxPrj\project1\source1.sch - Mon Oct 14 18:01:23 2019
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
ENTITY source1_source1_sch_tb IS
END source1_source1_sch_tb;
ARCHITECTURE behavioral OF source1_source1_sch_tb IS 

   COMPONENT source1
   PORT( Y	:	OUT	STD_LOGIC_VECTOR (2 DOWNTO 0); 
          X	:	IN	STD_LOGIC_VECTOR (2 DOWNTO 0));
   END COMPONENT;

   SIGNAL Y	:	STD_LOGIC_VECTOR (2 DOWNTO 0);
   SIGNAL X	:	STD_LOGIC_VECTOR (2 DOWNTO 0);

BEGIN

   UUT: source1 PORT MAP(
		Y => Y, 
		X => X
   );

	X <= "000", "001" after 100 ns, "010" after 200 ns, "011" after 300 ns, "100" after 400 ns, "101" after 500 ns, "110" after 600 ns, "111" after 700 ns, "000" after 800 ns; 
		
-- *** Test Bench - User Defined Section ***
   
-- *** End Test Bench - User Defined Section ***

END;
