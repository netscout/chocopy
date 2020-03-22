var express = require("express")
var uuid = require("uuid")

var app = express()
var id = uuid.v4()

app.get("/", function(req, res) {
    res.send(`안녕하세요! v0.4, ${id}`)
})

app.listen(3000, function() {
    console.log("앱이 3000번 포트에서 실행중입니다....")
})