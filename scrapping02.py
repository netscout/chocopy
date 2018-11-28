import requests as req
import bs4

#읽어올 페이지 주소 필요
url = "https://www.datacamp.com/tracks/data-scientist-with-python"

#주소에 해당하는 웹 페이지 읽어오기
res = req.get(url)

#특정 요소만 검색하기
bs = bs4.BeautifulSoup(res.text, features="html.parser")

courses = bs.select(".track__course")

#검색한 요소 출력하기
for c in courses:
    link = ""
    linkTag = c.select_one("a")
    if linkTag is None:
        break;
    link = linkTag.attrs["href"]
    title = c.select_one("h4.course-block__title").getText().strip()
    desc = c.select_one("p.course-block__description").getText().strip()
    print(link, title, desc)

