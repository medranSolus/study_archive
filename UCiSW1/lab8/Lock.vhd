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
use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
library UNISIM;
use UNISIM.VComponents.all;

entity Lock is
    Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
			  Rdy : in  STD_LOGIC;
			  Clk : in  STD_LOGIC;
			  Reset: in  STD_LOGIC;
			  Shift: in  STD_LOGIC;
			  LCDBusy: in  STD_LOGIC;
			  F0: in  STD_LOGIC;
			  Y : out  STD_LOGIC;
			  Opened : out  STD_LOGIC;
			  Q : out  STD_LOGIC_VECTOR (3 downto 0);
			  Symbol : out  STD_LOGIC_VECTOR (7 downto 0);
			  Counter : out  UNSIGNED (7 downto 0));
end Lock;

architecture Behavioral of Lock is
	type state_type is (q0, q1, q2, q3, q4, q5_O);
	signal state, next_state : state_type;
	signal stateCode : STD_LOGIC_VECTOR (3 downto 0) := "0000";
	signal nextStateCode : STD_LOGIC_VECTOR (3 downto 0) := "0000";
	signal count : UNSIGNED (7 downto 0) := X"00";
	signal asciiSymbol : STD_LOGIC_VECTOR (7 downto 0) := X"00";
	signal waitForLCD : STD_LOGIC := '0';
	
begin
	processClk : process(Clk) 
	begin 
		if rising_edge( Clk ) then 
			if Reset = '1' then 
				state <= q0;
				stateCode <= "0000";
			else 
				state <= next_state;
				stateCode <= nextStateCode;
			end if;
		end if;
	end process processClk;
	
	processState : process(state, Rdy, LCDBusy)
	begin
		next_state <= state;
		nextStateCode <= stateCode;
		Opened <= '0';
		asciiSymbol <= X"00";
		Symbol <= X"00";
		if (Rdy = '1' and F0 = '0') or state = q5_O or state = q4 then
			case state is
				when q0 =>
					if X = X"23" then
						next_state <= q1;
						nextStateCode <= "0001";
					end if;
				when q1 =>
					if X = X"42" then
						next_state <= q2;
						nextStateCode <= "0010";
					elsif X /= X"23" and X /= X"00" then
						next_state <= q0;
						nextStateCode <= "0000";
					end if;
				when q2 =>
					if X = X"3A" then
						next_state <= q3;
						nextStateCode <= "0011";
					elsif X = X"23" then
						next_state <= q1;
						nextStateCode <= "0001";
					elsif X /= X"00" then
						next_state <= q0;
						nextStateCode <= "0000";
					end if;
				when q3 =>
					if X = X"3A" then
						next_state <= q4;
						nextStateCode <= "0100";
					elsif X = X"23" then
						next_state <= q1;
						nextStateCode <= "0001";
					elsif X /= X"00" then
						next_state <= q0;
						nextStateCode <= "0000";
					end if;
				when q4 =>
					if waitForLCD = '1' then
						Symbol <= X"4F";
						Opened <= '1';
						next_state <= q5_O;
					elsif X = X"23" then
						next_state <= q1;
						nextStateCode <= "0001";
					elsif X /= X"00" then
						next_state <= q0;
						nextStateCode <= "0000";
					end if;
				when q5_O =>
					if LCDBusy = '0' then
						asciiSymbol <= STD_LOGIC_VECTOR(count);
						Symbol <= X"3" & asciiSymbol(3 downto 0);
						Opened <= '1';
						next_state <= q4;
					end if;
			end case;
		end if;
	end process processState;
	
	Y <= '1' when state = q4 else '0';
	Q <= stateCode;
	Counter <= count;
	
	process(Clk, Rdy, F0)
	begin
		if rising_edge( Clk ) then 
			if Reset = '1' then
				count <= X"00";
			elsif  Rdy = '1' and F0 = '0' then
				if next_state = q4 and state = q3 then
					count <= count + 1;
				end if;
			end if;
		end if;
	end process;
	
	process(Clk, Rdy, F0)
	begin
		if rising_edge( Clk ) then 
			if next_state = q4 and state = q3 then
				waitForLCD <= '1';
			elsif next_state = q5_O and state = q4 then
				waitForLCD <= '0';
			end if;
		end if;
	end process;
	
end Behavioral;

