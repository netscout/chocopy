var express = require("express")
var uuid = require("uuid")

var app = express()
var id = uuid.v4()
var count = 100000000

app.get("/", function(req, res) {
    var result = 0
    for(var i=1; i<=count; i++) {
        result += i
    }
    res.send(`안녕하세요!, ${id}, ${result}`)
})

app.listen(3000, function() {
    console.log("앱이 3000번 포트에서 실행중입니다....")
})
