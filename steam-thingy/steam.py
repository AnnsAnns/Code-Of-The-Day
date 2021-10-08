from requests import get
from time import sleep
from random import randint
from json import loads, dump
import re
import os

STEAM_URL = "https://store.steampowered.com/search/results/?query=&dynamic_data=&start={start}&count={count}&infinite=1"
PRODUCT_REGEX_STR = r'<a href=\"(.+?)\"\n*\s*data-ds-appid=\"(\d*)\"\s*data-ds-itemkey=\"(.+?)\"\s*data-ds-tagids=\"(\[(?:\d*,?)*\])\"(?:\s|\n|\r|.)+?<span class=\"title\">(.+?)<\/span>(?:\s|\n|\r|.)+?data-price-final=\"(\d+)\"(?:\s|\n|\r|.)+?<\/a>'
PRODUCT_REGEX = re.compile(PRODUCT_REGEX_STR)

def delay():
    sleep(randint(0, 1))

def get_page(offset: int, step: int=50):
    r = get(STEAM_URL.format(start=offset, count=step))
    r = loads(r.text)
    assert r["success"] == 1
    matches = re.findall(PRODUCT_REGEX, r["results_html"])
    print(f"{len(matches)=}")
    return matches, r["total_count"]

def save(games, cnt, path):
    print(f"Saving {cnt}")
    with open(path, "w") as fp:
        dump(games, fp)

def scrape_games(start: int=0, games: dict=None, step: int=100, end: int=30000) -> dict:
    games = dict() if games is None else games
    res, total = get_page(start, step)
    if end is None:
        end = total
    while got := len(res) or start < end:# and offset == 0:
        start += got
        res, total = get_page(start, step)

        for (url, appid, _, tags, title, price) in res:
            games[str(appid)] = {
                "id": int(appid),
                "url": url,
                "title": title,
                "price": int(price),
                "tag-ids": eval(tags)
            }
        if (start - got) % 2000 < 100:
            save(games, start - got, "all-games-save.json")
        delay()
    return games

if __name__ == "__main__":
    if os.path.exists("all-games.json"):
        with open("all-games.json", "r") as fp:
            games = loads(fp.read())
    else:
        if os.path.exists("all-games-save.json"):
            with open("all-games-save.json", "r") as fp:
                games = loads(fp.read())
                scrape_games(len(games.keys()), games, step=350)
        else:
            games = scrape_games()

        with open("all-games.json", "w") as fp:
            dump(games, fp, indent=4)

