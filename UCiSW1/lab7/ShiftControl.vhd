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
			  Clk : in  STD_LOGIC;
			  F0: in  STD_LOGIC;
			  F0_out: out  STD_LOGIC;
			  X_out : out  STD_LOGIC_VECTOR (7 downto 0);
			  Rdy_out : out  STD_LOGIC;
			  Shift : out  STD_LOGIC);
end ShiftControl;

architecture ShiftControlArch of ShiftControl is
	signal isShift : STD_LOGIC := '0';
	
begin
	Shift <= isShift;
	
	process (Clk, Rdy)
	begin
		if rising_edge(Clk) and Rdy = '1' then
		
		end if;
	end process;
	
	F0_out <= F0;
	X_out <= X;
	Rdy_out <= Rdy;
	
end ShiftControlArch;

