DiffMatchPatch Helper Class
=======================
Summary:
---------------
DMPCWMTHelper.cs was built to aid Google's DiffMatchPatch class in determining the user-relevant differences between two HTML-encoded strings. When comparing raw text, DiffMatchPatch.cs competently returns HTML-encoded differences; however, Google's algorithm doesn't come equipped with any tools for analyzing text in context of how it's displayed to a user.
On a high level, the approach we used was to parse through the HTML-encoded strings before calculating the differences, replacing each HTML tag with a unique unicode character in order to maintain their autonomy. Once we found the differences, we then needed to concatenate the text from each Diff into a single string. Given the tendency of HTML's block openers (e.g. <p>) to separate text on to separate lines, we included logic that would include and disinclude block openers and closers depending on the presence of other active block openers and closers. 
Additionally, we included logic that accounted for situations in which style had changed between similar text or where list items had been added. Try as we might to form a legible solution, our code is occasionally obtuse. This README exists to give a basic overview of what each method is intended to do and instructions on how to change key elements of the output. 

How to use:
---------------
The whole class uses a single parent method to perform all relevant functions: PrettyDiffFormatter(string s1, string s2).
The method is not Static. An instantiation of DMPCWMTHelper is necessary. Using it is as simple as instantiating the class and storing the results of that method into a string object. 
DMPCWMTHelper dMPCWMTHelper = new DMPCWMTHelper();
string result = dMPCWMTHelper.PrettyDiffFormatter(stringA, stringB);

How to change the color:
---------------
If you are interested in changing the formatting inormation for the Inserted and Deleted text, you need only change the parameters of the fifth instruction in the PrettyDiffFormatter method.
DefineStyleDupDict("background:#a0ffa0", "background:#ffa0a0; text-decoration: line-through");
The first parameter of this instruction becomes the style information for the inserted text, and the second parameter of this instruction becomes the style information for the deleted text. 

Methods:
---------------
**TagParser (ref string s1, ref string s2)**
-	Calls TagParser(ref string s) twice, using both of the arguments passed in by reference to replace all of the HTML tags with an unused unicode character. This method is used to bolster the PrettyDiffFormatter method in that it preprocesses the string so that the difference highlighting is appropriately applied to all HTML tags. Originally the HTML tags would break the difference highlighting. For example on a paragraph, ordered list, unordered list, list item, address, preformatted, and all header tags the difference highlighting was not being applied to the contents within those tags because the tags would force the text onto a new line.

		- Params: 
			ref string s1, and ref string s2 are strings passed by reference to iterate through. 
		
		- Returns:
			void

**TagParser (ref string s)**
-	Replaces all occurrances of HTML tags with an unsued unicode character, adding each HTML tag and its corresponding unicode character to a dictionary of strings and chars where the strings are the HTML tags and the chars are the undicode characters. The HTML tags are the keys, and the unused unicode characters are the values of the dictionary. This method is used to bolster the PrettyDiffFormatter method in that it preprocesses the string so that the difference highlighting is appropriately applied to all strings.

		- Params: 
			ref string s is a string passed by reference, used to iterate through and replace all HTML tags with unused unicode characters. 

		- Returns:
			void

**ReEncodeDiffs (List<Diff> diffList)**
-	Iterates through a list of Diff objects and replaces any HTML sensitive characters that live within the text of the Diff objects. Things like (<, >, &, and more) are replaced with their HTML couterparts (&lt;, &gt;, &amp; and more). We initially decode these so that they can be regarded as single characters when we run the differencing algorithm, but in order for the page not to regard them as html elements, we need to go back through our completed differences and return them to their longer form. Each Diff is comprised of two things and can be thought of as a pair; the first element of the Diff object is an operation represented by three enumerations: INSERT, DELETE, and EQUAL. The second element in the Diff "pair" is a string that contains the text that was inserted, deleted, or the same. We loop through a list of these Diff objects and make use of the HMLUtility class to encode the HTML strings.

		- Params: 
			List<Diff> diffList, an ordanary list to iterate through that holds Diff objects.

		- Returns:
			void

**Indexer<T> (IEnumerable<T> s, Func<T, bool> func)**
-	Searches through an IEnumerable object according to the function, func, that returns a true/false. 

		- Params: 
			IEnumerable s, the iterable object to search through.
			Func<T, bool> func, a function that takes in a generic object and returns a boolean based on the conditions specified by the function. Typically a lambda is passed in place of this.
			
		- Returns:
			The first index of the IEnumerable object that it finds that meets func's condidtions. Returns -1 otherwise to signal not found.

**DefineStyleDupDict (string s1, string s2)**
-	Given two strings that hold HTML and css specifications, it privately sets two properties, InsStyle and DelStyle, to create the <font> HTML tags that contain the green highlighting and red highlighting with strikethrough. The first string, s1, must contain the insert style specifications, whereas the second string, s2, must contain the delete style specifications. Iterates through a copy of the dictionary of strings and chars where the strings are the HTML tags and the chars are the unicode characters (_htmlTags), and builds a new dictionary (_finalDict) that is the opposite of HTML tags. That is, the strings are the values and the chars are the keys.

		- Params: 
			string s1, the string that has the insert style information.
			string s2, the string that has the delete and strikethrough style information.

		- Returns:
			void

**ReplaceStyledTags (Diff diff)**
-	Based on the operation of the Diff object, assigns a font style to correspond to the style information of the operation, and adds opening font tags with the styling applied to the strings of the Diff objects. If the Diff object's operation is EQUAL and both of the lists that contain inserted tags and deleted tags are empty, we replace the EQUAL diff with a new INSERT diff. Appends the <font> HTML tag with styling applied in front of the text in the Diff object. Changes the current Diff's operation to INSERT and returns a new Diff with duplicate text of operation DELETE if at least one of the lists of containing active deleted or inserted tags is not empty. The justification here is that if one of the lists is not empty, it means that the text inside the current EQUAL Diff is actually being represented in two different ways.

		- Params: 
			Diff diff, the Diff object to act upon using its operation and text fields.

		- Returns:
			A new Diff object where the EQUAL operation has been replaced with a DELETE operation if both lists that contain inserted tags and deleted tags are empty. The same Diff object with the font style applied is returned otherwise.

**AddClosingTags (Diff diff, List<Tuple<char, char>> targetList)**
-	Iterates through a list of unicode characters that correspond to either inserted HTML tags or deleted HTML tags, and appends the closing font tag (</font>) to the end of the text field in the Diff object.

		- Params: 
			Diff diff, the Diff object to act upon using its operation and text fields.
			List<Tuple<char, char>> targetList, the list of unicode characters that correspond to either inserted HTML tags or deleted HTML tags.

		- Returns:
			void
	
**AddOpeningTags (Diff diff, List<Tuple<char, char>> targetList)**
-	Iterates through a list of unicode characters that correspond to either inserted HTML tags or deleted HTML tags, and appends the opening font tag (<font ~style info here~>) to the beginning of the text  field in the Diff object.

		- Params: 
			Diff diff, the Diff object to act upon using its operation and text fields.
			List<Tuple<char, char>> targetList, the list of unicode characters that correspond to either inserted HTML tags or deleted HTML tags.

		- Returns:
			void
	
**ReplaceStyledTags (Diff diff,  List<Tuple<char, char>> targetList)**
-	Replaces all HTML tags with their corresponding HTML tags that hold the new style information based on what the differencing algorithm determined. Processes and replaces style tags such as bold tags, itilicized tags, and the like with the same tags with style information applied.Processes the closing style tags by replacing the unicode characters with the actual corresponding tags and adding a </font> tag afterwards. Processes and replaces the opening block tags so that the style information is applied, along with replacing the closing block tag unicode characters with the actual tags.

		-Params:
			Diff diff, the Diff object to act upon using its operation and text fields.
			List<Tuple<char, char>> targetList, the list of inserted HTML tags or deleted HTML tags.

		- Returns:
			void

**ProcessBlockCloser (Diff diff, int tagIndex, string replacementTag, char unicodeCharacter)**
-	Based on the operation of the Diff object, replaces the unicode characters with their respective closing HTML block tags. We considered a closing block tag to be </p>, </h1> through </h6>, 		  		  </address>, </ol>, </ul>, </pre>.

		- Params: 
			Diff diff, the Diff object to act upon using its operation and text fields.
			int tagIndex, the index of the unicode character within the string to replace with its corresponding current closing block tag.
			string replacementTag, the corresponding closing block tag with all of the style information previously applied.
			char unicodeCharacter, the unicode character that corrsponds to the current closing block tag.

		- Returns:
			void

**OtherList (List<Tuple<char, char>> targetList)**
-	Checks if the argument points to the internal list of deleted HTML tags and returns the opposite list of that. If the argument represents the list of inserted HTML tags, then the list of deleted style tags is returned, and vise versa.

		- Params: 
			List<Tuple<char, char>> targetList, the list of unicode characters that correspond to either inserted HTML tags or deleted HTML tags.	
		
		- Returns:
			_insTagList if the argument passed in represents the list of deleted HTML tags.
			_delTagList if the argument passed in represents the list of inserted HTML tags.

**ProcessBlockOpener (Diff diff, int tagIndex, string replacementTag, string repTagClass, char unicodeCharacter, string fontStyle)**
-	Based on the operation of the Diff object, replaces the unicode characters with their respective opening HTML block tags. We considered an opening block tag to be<p>, <h1> through <h6>, 		  		  <address>, <ol>, <ul>, <pre>. Appends style information in front of each one of these tags using a font tag. For example, <p> turns into <font ~style information goes here~><p>, only the <p> is encrypted with an unused unicode character.

		- Params: 
			Diff diff, the Diff object to act upon using its operation and text fields.
			int tagIndex, the index of the unicode character within the string to replace with its corresponding current opening block tag.
			string replacementTag, the corresponding opening block tag with all of the style information previously applied.
			string repTagClass, similar to the replacementTag string, only without the less than and greater than signs. So if replacementTag = "</p>", then repTagClass = "/p". Used for determining if the current opening tags correspong to ordered or unordered lists.
			char unicodeCharacter, the unicode character that corrsponds to the current opening block tag.
			
		- Returns:
			void

**ActiveTagEquality ()**
-	Checks the size of the internal lists that contain the inserted and deleted HTML tags.

		-Params: 
			None
		
		- Returns:
			True, if both of the internal lists are empty.
			False, otherwise.

**AddSpacesToSpecialTags (ref string s1, ref string s2)**
-	Takes in two strings by reference and adds spaces in specific locations of what we deemed as special tags. Special tags are those HTML tags that will break out of the difference highlighting, such as</p>, </h1> through </h6>,</address>, </ol>, </ul>, </pre>, and <li> tags and their respective closing tags. Adds spaces after the opening tags and before the closing tags. This can be thought of as padding the innards of the HTML tags with one space so that our differencing algorithm on the word level can split each HTML tag upon whitespace. This allows us to more accurately pinpoint the location of differences that are meaningful to the user.

		- Params:
			ref string s1, and ref string s2 are strings passed by reference to add the whitespaces before and after special tags. 

		- Returns:
			void

**AddSpaceBefore (string tagClass, StringBuilder stringBuilder1, StringBuilder stringBuilder2)**
-	Given a string builder for each string (comparison lesson and editable lesson), appends a single whitespace before special tags in each string.

		- Params:
			string tagClass, an HTML tag.
			StringBuilder stringBuilder1, the string builder used to append whitespace padding onto the comparison lesson text.
			StringBuilder stringBuilder2, the string builder used to append whitespace padding onto the editable lesson text.

		- Returns:
			void

**AddSpaceBefore (List<string> list, StringBuilder stringBuilder1, StringBuilder stringBuilder2)**
-	Given a list of HTML tags and a string builder for each string (comparison lesson and editable lesson), appends a single whitespace before special tags in each string.

		- Params:
			string tagClass, an HTML tag.
			StringBuilder stringBuilder1, the string builder used to append whitespace padding onto the comparison lesson text.
			StringBuilder stringBuilder2, the string builder used to append whitespace padding onto the editable lesson text.

		- Returns:
			void

**AddSpaceAfter (string tagClass, StringBuilder stringBuilder1, StringBuilder stringBuilder2)**
-	Given a string builder for each string (comparison lesson and editable lesson), appends a single whitespace after special tags in each string.

		- Params:
			string tagClass, an HTML tag.
			StringBuilder stringBuilder1, the string builder used to append whitespace padding onto the comparison lesson text.
			StringBuilder stringBuilder2, the string builder used to append whitespace padding onto the editable lesson text.

		- Returns:
			void

**AddSpaceAfter (List<string> list, StringBuilder stringBuilder1, StringBuilder stringBuilder2)**
-	Given a list of HTML tags and a string builder for each string (comparison lesson and editable lesson), appends a single whitespace after special tags in each string.

		- Params:
			string tagClass, an HTML tag.
			StringBuilder stringBuilder1, the string builder used to append whitespace padding onto the comparison lesson text.
			StringBuilder stringBuilder2, the string builder used to append whitespace padding onto the editable lesson text.

		- Returns:
			void

**PrettyDiffFormatter (string s1, string s2)**
-	This is what puts everything together and produces the beautiful text with difference highlighting. Utilizes all necessary methods within this class to apply style information to each appropirate HTML tag, highlighting with green any text that was inserted and highlighting with red and strikethrough any text that was deleted. This is done on a word level, meaning that our algorithm compares on a word by word basis. Text that is the same does not get any additional style information applied to it. The internal loop accounts for cases in which an additional Diff must be added due to disparate style data. 

		- Params:
			string s1, the comparison lesson text.
			string s2, the editable lesson text.
		
		- Returns:
			The string containing all of the HTML style and css information applied to text that was inserted and text that was deleted.

**SplitDiffConcatenator (List<Diff> diffList)**
-	Takes a list of Diff objects and checks that all of the inserted text appears together in a sequence followed by all of the deleted text afterwards, with any similar text remaining in between. Ensures that the inserted text appears exactly where it was inserted, and the deleted text appears exactly where it was deleted.

		- Params:
			List<Diff> diffList, the list of Diff objects after the comparison lesson text and editable lesson text strings have been compared.

		- Returns:
			void

**OppositeOp (Diff currentDiff)**
-	Given a Diff object, returns the opposite operation enum of that object.

		- Params:
			Diff currentDiff, the Diff object whose operation is checked.

		- Returns:
			Operation.DELETE if the Diff object's current operation is INSERT
			Operation.INSERT if the Diff object's current operation is DELETE