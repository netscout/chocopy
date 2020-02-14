from selenium import webdriver
import bs4

base_url = "https://www.datacamp.com"
url = f"{base_url}/tracks/machine-learning-scientist-with-python"

driver = webdriver.Chrome()
driver.implicitly_wait(3)
driver.get(url)

btn = driver.find_element_by_xpath("""//*[@id="gatsby-focus-wrapper"]/div/div[1]/div[1]/div/div/div[4]/button""")
btn.click()

bs = bs4.BeautifulSoup(driver.page_source, features="html.parser")

courses = bs.select("#gatsby-focus-wrapper > div > div.container.css-93pq91 > div.col-md-8 > div > div > div > div.css-2cldv8 > a")
courseList = []
for c in courses:
    link = c.attrs["href"]
    title = c.select_one("h4").getText().strip()
    desc = c.select_one("p").getText().strip()
    courseList.append({"link": link, "title": title, "desc": desc})

for c in courseList:
    driver.get(f"{base_url}{c['link']}")

    bs_detail = bs4.BeautifulSoup(driver.page_source, features="html.parser")

    chapters = bs_detail.select_one("ol.chapters")

    chapters_elem = chapters.select("li.chapter")

    chapter_list = []
    for chap in chapters_elem:
        chap_title = chap.select_one("h4.chapter__title").getText().strip()
        chap_desc = chap.select_one("p.chapter__description").getText().strip()
        chap_details_elem = chap.select("h5.chapter__exercise-title")

        chap_detail_titles = []
        for cd in chap_details_elem:
            cd_title = cd.getText().strip()
            chap_detail_titles.append(cd_title)
        
        chapter_detail = {"title":chap_title, "desc":chap_desc, "details": chap_detail_titles}
        #print(chapter_detail)
        chapter_list.append(chapter_detail)
    c["chapter_detail"] = chapter_list

print(courseList)
print(len(courseList))