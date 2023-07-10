lista = document.querySelectorAll("button.danie");
dania = document.querySelectorAll("div.meal-info")
var t = 0;


for (var i = 0; i < lista.length; i++) {

    lista[i].onclick = function (t) {
        parent = this.parentNode;
        sibling = parent.nextElementSibling;
        sibling.classList.toggle("show")
    }
}
