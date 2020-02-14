import requests as req
import bs4

url = "https://www.datacamp.com/tracks/machine-learning-scientist-with-python"

res = req.get(url)

bs = bs4.BeautifulSoup(res.text, features="html.parser")

courses = bs.select("#gatsby-focus-wrapper > div > div.container.css-93pq91 > div.col-md-8 > div > div > div > div.css-2cldv8 > a")
courseList = []
for c in courses:
    link = c.attrs["href"]
    title = c.select_one("h4").getText().strip()
    desc = c.select_one("p").getText().strip()
    courseList.append({"link": link, "title": title, "desc": desc})
print(courseList)