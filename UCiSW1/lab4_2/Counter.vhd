----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    17:43:03 11/04/2019 
-- Design Name: 
-- Module Name:    Counter - CounterArch 
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

entity Counter is
    Port ( DIR : in  STD_LOGIC;
			  CE : in  STD_LOGIC;
           CLR : in  STD_LOGIC;
           CLK : in  STD_LOGIC;
			  Q : out STD_LOGIC_VECTOR(2 downto 0));
end Counter;

architecture CounterArch of Counter is
	signal Q_INT : STD_LOGIC_VECTOR(2 downto 0);
	signal D_INT : STD_LOGIC_VECTOR(2 downto 0);
begin
	Q <= Q_INT;
	FD0 : FDCE port map( CLR => CLR, CE => CE, C => CLK, Q => Q_INT(0), D => D_INT(0));
	FD1 : FDCE port map( CLR => CLR, CE => CE, C => CLK, Q => Q_INT(1), D => D_INT(1));
	FD2 : FDCE port map( CLR => CLR, CE => CE, C => CLK, Q => Q_INT(2), D => D_INT(2));
	with DIR & Q_INT select
	D_INT <=	"000" when "0110",
				"111" when "0000",
				"001" when "0111",
				"010" when "0001",
				"011" when "0010",
				"100" when "0011",
				"101" when "0100",
				"110" when "0101",
				"101" when "1110",
				"100" when "1101",
				"011" when "1100",
				"010" when "1011",
				"001" when "1010",
				"111" when "1001",
				"000" when "1111",
				"110" when "1000",
				"XXX" when others;

end CounterArch;

