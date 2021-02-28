--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   17:19:19 11/04/2019
-- Design Name:   
-- Module Name:   C:/Users/lab/lab4_1/AdderModuloTest.vhd
-- Project Name:  lab4_1
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: AdderModuloArch
-- 
-- Dependencies:
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
--
-- Notes: 
-- This testbench has been automatically generated using types std_logic and
-- std_logic_vector for the ports of the unit under test.  Xilinx recommends
-- that these types always be used for the top-level I/O of a design in order
-- to guarantee that the testbench will bind correctly to the post-implementation 
-- simulation model.
--------------------------------------------------------------------------------
LIBRARY ieee;
USE ieee.std_logic_1164.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY AdderModuloTest IS
END AdderModuloTest;
 
ARCHITECTURE behavior OF AdderModuloTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT AdderModuloArch
    PORT(
         X : IN  std_logic_vector(3 downto 0);
         Y : OUT  std_logic_vector(3 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal X : std_logic_vector(3 downto 0) := (others => '0');

 	--Outputs
   signal Y : std_logic_vector(3 downto 0);

 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: AdderModuloArch PORT MAP (
          X => X,
          Y => Y
        );
	X <=  "1111", "0000" after 100 ns, "0001" after 200 ns, "0010" after 300 ns, "0011" after 400 ns, "0100" after 500 ns, "0101" after 600 ns, "0110" after 700 ns,
			"0111" after 800 ns, "1000" after 900 ns, "1001" after 1000 ns, "1010" after 1100 ns, "1011" after 1200 ns, "1100" after 1300 ns,
			"1101" after 1400 ns, "1110" after 1500 ns;

END;
