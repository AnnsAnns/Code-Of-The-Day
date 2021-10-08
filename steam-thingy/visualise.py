import pandas as pd
import json
import numpy as np
import matplotlib.pyplot as plt

all = json.load(open("games-by-tags.json"))

#print(genres)


genres = [x for x in all.keys()]
avg = {}
for genre in genres:
    sm = 0
    cnt = 0
    for game in all[genre]:
        cnt += 1
        sm += game["price"] / 100

    avg[genre] = sm / (cnt or 1)

avg = [
    x for x in avg.items() if x[1]
]
avg.sort(key=lambda x: x[1])
avg = {
    genre: price for genre, price in avg[::-1]
}


genres = [x for x in avg.keys()]
prices = [x for x in avg.values()]

#sub_genres = [genres[i:i+4] for i in range(0, len(genres), 4)]
#sub_prices = [prices[i:i+4] for i in range(0, len(prices), 4)]

plt.rcdefaults()

fig, ax = plt.subplots(dpi=120.0, figsize=(130.0, 24.0))

rects = ax.bar(genres, prices, width=0.35)
ax.set_xlabel("Genre")
ax.set_ylabel("Price in €")
ax.set_title("Steam average prices by genre")
plt.setp(ax.get_xticklabels(), rotation=60, horizontalalignment="right")

def autolabel(rects):
    """
    Attach a text label above each bar displaying its height
    """
    for rect in rects:
        height = rect.get_height()
        ax.text(rect.get_x() + rect.get_width()/2., height + 0.1,
                "{:.2f}".format(height),
                ha='center', va='bottom')
autolabel(rects)

#plt.show()
plt.savefig("steam-prices-plotted.png")