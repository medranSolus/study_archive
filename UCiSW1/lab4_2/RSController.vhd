----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    18:59:53 11/04/2019 
-- Design Name: 
-- Module Name:    RSController - RSControllerArch 
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

entity RSController is
    Port ( Result : out  STD_LOGIC_VECTOR(1 downto 0);
           CHAR : in  STD_LOGIC_VECTOR (7 downto 0));
end RSController;

architecture RSControllerArch of RSController is
begin
	--  UP  DOWN  RESET
	with CHAR select
	Result <= "01" when "01110101", -- UP
				 "10" when "01100100", -- DOWN
				 "11" when "01110010", -- RESET
				 "00" when others;
	

end RSControllerArch;

