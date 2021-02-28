----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    17:11:01 11/04/2019 
-- Design Name: 
-- Module Name:    AdderModuloArch - AdderModulo 
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
--library UNISIM;
--use UNISIM.VComponents.all;

entity AdderModuloArch is
    Port ( X : in  STD_LOGIC_VECTOR (3 downto 0);
           Y : out  STD_LOGIC_VECTOR (3 downto 0));
end AdderModuloArch;

architecture AdderModulo of AdderModuloArch is

begin
	with X(3 downto 0) select
	Y <=	"0000" when "1111",
			"0001" when "0000",
			"0010" when "0001",
			"0011" when "0010",
			"0100" when "0011",
			"0101" when "0100",
			"0110" when "0101",
			"0111" when "0110",
			"1000" when "0111",
			"1001" when "1000",
			"1010" when "1001",
			"1011" when "1010",
			"1100" when "1011",
			"1101" when "1100",
			"1110" when "1101",
			"1111" when "1110",
			"XXXX" when others;

end AdderModulo;

