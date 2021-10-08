import json

import csv

def convert_all_games_to_csv(data: dict, csv_path: str):
    with open(csv_path, "w", newline="") as fp:
        hdr = [x for x in next(iter(data.values())).keys()]
        print(hdr)
        writer = csv.DictWriter(fp, fieldnames=hdr)
        writer.writeheader()
        for entry in data.values():
            try:
                writer.writerow(entry)
            except:
                print(f"Failed writing: {entry=}")

convert_all_games_to_csv(
    json.load(open("all-games-save.json", "r")),
    "all-games.csv"
)

