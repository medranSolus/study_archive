----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    17:07:37 12/02/2019 
-- Design Name: 
-- Module Name:    Lock - Behavioral 
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

entity Lock is
    Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
           Rdy : in  STD_LOGIC;
           Clk_XT : in  STD_LOGIC;
			  Reset: in STD_LOGIC;
			  Shift: in  STD_LOGIC;
           Y : out  STD_LOGIC;
			  IsShift : out  STD_LOGIC);
end Lock;

architecture Behavioral of Lock is
	type state_type is (q0, q0_1, q1, q1_1, q2, q2_1, q3, q3_1, q4, q4_1);
	signal state, next_state : state_type;
begin
	processClk : process(Clk_XT) 
	begin 
		if rising_edge( Clk_XT ) then 
			if Reset = '1' then 
				state <= q0; 
			else 
				state <= next_state;
			end if; 
		end if;
	end process processClk;
	IsShift <= Shift;
	processState : process(state, Rdy) 
	begin
		next_state <= state;
		if Rdy = '1' then
			case state is
				when q0 =>
					if X = X"F0" then
						next_state <= q0_1;
					elsif X = X"23" then
						next_state <= q1;
					end if;
				when q0_1 =>
					next_state <= q0;
				when q1 =>
					if X = X"F0" then
						next_state <= q1_1;
					elsif X = X"42" then
						next_state <= q2;
					elsif X /= X"23" and X /= X"00" then
						next_state <= q0;
					end if;
				when q1_1 =>
					next_state <= q1;
				when q2 =>
					if X = X"F0" then
						next_state <= q2_1;
					elsif X = X"3A" and Shift = '1' then
						next_state <= q3;
					elsif X = X"23" then
						next_state <= q1;
					elsif X /= X"00" then
						next_state <= q0;
					end if;
				when q2_1 =>
					next_state <= q2;
				when q3 =>
					if X = X"F0" then
						next_state <= q3_1;
					elsif X = X"3A" and Shift = '1' then
						next_state <= q4;
					elsif X = X"23" then
						next_state <= q1;
					elsif X /= X"00" then
						next_state <= q0;
					end if;
				when q3_1 =>
					next_state <= q3;
				when q4 =>
					if X = X"F0" then
						next_state <= q4_1;
					elsif X = X"23" then
						next_state <= q1;
					elsif X /= X"00" then
						next_state <= q0;
					end if;
				when q4_1 =>
					next_state <= q4;
			end case;
		end if;
	end process processState;
	
	Y <= '1' when state = q4 or state = q4_1 else '0';
end Behavioral;

