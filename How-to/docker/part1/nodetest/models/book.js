var mongoose = require("mongoose")

const BookSchema = new mongoose.Schema({
    title: {
        type: String,
        required: "제목은 필수"
    },
    desc: {
        type: String
    },
    createdAt: {
        type: Date,
        default: Date.now
    }
})

module.exports = mongoose.model("Book", BookSchema)