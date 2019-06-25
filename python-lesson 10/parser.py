import requests
from bs4 import BeautifulSoup
import csv

def get_html(url):
    user_agent = {"User-Agent": "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 YaBrowser/19.6.1.153 Yowser/2.5 Safari/537.36"}
    r = requests.get(url, headers=user_agent)
    return r.text

def write_csv(data):
    with open("parser_jquery.csv", "a", newline="", encoding="utf-8") as file:
        order = ["author", "since"]
        writer = csv.DictWriter(file, fieldnames=order, delimiter=";")
        writer.writerow(data)

def get_articles(html):
    soup = BeautifulSoup(html, "lxml")
    ts = soup.find("div", class_="testimonial-container").find_all("article")
    return ts # Или пустой список, или список с отзывами

def get_page_data(ts):
    for t in ts:
        try:
            since = t.find("p", class_="traxer-since").text.strip()            
        except:
            since = ""
        try:
            author = t.find("p", class_="testimonial-author").text.strip()
        except:
            author = ""
        data = {"author": author, "since": since}
        write_csv(data)
            
def main():
    # 1. Получение контейнеров с отзывами и список отзывов
    # 2. Если список есть, то парсим отзывы
    # 3. Если список пустой, то цикл прерывается 
    while True:
        page = 1
        url = "https://catertrax.com/why-catertrax/traxers/page/{}/".format(str(page))

        articles = get_articles(get_html(url))

        if articles:
            get_page_data(articles)
            page = page + 1
        else:
            break
        


if __name__ == "__main__":
    main()
