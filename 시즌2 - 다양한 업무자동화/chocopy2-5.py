import os
import PyPDF2

currentPath = os.path.dirname(__file__)
pdfPath = os.path.join(currentPath, "pdf")

pdfFiles = ["pdf문서1.pdf", "pdf문서2.pdf", "pdf문서3.pdf"]
pagesToRemoveDict = {
    "pdf문서1.pdf" : [1,2], #1번 문서는 2,3페이지를 제외하고
    "pdf문서2.pdf" : [0], #2번 문서는 첫번째 페이지를 제외
    "pdf문서3.pdf" : [1] #3번 문서는 2페이지를 제외
}

writer = PyPDF2.PdfFileWriter()

for pdf in pdfFiles:
    pdfData = open(os.path.join(pdfPath, pdf), "rb")
    reader = PyPDF2.PdfFileReader(pdfData)

    pagesToRemove = pagesToRemoveDict[pdf]

    for pageNo in range(0, reader.numPages):
        if pageNo in pagesToRemove:
            continue
        page = reader.getPage(pageNo)
        writer.addPage(page)

pdfResult = open("result.pdf", "wb")
writer.write(pdfResult)
pdfResult.close()