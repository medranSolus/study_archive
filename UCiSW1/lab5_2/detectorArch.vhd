--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   19:35:43 11/18/2019
-- Design Name:   
-- Module Name:   C:/Users/lab/lab5_2/detectorArch.vhd
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
 
ENTITY detectorArch IS
END detectorArch;
 
ARCHITECTURE behavior OF detectorArch IS 
 
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
   signal CE : std_logic := '0';
   signal Reset : std_logic := '0';
   signal CLK : std_logic := '0';

 	--Outputs
   signal Y : std_logic;

   -- Clock period definitions
   constant CLK_period : time := 10 ns;
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: detector PORT MAP (
          X => X,
          CE => CE,
          Reset => Reset,
          CLK => CLK,
          Y => Y
        );

   -- Clock process definitions
   CLK_process :process
   begin
		CLK <= '0';
		wait for CLK_period/2;
		CLK <= '1';
		wait for CLK_period/2;
   end process;
 

   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	

      wait for CLK_period*10;

      -- insert stimulus here 

      wait;
   end process;

END;
