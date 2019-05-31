import csv
from bs4 import BeautifulSoup
import requests
import re


def get_html(url):
    r = requests.get(url)
    if r.ok:
        return r.text
    print(r.status_code)

def write_csv(data):
    with open("coinmarket.csv", "a", newline="") as file:
        writer = csv.writer(file, delimiter = ";")
        writer.writerow((data["name"],data["symbol"],data["link"],data["price"]))

def get_page_data(html):
    soup = BeautifulSoup(html, "lxml")
    trs = soup.find("table", id="currencies").find("tbody").find_all("tr")
    for tr in trs:
        tds = tr.find_all("td")
        try:
            name = tds[1].find("a", class_="currency-name-container link-secondary").text.strip()
        except:
            name = ""
        try:
            symbol = tds[1].find("a").text.strip()
        except:
            symbol = ""
        try:
            link = "https://coinmarketcap.com" + tds[1].find("a", class_="currency-name-container link-secondary").get("href")
        except:
            link = ""
        try:
            price = tds[3].find("a").get("data-usd")
        except:
            price = ""

        data = {
            "name": name,
            "symbol": symbol,
            "link": link,
            "price": price
        }
        write_csv(data)       


def main():
    url = "https://coinmarketcap.com"
    while True:
        get_page_data(get_html(url))

        soup = BeautifulSoup(get_html(url), "lxml")
        try:
            pattern = "Next"
            url = "https://coinmarketcap.com/" + soup.find("ul", class_="pagination").find("a", text=re.compile(pattern)).get("href")
        except:
            break
    print("good")

if __name__ == "__main__":
    main()