import scrappingLib as scrap

webpages = [
    {"link":"https://www.datacamp.com/tracks/data-scientist-with-python", "filename":"DS with Python.xlsx"},
    {"link":"https://www.datacamp.com/tracks/data-scientist-with-r", "filename":"DS with R.xlsx"}
    ]

for wp in webpages:
    #읽어올 웹 페이지의 주소가 필요
    url = wp["link"]

    #강좌를 저장할 강좌 목록
    courseList = []

    #강좌 목록을 읽어오기
    courseList = scrap.getCourseList(url)

    #세부 챕터 읽어오기
    for c in courseList:
        chapterList = scrap.getChapterList(c["link"])
        c["chapters"] = chapterList

    #엑셀로 읽어온 내용 저장하기
    scrap.saveToExcel(wp["filename"], courseList)
