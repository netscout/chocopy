import requests as req

#읽어올 페이지 주소 필요
url = "https://www.datacamp.com/tracks/data-scientist-with-python"

#주소에 해당하는 웹 페이지 읽어오기
res = req.get(url)

#읽어온 웹 페이지 출력하기
print(res.text)
