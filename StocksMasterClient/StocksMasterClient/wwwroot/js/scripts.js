function drawCanvas() {
    var canvas = document.getElementById("canvas");
    canvas.width = 650;
    canvas.height = 600;

    var set1 = document.getElementById("set1").value;
    var set2 = document.getElementById("set2").value;
    var set3 = document.getElementById("set3").value;
    var set4 = document.getElementById("set4").value;
    var set5 = document.getElementById("set5").value;
    //var set6 = document.getElementById("set6").value;
    //var set7 = document.getElementById("set7").value;

    var ctx = canvas.getContext('2d');
    ctx.translate(0, canvas.height);
    ctx.scale(1, -1);
    ctx.translate(50, 50);

    // draw horizontal lines
    ctx.beginPath();
    for (var y = 0; y < canvas.height; y += 100) {
        ctx.strokeStyle = "#CCC";
        ctx.moveTo(0, y);
        ctx.lineTo(canvas.width, y);
        ctx.stroke();
    }
    ctx.closePath();

    drawLine(ctx, stringToArray(set5), "purple");
    drawLine(ctx, stringToArray(set4), "green");
    drawLine(ctx, stringToArray(set3), "orange");
    drawLine(ctx, stringToArray(set2), "blue");
    drawLine(ctx, stringToArray(set1), "red");
    //drawLine(ctx, stringToArray(set6), "black");
    //drawLine(ctx, stringToArray(set7), "pink");    
}

function stringToArray(s) {
    var str = s.split(",");
    var array = [];
    for (var i = 0; i < str.length; i++) {
        array.push(parseFloat(str[i]));
    }
    return array;
}

function drawLine(ctx, _points, color) {
    ctx.beginPath();
    ctx.moveTo(0, _points[0] / 2);
    for (var i = 1, x = 6; i < _points.length; i++, x += 6) {
        ctx.strokeStyle = color;
        ctx.lineTo(x, _points[i] / 2);
        ctx.stroke();
        ctx.moveTo(x, _points[i] / 2)
    }
    ctx.closePath();
}

function drawLabels() {
    var canvas = document.getElementById("canvas");

    var ctx = canvas.getContext('2d');
    ctx.translate(0, canvas.height);
    ctx.scale(1, -1);
    ctx.translate(-50, 0);
    ctx.font = "18px Arial";
    ctx.fillStyle = "black";
    ctx.fillText("$0", 0, 600);
    ctx.fillText("$200", 0, 500);
    ctx.fillText("$400", 0, 400);
    ctx.fillText("$600", 0, 300);
    ctx.fillText("$800", 0, 200);
    ctx.fillText("$1000", 0, 100);
    ctx.textAlign = "center";
    ctx.fillText("days", (canvas.width / 2) + 25, canvas.height + 25);
}

function applyFilter(element) {
    window.location.href = "/StocksData/List?companyId=" + element.value;
}