//______________________________________________________________________________________________________
//Sólo letras
//<asp:TextBox ID="" runat="server" onkeypress="return letra(event)"></asp:TextBox>
function letra(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = [8,9, 16, 39, 46];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
//Fin sólo letras.
//______________________________________________________________________________________________________

//______________________________________________________________________________________________________
//Sólo letras Mayúsculas
//  <asp:TextBox ID="" runat="server" onkeypress="return letra(event)" onKeyUp="mayuscula(this)"></asp:TextBox>
function mayuscula(txtEntrada) {
    txtEntrada.value = txtEntrada.value.toUpperCase();
}
// Fin sólo letras mayúsculas
//______________________________________________________________________________________________________

//______________________________________________________________________________________________________
//Sólo números
//<asp:TextBox ID="" runat="server" onkeypress="return numero(event)"> </asp:TextBox>
function numero(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " ";
    especiales = [8, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
//Fin sólo números
//______________________________________________________________________________________________________


//______________________________________________________________________________________________________
//Sólo números con signo menos (-)
//<asp:TextBox ID="" runat="server" onkeypress="return numeroConSignoMenos(event)"> </asp:TextBox>
function numeroConSignoMenos(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " ";
    especiales = [8, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57,45];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {

            if (key == especiales[11]) {
                if (val.length == 0) { 
                     tecla_especial = true;
        break;
                }
                          
       }
       else {
           tecla_especial = true;
           break;

       }

           
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
//Fin sólo números
//______________________________________________________________________________________________________




//______________________________________________________________________________________________________
//bloquedo
//<asp:TextBox ID="" runat="server" onkeypress="return bloqueado(event)"> </asp:TextBox>
function bloqueado(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " ";
    especiales = [8];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
//Fin sólo números
//______________________________________________________________________________________________________




//______________________________________________________________________________________________________
//Fecha
//Dependiendo del formato se selecciona el patron.
var patron = new Array(2, 2, 4)  // (dd/mm/yyyy) <asp:TextBox ID="txtFecha" runat="server" Width="300px" onkeyup="Format(this,'/',patron,true)"></asp:TextBox>
var patron2 = new Array(3, 3, 2, 2)// (722-153-34-16)<asp:TextBox ID="" runat="server" Width="300px" onkeyup="Format(this,'-',patron2,true)"></asp:TextBox>
var patron3 = new Array(3, 3, 3, 3) // (192.168.003.004) <asp:TextBox ID="" runat="server" Width="300px" onkeyup="Format(this,'.',patron3,true)"></asp:TextBox>
var patron4 = new Array(2, 2)  // (hh:mm) <asp:TextBox ID="txtFecha" runat="server" Width="300px" onkeyup="Format(this,':',patron4,true)"></asp:TextBox>
function Format(d, sep, pat, nums) {
    if (d.valant != d.value) {
        val = d.value
        largo = val.length
        val = val.split(sep)
        val2 = ''
        for (r = 0; r < val.length; r++) {
            val2 += val[r]
        }
        if (nums) {
            for (z = 0; z < val2.length; z++) {
                if (isNaN(val2.charAt(z))) {
                    letra = new RegExp(val2.charAt(z), "g")
                    val2 = val2.replace(letra, "")
                }
            }
        }
        val = ''
        val3 = new Array()
        for (s = 0; s < pat.length; s++) {
            val3[s] = val2.substring(0, pat[s])
            val2 = val2.substr(pat[s])
        }
        for (q = 0; q < val3.length; q++) {
            if (q == 0) {
                val = val3[q]
            }
            else {
                if (val3[q] != "") {
                    val += sep + val3[q]
                }
            }
        }
        d.value = val
        d.valant = val
    }
}
//Fin del formato
//______________________________________________________________________________________________________

//______________________________________________________________________________________________________
// inicio Teléfono
// Codigo:
// <asp:TextBox runat="server" onkeydown="javascript:backspacerDOWN(this,event);" onkeyup="javascript:backspacerUP(this,event);"  id="txtTelefono"></asp:TextBox>
var zChar = new Array(' ', '(', ') ', '-', '.');
var maxphonelength = 13;
var phonevalue1;
var phonevalue2;
var cursorposition;
function ParseForNumber1(object) {
    phonevalue1 = ParseChar(object.value, zChar);
}
function ParseForNumber2(object) {
    phonevalue2 = ParseChar(object.value, zChar);
}
function backspacerUP(object, e) {
    if (e) {
        e = e
    } else {
        e = window.event
    }
    if (e.which) {
        var keycode = e.which
    } else {
        var keycode = e.keyCode
    }

    ParseForNumber1(object)

    if (keycode >= 48) {
        ValidatePhone(object)
    }
}
function backspacerDOWN(object, e) {
    if (e) {
        e = e
    } else {
        e = window.event
    }
    if (e.which) {
        var keycode = e.which
    } else {
        var keycode = e.keyCode
    }
    ParseForNumber2(object)
}
function GetCursorPosition() {

    var t1 = phonevalue1;
    var t2 = phonevalue2;
    var bool = false
    for (i = 0; i < t1.length; i++) {
        if (t1.substring(i, 1) != t2.substring(i, 1)) {
            if (!bool) {
                cursorposition = i
                bool = true
            }
        }
    }
}
function ValidatePhone(object) {

    var p = phonevalue1

    p = p.replace(/[^\d]*/gi, "")

    if (p.length < 3) {
        object.value = p
    } else if (p.length == 3) {
        pp = p;
        d4 = p.indexOf('(')
        d5 = p.indexOf(')')
        if (d4 == -1) {
            pp = "(" + pp;
        }
        if (d5 == -1) {
            pp = pp + ")";
        }
        object.value = pp;
    } else if (p.length > 3 && p.length < 7) {
        p = "(" + p;
        l30 = p.length;
        p30 = p.substring(0, 4);
        p30 = p30 + ")"

        p31 = p.substring(4, l30);
        pp = p30 + p31;

        object.value = pp;

    } else if (p.length >= 7) {
        p = "(" + p;
        l30 = p.length;
        p30 = p.substring(0, 4);
        p30 = p30 + ")"

        p31 = p.substring(4, l30);
        pp = p30 + p31;

        l40 = pp.length;
        p40 = pp.substring(0, 8);
        p40 = p40 + "-"

        p41 = pp.substring(8, l40);
        ppp = p40 + p41;

        object.value = ppp.substring(0, maxphonelength);
    }

    GetCursorPosition()

    if (cursorposition >= 0) {
        if (cursorposition == 0) {
            cursorposition = 2
        } else if (cursorposition <= 2) {
            cursorposition = cursorposition + 1
        } else if (cursorposition <= 5) {
            cursorposition = cursorposition + 2
        } else if (cursorposition == 6) {
            cursorposition = cursorposition + 2
        } else if (cursorposition == 7) {
            cursorposition = cursorposition + 4
            e1 = object.value.indexOf(')')
            e2 = object.value.indexOf('-')
            if (e1 > -1 && e2 > -1) {
                if (e2 - e1 == 4) {
                    cursorposition = cursorposition - 1
                }
            }
        } else if (cursorposition < 11) {
            cursorposition = cursorposition + 3
        } else if (cursorposition == 11) {
            cursorposition = cursorposition + 1
        } else if (cursorposition >= 12) {
            cursorposition = cursorposition
        }

        var txtRange = object.createTextRange();
        txtRange.moveStart("character", cursorposition);
        txtRange.moveEnd("character", cursorposition - object.value.length);
        txtRange.select();
    }

}
function ParseChar(sStr, sChar) {
    if (sChar.length == null) {
        zChar = new Array(sChar);
    }
    else zChar = sChar;

    for (i = 0; i < zChar.length; i++) {
        sNewStr = "";

        var iStart = 0;
        var iEnd = sStr.indexOf(sChar[i]);

        while (iEnd != -1) {
            sNewStr += sStr.substring(iStart, iEnd);
            iStart = iEnd + 1;
            iEnd = sStr.indexOf(sChar[i], iStart);
        }
        sNewStr += sStr.substring(sStr.lastIndexOf(sChar[i]) + 1, sStr.length);

        sStr = sNewStr;
    }

    return sNewStr;
}
// FIN Teléfono
//______________________________________________________________________________________________________


//____________________________________Codigo para el popup MGSBOX_____________________________________________

function mensaje(Titulo, Cuerpo,Color,Redireccionar,urlRedireccionar ) {
    alert(Cuerpo, Titulo, Redireccionar, Color, urlRedireccionar);
}

var ALERT_BUTTON_TEXT = "Aceptar";
if (document.getElementById) {
    window.alert = function (txt, titulo, redi, color, URL_REDIRECCIONAR) {


        switch (color) {
            case "Verde":
                if (redi == "Si") 
                {
                    alertVerdeSiRedirecciona(txt, titulo, URL_REDIRECCIONAR);
                }
                else
                {
                    alertVerdeNoRedirecciona(txt, titulo);
                }
                break;
            case "Amarillo":
                if (redi == "Si") {
                    alertAmarilloSiRedirecciona(txt, titulo, URL_REDIRECCIONAR);
                }
                else {
                    alertAmarilloNoRedirecciona(txt, titulo, "");
                }
                break;
            case "Rojo":
                if (redi == "Si") {
                    alertRojoSiRedirecciona(txt, titulo, URL_REDIRECCIONAR);
                }
                else {
                    alertRojoNoRedirecciona(txt, titulo, "");
                }
                break;

            case "Azul":
                if (redi == "Si") {
                    alertAzulSiRedirecciona(txt, titulo, URL_REDIRECCIONAR);
                }
                else {
                    alertAzulNoRedirecciona(txt, titulo, "");
                }
                break;
            default:
                if (redi == "Si") {
                    alertGrisSiRedirecciona(txt, titulo, URL_REDIRECCIONAR);
                }
                else {
                    alertGrisNoRedirecciona(txt, titulo, "");
                }
               


        }


    }
}



//_______________________________________________________________________Verde
    function alertVerdeNoRedirecciona(txt, titulo) {
    d = document;
    if (d.getElementById("modalContainerVerde")) return;
    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
    mObj.id = "modalContainerVerde";
     mObj.style.height = document.documentElement.scrollHeight + "px";
    alertObj = mObj.appendChild(d.createElement("div"));
    alertObj.id = "alertBoxVerde";
    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
    h1 = alertObj.appendChild(d.createElement("h1"));
    h1.appendChild(d.createTextNode(titulo));
    msg = alertObj.appendChild(d.createElement("p"));
    msg.innerHTML = txt;
    btn = alertObj.appendChild(d.createElement("a"));
    btn.id = "closeBtnVerde";
    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
    btn.href = "#";
    btn.onclick = function () { removeAlertVerdeNoRedirecciona(); return false; }
    }
    function removeAlertVerdeNoRedirecciona() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerVerde"));
}

    function alertVerdeSiRedirecciona(txt, titulo, URL_REDIRECCIONAR) {
    // shortcut reference to the document object
    d = document;

    // if the modalContainer object already exists in the DOM, bail out.
    if (d.getElementById("modalContainerVerde")) return;

    // create the modalContainer div as a child of the BODY element
    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
    mObj.id = "modalContainerVerde";
    // make sure its as tall as it needs to be to overlay all the content on the page
    mObj.style.height = document.documentElement.scrollHeight + "px";

    // create the DIV that will be the alert 
    alertObj = mObj.appendChild(d.createElement("div"));
    alertObj.id = "alertBoxVerde";
    // MSIE doesnt treat position:fixed correctly, so this compensates for positioning the alert
    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
    // center the alert box
    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";

    // create an H1 element as the title bar
    h1 = alertObj.appendChild(d.createElement("h1"));
    h1.appendChild(d.createTextNode(titulo));

    // create a paragraph element to contain the txt argument
    msg = alertObj.appendChild(d.createElement("p"));
    msg.innerHTML = txt;

    // create an anchor element to use as the confirmation button.
    btn = alertObj.appendChild(d.createElement("a"));
    btn.id = "closeBtnVerde";

    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
    btn.href = URL_REDIRECCIONAR;
    }
//Termina Verde

//_______________________________________________________________________Amarillo
    function alertAmarilloNoRedirecciona(txt, titulo) {
        d = document;
        if (d.getElementById("modalContainerAmarillo")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerAmarillo";
        mObj.style.height = document.documentElement.scrollHeight + "px";
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxAmarillo";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnAmarillo";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.onclick = function () { removeAlertAmarilloNoRedirecciona(); return false; }
    }
    function removeAlertAmarilloNoRedirecciona() {
        document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerAmarillo"));
    }
    function alertAmarilloSiRedirecciona(txt, titulo, URL_REDIRECCIONAR) {
        // shortcut reference to the document object
        d = document;

        // if the modalContainer object already exists in the DOM, bail out.
        if (d.getElementById("modalContainerAmarillo")) return;

        // create the modalContainer div as a child of the BODY element
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerAmarillo";
        // make sure its as tall as it needs to be to overlay all the content on the page
        mObj.style.height = document.documentElement.scrollHeight + "px";

        // create the DIV that will be the alert 
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxAmarillo";
        // MSIE doesnt treat position:fixed correctly, so this compensates for positioning the alert
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        // center the alert box
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";

        // create an H1 element as the title bar
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));

        // create a paragraph element to contain the txt argument
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;

        // create an anchor element to use as the confirmation button.
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnAmarillo";

        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = URL_REDIRECCIONAR;
    }
//Termina Amarillo


//_______________________________________________________________________Rojo
    function alertRojoNoRedirecciona(txt, titulo) {
        d = document;
        if (d.getElementById("modalContainerRojo")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerRojo";
        mObj.style.height = document.documentElement.scrollHeight + "px";
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxRojo";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnRojo";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.onclick = function () { removeAlertRojoNoRedirecciona(); return false; }
    }
    function removeAlertRojoNoRedirecciona() {
        document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerRojo"));
    }
    function alertRojoSiRedirecciona(txt, titulo, URL_REDIRECCIONAR) {
        // shortcut reference to the document object
        d = document;

        // if the modalContainer object already exists in the DOM, bail out.
        if (d.getElementById("modalContainerRojo")) return;

        // create the modalContainer div as a child of the BODY element
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerRojo";
        // make sure its as tall as it needs to be to overlay all the content on the page
        mObj.style.height = document.documentElement.scrollHeight + "px";

        // create the DIV that will be the alert 
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxRojo";
        // MSIE doesnt treat position:fixed correctly, so this compensates for positioning the alert
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        // center the alert box
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";

        // create an H1 element as the title bar
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));

        // create a paragraph element to contain the txt argument
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;

        // create an anchor element to use as the confirmation button.
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnRojo";

        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = URL_REDIRECCIONAR;
    }
    //Termina Rojo

    //_______________________________________________________________________Azul
    function alertAzulNoRedirecciona(txt, titulo) {
        d = document;
        if (d.getElementById("modalContainerAzul")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerAzul";
        mObj.style.height = document.documentElement.scrollHeight + "px";
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxAzul";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnAzul";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.onclick = function () { removeAlertAzulNoRedirecciona(); return false; }
    }
    function removeAlertAzulNoRedirecciona() {
        document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerAzul"));
    }
    function alertAzulSiRedirecciona(txt, titulo, URL_REDIRECCIONAR) {
        // shortcut reference to the document object
        d = document;

        // if the modalContainer object already exists in the DOM, bail out.
        if (d.getElementById("modalContainerAzul")) return;

        // create the modalContainer div as a child of the BODY element
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerAzul";
        // make sure its as tall as it needs to be to overlay all the content on the page
        mObj.style.height = document.documentElement.scrollHeight + "px";

        // create the DIV that will be the alert 
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxAzul";
        // MSIE doesnt treat position:fixed correctly, so this compensates for positioning the alert
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        // center the alert box
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";

        // create an H1 element as the title bar
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));

        // create a paragraph element to contain the txt argument
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;

        // create an anchor element to use as the confirmation button.
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnAzul";

        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = URL_REDIRECCIONAR;
    }
    //Termina Azul

    //_______________________________________________________________________Gris
    function alertGrisNoRedirecciona(txt, titulo) {
        d = document;
        if (d.getElementById("modalContainerGris")) return;
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerGris";
        mObj.style.height = document.documentElement.scrollHeight + "px";
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxGris";
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnGris";
        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = "#";
        btn.onclick = function () { removeAlertGrisNoRedirecciona(); return false; }
    }
    function removeAlertGrisNoRedirecciona() {
        document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainerGris"));
    }
    function alertGrisSiRedirecciona(txt, titulo, URL_REDIRECCIONAR) {
        // shortcut reference to the document object
        d = document;

        // if the modalContainer object already exists in the DOM, bail out.
        if (d.getElementById("modalContainerGris")) return;

        // create the modalContainer div as a child of the BODY element
        mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
        mObj.id = "modalContainerGris";
        // make sure its as tall as it needs to be to overlay all the content on the page
        mObj.style.height = document.documentElement.scrollHeight + "px";

        // create the DIV that will be the alert 
        alertObj = mObj.appendChild(d.createElement("div"));
        alertObj.id = "alertBoxGris";
        // MSIE doesnt treat position:fixed correctly, so this compensates for positioning the alert
        if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
        // center the alert box
        alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";

        // create an H1 element as the title bar
        h1 = alertObj.appendChild(d.createElement("h1"));
        h1.appendChild(d.createTextNode(titulo));

        // create a paragraph element to contain the txt argument
        msg = alertObj.appendChild(d.createElement("p"));
        msg.innerHTML = txt;

        // create an anchor element to use as the confirmation button.
        btn = alertObj.appendChild(d.createElement("a"));
        btn.id = "closeBtnGris";

        btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
        btn.href = URL_REDIRECCIONAR;
    }
    //Termina Azul