import praw
import glob, os
import random

#IDK if this works tbh

files = []

os.chdir("/image_folder/") # Everything in that folder will be tried to posted
for file in glob.glob("*.*"):
    files = files + file

reddit = praw.Reddit(client_id='CLIENT_ID',
                     client_secret="CLIENT_SECRET", password='PASSWORD',
                     user_agent='USERAGENT', username='USERNAME') 
# Look at here to find the infos: https://praw.readthedocs.io/en/latest/getting_started/authentication.html#oauth


while True:
  title = 'My favorite picture' # Title
  image = str(random.choice(files)) # Picks one image
  reddit.subreddit('reddit_api_test').submit_image(title, image) # Replace subreddit
  time.sleep(21600) # 6 Hours in secs lol