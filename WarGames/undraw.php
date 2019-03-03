/*
 * This is a script used for analyzing the PHP script used for natas26 from
 * http://overthewire.org/wargames/natas/natas27.html. It unserializes the 
 * cookie given and prints the $drawing object in a human-readable manner.
*/

<?
$drawing = unserialize(base64_decode("YToxOntpOjA7YTo0OntzOjI6IngxIjtzOjE6IjIiO3M6MjoieTEiO3M6MToiMiI7czoyOiJ4MiI7czoxOiIyIjtzOjI6InkyIjtzOjE6IjIiO319"));
print_r($drawing);
?>
