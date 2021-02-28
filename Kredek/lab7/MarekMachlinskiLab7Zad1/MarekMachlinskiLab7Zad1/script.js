//Liczba żołnierzy
var guardsCount = 0;
//Liczba inkwizytorów
var inquisitionCount = 0;
//Liczba marines
var marinesCount = 0;
//Liczba rycerzy
var knightsCount = 0;
//Liczba tytanów
var titansCount = 0;
//Zabici heretycy
var heretisSlaughtered = 0;
//Imperialne pieczęcie
var imperialAprooval = 0;
//Czy ukończono grę
var end = false;

//Funckja co odmierzony czas aktualizuje imperialne pieczęcie i zabitych heretyków sprawdzając czy spełniono warunki końca gry
setInterval(() => {
    if (!end) {
        var currentSlaughter = guardsCount + 12 * inquisitionCount + 55 * marinesCount + 1099 * knightsCount + 8000 * titansCount;
        heretisSlaughtered += currentSlaughter;
        imperialAprooval += currentSlaughter;
        document.getElementById("heretics-count").innerHTML = heretisSlaughtered;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        if (heretisSlaughtered >= 10000000) {
            document.getElementById("victory").style.display = "block";
            document.getElementById("game").style.display = "none";
            end = true;
        }
    }
}, 8000);

//Funkcja w przypadku kliknięcia na pole mapy graficznej zwiększa liczbę pieczęci i zabitych oraz sprawdza czy spełniono warunki końca gry
$("map[name=abaddon-click] area").on("click", function (e) {
    if (!end) {
        ++heretisSlaughtered;
        ++imperialAprooval;
        document.getElementById("heretics-count").innerHTML = heretisSlaughtered;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        if (heretisSlaughtered >= 10000000) {
            document.getElementById("victory").style.display = "block";
            document.getElementById("game").style.display = "none";
            end = true;
        }
    }
    e.stopPropagation();
    return false;
});

//Zwiększa liczbę żołnierzy
function buyGuardsman() {
    if (imperialAprooval >= 10) {
        imperialAprooval -= 10;
        ++guardsCount;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        document.getElementById("guards-count").innerHTML = guardsCount;
    }
}

//Zwiększa liczbę inkwizytorów
function buyInquisitor() {
    if (imperialAprooval >= 100) {
        imperialAprooval -= 100;
        ++inquisitionCount;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        document.getElementById("inquisition-count").innerHTML = inquisitionCount;
    }
}

//Zwiększa liczbę marines
function buyMarine() {
    if (imperialAprooval >= 500) {
        imperialAprooval -= 500;
        ++marinesCount;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        document.getElementById("marines-count").innerHTML = marinesCount;
    }
}

//Zwiększa liczbę rycerzy
function buyKnight() {
    if (imperialAprooval >= 1000) {
        imperialAprooval -= 1000;
        ++knightsCount;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        document.getElementById("knights-count").innerHTML = knightsCount;
    }
}

//Zwiększa liczbę tytanów
function buyTitan() {
    if (imperialAprooval >= 5000) {
        imperialAprooval -= 5000;
        ++titansCount;
        document.getElementById("seals-count").innerHTML = imperialAprooval;
        document.getElementById("titans-count").innerHTML = titansCount;
    }
}
