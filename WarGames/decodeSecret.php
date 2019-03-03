/*
 * This is a simple script for natas8 from http://overthewire.org/wargames/natas/natas9.html
 * that decodes an encoded secret given from the natas8 PHP script and encoding scheme.
*/

<?
$hex = base64_decode(strrev(hex2bin("3d3d516343746d4d6d6c315669563362")));
echo $hex . "\n";
?>
