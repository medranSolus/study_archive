----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    17:05:33 12/02/2019 
-- Design Name: 
-- Module Name:    lab6 - lab6Arch 
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

entity lab6 is
    Port ( PS2_CLK : in  STD_LOGIC;
           PS2_Data : in  STD_LOGIC;
           Reset : in  STD_LOGIC;
           Clk_XT : in  STD_LOGIC;
			  Y : out  STD_LOGIC;
			  IsShift : out  STD_LOGIC);
end lab6;

architecture lab6Arch of lab6 is
	component PS2_Rx is
		Port ( PS2_Clk : in  STD_LOGIC;
				 PS2_Data : in  STD_LOGIC;
				 Reset : in  STD_LOGIC;
				 Clk : in  STD_LOGIC;
				 DO_Rdy : out  STD_LOGIC;
				 DO : out  STD_LOGIC_VECTOR (7 downto 0));
	end component;
	component Lock is
		Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
				 Rdy : in  STD_LOGIC;
				 Clk_XT : in  STD_LOGIC;
				 Reset: in  STD_LOGIC;
				 Shift: in  STD_LOGIC;
				 Y : out  STD_LOGIC;
			    IsShift : out  STD_LOGIC);
	end component;
	component ShiftControl is
		 Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
				  Rdy : in  STD_LOGIC;
				  Clk_XT : in  STD_LOGIC;
				  X_out : out  STD_LOGIC_VECTOR (7 downto 0);
				  Rdy_out : out  STD_LOGIC;
				  Shift : out  STD_LOGIC);
	end component;

	signal BYTE : STD_LOGIC_VECTOR (7 downto 0) := "00000000";
	signal BYTEshift : STD_LOGIC_VECTOR (7 downto 0) := "00000000";
	signal Rdy : STD_LOGIC := '0';
	signal Rdyshift : STD_LOGIC := '0';
	signal shift : STD_LOGIC := '0';
begin
	PS2 : PS2_Rx port map (PS2_Clk => PS2_CLK, PS2_Data => PS2_Data, Reset => Reset, Clk => Clk_XT, DO_Rdy => Rdy, DO => BYTE);
	Shiftc : ShiftControl port map (X => BYTE, Rdy => Rdy, Clk_XT => Clk_XT, X_out => BYTEshift, Rdy_out => Rdyshift, Shift => shift);
	Lck : Lock port map (X => BYTEshift, Rdy => Rdyshift, Clk_XT => Clk_XT, Reset => Reset, Y => Y, Shift => shift, IsShift => IsShift);
end lab6Arch;

