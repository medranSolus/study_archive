%0 - tab don't have parity number of '1'
%1 - tab have parity number of '1'
function parity = isParity(tab)
    parity = 1;
    for i = 1 : length(tab)
        parity = parity + tab(i);
    end
    parity = mod(parity,2); 
end