--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   18:43:59 11/04/2019
-- Design Name:   
-- Module Name:   C:/Users/lab/lab4_2/CounterTest.vhd
-- Project Name:  lab4_2
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Counter
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
 
ENTITY CounterTest IS
END CounterTest;
 
ARCHITECTURE behavior OF CounterTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Counter
    PORT(
         DIR : IN  std_logic;
         CE : IN  std_logic;
         CLR : IN  std_logic;
         CLK : IN  std_logic;
         Q : OUT  std_logic_vector(2 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal DIR : std_logic := '0';
   signal CE : std_logic := '1';
   signal CLR : std_logic := '0';
   signal CLK : std_logic := '0';

 	--Outputs
   signal Q : std_logic_vector(2 downto 0);

 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Counter PORT MAP (
          DIR => DIR,
          CE => CE,
          CLR => CLR,
          CLK => CLK,
          Q => Q
        );
	CLK <= not CLK after 50 ns;
	DIR <= not DIR after 2000 ns;
END;
