import csv
from bs4 import BeautifulSoup
import requests

def get_html(url):
    r = requests.get(url)
    if r.ok:
        return r.text
    else:
        print(r.status_code)

def write_csv(data):
    with open("yandex.csv", "a") as file:
        writer = csv.writer(file)
        writer.writerow([
            data["header"],
            data["url"],
            data["descr"]
        ])
                
def get_page_data(html):
    soup = BeautifulSoup(html, "lxml")
    lis = soup.find_all("li", class_="serp-item")
    print(len(lis))
    for li in lis:
        try:
            header = li.find("div", class_="organic__url-text").text.strip()
        except:
            header = ""
        try:
            url = li.find("h2", class_="organic__title-wrapper typo typo_text_l typo_line_m").find("a").get("href")
        except:
            url = ""
        try:
            descr = li.find("div", class_="text-container typo typo_text_m typo_line_m organic__text").text.strip()
        except:
            descr = ""

        data = {
            "header": header,
            "url": url,
            "descr": descr
        }
        write_csv(data)
        # print(header, url, descr)
        
        


def main():
    # for i in range(0, 10):
        pattern = "https://yandex.ru/search/?lr=21726&text=yandex&p=0"
        get_page_data(get_html(pattern))


if __name__ == "__main__":
    main()