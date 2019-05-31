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
    with open("yandx.csv", "a") as file:
        writer = csv.writer(file)
        pass

def get_page_data(html):
    soup = BeautifulSoup(html, "lxml")
    lis = soup.find_all("li", class_="serp-item")
    for li in lis:
        try:
            header = li.find("div", class_="organic__url-text").text
        except:
            header = ""
        try:
            url = li.find("h2", class_="organic__title-wrapper typo typo_text_l typo_line_m").find("a").get("href")
        except:
            url = ""
        print(header, url)
        
        


def main():
    url = "https://yandex.ru/search/?lr=21726&text=yandex&p=0"
    get_page_data(get_html(url))

if __name__ == "__main__":
    main()