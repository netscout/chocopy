import os

#현재 replace_txt.py가 위치한 폴더 경로 가져오기
currentPath = os.path.dirname(os.path.abspath(__file__))

#os.walk를 이용해서 모든 하위 폴더의 파일까지 순차적으로 접근
for root, dirs, files in os.walk(currentPath):
    for file in files:
        #파일을 하나씩 접근해서 .cs로 끝나지 않는 파일은 제외
        filePath = os.path.join(root, file)
        if not filePath.endswith(".cs"):
            continue
        
        print(filePath)
        #파일에서 내용 읽어오기
        with open(filePath, 'r', encoding='utf8') as file :
            filedata = file.read()

        #키워드 찾아서 바꾸기
        filedata = filedata.replace(
            "namespace MyProject",
            "namespace SomeProject.Libs")

        filedata = filedata.replace(
            "using MyProject",
            "using SomeProject.Libs")
            
        #바꾼 내용을 파일에 다시 기록
        with open(filePath, 'w', encoding='utf8') as file:
            file.write(filedata)
