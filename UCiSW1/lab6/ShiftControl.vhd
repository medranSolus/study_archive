----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    18:57:47 12/02/2019 
-- Design Name: 
-- Module Name:    ShiftControl - ShiftControlArch 
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
use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
library UNISIM;
use UNISIM.VComponents.all;

entity ShiftControl is
    Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
           Rdy : in  STD_LOGIC;
           Clk_XT : in  STD_LOGIC;
           X_out : out  STD_LOGIC_VECTOR (7 downto 0);
			  Rdy_out : out  STD_LOGIC;
           Shift : out  STD_LOGIC);
end ShiftControl;

architecture ShiftControlArch of ShiftControl is
	signal break : STD_LOGIC := '0';
	signal isShift : STD_LOGIC := '0';
begin
	Shift <= isShift;
	process(Clk_XT, Rdy)
	begin
		Rdy_out <= Rdy;
		if rising_edge(Clk_XT) and Rdy = '1' then
			if X = X"F0" then
				break <= '1';
					if isShift = '0' then
						isShift <= '0';
					else
						isShift <= '1';
					end if;
				X_out <= X"F0";
			elsif X = X"12" and break = '0' then
				break <= '0';
				isShift <= '1';
				X_out <= X"00";
			else
				break <= '0';
				if X = X"12" then
					isShift <= '0';
					X_out <= X"00";
				else
					if isShift = '0' then
						isShift <= '0';
					else
						isShift <= '1';
					end if;
					X_out <= X;
				end if;
			end if;
		end if;
	end process;
end ShiftControlArch;

