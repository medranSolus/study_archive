--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   19:36:38 11/18/2019
-- Design Name:   
-- Module Name:   C:/Users/lab/lab5_2/detectorTest.vhd
-- Project Name:  lab5_2
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: detector
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
 
ENTITY detectorTest IS
END detectorTest;
 
ARCHITECTURE behavior OF detectorTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT detector
    PORT(
         X : IN  std_logic;
         CE : IN  std_logic;
         Reset : IN  std_logic;
         CLK : IN  std_logic;
         Y : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal X : std_logic := '0';
   signal CE : std_logic := '1';
   signal Reset : std_logic := '0';
   signal CLK : std_logic := '1';

 	--Outputs
   signal Y : std_logic;

 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: detector PORT MAP (
          X => X,
          CE => CE,
          Reset => Reset,
          CLK => CLK,
          Y => Y
        );

   process
		variable vector : STD_LOGIC_VECTOR(17 downto 0) := "010011000100110110";
		variable i : NATURAL := 18;
	begin
		loop
			CLK <= not CLK;
			if CLK = '1' then
				exit when i = 0;
				i := i - 1;
				X <= vector(i);
			end if;
			wait for 50 ns;
		end loop;
	end process;

END;
