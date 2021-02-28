--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   19:34:20 11/04/2019
-- Design Name:   
-- Module Name:   C:/Users/lab/lab4_2/RSTest.vhd
-- Project Name:  lab4_2
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: RSController
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
 
ENTITY RSTest IS
END RSTest;
 
ARCHITECTURE behavior OF RSTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT RSController
    PORT(
         Result : OUT  std_logic_vector(1 downto 0);
         CHAR : IN  std_logic_vector(7 downto 0)
        );
    END COMPONENT;
    

   --Inputs
   signal CHAR : std_logic_vector(7 downto 0) := (others => '0');

 	--Outputs
   signal Result : std_logic_vector(1 downto 0);
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 

 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: RSController PORT MAP (
          Result => Result,
          CHAR => CHAR
        );

END;
