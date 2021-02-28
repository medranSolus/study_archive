var isRed = false;

function setBackground() {
    document.getElementById("main-body").style.backgroundImage = "url(/Images/background.jpg)";
    document.getElementById("main-body").style.backgroundRepeat = "no - repeat";
}

setInterval(() => {
    if (!isRed) {
        document.getElementById("navigation").style.backgroundColor = "red";
        isRed = true;
    }
    else {
        document.getElementById("navigation").style.backgroundColor = "darkblue";
        isRed = false;
    }
}, 2500);

function bannerClicked() {
    alert("WOOOOOOO!   https://www.recipegirl.com/wp-content/uploads/2017/06/baked-bday-cake-doughnuts-1-600x400.jpg");
}