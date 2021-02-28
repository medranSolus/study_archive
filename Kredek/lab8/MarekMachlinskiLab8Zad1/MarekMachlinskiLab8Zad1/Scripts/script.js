//Następny numer identyfikatora dla nowego programisty
var idCounter = 0;

//Co sekundę tworzy nowego programistę i dodaje go do ekranu w losowym miejscu
setInterval(function () {
    var image = document.createElement("img");
    image.style.position = "absolute";
    image.style.top = Math.floor(Math.random() * 730) + "px";
    image.style.left = Math.floor(Math.random() * 1700) + "px";
    image.name = "programmer";
    image.width = image.height = 100;
    if (Math.floor(Math.random() * 2) + 1 == 1) {
        image.alt = "programmerMan";
        image.src = "/Content/Images/programmerMan.png"
    }
    else {
        image.alt = "programmerWoman";
        image.src = "/Content/Images/programmerWoman.png"
    }
    image.style.display = "block";
    image.id = idCounter;
    image.setAttribute("onclick", "programmerClicked(" + image.id + ")");
    ++idCounter;
    document.body.appendChild(image);
}, 1000);

//Porusza wszystkimi programistami na ekranie co 1.8 sekundy, losując kierunek ruchu
setInterval(function () {
    var programmers = document.getElementsByName("programmer");
    programmers.forEach((value, index, list) => {
        var direction = Math.floor(Math.random() * 8) + 1;
        if ((direction == 2 || direction == 5 || direction == 6)) {
            if ($(value).position().top > 100)
                $("#" + value.id).animate({ top: "-=100px" });
            else
                $("#" + value.id).animate({ top: "0px" });
        }
        else if (direction == 4 || direction == 7 || direction == 8) {
            if ($(value).position().top < 700)
                $("#" + value.id).animate({ top: "+=100px" });
            else
                $("#" + value.id).animate({ top: "700px" });
        }
        if (direction == 1 || direction == 5 || direction == 8) {
            if ($(value).position().left > 100)
                $("#" + value.id).animate({ left: "-=100px" });
            else
                $("#" + value.id).animate({ left: "0px" });
        }
        else if (direction == 3 || direction == 6 || direction == 7) {
            if ($(value).position().left < 1700)
                $("#" + value.id).animate({ left: "+=100px" });
            else
                $("#" + value.id).animate({ left: "1700px" });
        }
    });
}, 1800);