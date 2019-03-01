#Version Control   

##Why Source Control is Important:
Often times referred to as a Version Control System (VCS) it is defined as a system that keeps records of files or several files,
Which is a basis of any good software development. This process works in a way where files are checked in and checked out from a repository.
When working on a project, developers should push changes to their repository every time they have something running, and also every time they make a change.
They should also commit changes every half hour or so. If this process is maintained, debugging projects will be much simpler to accomplish.   


###How to Use VCS (Github):
https://services.github.com/kit/downloads/github-git-cheat-sheet.pdf   


##Branching:
* A branch is just a different trunk off the Master.  
* The master branch is the release branch.  
* The master branch should always be ready to release as is.  
* Master should never directly be pushed to, and instead changes should
  be done by a pull request.  
* Project changes will be done to the dev branch in CW Master Teacher via pull request.  
* Naming for branches off dev will be as such.   
* FirstInitialLastName/DescriptiveBranchName example: Adam Lee -> alee/DesignDocs   


##Merge Conflicts:
* Occurs when changes have been made to the same file by two different contributors,
  without having the others changes.   
* Git does not know which changes to take, therefore
  a merge conflict occurs.  


###To resolve a conflict:
a) Take your changes  
b) Take their changes  
c) Take some combination of the two   
> You can use a git merge tool or edit the files manually.  


###Best practices for avoiding conflicts:
* Only have one person working on a particular file at one time(ideally).      
* Keep your local and remote changes synced as often as possible.   
* Tell git to rebase when pulling commits.(http://kernowsoul.com/blog/2012/06/20/4-ways-to-avoid-merge-commits-in-git/)   
* Don't check in any build binary output or user settings.   
* Ensure that IDE's use the same formatting and keyword settings.   


##Coordinating Pull Requests:
* Submit your pull request when coding conventions are followed and the project is compiling and running.  
* If the Pull Request contains code changes, you must have 90% code coverage.  
* Ping your group ambassador, Ryan Newsom, or Adam Lee to get pull request approved and merged.  
* All pull requests must have been approved by the group ambassador prior to being accepted.  


###Requirements for pull Requests:
1. Group ambassadors must approve the pull requestt prior to being merged.    
2. Pull requests will not be merged if there exists any merge conflicts that are left unsolved. Merge conflicts should be handled on your end.   
3. Stop pull requests from being merged if they have any unsuccessful builds.   
> Ideally this would be run by a build job(Jenkins, CITravis, etc.)    
4. All unit tests must be passing
