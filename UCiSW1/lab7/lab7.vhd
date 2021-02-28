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

entity lab7 is
    Port ( PS2_Clk : in  STD_LOGIC;
           PS2_Data : in  STD_LOGIC;
           Clk : in  STD_LOGIC;
			  Reset: in  STD_LOGIC;
			  Y : out  STD_LOGIC;
			  IsShift : out  STD_LOGIC;
			  LCD_E: out  STD_LOGIC;
			  LCD_RS: out  STD_LOGIC;
			  LCD_RW: out  STD_LOGIC;
			  LCD_D: out  STD_LOGIC_VECTOR ( 3 downto 0);
			  SF_CE: out  STD_LOGIC);
end lab7;

architecture lab7Arch of lab7 is
	component PS2_Kbd is
		Port ( PS2_Clk : in  STD_LOGIC;
				 PS2_Data : in  STD_LOGIC;
				 Clk_50MHz : in  STD_LOGIC;
				 Clk_Sys : in  STD_LOGIC;
				 E0 : out  STD_LOGIC;
				 F0 : out  STD_LOGIC;
				 DO_Rdy : out  STD_LOGIC;
				 DO : out  STD_LOGIC_VECTOR (7 downto 0));
	end component;
	component Lock is
		Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
				 Rdy : in  STD_LOGIC;
				 Clk : in  STD_LOGIC;
				 Reset: in  STD_LOGIC;
				 Shift: in  STD_LOGIC;
				 F0: in  STD_LOGIC;
				 Y : out  STD_LOGIC;
				 Q : out  STD_LOGIC_VECTOR (3 downto 0);
			    Counter : out  UNSIGNED (7 downto 0));
	end component;
	component ShiftControl is
		 Port ( X : in  STD_LOGIC_VECTOR (7 downto 0);
				  Rdy : in  STD_LOGIC;
				  Clk : in  STD_LOGIC;
				  F0: in  STD_LOGIC;
				  F0_out: out  STD_LOGIC;
				  X_out : out  STD_LOGIC_VECTOR (7 downto 0);
				  Rdy_out : out  STD_LOGIC;
				  Shift : out  STD_LOGIC);
	end component;
	component LCD1x64 is
		Port ( Line : in  STD_LOGIC_VECTOR (63 downto 0);
		       Blank : in  STD_LOGIC_VECTOR (15 downto 0);
			    Reset: in  STD_LOGIC;
				 Clk_50MHz : in  STD_LOGIC;
				 LCD_E: out  STD_LOGIC;
			    LCD_RS: out  STD_LOGIC;
			    LCD_RW: out  STD_LOGIC;
			    LCD_D: out  STD_LOGIC_VECTOR ( 3 downto 0);
			    SF_CE: out  STD_LOGIC);
	end component;
	
	signal ps2OutByte : STD_LOGIC_VECTOR (7 downto 0) := "00000000";
	signal shiftOutByte : STD_LOGIC_VECTOR (7 downto 0) := "00000000";
	signal Rdy : STD_LOGIC := '0';
	signal RdyShift : STD_LOGIC := '0';
	signal Shift : STD_LOGIC := '0';
	signal F0 : STD_LOGIC := '0';
	signal F0Shift : STD_LOGIC := '0';
	signal blank : STD_LOGIC_VECTOR (15 downto 0) := X"3FFE";
	signal state : STD_LOGIC_VECTOR (3 downto 0) := "0000";
	signal line : STD_LOGIC_VECTOR (63 downto 0) := X"0000000000000000";
	signal rst : STD_LOGIC := '0';
	signal count : UNSIGNED (7 downto 0) := X"00";
	
begin
	PS2 : PS2_Kbd port map (PS2_Clk => PS2_Clk, PS2_Data => PS2_Data, Clk_50MHz => Clk, Clk_Sys => Clk, DO_Rdy => Rdy, DO => ps2OutByte, F0 => F0);
	
	Shiftc : ShiftControl port map (X => ps2OutByte, Rdy => Rdy, Clk => Clk, X_out => shiftOutByte, Rdy_out => RdyShift, Shift => Shift, F0 => F0, F0_out => F0Shift);
	
	Lck : Lock port map (X => shiftOutByte, Rdy => RdyShift, Clk => Clk, Reset => Reset, Y => Y, Shift => Shift, F0 => F0Shift, Q => state, Counter => count);
	
	line <=  STD_LOGIC_VECTOR(count) & X"0000000000000" & state;
	
	LCD : LCD1x64 port map (Clk_50MHz => Clk, LCD_E => LCD_E, LCD_RS => LCD_RS, LCD_RW => LCD_RW, LCD_D => LCD_D, SF_CE => SF_CE, Reset => rst, Blank => blank, Line => line);
	
	IsShift <= Shift;

end lab7Arch;

