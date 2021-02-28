----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    19:29:17 11/18/2019 
-- Design Name: 
-- Module Name:    detector - detectorArch 
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

entity detector is
    Port ( X : in  STD_LOGIC;
           CE : in  STD_LOGIC;
           Reset : in  STD_LOGIC;
           CLK : in  STD_LOGIC;
           Y : out  STD_LOGIC);
end detector;

architecture detectorArch of detector is
	type state_type is ( q0, q1, q2, q3, q4, q5, q6 );
	signal state, next_state : state_type;
begin
	processClk : process( Clk )
	begin 
		if rising_edge( Clk ) then 
			if Reset = '1' then 
				state <= q0; 
			else 
				state <= next_state; 
			end if; 
		end if; 
	end process processClk;
	
	processCase : process(state, X) 
	begin
		case state is
			when q0 =>
				if X = '0' then
					next_state <= q1;
				else
					next_state <= q0;
				end if;
			when q1 =>
				if X = '0' then
					next_state <= q2;
				else
					next_state <= q0;
				end if;
			when q2 =>
				if X = '1' then
					next_state <= q3;
				else
					next_state <= q0;
				end if;
			when q3 =>
				if X = '1' then
					next_state <= q4;
				else
					next_state <= q0;
				end if;
			when q4 =>
				if X = '0' then
					next_state <= q5;
				else
					next_state <= q0;
				end if;
			when q5 =>
				if X = '0' then
					next_state <= q6;
				else
					next_state <= q0;
				end if;
			when q6 =>
				if X = '0' then
					next_state <= q1;
				else
					next_state <= q0;
				end if;
		end case;
	end process processCase;
	
	Y <= '1' when state = q6 else
		  '0';

end detectorArch;

