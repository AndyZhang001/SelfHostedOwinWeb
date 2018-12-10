var a = 1;
console.log(a);
function onPageLoad() {
    var userName = window.prompt("Hello, please input your name:", "");
    console.log("user" + userName);
    var userElement = document.getElementById("user");
    console.log(userElement);
    userElement.innerHTML = "Current User: " + userName;
}

