----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    18:38:27 10/21/2019 
-- Design Name: 
-- Module Name:    lab3mod - lab3ARCH 
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

entity lab3mod is
    Port ( Q : out  STD_LOGIC_VECTOR (2 downto 0);
           CLK : in  STD_LOGIC;
           CE : in  STD_LOGIC;
           CLR : in  STD_LOGIC);
end lab3mod;

architecture lab3ARCH of lab3mod is

begin


end lab3ARCH;

