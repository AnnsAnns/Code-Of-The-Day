import re

REGEX = re.compile(r'<a class=\"label label-link label-color-.+?\" href=\"\/tag\/(\d+)\/\">(.+?)<\/a>')

with open("raw-tags.html", "r") as fp:
    matches = re.findall(REGEX, fp.read())

print(matches)

import json

js = [
    {
    "id": match[0], "name": match[1] } for match in matches
]


with open("tags-mapping.json", "w") as fp:
    json.dump(js, fp, indent=4)
