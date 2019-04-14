import os
import re

def findPatterns(str):
    pattern = r'(([\'|"])(css|js|demo-images|fonts|images)\/.*\.(css|js|png|jpg|gif)([\'|"]))'
    
    p = re.compile(pattern)
    results = p.findall(str)
    
    return results

#다음으로는 이 경로를 장고 경로로 바꿔줘야 하지.
#"css/~~.css" -> "{% static '/css/~~.css' %}"
#일단 "나 '로 시작하는지 판단하고
#그 문자를 앞뒤에서 제거한다.

def transformLink(link):
    curQuote = None
    quote1 = "'"
    quote2 = '"'

    if link.startswith(quote1):
        _link = link.strip(quote1)
        curQuote = quote1
    else:
        _link = link.strip(quote2)
        curQuote = quote2
    
##    if not _link.startswith("/"):
##        _link = f"/{_link}"

    if curQuote == quote1:
        newLink = f'{{% static "{_link}" %}}'
        newLink = quote1 + newLink + quote1
    else:
        newLink = f"{{% static '{_link}' %}}"
        newLink = quote2 + newLink + quote2
    return newLink

currentPath = os.path.dirname(os.path.abspath(__file__))

#os.walk를 이용해서 모든 하위 폴더의 파일까지 순차적으로 접근
for root, dirs, files in os.walk(currentPath):
    for file in files:
        #파일을 하나씩 접근해서 .cs로 끝나지 않는 파일은 제외
        filePath = os.path.join(root, file)
        if not filePath.endswith(".html"):
            continue
        
        print(filePath)
        #파일에서 내용 읽어오기
        with open(filePath, 'r', encoding='utf8') as file :
            filedata = file.read()

        results = findPatterns(filedata)
        print(results)
        for result in results:
            transformed = transformLink(result[0])
            print(f"from : {result[0]} -> {transformed}")
            filedata = filedata.replace(result[0], transformed)
        
            
        #바꾼 내용을 파일에 다시 기록
        with open(filePath, 'w', encoding='utf8') as file:
            file.write(filedata)