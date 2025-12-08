 //each character input send request to backend  and return data without reload

let InputSearch = document.getElementById("SearchInput");
InputSearch.addEventListener("keyup", () => {

    // Creating Our XMLHttpRequest object
    let xhr = new XMLHttpRequest();

    // Making our connection
    let url = `https://localhost:44395/Employee?SearchInput=${InputSearch.value}`;
    xhr.open("GET", url, true);

    // function execute after request is successful
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }

    // Sending our request
    xhr.send();
});