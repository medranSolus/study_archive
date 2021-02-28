--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   17:56:53 12/02/2019
-- Design Name:   
-- Module Name:   C:/Users/lab/lab6/LockTest.vhd
-- Project Name:  lab6
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Lock
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
USE ieee.numeric_std.ALL;
 
ENTITY LockTest IS
END LockTest;
 
ARCHITECTURE behavior OF LockTest IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Lock
    PORT(
         X : IN  std_logic_vector(7 downto 0);
         Rdy : IN  std_logic;
         CLK : IN  std_logic;
         Reset : IN  std_logic;
         Y : OUT  std_logic);
    END COMPONENT;
    

   --Inputs
   signal X : std_logic_vector(7 downto 0) := (others => '0');
   signal Rdy : std_logic := '0';
   signal CLK : std_logic := '0';
   signal Reset : std_logic := '0';

 	--Outputs
   signal Y : std_logic;
	
	constant Tclk : TIME := 1 us / 50;  -- MHz 
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Lock PORT MAP (
          X => X,
          Rdy => Rdy,
          CLK => CLK,
          Reset => Reset,
          Y => Y);
		  
	CLK <= not CLK after Tclk / 2;
	
	process
		type typeByteArray is array ( NATURAL range <> ) of STD_LOGIC_VECTOR( 7 downto 0 ); 
		variable arrBytes : typeByteArray( 16 downto 0 ) := (X"10", X"20", X"23", X"42", X"3A", X"3A", X"30", X"4F", X"23", X"42", X"3A", X"23", X"42", X"3A", X"3A", X"54", X"11");
	begin
		wait for 100 ns;
		for i in arrBytes'RANGE loop
			X <= arrBytes(i);
			Rdy <= '1';
			wait for Tclk;
			Rdy <= '0';
			wait for 9 * Tclk;
			X <= X"F0";
			Rdy <= '1';
			wait for Tclk;
			Rdy <= '0';
			wait for 4 * Tclk;
			X <= arrBytes(i);
			Rdy <= '1';
			wait for Tclk;
			Rdy <= '0';
			wait for 14 * Tclk;
		end loop;
		wait;
	end process;
END;
