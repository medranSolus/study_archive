%decyzja, czy nast�pi zamiana bitu na podstawie wsp�czynnika zaszumienia
%alpha
%information: 0 -> brak zamiany, 1 -> zamiana bitu
function information = isBitChanged(alpha)
    information = 0;
    a = rand(1) * 100;
    if a < alpha
        information = 1;
    end
end