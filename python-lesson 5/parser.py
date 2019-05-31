import csv


def write_csv(data):
    with open("writer.csv", "a", newline = "") as file:
        writer = csv.writer(file, delimiter = ";")
        writer.writerow((
            data["name"],
            data["surname"],
            data["age"]
        ))
def write_csv2(data):
    with open("file.csv", "a", newline = "") as file:
        order = ["name", "surname", "age"]
        writer = csv.DictWriter(file, fieldnames = order, delimiter = ";")
        writer.writerow(data)
 

def main():
    d = {"name": "Petr", "surname": "Ivanov", "age": 21}
    d1 = {"name": "Ivan", "surname": "Petrov", "age": 21}
    d2 = {"name": "Inna", "surname": "Ivanova", "age": 21}

    l = [d, d1, d2]

    # for i in l:
    #     write_csv2(i)
    with open("file.csv") as file:
        order = ["name", "surname", "age"]
        reader = csv.DictReader(file, fieldnames=order, delimiter = ";")
        for row in reader:
            print(row)
        

if __name__ == "__main__":
    main()