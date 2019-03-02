# Software-Projects
This repository contains some of my software projects for presentation to Charter Comminications for the Cyber 
Security Engineer position as demonstration of scripting experience. The bulk of work here is software that I took 
upon myself to write on top of coursework. I have a large passion for software and love to write code. None of the projects 
are in PHP, but I am familiar with PHP coding standards. Below are details on what is contained in each directory and what 
the software accompishes.

## 1. C# ASP.NET Software Application
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

## 2. Python Software
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

## 3. C and C++ Software
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

## Why I'm Well Suited To Perform The Position
I've worked a full-time job while in school. I love challenges and I'm a hard worker. I'm very bright and I learn quickly. 
I want to make projects succeed. I act professionally and I take my work seriously. I'm good at working in a team and I 
have the necessary programming skills.

I'm really excited to help out a widely used telecommunicatons and mass media company. I would love to be able to say I work 
at Charter Comminications as a Cyber Security Engineer.
