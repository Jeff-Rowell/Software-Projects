/*
 * This script is written for natas11 from http://overthewire.org/wargames/natas/natas12.html
 * It solves the challenge by reversing the encoding mechanism the the natas11 PHP script
 * implements. The first step is to find the key that is used in the original xor_encrypt
 * function that is given in the natas11 PHP script. Once we find that key, it is straighforward
 * to undo the xor_encrypt and base64 encoding. See https://r00tblogger.wordpress.com/2019/01/29/overthewire-natas-wargame/
 * for my full write-up.
*/

<?
$cookie = base64_decode('ClVLIh4ASCsCBE8lAxMacFMZV2hdVVotEhhUJQNVAmhSRwgMN0cIaAw=');

function xor_encrypt1($in) {
    $key = json_encode(array("showpassword"=>”no”, "bgcolor"=>"#ffffff"));
    $text = $in;
    $outText = '';

    // Iterate through each character
    for($i=0;$i<strlen($text);$i++) {
    $outText .= $text[$i] ^ $key[$i % strlen($key)];
    }

    return $outText;
}

function xor_encrypt2() {
    $text = json_encode(array("showpassword"=>"yes", "bgcolor"=>"#ffffff"));
    $key = "qw8J";
    $outText = '';

    // Iterate through each character
    for($i=0;$i<strlen($text);$i++) {
    $outText .= $text[$i] ^ $key[$i % strlen($key)];
    }

    return $outText;
}

echo xor_encrypt1($cookie) . "\n";
echo base64_encode(xor_encrypt2()) . "\n";
?>
