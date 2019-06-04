import requests
from bs4 import BeautifulSoup
import csv


def get_html(url):
    r = requests.get(url)
    return r.text

def write_csv(data):
    with open("coinmarket", "a") as file:
        writer = csv.writer(file)
        # writer.writerow(data[
                
        # ])

def get_page_data(html):
    soup = BeautifulSoup(html, "lxml")
    trs = soup.find("table", id = "currencies").find("tbody").find_all("tr")
    for tr in trs:
        tds = tr.find_all("td")
        name = tds[1].find("a", class_="currency-name-container link-secondary").text
        symbol = tds[1].find("a").text
        url = tds[1].find("a").get("href")
        print(name, symbol, "https://https://coinmarketcap.com" + url)
        # for td in tds:



def main():
    url = "https://coinmarketcap.com"
    get_page_data(get_html(url))



if __name__ == "__main__":
    main()