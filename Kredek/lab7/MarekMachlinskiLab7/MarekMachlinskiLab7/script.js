document.getElementById("text-information").style.backgroundColor = "crimson";

var counter = 1;

function changeFooterTextAndColor() {
    var footer = document.getElementById("footer-block");
    footer.innerHTML = "Licznik: " + counter;
    if (counter % 100 == 0)
        alert("EMPEROR PROTECT!");
    if (counter % 2 == 0) {
        footer.style.backgroundColor = "goldenrod";
        footer.style.color = "black";
    }
    else {
        footer.style.backgroundColor = "black";
        footer.style.color = "goldenrod";
    }
    counter++;
}

function bannerClick() {
    var menu = document.getElementById("menu");
    menu.style.backgroundColor = "red";
    menu.style.fontSize = "150%";
    menu.style.width = "50%";
    menu.style.marginLeft = "25%";
    menu.style.color = "whitesmoke";
    menu.style.textAlign = "center";
    menu.innerHTML = "Inqusition watch over you!";

}
//Ustawienie zdarzenia cyklicznego w zależności od czasu
setInterval(changeFooterTextAndColor, 1000);