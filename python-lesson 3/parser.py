import requests
from bs4 import BeautifulSoup
import csv


def get_html(url):
    # получаем html код страницы
    r = requests.get(url)
    return r.text

def write_csv(data):
    # записываем данные в файл csv
    with open("coinmarket.csv", "a") as file:
        writer = csv.writer(file)
        writer.writerow([
            data["name"],
            data["symbol"],
            data["url"],
            data["price"]
        ])

def get_page_data(html):
    # создаем объект класса для получения доступа к элементам страницы
    soup = BeautifulSoup(html, "lxml")
    # ищем все теги TR в теге TBODY в теге TABLE с id currencies
    trs = soup.find("table", id = "currencies").find("tbody").find_all("tr")
    # создаем цикл "пробежки" по каждому тегу TR
    for tr in trs:
        # ищем все теги TD в текущем теге TR
        tds = tr.find_all("td")
        # получаем текст тега A с классом в теге td по номером 1
        name = tds[1].find("a", class_="currency-name-container link-secondary").text
        # получаем краткий символ тега A 
        symbol = tds[1].find("a").text
        # получаем ссылку из атрибута href и коннектим с ссылкой сайта для корректного отображения 
        url = "https://https://coinmarketcap.com" + tds[1].find("a").get("href")        
        # получаем цену из атрибута data-usd тега a с классом price
        price = tds[3].find("a", class_="price").get("data-usd")
        # print(name, symbol, "https://https://coinmarketcap.com" + url, price)
        # создаем словать с ключами и заносим все найденные данные
        data = {
            "name": name,
            "symbol": symbol,
            "url": url,
            "price": price
        }
        # пишем данные в файл
        write_csv(data)



def main():
    url = "https://coinmarketcap.com"
    get_page_data(get_html(url))



if __name__ == "__main__":
    main()