using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CWMasterTeacher3;

namespace CWMasterTeacherDataModel
{
    /*
     * This class was created with the express purpose of supporting the existing 
     * and modified methods in Google's DiffMatchPatch class. It is intended to 
     * perform additional functions related to parsing the html so that the differences 
     * are displayed clearly and effectively.
     */
    public class DMPCWMTHelper
    {
        Dictionary<string, char> _htmlTags;
        Dictionary<char, string> _insDict;
        Dictionary<char, string> _delDict;
        Dictionary<char, string> _equalDict;

        List<char> _insList;
        List<char> _delList;
        List<char> _equalList;

        Queue<Tuple<Operation, string>> _wrapperQueue;
        StringComparer _stringComparer;
        StringBuilder _stringBuilder;

        int _unicodeIndex;

        public readonly Func<string, bool> isOpenTag;
        public readonly Func<char, bool> firstInUnicodeRange;
        public readonly Func<string, bool> isBlockOpener;

        public DMPCWMTHelper()
        {
            _htmlTags = new Dictionary<string, char>();
            _insDict = new Dictionary<char, string>();
            _delDict = new Dictionary<char, string>();
            _equalDict = new Dictionary<char, string>();

            _unicodeIndex = 0xE000;
            _insList = new List<char>();
            _delList = new List<char>();
            _equalList = new List<char>();

            _wrapperQueue = new Queue<Tuple<Operation, string>>();
            _stringComparer = StringComparer.InvariantCulture;
            _stringBuilder = new StringBuilder();

            isBlockOpener = (x) => _stringComparer.Equals(x, "<p ") || _stringComparer.Equals(x, "<li");
            firstInUnicodeRange = (x) => x >= 0xE000;
            isOpenTag = (x) => !x.Contains("/");
        }

        //for external use, no setter.
        public int UnicodeIndex
        {
            get
            {
                return _unicodeIndex;
            }
        }

        //Automatically increments the unicode target after returning current value, for private use
        private char CurrentUnicode
        {
            get
            {
                return (char)_unicodeIndex++;
            }
        }

        //returns a copy
        public Dictionary<string, char> HtmlTags
        {
            get
            {
                var newDict = new Dictionary<string, char>(_htmlTags.Comparer);
                foreach (var x in _htmlTags.Keys){
                    newDict.Add(x, _htmlTags[x]);
                }
                return newDict;
            }
        }

        //returns a copy
        public Dictionary<char, string> InsDict
        {
            get
            {
                var newDict = new Dictionary<char, string>(_insDict.Comparer);
                foreach (var x in _insDict.Keys)
                {
                    newDict.Add(x, _insDict[x]);
                }
                return newDict;
            }
        }

        //returns a copy
        public Dictionary<char, string> DelDict
        {
            get
            {
                var newDict = new Dictionary<char, string>(_delDict.Comparer);
                foreach (var x in _delDict.Keys)
                {
                    newDict.Add(x, _delDict[x]);
                }
                return newDict;
            }
        }

        //returns a copy
        public Dictionary<char, string> EqualDict
        {
            get
            {
                var newDict = new Dictionary<char, string>(_equalDict.Comparer);
                foreach (var x in _equalDict.Keys)
                {
                    newDict.Add(x, _equalDict[x]);
                }
                return newDict;
            }
        }
        
        public void TagParser(ref string s1, ref string s2)
        {
            TagParser(ref s1);
            TagParser(ref s2);            
        }

        /* Removes html tags, replaces them with unicode characters, and adds unique 
         * pairs of html tags and unicode characters to the dictionary
         */
        public void TagParser(ref string s)
        {
            int LTIndex = s.IndexOf("<");
            int GTIndex;
            string tag;

            while (LTIndex != -1)
            {
                GTIndex = s.IndexOf(">");
                tag = s.Substring(LTIndex, GTIndex - LTIndex + 1);

                if (!_htmlTags.ContainsKey(tag))
                {
                    _htmlTags.Add(tag, (char)CurrentUnicode);
                }

                s = s.Substring(0, LTIndex) + _htmlTags[tag] + s.Substring(GTIndex + 1, s.Length - GTIndex - 1);
                LTIndex = s.IndexOf("<");
            }
        }

        public void ReEncodeDiffs(List<Diff> diffList)
        {
            foreach (var diff in diffList)
            {
                diff.text = HttpUtility.HtmlEncode(diff.text);
            }
        }

        public int Indexer<T>(IEnumerable<T> s, Func<T, bool> func)
        {
            for (int i = 0; i < s.Count(); i++)
            {
                if (func(s.ElementAt(i)))
                {
                    return i;
                }
            }
            //not found
            return -1;
        }

        public void AppendStyleToTags(string style, Operation operation)
        {
            if (operation == Operation.INSERT)
            {
                AppendStyleToTags(style, _insDict);
            }
            if (operation == Operation.DELETE)
            {
                AppendStyleToTags(style, _delDict);
            }
            if (operation == Operation.EQUAL)
            {
                AppendStyleToTags(style, _equalDict);
            }
        }

        private void AppendStyleToTags(string style, Dictionary<char, string> targetDict)
        {
            List<string> htmlTags = _htmlTags.Keys.ToList();
            List<char> unicodeTags = _htmlTags.Values.ToList();
            string x, xStyled;
            char xUnicodeChar;

            for (int i = 0; i < htmlTags.Count; i++)
            {

                x = htmlTags[i];
                xUnicodeChar = unicodeTags[i];

                if (isOpenTag(x))
                {
                    int insertIndex = x.IndexOf(">");
                    xStyled = x.Substring(0, insertIndex) + " " + style + ">";
                    targetDict.Add(xUnicodeChar, xStyled);
                }
                else
                {
                    targetDict.Add(xUnicodeChar, x);
                }
            }
        }

        public void ReplaceStyledTags(Diff diff)
        {
            List<char> targetList = null;
            Dictionary<char, string> targetDict = null;

            switch (diff.operation)
            {
                case Operation.INSERT:
                    targetList = _insList;
                    targetDict = _insDict;
                    break;
                case Operation.DELETE:
                    targetList = _delList;
                    targetDict = _delDict;
                    break;
                case Operation.EQUAL:
                    targetList = _equalList;
                    targetDict = _equalDict;
                    break;
            }

            ReplaceStyledTags(diff, targetDict, targetList);
        }

        private void ReplaceStyledTags(Diff diff, Dictionary<char, string> targetDict, List<char> targetList)
        {
            int tagIndex = Indexer(diff.text, firstInUnicodeRange);
            string replacementTag;
            char unicodeCharacter;

            while (tagIndex > -1)
            {
                unicodeCharacter = diff.text[tagIndex];
                replacementTag = targetDict[unicodeCharacter];
                _stringBuilder.Clear();
                _stringBuilder.Append(diff.text);

                if (isBlockOpener(replacementTag.Substring(0, 3)))
                {
                    if (_wrapperQueue.Count == 0)
                    {
                        _stringBuilder.Replace(unicodeCharacter.ToString(), replacementTag, tagIndex, 1);
                        _wrapperQueue.Enqueue(new Tuple<Operation, string>(diff.operation, replacementTag));
                        //diff.text = diff.text.Substring(0, tagIndex) + replacementTag + diff.text.Substring(tagIndex + 1, diff.text.Length - tagIndex - 1);
                    }
                    else
                    {
                        if(OperationWrapperIsEnqueued(diff.operation))
                        {
                            _stringBuilder.Remove(tagIndex, 1);
                        }
                        else
                        {
                            _wrapperQueue.Enqueue(new Tuple<Operation, string>(diff.operation, replacementTag));
                            _stringBuilder.Remove(tagIndex, 1);
                        }
                        
                    }
                }

                diff.text = _stringBuilder.ToString();
                tagIndex = Indexer(diff.text, firstInUnicodeRange);
            }
        }

        private bool OperationWrapperIsEnqueued(Operation op)
        {
            foreach (var x in _wrapperQueue)
            {
                if (x.Item1.Equals(op)) return true;
            }
            return false;
        }
    }
}
 