import csv
from peewee import *

db = PostgresqlDatabase(database = "test", user = "postgres", password = "ea656454pb", host = "localhost")


class Coin(Model):
    name = CharField()
    symbol = CharField()
    url = TextField()
    price = CharField()

    class Meta:
        database = db




def main():

    db.connect()
    db.create_tables([Coin])

    with open("coinmarket.csv") as file:
        order = ["name", "symbol", "url", "price"]
        reader = csv.DictReader(file, fieldnames=order, delimiter = ";")

        coins = list(reader)

        # способ 1 - не очень
        # for row in coins:
        #     # coin = Coin(name = row["name"], symbol = row["symbol"], url = row["url"], price = row["price"])
        #     # coin.save()

        # * - список аргументов
        # ** - словарь именованных аргументов
        # способ 2 - тоже не самый лучший
        # with db.atomic():
        #     for row in coins:
        #         Coin.create(**row)

        # способ 3 - самый быстрый
        with db.atomic():
            for index in range(0, len(coins), 100):
                Coin.insert_many(coins[index:index + 100]).execute()

        print("done")

if __name__ == "__main__":
    main()