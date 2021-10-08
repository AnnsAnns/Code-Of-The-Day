
import json

games = json.load(open("all-games-save.json"))
tags = json.load(open("tags-mapping.json"))

out = {}
for tag in tags:
    temp = []
    tagid = int(tag["id"])
    for game in games.values():
        if tagid in game["tag-ids"]:
            temp += [game]
    out[tag["name"]] = temp

with open("games-by-tags.json", "w") as fp:
    json.dump(out, fp, indent=4)
