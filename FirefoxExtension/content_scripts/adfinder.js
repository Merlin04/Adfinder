function addPopup() {
    var srContainer = document.createElement("div");
    srContainer.id = "adfinder-shadow-container";
    srContainer.style = "position:fixed; top:0px; right:0px; z-index:9999;";
    document.querySelector("html").append(srContainer);
    var sr = srContainer.attachShadow({mode: "open"});
    var jqueryScript = document.createElement("script");
    jqueryScript.src = "https://cdn.jsdelivr.net/npm/jquery@3.4.1/dist/jquery.min.js";
    sr.append(jqueryScript);
    var fomanticCSS = document.createElement("link");
    fomanticCSS.rel = "stylesheet";
    fomanticCSS.type = "text/css";
    fomanticCSS.href = "https://cdn.jsdelivr.net/npm/fomantic-ui@2.8.4/dist/semantic.min.css";
    sr.append(fomanticCSS);
    var fomanticScript = document.createElement("script");
    fomanticScript.src = "https://cdn.jsdelivr.net/npm/fomantic-ui@2.8.4/dist/semantic.min.js";
    sr.append(fomanticScript);

    // Create popup container
    var popupDiv = document.createElement("div");
    popupDiv.classList = "ui segment transition hidden";
    popupDiv.style = "margin: 1rem;";
    popupDiv.id = "adfinder-popup"

    // Create message
    var negativeMessage = document.createElement("div");
    negativeMessage.classList = "ui negative message";

    var messageHeader = document.createElement("div");
    messageHeader.classList = "header";
    messageHeader.innerText = "Watch out";
    negativeMessage.append(messageHeader);

    var messageBody = document.createElement("p");
    messageBody.style = "font-family: Lato;";
    messageBody.innerText = "Adfinder has detected that this page may have a promotional bias.";
    negativeMessage.append(messageBody);

    popupDiv.append(negativeMessage);

    // Buttons
    var buttonsContainer = document.createElement("div");
    buttonsContainer.style = "text-align: right;";

    var manageButton = document.createElement("div");
    manageButton.classList = "ui mini button";
    manageButton.innerText = "Manage warnings";
    buttonsContainer.append(manageButton);

    var closeButton = document.createElement("div");
    closeButton.classList = "ui mini button";
    closeButton.innerText = "Close";
    closeButton.id = "closeButton";

    buttonsContainer.append(closeButton);

    popupDiv.append(buttonsContainer);

    // Close script
    var closeScript = document.createElement("script");
    closeScript.innerText = "" +
    "var sr = document.getElementById('adfinder-shadow-container').shadowRoot;" +
    "$(sr).ready(() => {" +
    "    setTimeout(() => {$(sr.getElementById('adfinder-popup')).transition('fade');}, 500);" +
    "    sr.getElementById('closeButton').addEventListener('click', () => {" +
    "        $(sr.getElementById('adfinder-popup')).transition({animation: 'fade', onComplete: () => {document.getElementById('adfinder-shadow-container').outerHTML = '';}});" +
    "    });" +
    "});";
    popupDiv.append(closeScript);

    sr.append(popupDiv);
}

$.post("https://localhost:5001/MLLookup", {articleTitle: $('#firstHeading').text()}, data => {
    console.log(data);
    addPopup();
});