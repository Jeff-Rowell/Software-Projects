# Software-Projects
I have a large passion for software and I love to write code. I am very familiar with PHP, Python, 
C, C++, Java, and C# coding standards. I have solid programming experience and can write scripts 
with confidence. I am CompTIA Security+ and Cisco CCNA certified, and have the required knowledge set with a willingness to
continuously learn and expand my knowledge.

## 1. Dijksrta's Algorithm for Shortest Path
In the `/Dijkstras/Dijkstra.java` file is a program that computes the forwarding table of the shortest path using Dijkstra's
algorithm. The `/Dijkstras/Pair.java` and `Dijkstras/Tuple.java` files are simple data structures to help store the routing 
information. By default the program reads in router data from a file named `topo.txt` and prompts the user
for the number of routers in their network. The data in the `topo.txt` and `topo2.txt` files have the following format:

| Source Router        | Destination Router           | Associated Cost  |
| ------------- |:-------------:| -----:|
| router 0     | router 1 | cost 1 |
| router 0      | router 3      |   cost 2 |
| router 1 | router 3      |    cost 3 |
| etc | etc | etc |

This data is read in by my program and fed into Dijkstras algorithm. Running the Dijkstra.java 
file shows each iteration in computing the shortest path, and at the end yields the 
forwarding table if a shortest path is found, as shown below.

<img width="699" alt="screen shot 2019-03-03 at 4 04 16 pm" src="https://user-images.githubusercontent.com/32188816/53703419-20d70d80-3dcf-11e9-9f3d-449b4a00aeae.png">

When entering a number of routers in the network that is not compatable with the default `topo.txt` file, my program
prompts the user to enter the name of the file with the proper number of routers to use to compute the shortest path.
This is shown below.


<img width="616" alt="topo2" src="https://user-images.githubusercontent.com/32188816/53703433-60055e80-3dcf-11e9-8696-ab31633321f9.png">


## 2. Python and PHP Capture the Flag Scripts
In the `/WarGames` folder I have a number of Python and PHP scripts that accomplish various tasks ranging from
RCE on a remote web server, performing SQL injections, decrypting SQL queries encoded with the depractated 
ECB cipher algorithm, and much more. These are scripts written for a wargame/capture the flag challenge hosted by overthewire.org. 
I documented my work through this wargame which can be seen [here](https://r00tblogger.wordpress.com/2019/01/29/overthewire-natas-wargame/). The following briefly outlines what some of the scripts do, but there are more detailed explanations in the hyperlink above and in the comments in the scripts.
  * `WarGames/logger.php` is a script that creates a PHP class that runs a system call on the remote web server to view the contents of a password file upon deconstruction of an instance of the class. The target web server uses a base64 serialization of this object as a cookie, which allowed me to intercept the HTTP GET request and inject my own serialized object. 
  * `WarGames/brute.py` is a script that brute forces a password using SQL's `LIKE BINARY` to guess one character at a time.
  * `WarGames/brute2.py` is another script that brute forces a password, this time by using grep to find each subsequent character.
  * `WarGames/brute3.py` is similar to `WarGames/brute.py` in that it brute forces a password, but uses a time-based SQL injection to determine if a letter is contained in the password or not.
  * `WarGames/hash.php` is a PHP script that brute forces the human readable text that generated a given unsalted MD5 hash.
  * `WarGames/url-dec.py` is a script that deciphers an SQL query given as a value to a URL parameter. The problem is that
  the URL was "encrypted" with an ECB algorithm, so it was easy to find the encryption key.

## 3. C# ASP.NET Software Application
I have worked on and written real applications (not just scripts), and this software is one example. This is an application that 
I worked on for a Software Development Methods and Tools class at MSU in a team with 8 other students. For this application I wrote 
interfaces, mocks and unit tests shown in the following classes over the course of several 2-week sprints:
  * All of the test classes in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacherDomain/DomainObjects`
  * All of the test classes in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacherDomain/ViewObjects`
  * The `DiffMatchPatchAndHelperTest` class in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacher3/Services/`
  * All of the test classes in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacherDataModel/ObjectBuilders`
  * All of the test classes in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacherService/CUDServices`
  * All of the test classes in `/C# ASP.Net Software/CWTesting/Tests/Selenium`
  * All of the test classes in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacherDomain/` besides the `HolidayDomainObjBasicTest` and the `HolidayDomainObjTest` tests.
  
All unit tests and mocks yielded above 95% code coverage and all tests pass. After writing unit tests, I worked as part of
a team that implemented new features, involving both front and back end development. The features we implemented added functionality
to highlight the differences between two documents (either being a lesson plan, a master teacher context, teacher notes, etc) with new 
additions highlighted in green, and deleted content highlighted in red with strikethrough. This feature development was very similar to 
the functionality provided by `git diff`, but involved development experience with JQuery, AJAX, HTML, Javascript, and obviously C#.
The following classes and test classes I helped develop in no small part:  
  * The main difference highlighting class `/C# ASP.Net Software/CWMasterTeacherDomain/DiffMatchPatch.cs` adopted from Google.
    * Slight modifications were made and are noted clearly in the top of the file specified. Code origin is noted as well.
  * The helper class for highlighting differences located in `/C# ASP.Net Software/CWMasterTeacherDataModel/DMPCWMTHelper.cs`
    * Its readme file located in `/C# ASP.Net Software/CWMasterTeacherDomain/DMPCWMTHelper_README.md` that explains the added features and how to use all of the newly implemented methods.
  * The test class for the new features is located in `/C# ASP.Net Software/CWTesting/Tests/CWMasterTeacher3/Services/DiffMatchPatchAndHelperTest.cs`
  * Selenium tests to verify new feature buttons work and do not break.
    * Test files located in `/C# ASP.Net Software/CWTesting/Tests/Selenium`

## 4. Python Software
I used to know a bit of Perl, but I stopped using it after I learned Python. Python is my second-favorite programming language, 
and my first choice for new projects. I have written real programs (not just scripts) in Python for over 3 years. A large portion 
of my Python work has been with machine learning. I've built a nefarious network traffic classifier in Python using a Convolutional 
Neural Network (CNN) outside of my school work at MSU. The code is in the `/Python Software/prototype1.py` file 
and the PCAP training data is in the `/Python Software/good_pcaps` and `/Python Software/bad_pcaps` folders. This was an additional 
project I took upon myself and was not required for any course or credit. I am presenting this work at the Undergraduate Research 
Conference at MSU in April. The model architecture is as follows.

![tensor_board_graph_cropped](https://user-images.githubusercontent.com/32188816/53288657-410c2a00-3748-11e9-945e-6861e8dadd08.png)

The model is trained on packet capture files (PCAPs) that I obtained from penetration testing in a virtual lab environment. The 
first prototype classifies network traffic with a 97.436% test accuracy as shown below. I am still making improvements.

<img width="848" alt="traffic-classy" src="https://user-images.githubusercontent.com/32188816/53671788-30265180-3c3d-11e9-9493-1519104438f7.png">

## 5. C and C++ Software
I am an expert C programmer, with more than three years of experience programming in C. C is my favorite programming language. 
I am very familiar with typical C idioms. I tend to use a fairly object-oriented style, with structs representing objects and 
consistently named functions that operate on them. I like the K&R brace style. I read and worked all the exercises in the book 
"The C Programming Language" which is shown in the `/C and C++ Software/TCPL/` folder. I can write excellent C code with 
confidence. This software was not associated with my coursework at MSU, but rather I took these excercises upon myself on top of 
my course work. The code in the `/C and C++ Software/TCPL/` folder contains a number of different softwares ranging from a program
syntax checker to a reverse polish notation calculator.

Additionally, I have done some work with C++ and the Standard Template Library as part of schoolwork, but not for a real project. 
This is shown in the `/C and C++ Software/CPU_Scheduler` folder. This project is as a very lightweight kernel that round robins 
through executables given as command line arguments for a fixed length of time until a SIG_TERM signal is recieved. All system calls
are asserted, and static and dynamic analysis was done with Valgrind and Scanbuild, respectively. No warnings are generated and no
memory leaks. The output from running this program is in the file `/C and C++ Software/CPU_Scheduler/output.txt`. I don't have trouble 
reading C++, but I don't have familiarity with C++ idioms. 
