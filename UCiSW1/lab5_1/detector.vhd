----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    17:04:18 11/18/2019 
-- Design Name: 
-- Module Name:    detector - Behavioral 
-- Project Name: 
-- Target Devices: 
-- Tool versions: 
-- Description: 
--
-- Dependencies: 
--
-- Revision: 
-- Revision 0.01 - File Created
-- Additional Comments: 
--
----------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
library UNISIM;
use UNISIM.VComponents.all;

entity detector is
    Port ( X : in  STD_LOGIC;
			  CE : in  STD_LOGIC;
			  CLR : in  STD_LOGIC;
			  CLK : in  STD_LOGIC;
           Y : out  STD_LOGIC;
			  Qo : out  STD_LOGIC_VECTOR(2 downto 0));
end detector;

architecture detectorArch of detector is
	signal D : STD_LOGIC_VECTOR (2 downto 0) := "000";
	signal Q : STD_LOGIC_VECTOR (2 downto 0) := "000";
begin
	gen: for i in 0 to 2 generate
		FDCEi : FDCE port map(C => CLK, CLR => CLR, CE => CE, Q => Q(i), D => D(i));
	end generate;
	
	Y <= Q(2) and Q(1) and Q(0);
	
	Qo <= Q;
	
	with X & Q select
	D <= "001" when "0000",
		  "010" when "0001",
		  "011" when "1010",
		  "100" when "1011",
		  "101" when "0100",
		  "111" when "0101",
		  "001" when "0111",
		  "000" when others;
end detectorArch;

