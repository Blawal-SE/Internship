var arr = ["bannana", "orange", "apple"];

function indexof() {
    var x = document.getElementById('str').value;
    var result = x.replace(/blawal/i, "umair");


    alert(result);


}

function arr1() {

    var txt = "";
    arr.forEach(function(value, index, array) {
        debugger;
        alert(arr.length);
        txt += "<p>" + value + "<p>";
    });

    document.getElementById('fruits').innerHTML = txt;
}

function searcharr() {
    debugger;
    var tosearch = document.getElementById("searchtxt").value;
    arr.forEach(function(value, index, array) {
        if (value == tosearch) {
            alert("found");
            break;

        }
    });

}